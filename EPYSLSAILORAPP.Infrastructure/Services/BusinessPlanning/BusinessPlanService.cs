using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static Postgrest.Constants;
using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain;
using Postgrest;
using static Postgrest.QueryOptions;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Newtonsoft.Json.Linq;
using EPYSLSAILORAPP.Domain.RPC;
using Dapper;
using static Dapper.SqlMapper;
using System.Drawing.Printing;

namespace EPYSLSAILORAPP.Infrastructure.Services.BusinessPlanning
{
    public class BusinessPlanService : IBusinessPlanService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public BusinessPlanService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<List<tran_bp_year_dto>> GetAll(DtParameters param)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"select gfy.year_name, * from tran_bp_year tb inner join gen_fiscal_year gfy on tb.fiscal_year_id = gfy.fiscal_year_id";

                    var dataList = connection.Query<tran_bp_year_ext>(query).ToList();

                    if (dataList.Count() > 0)
                    {
                        dataList.FirstOrDefault().TotalCount = dataList.Count();
                    }

                    return JsonConvert.DeserializeObject<List<tran_bp_year_dto>>(JsonConvert.SerializeObject(dataList)); ;

                }

            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {

            }

        }

        public async Task<List<rpc_event_tran_data_getall>> rpc_event_tran_data_GetAll(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_bp_event_tran_data_getall( @filter_fiscal_year_id)";

                    var dataList = connection.Query<rpc_event_tran_data_getall>(query,
                          new { filter_fiscal_year_id = param.MasterID.Value }
                         ).AsList();

                    return dataList;

                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {

            }
        }

        private async Task<bool> SaveBusinessPlanDataTodb_PG(tran_bp_year_dto obj_tran_bp_year)
        {
            List<tran_bp_event_month_outlet_dto> tran_bp_event_month_outlet_list = new List<tran_bp_event_month_outlet_dto>();

            long tranBpYearId;

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var yearCommand = new NpgsqlCommand("tran_bp_insert", connection);
                        yearCommand.CommandType = CommandType.StoredProcedure;

                        yearCommand.Parameters.AddWithValue("p_fiscal_year_id", NpgsqlDbType.Bigint, obj_tran_bp_year.fiscal_year_id);
                        yearCommand.Parameters.AddWithValue("p_gross_sales", NpgsqlDbType.Numeric, obj_tran_bp_year.gross_sales);

                        yearCommand.Parameters.AddWithValue("p_yearly_gross_discount", NpgsqlDbType.Numeric, obj_tran_bp_year.yearly_gross_discount);
                        yearCommand.Parameters.AddWithValue("p_yearly_gross_net_amount", NpgsqlDbType.Numeric, obj_tran_bp_year.yearly_gross_net);

                        yearCommand.Parameters.AddWithValue("p_added_by", NpgsqlDbType.Bigint, obj_tran_bp_year.added_by);
                        yearCommand.Parameters.AddWithValue("p_updated_by", NpgsqlDbType.Bigint, obj_tran_bp_year.added_by);
                        yearCommand.Parameters.AddWithValue("p_event_list_json", NpgsqlDbType.Text, obj_tran_bp_year.event_list_json.ToString());
                        yearCommand.Parameters.AddWithValue("p_remarks", NpgsqlDbType.Text, obj_tran_bp_year.remarks);

                        yearCommand.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }


        }

        public async Task<bool> SaveBusinessPlan(BusinessPlanDTO param)
        {

            tran_bp_year_dto obj_tran_bp_year = new tran_bp_year_dto();
            obj_tran_bp_year.fiscal_year_id = param.fiscal_year_id.Value;
            obj_tran_bp_year.gross_sales = param.gross_sales.Value;
            obj_tran_bp_year.yearly_gross_discount = param.yearly_gross_discount;
            obj_tran_bp_year.yearly_gross_net = param.yearly_gross_net;

            obj_tran_bp_year.added_by = param.added_by.Value;
            obj_tran_bp_year.date_added = param.date_added.Value;
            obj_tran_bp_year.remarks = param.remarks;

            List<tran_bp_event_dto> obj_tran_event_list = new List<tran_bp_event_dto>();
            List<tran_bp_event_month_dto> obj_tran_event_month_list = new List<tran_bp_event_month_dto>();
            List<tran_bp_event_month_outlet_dto> obj_tran_event_month_outlet_list = new List<tran_bp_event_month_outlet_dto>();

            var unique_events = param.event_monthly_outlet_list.Select(p => new { p.event_id.Value }).Distinct().ToList();

            obj_tran_event_list = new List<tran_bp_event_dto>();

            for (int event_index = 0; event_index < unique_events.Count; event_index++)
            {

                obj_tran_event_month_list = new List<tran_bp_event_month_dto>();
                obj_tran_event_month_outlet_list = new List<tran_bp_event_month_outlet_dto>();

                tran_bp_event_dto obj_tran_event = new tran_bp_event_dto();
                obj_tran_event.event_id = Convert.ToInt64(unique_events[event_index].Value);
                obj_tran_event.readygoods_qnty = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_qnty.Value;
                obj_tran_event.readygoods_value = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_value.Value;
                obj_tran_event.readygoods_cpu = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_cpu != null ?
                                param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_cpu.Value : 0;
                obj_tran_event.event_gross_sales = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().event_gross_sales.Value;
                obj_tran_event.event_discount_amount = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().event_gross_discount.Value;
                obj_tran_event.event_net_amount = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().event_gross_net.Value;

                obj_tran_event.added_by = param.added_by.Value;
                obj_tran_event.date_added = param.date_added.Value;

                var unique_months = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).Select(p => new { p.month_id.Value }).Distinct().ToList();

                for (int month_index = 0; month_index < unique_months.Count; month_index++)
                {
                    tran_bp_event_month_dto obj_tran_bp_event_month = new tran_bp_event_month_dto();
                    obj_tran_bp_event_month.month_id = Convert.ToInt64(unique_months[month_index].Value);
                    obj_tran_bp_event_month.monthly_gross_sales = param.event_monthly_outlet_list
                        .Where(p => p.event_id == obj_tran_event.event_id && p.month_id == obj_tran_bp_event_month.month_id)
                        .Sum(p => p.outlet_gross_sales).Value;
                    obj_tran_bp_event_month.monthly_discount_amount = param.event_monthly_outlet_list
                        .Where(p => p.event_id == obj_tran_event.event_id && p.month_id == obj_tran_bp_event_month.month_id)
                        .Sum(p => p.outlet_gross_discount).Value;
                    obj_tran_bp_event_month.monthly_net_amount = param.event_monthly_outlet_list
                        .Where(p => p.event_id == obj_tran_event.event_id && p.month_id == obj_tran_bp_event_month.month_id)
                        .Sum(p => p.outlet_gross_net).Value;

                    obj_tran_bp_event_month.added_by = param.added_by.Value;
                    obj_tran_bp_event_month.date_added = param.date_added.Value;


                    var monthwise_unique_list = param.event_monthly_outlet_list
                        .Where(p => p.event_id == obj_tran_event.event_id && p.month_id == obj_tran_bp_event_month.month_id).ToList();

                    foreach (var item in monthwise_unique_list)
                    {
                        tran_bp_event_month_outlet_dto obj_tran_bp_event_month_outlet = new tran_bp_event_month_outlet_dto();
                        obj_tran_bp_event_month_outlet.outlet_id = item.outlet_id.Value;
                        obj_tran_bp_event_month_outlet.outlet_gross_sales = item.outlet_gross_sales.Value;
                        obj_tran_bp_event_month_outlet.outlet_discount_amount = item.outlet_gross_discount.Value;
                        obj_tran_bp_event_month_outlet.outlet_net_amount = item.outlet_gross_net.Value;

                        obj_tran_bp_event_month_outlet.added_by = param.added_by.Value;
                        obj_tran_bp_event_month_outlet.date_added = param.date_added.Value;

                        obj_tran_event_month_outlet_list.Add(obj_tran_bp_event_month_outlet);
                    }

                    obj_tran_bp_event_month.tran_bp_event_month_outlet_list = obj_tran_event_month_outlet_list;
                    obj_tran_bp_event_month.event_month_outlet_list_json = JArray.Parse(JsonConvert.SerializeObject(obj_tran_event_month_outlet_list));

                    obj_tran_event_month_outlet_list = new List<tran_bp_event_month_outlet_dto>();


                    obj_tran_event_month_list.Add(obj_tran_bp_event_month);

                }

                obj_tran_event.tran_bp_month_list = obj_tran_event_month_list;

                obj_tran_event.event_month_list_json = JArray.Parse(JsonConvert.SerializeObject(obj_tran_event_month_list));



                obj_tran_event_month_list = new List<tran_bp_event_month_dto>();


                obj_tran_event_list.Add(obj_tran_event);

            }

            obj_tran_bp_year.tran_bp_event_list = obj_tran_event_list;
            obj_tran_bp_year.event_list_json = JArray.Parse(JsonConvert.SerializeObject(obj_tran_event_list));



            return await SaveBusinessPlanDataTodb_PG(obj_tran_bp_year); ;
        }

        public async Task<bool> UpdateBusinessPlan(BusinessPlanDTO param)
        {
            tran_bp_year_dto obj_tran_bp_year = new tran_bp_year_dto();
            obj_tran_bp_year.tran_bp_year_id = param.tran_bp_year_id.Value;
            obj_tran_bp_year.fiscal_year_id = param.fiscal_year_id.Value;
            obj_tran_bp_year.gross_sales = param.gross_sales.Value;
            obj_tran_bp_year.yearly_gross_discount = param.yearly_gross_discount.Value;
            obj_tran_bp_year.yearly_gross_net = param.yearly_gross_net.Value;

            obj_tran_bp_year.added_by = param.added_by.Value;
            obj_tran_bp_year.date_added = param.date_added.Value;
            obj_tran_bp_year.updated_by = param.added_by.Value;
            obj_tran_bp_year.date_updated = param.date_added.Value;
            obj_tran_bp_year.remarks = param.remarks;
            obj_tran_bp_year.is_submitted = param.is_submitted;
            obj_tran_bp_year.is_approved = param.is_approved;

            List<tran_bp_event_dto> obj_tran_event_list = new List<tran_bp_event_dto>();
            List<tran_bp_event_month_dto> obj_tran_event_month_list = new List<tran_bp_event_month_dto>();
            List<tran_bp_event_month_outlet_dto> obj_tran_event_month_outlet_list = new List<tran_bp_event_month_outlet_dto>();

            var unique_events = param.event_monthly_outlet_list.Select(p => new { p.tran_bp_event_id.Value }).Distinct().ToList();

            obj_tran_event_list = new List<tran_bp_event_dto>();

            for (int event_index = 0; event_index < unique_events.Count; event_index++)
            {

                obj_tran_event_month_list = new List<tran_bp_event_month_dto>();
                obj_tran_event_month_outlet_list = new List<tran_bp_event_month_outlet_dto>();

                tran_bp_event_dto obj_tran_event = new tran_bp_event_dto();
                obj_tran_event.tran_bp_event_id = Convert.ToInt64(unique_events[event_index].Value);
                obj_tran_event.event_id = param.event_monthly_outlet_list.Where(p => p.tran_bp_event_id == obj_tran_event.tran_bp_event_id).FirstOrDefault().event_id.Value;
                obj_tran_event.readygoods_qnty = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_qnty.Value;
                obj_tran_event.readygoods_value = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_value.Value;
                obj_tran_event.readygoods_cpu = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_cpu!=null?
                    param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).FirstOrDefault().readygoods_cpu.Value:0;

                obj_tran_event.event_gross_sales = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).Sum(p => p.outlet_gross_sales).Value;
                obj_tran_event.event_discount_amount = param.event_monthly_outlet_list.Where(p => p.event_id == obj_tran_event.event_id).Sum(p => p.outlet_gross_discount).Value;
                obj_tran_event.event_net_amount = obj_tran_event.event_gross_sales - obj_tran_event.event_discount_amount;


                obj_tran_event.added_by = param.added_by.Value;
                obj_tran_event.date_added = param.date_added.Value;
                obj_tran_event.updated_by = param.added_by.Value;
                obj_tran_event.date_updated = param.date_added.Value;

                var unique_months = param.event_monthly_outlet_list.Where(p => p.tran_bp_event_id == obj_tran_event.tran_bp_event_id).Select(p => new { p.tran_bp_event_month_id.Value }).Distinct().ToList();

                for (int month_index = 0; month_index < unique_months.Count; month_index++)
                {
                    tran_bp_event_month_dto obj_tran_bp_event_month = new tran_bp_event_month_dto();
                    obj_tran_bp_event_month.tran_bp_event_month_id = Convert.ToInt64(unique_months[month_index].Value);
                    obj_tran_bp_event_month.month_id = param.event_monthly_outlet_list
                        .Where(p => p.event_id == obj_tran_event.event_id && p.tran_bp_event_month_id == obj_tran_bp_event_month.tran_bp_event_month_id).FirstOrDefault().month_id.Value;

                    obj_tran_bp_event_month.monthly_gross_sales = param.event_monthly_outlet_list
                        .Where(p => p.event_id == obj_tran_event.event_id && p.tran_bp_event_month_id == obj_tran_bp_event_month.tran_bp_event_month_id)
                        .Sum(p => p.outlet_gross_sales).Value;
                    obj_tran_bp_event_month.monthly_discount_amount = param.event_monthly_outlet_list
                       .Where(p => p.event_id == obj_tran_event.event_id && p.tran_bp_event_month_id == obj_tran_bp_event_month.tran_bp_event_month_id)
                       .Sum(p => p.outlet_gross_discount).Value;

                    obj_tran_bp_event_month.monthly_net_amount =
                        obj_tran_bp_event_month.monthly_gross_sales - obj_tran_bp_event_month.monthly_discount_amount;

                    obj_tran_bp_event_month.added_by = param.added_by.Value;
                    obj_tran_bp_event_month.date_added = param.date_added.Value;
                    obj_tran_bp_event_month.updated_by = param.added_by.Value;
                    obj_tran_bp_event_month.date_updated = param.date_added.Value;

                    var monthwise_unique_list = param.event_monthly_outlet_list
                        .Where(p => p.tran_bp_event_id == obj_tran_event.tran_bp_event_id && p.tran_bp_event_month_id == obj_tran_bp_event_month.tran_bp_event_month_id).ToList();

                    foreach (var item in monthwise_unique_list)
                    {
                        tran_bp_event_month_outlet_dto obj_tran_bp_event_month_outlet = new tran_bp_event_month_outlet_dto();
                        obj_tran_bp_event_month_outlet.tran_bp_event_month_outlet_id = item.tran_bp_event_month_outlet_id.Value;
                        obj_tran_bp_event_month_outlet.tran_bp_event_month_id = item.tran_bp_event_month_id.Value;
                        obj_tran_bp_event_month_outlet.outlet_id = item.outlet_id.Value;
                        obj_tran_bp_event_month_outlet.outlet_gross_sales = item.outlet_gross_sales.Value;
                        obj_tran_bp_event_month_outlet.outlet_discount_amount = item.outlet_gross_discount!=null? item.outlet_gross_discount.Value:0;
                        obj_tran_bp_event_month_outlet.outlet_net_amount = item.outlet_gross_sales.Value - (obj_tran_bp_event_month_outlet.outlet_discount_amount);

                        obj_tran_bp_event_month_outlet.added_by = param.added_by.Value;
                        obj_tran_bp_event_month_outlet.date_added = param.date_added.Value;
                        obj_tran_bp_event_month_outlet.updated_by = param.added_by.Value;
                        obj_tran_bp_event_month_outlet.date_updated = param.date_added.Value;

                        obj_tran_event_month_outlet_list.Add(obj_tran_bp_event_month_outlet);
                    }

                    obj_tran_bp_event_month.tran_bp_event_month_outlet_list = obj_tran_event_month_outlet_list;

                    obj_tran_bp_event_month.event_month_outlet_list_json = JArray.Parse(JsonConvert.SerializeObject(obj_tran_event_month_outlet_list));

                    obj_tran_event_month_outlet_list = new List<tran_bp_event_month_outlet_dto>();


                    obj_tran_event_month_list.Add(obj_tran_bp_event_month);

                }

                obj_tran_event.tran_bp_month_list = obj_tran_event_month_list;

                obj_tran_event.event_month_list_json = JArray.Parse(JsonConvert.SerializeObject(obj_tran_event_month_list));

                obj_tran_event_month_list = new List<tran_bp_event_month_dto>();

                obj_tran_event_list.Add(obj_tran_event);

            }

            obj_tran_bp_year.tran_bp_event_list = obj_tran_event_list;

            obj_tran_bp_year.event_list_json = JArray.Parse(JsonConvert.SerializeObject(obj_tran_event_list));

            return await UpdateusinessPlanDataTodb_PG(obj_tran_bp_year);
        }

        private async Task<bool> UpdateusinessPlanDataTodb_PG(tran_bp_year_dto obj_tran_bp_year)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {

                        var yearCommand = new NpgsqlCommand("tran_bp_update", connection);
                        yearCommand.CommandType = CommandType.StoredProcedure;
                        yearCommand.Parameters.AddWithValue("p_tran_bp_year_id", NpgsqlDbType.Bigint, obj_tran_bp_year.tran_bp_year_id);
                        yearCommand.Parameters.AddWithValue("p_fiscal_year_id", NpgsqlDbType.Bigint, obj_tran_bp_year.fiscal_year_id);
                        yearCommand.Parameters.AddWithValue("p_gross_sales", NpgsqlDbType.Numeric, obj_tran_bp_year.gross_sales);

                        yearCommand.Parameters.AddWithValue("p_yearly_gross_discount", NpgsqlDbType.Numeric, obj_tran_bp_year.yearly_gross_discount);
                        yearCommand.Parameters.AddWithValue("p_yearly_gross_net_amount", NpgsqlDbType.Numeric, obj_tran_bp_year.yearly_gross_net);

                        yearCommand.Parameters.AddWithValue("p_updated_by", NpgsqlDbType.Bigint, obj_tran_bp_year.added_by);
                        yearCommand.Parameters.AddWithValue("p_event_list_json", NpgsqlDbType.Text, obj_tran_bp_year.event_list_json.ToString());
                        yearCommand.Parameters.AddWithValue("p_remarks", NpgsqlDbType.Text, obj_tran_bp_year.remarks);

                        yearCommand.Parameters.AddWithValue("p_is_approved", NpgsqlDbType.Boolean, obj_tran_bp_year.is_approved == null ? DBNull.Value : obj_tran_bp_year.is_approved);
                        yearCommand.Parameters.AddWithValue("p_is_submitted", NpgsqlDbType.Boolean, obj_tran_bp_year.is_submitted == null ? DBNull.Value : obj_tran_bp_year.is_submitted);


                        yearCommand.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                //transaction.Rollback();
                return false;
            }

        }


        public async Task<List<rpc_tran_bp_year_DTO>> GetAllJoined_TranBpYearAsync(DtParameters param)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                   // string query = $"SELECT * FROM proc_sp_get_data_tran_bp_year( @row_index,@page_size,@search_text)";
                    string query = $"SELECT * FROM proc_fetch_tran_bp_year_data( @row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_tran_bp_year_DTO>(query,
                          new { 
                              
                              row_index = param.Start,
                              page_size = param.Length ,
                              search_text=param.Search.Value,

                          }
                         ).AsList();

                    return dataList;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_bp_year_dto>> get_tran_BP_YearService_GetAll()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_bp_year";

                    var dataList = connection.Query<tran_bp_year_dto_ext>(query).ToList();

                    return JsonConvert.DeserializeObject<List<tran_bp_year_dto>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }


        public async Task<bool> UpdateApproveReject(tran_bp_year_dto entity)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"update tran_bp_year
                                     set is_approved=@is_approved, is_submitted=@is_submitted,
                                         approved_by=@approved_by, approve_date=@approve_date,approve_remarks=@approve_remarks
                                     where tran_bp_year_id=@Id";

                    connection.Execute(query,
                        new
                        {
                            is_approved = entity.is_approved,
                            is_submitted = entity.is_submitted,
                            approved_by = entity.approved_by,
                            approve_date = entity.approve_date,
                            approve_remarks = entity.approve_remarks,
                            Id = entity.tran_bp_year_id
                        });

                    return true;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
