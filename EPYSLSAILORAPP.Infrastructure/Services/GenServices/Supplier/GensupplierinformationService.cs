
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.DTO;
using static Postgrest.Constants;
using Newtonsoft.Json.Linq;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using static Postgrest.Constants;
using AutoMapper;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using static Postgrest.QueryOptions;
using Postgrest;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;
using NpgsqlTypes;
using System.Data;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenSupplierInformationService : IGenSupplierInformationService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenSupplierInformationService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_supplier_information_DTO objgen_supplier_information)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_supplier_information_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_contact_code", NpgsqlDbType.Text, objgen_supplier_information.contact_code == null ? DBNull.Value : objgen_supplier_information.contact_code);

                        Command.Parameters.AddWithValue("in_name", NpgsqlDbType.Text, objgen_supplier_information.name == null ? DBNull.Value : objgen_supplier_information.name);

                        Command.Parameters.AddWithValue("in_short_name", NpgsqlDbType.Text, objgen_supplier_information.short_name == null ? DBNull.Value : objgen_supplier_information.short_name);

                        Command.Parameters.AddWithValue("in_display_code", NpgsqlDbType.Text, objgen_supplier_information.display_code == null ? DBNull.Value : objgen_supplier_information.display_code);

                        Command.Parameters.AddWithValue("in_email", NpgsqlDbType.Text, objgen_supplier_information.email == null ? DBNull.Value : objgen_supplier_information.email);

                        Command.Parameters.AddWithValue("in_office_address", NpgsqlDbType.Text, objgen_supplier_information.office_address == null ? DBNull.Value : objgen_supplier_information.office_address);

                        Command.Parameters.AddWithValue("in_factory_address", NpgsqlDbType.Text, objgen_supplier_information.factory_address == null ? DBNull.Value : objgen_supplier_information.factory_address);

                        Command.Parameters.AddWithValue("in_fax_no", NpgsqlDbType.Text, objgen_supplier_information.fax_no == null ? DBNull.Value : objgen_supplier_information.fax_no);

                        Command.Parameters.AddWithValue("in_gen_contact_category", NpgsqlDbType.Text, objgen_supplier_information.obj_contact_category == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.obj_contact_category));

                        Command.Parameters.AddWithValue("in_contact_person", NpgsqlDbType.Text, objgen_supplier_information.contact_person == null ? DBNull.Value : objgen_supplier_information.contact_person);

                        Command.Parameters.AddWithValue("in_country_id", NpgsqlDbType.Bigint, objgen_supplier_information.country_id == null ? DBNull.Value : objgen_supplier_information.country_id);

                        Command.Parameters.AddWithValue("in_city", NpgsqlDbType.Text, objgen_supplier_information.city == null ? DBNull.Value : objgen_supplier_information.city);

                        Command.Parameters.AddWithValue("in_agent_name", NpgsqlDbType.Text, objgen_supplier_information.agent_name == null ? DBNull.Value : objgen_supplier_information.agent_name);

                        Command.Parameters.AddWithValue("in_geographical_location_list", NpgsqlDbType.Text, objgen_supplier_information.List_geographical_location == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_geographical_location));

                        Command.Parameters.AddWithValue("in_vat_bin_number", NpgsqlDbType.Text, objgen_supplier_information.vat_bin_number == null ? DBNull.Value : objgen_supplier_information.vat_bin_number);

                        Command.Parameters.AddWithValue("in_vat_bin_number_start_date", NpgsqlDbType.Date, objgen_supplier_information.vat_bin_number_start_date == null ? DBNull.Value : objgen_supplier_information.vat_bin_number_start_date);

                        Command.Parameters.AddWithValue("in_vat_bin_number_expire_date", NpgsqlDbType.Date, objgen_supplier_information.vat_bin_number_expire_date == null ? DBNull.Value : objgen_supplier_information.vat_bin_number_expire_date);

                        Command.Parameters.AddWithValue("in_vat_bin_documnet", NpgsqlDbType.Text, objgen_supplier_information.List_vat_bin_docs == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_vat_bin_docs));

                        Command.Parameters.AddWithValue("in_tin_number", NpgsqlDbType.Text, objgen_supplier_information.tin_number == null ? DBNull.Value : objgen_supplier_information.tin_number);

                        Command.Parameters.AddWithValue("in_tin_document", NpgsqlDbType.Text, objgen_supplier_information.List_tin_docs == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_tin_docs));

                        Command.Parameters.AddWithValue("in_trade_license", NpgsqlDbType.Text, objgen_supplier_information.trade_license == null ? DBNull.Value : objgen_supplier_information.trade_license);

                        Command.Parameters.AddWithValue("in_trade_license_start_date", NpgsqlDbType.Date, objgen_supplier_information.trade_license_start_date == null ? DBNull.Value : objgen_supplier_information.trade_license_start_date);

                        Command.Parameters.AddWithValue("in_trade_license_expire_date", NpgsqlDbType.Date, objgen_supplier_information.trade_license_expire_date == null ? DBNull.Value : objgen_supplier_information.trade_license_expire_date);

                        Command.Parameters.AddWithValue("in_trade_license_document", NpgsqlDbType.Text, objgen_supplier_information.List_tradelincense_docs == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_tradelincense_docs));

                        Command.Parameters.AddWithValue("in_branch_list", NpgsqlDbType.Text, objgen_supplier_information.List_branch == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_branch));

                        Command.Parameters.AddWithValue("in_concern_person_list", NpgsqlDbType.Text, objgen_supplier_information.List_concern_person == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_concern_person));
                        

                        Command.Parameters.AddWithValue("in_inco_term_list", NpgsqlDbType.Text, objgen_supplier_information.List_incoterm == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_incoterm));

                        Command.Parameters.AddWithValue("in_mode_of_transction_list", NpgsqlDbType.Text, objgen_supplier_information.List_mode_of_transaction == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_mode_of_transaction));

                        Command.Parameters.AddWithValue("in_credit_period", NpgsqlDbType.Text, objgen_supplier_information.obj_credit_period == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.obj_credit_period));

                        Command.Parameters.AddWithValue("in_calculation_of_tenure", NpgsqlDbType.Text, objgen_supplier_information.obj_calculation_of_tenure == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.obj_calculation_of_tenure));

                        Command.Parameters.AddWithValue("in_payment_method_list", NpgsqlDbType.Text, objgen_supplier_information.List_paymentmethod == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_paymentmethod));

                        Command.Parameters.AddWithValue("in_payment_term_list", NpgsqlDbType.Text, objgen_supplier_information.List_paymentterm == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_paymentterm));

                        Command.Parameters.AddWithValue("in_bank_account_info_list", NpgsqlDbType.Text, objgen_supplier_information.List_bank_account_info == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_bank_account_info));

                        Command.Parameters.AddWithValue("in_item_sub_group_detail_list", NpgsqlDbType.Text, objgen_supplier_information.List_item_subgroup_info == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_item_subgroup_info));

                        Command.Parameters.AddWithValue("in_active", NpgsqlDbType.Boolean, objgen_supplier_information.active == null ? DBNull.Value : objgen_supplier_information.active);

                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_supplier_information.added_by == null ? DBNull.Value : objgen_supplier_information.added_by);

                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_supplier_information.updated_by == null ? DBNull.Value : objgen_supplier_information.updated_by);

                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_supplier_information.date_added == null ? DBNull.Value : objgen_supplier_information.date_added);

                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_supplier_information.date_updated == null ? DBNull.Value : objgen_supplier_information.date_updated);


                        Command.ExecuteNonQuery();

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


       

        public async Task<bool> UpdateAsync(gen_supplier_information_DTO objgen_supplier_information)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("gen_supplier_information_update", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_gen_supplier_information_id", NpgsqlDbType.Bigint, objgen_supplier_information.gen_supplier_information_id == null ? DBNull.Value : objgen_supplier_information.gen_supplier_information_id);

                            Command.Parameters.AddWithValue("in_contact_code", NpgsqlDbType.Text, objgen_supplier_information.contact_code == null ? DBNull.Value : objgen_supplier_information.contact_code);

                            Command.Parameters.AddWithValue("in_name", NpgsqlDbType.Text, objgen_supplier_information.name == null ? DBNull.Value : objgen_supplier_information.name);

                            Command.Parameters.AddWithValue("in_short_name", NpgsqlDbType.Text, objgen_supplier_information.short_name == null ? DBNull.Value : objgen_supplier_information.short_name);

                            Command.Parameters.AddWithValue("in_display_code", NpgsqlDbType.Text, objgen_supplier_information.display_code == null ? DBNull.Value : objgen_supplier_information.display_code);

                            Command.Parameters.AddWithValue("in_email", NpgsqlDbType.Text, objgen_supplier_information.email == null ? DBNull.Value : objgen_supplier_information.email);

                            Command.Parameters.AddWithValue("in_office_address", NpgsqlDbType.Text, objgen_supplier_information.office_address == null ? DBNull.Value : objgen_supplier_information.office_address);

                            Command.Parameters.AddWithValue("in_factory_address", NpgsqlDbType.Text, objgen_supplier_information.factory_address == null ? DBNull.Value : objgen_supplier_information.factory_address);

                            Command.Parameters.AddWithValue("in_fax_no", NpgsqlDbType.Text, objgen_supplier_information.fax_no == null ? DBNull.Value : objgen_supplier_information.fax_no);

                            Command.Parameters.AddWithValue("in_gen_contact_category", NpgsqlDbType.Text, objgen_supplier_information.obj_contact_category == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.obj_contact_category));

                            Command.Parameters.AddWithValue("in_contact_person", NpgsqlDbType.Text, objgen_supplier_information.contact_person == null ? DBNull.Value : objgen_supplier_information.contact_person);

                            Command.Parameters.AddWithValue("in_country_id", NpgsqlDbType.Bigint, objgen_supplier_information.country_id == null ? DBNull.Value : objgen_supplier_information.country_id);

                            Command.Parameters.AddWithValue("in_city", NpgsqlDbType.Text, objgen_supplier_information.city == null ? DBNull.Value : objgen_supplier_information.city);

                            Command.Parameters.AddWithValue("in_agent_name", NpgsqlDbType.Text, objgen_supplier_information.agent_name == null ? DBNull.Value : objgen_supplier_information.agent_name);

                            Command.Parameters.AddWithValue("in_geographical_location_list", NpgsqlDbType.Text, objgen_supplier_information.List_geographical_location == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_geographical_location));

                            Command.Parameters.AddWithValue("in_vat_bin_number", NpgsqlDbType.Text, objgen_supplier_information.vat_bin_number == null ? DBNull.Value : objgen_supplier_information.vat_bin_number);

                            Command.Parameters.AddWithValue("in_vat_bin_number_start_date", NpgsqlDbType.Date, objgen_supplier_information.vat_bin_number_start_date == null ? DBNull.Value : objgen_supplier_information.vat_bin_number_start_date);

                            Command.Parameters.AddWithValue("in_vat_bin_number_expire_date", NpgsqlDbType.Date, objgen_supplier_information.vat_bin_number_expire_date == null ? DBNull.Value : objgen_supplier_information.vat_bin_number_expire_date);

                            Command.Parameters.AddWithValue("in_vat_bin_documnet", NpgsqlDbType.Text, objgen_supplier_information.List_vat_bin_docs == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_vat_bin_docs));

                            Command.Parameters.AddWithValue("in_tin_number", NpgsqlDbType.Text, objgen_supplier_information.tin_number == null ? DBNull.Value : objgen_supplier_information.tin_number);

                            Command.Parameters.AddWithValue("in_tin_document", NpgsqlDbType.Text, objgen_supplier_information.List_tin_docs == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_tin_docs));

                            Command.Parameters.AddWithValue("in_trade_license", NpgsqlDbType.Text, objgen_supplier_information.trade_license == null ? DBNull.Value : objgen_supplier_information.trade_license);

                            Command.Parameters.AddWithValue("in_trade_license_start_date", NpgsqlDbType.Date, objgen_supplier_information.trade_license_start_date == null ? DBNull.Value : objgen_supplier_information.trade_license_start_date);

                            Command.Parameters.AddWithValue("in_trade_license_expire_date", NpgsqlDbType.Date, objgen_supplier_information.trade_license_expire_date == null ? DBNull.Value : objgen_supplier_information.trade_license_expire_date);

                            Command.Parameters.AddWithValue("in_trade_license_document", NpgsqlDbType.Text, objgen_supplier_information.List_tradelincense_docs == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_tradelincense_docs));

                            Command.Parameters.AddWithValue("in_branch_list", NpgsqlDbType.Text, objgen_supplier_information.List_branch == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_branch));

                            Command.Parameters.AddWithValue("in_concern_person_list", NpgsqlDbType.Text, objgen_supplier_information.List_concern_person == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_concern_person));


                            Command.Parameters.AddWithValue("in_inco_term_list", NpgsqlDbType.Text, objgen_supplier_information.List_incoterm == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_incoterm));

                            Command.Parameters.AddWithValue("in_mode_of_transction_list", NpgsqlDbType.Text, objgen_supplier_information.List_mode_of_transaction == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_mode_of_transaction));

                            Command.Parameters.AddWithValue("in_credit_period", NpgsqlDbType.Text, objgen_supplier_information.obj_credit_period == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.obj_credit_period));

                            Command.Parameters.AddWithValue("in_calculation_of_tenure", NpgsqlDbType.Text, objgen_supplier_information.obj_calculation_of_tenure == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.obj_calculation_of_tenure));

                            Command.Parameters.AddWithValue("in_payment_method_list", NpgsqlDbType.Text, objgen_supplier_information.List_paymentmethod == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_paymentmethod));

                            Command.Parameters.AddWithValue("in_payment_term_list", NpgsqlDbType.Text, objgen_supplier_information.List_paymentterm == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_paymentterm));

                            Command.Parameters.AddWithValue("in_bank_account_info_list", NpgsqlDbType.Text, objgen_supplier_information.List_bank_account_info == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_bank_account_info));

                            Command.Parameters.AddWithValue("in_item_sub_group_detail_list", NpgsqlDbType.Text, objgen_supplier_information.List_item_subgroup_info == null ? DBNull.Value : JsonConvert.SerializeObject(objgen_supplier_information.List_item_subgroup_info));

                            Command.Parameters.AddWithValue("in_active", NpgsqlDbType.Boolean, objgen_supplier_information.active == null ? DBNull.Value : objgen_supplier_information.active);

                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_supplier_information.added_by == null ? DBNull.Value : objgen_supplier_information.added_by);

                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_supplier_information.updated_by == null ? DBNull.Value : objgen_supplier_information.updated_by);

                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_supplier_information.date_added == null ? DBNull.Value : objgen_supplier_information.date_added);

                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_supplier_information.date_updated == null ? DBNull.Value : objgen_supplier_information.date_updated);


                            Command.ExecuteNonQuery();

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
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_supplier_information_DTO>> GetAllAsync()
        {
            List<gen_supplier_information_entity> list = new List<gen_supplier_information_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_supplier_information_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<gen_supplier_information_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_supplier_information_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    if (param.MasterID.HasValue && param.MasterID.Value>0)
                    {
                        string query = @"WITH cte_saved AS (SELECT m.gen_supplier_information_id, m.short_name,m.name,m.contact_person,m.office_address
                                           FROM gen_supplier_information m
                                           inner join tran_tech_pack_embellishment_info a on a.supplier_id = m.gen_supplier_information_id
                                           where a.tran_tech_pack_id=@tran_tech_pack_id and 
                                                 case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                           m.name ILIKE  '%' || @search_text || '%'
                                                           
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY";

                        var dataList = connection.Query<gen_supplier_information_entity>(query,
                            new
                            {
                                tran_tech_pack_id = param.MasterID.Value,
                                search_text = param.Search.Value,
                                row_index = param.Start,
                                page_size = param.Length
                            }).ToList();

                        return dataList;
                    }
                    else
                    {

                        string query = @"WITH cte_saved AS (SELECT m.gen_supplier_information_id, m.short_name,m.name,m.contact_person,m.office_address
                                           FROM gen_supplier_information m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                           m.name ILIKE  '%' || @search_text || '%'
                                                          
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                        var dataList = connection.Query<gen_supplier_information_entity>(query,
                            new
                            {
                                search_text = param.Search.Value,
                                row_index = param.Start,
                                page_size = param.Length
                            }).ToList();

                        return dataList;
                    }

                   

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<gen_supplier_information_DTO> GetSingleAsync(Int64 Id)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_supplier_information m   where m.gen_supplier_information_id=@Id";

                    var objData = connection.Query<gen_supplier_information_entity>(query,
                        new { Id = Id }).ToList().FirstOrDefault();

                    var objRet= JsonConvert.DeserializeObject<gen_supplier_information_DTO>(JsonConvert.SerializeObject(objData));


                    objRet.List_geographical_location = !string.IsNullOrEmpty(objData.geographical_location_list) ? JsonConvert.DeserializeObject<List<gen_geographical_location_DTO>>(objData.geographical_location_list) : new List<gen_geographical_location_DTO>();

                    objRet.List_concern_person = !string.IsNullOrEmpty(objData.concern_person_list) ? JsonConvert.DeserializeObject<List<concern_person>>(objData.concern_person_list) : new List<concern_person>();

                    objRet.List_branch = !string.IsNullOrEmpty(objData.branch_list) ? JsonConvert.DeserializeObject<List<branch>>(objData.branch_list): new List<branch>();

                    objRet.List_incoterm = !string.IsNullOrEmpty(objData.inco_term_list) ? JsonConvert.DeserializeObject<List<gen_inco_term_DTO>>(objData.inco_term_list.ToLower()): new List<gen_inco_term_DTO>();

                    objRet.List_paymentmethod = !string.IsNullOrEmpty(objData.payment_method_list) ? JsonConvert.DeserializeObject<List<gen_payment_method_DTO>> (objData.payment_method_list.ToLower()) : new List<gen_payment_method_DTO>();

                    objRet.List_paymentterm = !string.IsNullOrEmpty(objData.payment_term_list) ? JsonConvert.DeserializeObject<List<gen_payment_term_DTO>>(objData.payment_term_list.ToLower()) : new List<gen_payment_term_DTO>();

                    objRet.List_mode_of_transaction =!string.IsNullOrEmpty(objData.mode_of_transction_list) ? JsonConvert.DeserializeObject<List<gen_mode_of_transaction_DTO>>(objData.mode_of_transction_list) : new List<gen_mode_of_transaction_DTO>();

                    objRet.List_bank_account_info = !string.IsNullOrEmpty(objData.bank_account_info_list) ? JsonConvert.DeserializeObject<List<bank_account_info>>(objData.bank_account_info_list) : new List<bank_account_info>();

                    objRet.List_item_subgroup_info = !string.IsNullOrEmpty(objData.item_sub_group_detail_list) ? JsonConvert.DeserializeObject<List<item_subgroup_info>>(objData.item_sub_group_detail_list) : new List<item_subgroup_info>();

                    objRet.List_vat_bin_docs = !string.IsNullOrEmpty(objData.vat_bin_documnet) ? JsonConvert.DeserializeObject<List<file_upload>>(objData.vat_bin_documnet) : new List<file_upload>();

                    objRet.List_tin_docs = !string.IsNullOrEmpty(objData.tin_document) ? JsonConvert.DeserializeObject<List<file_upload>>(objData.tin_document) : new List<file_upload>();

                    objRet.List_tradelincense_docs = !string.IsNullOrEmpty(objData.trade_license_document) ?JsonConvert.DeserializeObject<List<file_upload>>(objData.trade_license_document) : new List<file_upload>();

                    if (!string.IsNullOrEmpty(objData.gen_contact_category))
                    {
                        objRet.obj_contact_category = !string.IsNullOrEmpty(objData.gen_contact_category) ? JsonConvert.DeserializeObject<gen_contact_category_DTO>(clsUtil.GetUnScapeString(objData.gen_contact_category).Trim('"')) : new gen_contact_category_DTO();
                        
                        var contact_cat_lst= new List<gen_contact_category_DTO>();
                        contact_cat_lst.Add(objRet.obj_contact_category);
                        objRet.List_contact_category = contact_cat_lst;
                    }

                    if (!string.IsNullOrEmpty(objData.credit_period))
                    {
                        objRet.obj_credit_period = !string.IsNullOrEmpty(objData.credit_period) ? JsonConvert.DeserializeObject<gen_credit_period_DTO>(clsUtil.GetUnScapeString(objRet.credit_period).Trim('"')) : new gen_credit_period_DTO();

                        var credit_pr_lst = new List<gen_credit_period_DTO>();
                        credit_pr_lst.Add(objRet.obj_credit_period);
                        objRet.List_creditperiod = credit_pr_lst;
                    }

                    if (!string.IsNullOrEmpty(objData.calculation_of_tenure))
                    {
                        objRet.obj_calculation_of_tenure = !string.IsNullOrEmpty(objData.calculation_of_tenure) ? JsonConvert.DeserializeObject<gen_calculation_of_tenure_DTO>(clsUtil.GetUnScapeString(objRet.calculation_of_tenure).Trim('"')) : new gen_calculation_of_tenure_DTO();

                        var calculation_of_tenure_lst = new List<gen_calculation_of_tenure_DTO>();
                        calculation_of_tenure_lst.Add(objRet.obj_calculation_of_tenure);
                        objRet.List_calculationoftenure = calculation_of_tenure_lst;
                    }
                    return objRet;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        
        }

        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new gen_supplier_information_entity { gen_supplier_information_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

              

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

