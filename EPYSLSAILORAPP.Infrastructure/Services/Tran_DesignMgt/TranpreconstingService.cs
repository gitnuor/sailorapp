using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using ServiceStack;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{
    public class TranpreconstingService : ITranpreconstingService
    {
        private readonly IConfiguration _configuration;
        private readonly ITranPreCostingReviewService _TranPreCostingReviewService;
        private readonly IMapper _mapper;

        public TranpreconstingService(IConfiguration configuration, IMapper mapper, ITranPreCostingReviewService TranPreCostingReviewService)
        {

            _mapper = mapper;
            _configuration = configuration;
            _TranPreCostingReviewService = TranPreCostingReviewService;

        }

        public async Task<bool> SaveAsync(tran_pre_costing_DTO entity)
        {
            try
            {

                var objtran_pre_costing = _mapper.Map<tran_pre_costing_entity>(entity);

                var list_precosting_det = _mapper.Map<List<tran_pre_costing_item_detail_entity>>(entity.List_PreCostingDet);

                var rowindex = 0;

                foreach (var objsingle in list_precosting_det)
                {
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                    objsingle.placement_info = JArray.Parse(JsonConvert.SerializeObject(entity.List_PreCostingDet[rowindex].List_placement_info));
                    rowindex++;
                }
                foreach (var objsingle in entity.List_SubContractDetl)
                {
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                }
                foreach (var objsingle in entity.List_EmbellishmentDetl)
                {
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                }

                objtran_pre_costing.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.List_detl_style_color)).ToString();
                objtran_pre_costing.item_detl_list = JArray.Parse(JsonConvert.SerializeObject(list_precosting_det)).ToString();
                objtran_pre_costing.embellishment_det_listl = JArray.Parse(JsonConvert.SerializeObject(entity.List_EmbellishmentDetl)).ToString();
                objtran_pre_costing.subcontract_det_list = JArray.Parse(JsonConvert.SerializeObject(entity.List_SubContractDetl)).ToString();

                return await tran_pre_costing_insert_sp(objtran_pre_costing);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<bool> UpdateAsync(tran_pre_costing_DTO entity)
        {
            try
            {
                var objtran_pre_costing = _mapper.Map<tran_pre_costing_entity>(entity);

                objtran_pre_costing.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.List_detl_style_color)).ToString();

                foreach (var objsingle in entity.List_PreCostingDet)
                {
                    objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                    objsingle.updated_by = entity.added_by;
                    objsingle.date_updated = entity.date_added;

                    objsingle.placement_info = JArray.Parse(JsonConvert.SerializeObject(objsingle.List_placement_info));
                }
                foreach (var objsingle in entity.List_SubContractDetl)
                {
                    objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                    objsingle.updated_by = entity.added_by;
                    objsingle.date_updated = entity.date_added;
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                }
                foreach (var objsingle in entity.List_EmbellishmentDetl)
                {
                    objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                    objsingle.updated_by = entity.added_by;
                    objsingle.date_updated = entity.date_added;
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                }

                objtran_pre_costing.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.List_detl_style_color)).ToString();
                objtran_pre_costing.item_detl_list = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_detail_entity>>(entity.List_PreCostingDet))).ToString();
                objtran_pre_costing.embellishment_det_listl = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_embellishment_detail_entity>>(entity.List_EmbellishmentDetl))).ToString();
                objtran_pre_costing.subcontract_det_list = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_subcontract_detail_entity>>(entity.List_SubContractDetl))).ToString();

                return await tran_pre_costing_update_sp(objtran_pre_costing);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> PreCostingReviewAdd(tran_pre_costing_DTO entity)
        {
            try
            {
                var objtran_pre_costing = _mapper.Map<tran_pre_costing_entity>(entity);

                objtran_pre_costing.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.List_detl_style_color)).ToString();

                foreach (var objsingle in entity.List_PreCostingDet)
                {
                    objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                    objsingle.updated_by = entity.added_by;
                    objsingle.date_updated = entity.date_added;

                    objsingle.placement_info = JArray.Parse(JsonConvert.SerializeObject(objsingle.List_placement_info));
                }
                foreach (var objsingle in entity.List_SubContractDetl)
                {
                    objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                    objsingle.updated_by = entity.added_by;
                    objsingle.date_updated = entity.date_added;
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                }
                foreach (var objsingle in entity.List_EmbellishmentDetl)
                {
                    objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                    objsingle.updated_by = entity.added_by;
                    objsingle.date_updated = entity.date_added;
                    objsingle.added_by = entity.added_by;
                    objsingle.date_added = entity.date_added;
                }

                objtran_pre_costing.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.List_detl_style_color)).ToString();
                objtran_pre_costing.item_detl_list = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_detail_entity>>(entity.List_PreCostingDet))).ToString();
                objtran_pre_costing.embellishment_det_listl = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_embellishment_detail_entity>>(entity.List_EmbellishmentDetl))).ToString();
                objtran_pre_costing.subcontract_det_list = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_subcontract_detail_entity>>(entity.List_SubContractDetl))).ToString();

                tran_pre_costing_entity last_version = new tran_pre_costing_entity();
                if (entity.pre_costing_review.tran_pre_costing_review_id != null)
                {
                    var obj_tran_pre_costing_review = await _TranPreCostingReviewService.GetSingleAsync(entity.pre_costing_review.tran_pre_costing_review_id.Value);

                    last_version = JsonConvert.DeserializeObject<List<tran_pre_costing_entity>>(obj_tran_pre_costing_review.new_data).FirstOrDefault();
                }
                else
                {

                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = @"SELECT m.*  FROM tran_pre_costing m   where m.tran_pre_costing_id=@Id";

                        last_version = connection.Query<tran_pre_costing_entity>(query,
                            new { @Id = entity.tran_pre_costing_id }).ToList().FirstOrDefault();
                    }

                }

                var model = entity.pre_costing_review;
                model.new_data = JArray.Parse(JsonConvert.SerializeObject(new List<tran_pre_costing_entity>() { objtran_pre_costing })).ToString();
                model.old_data = JArray.Parse(JsonConvert.SerializeObject(new List<tran_pre_costing_entity>() { last_version })).ToString();


                await _TranPreCostingReviewService.SaveAsync(model);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<bool> PreCostingReviewAction(tran_pre_costing_DTO entity)
        {
            try
            {

                if (entity.pre_costing_review.is_approved_by_designer == (long?)Enum_PreCostingReviewApproval_Status.ModifiedRequestByMerchant)
                {
                    var objtran_pre_costing = _mapper.Map<tran_pre_costing_entity>(entity);

                    objtran_pre_costing.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.List_detl_style_color)).ToString();

                    foreach (var objsingle in entity.List_PreCostingDet)
                    {
                        objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                        objsingle.added_by = entity.added_by;
                        objsingle.date_added = entity.date_added;
                        objsingle.updated_by = entity.added_by;
                        objsingle.date_updated = entity.date_added;

                        objsingle.placement_info = JArray.Parse(JsonConvert.SerializeObject(objsingle.List_placement_info));
                    }
                    foreach (var objsingle in entity.List_SubContractDetl)
                    {
                        objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                        objsingle.updated_by = entity.added_by;
                        objsingle.date_updated = entity.date_added;
                        objsingle.added_by = entity.added_by;
                        objsingle.date_added = entity.date_added;
                    }
                    foreach (var objsingle in entity.List_EmbellishmentDetl)
                    {
                        objsingle.tran_pre_costing_id = entity.tran_pre_costing_id;
                        objsingle.updated_by = entity.added_by;
                        objsingle.date_updated = entity.date_added;
                        objsingle.added_by = entity.added_by;
                        objsingle.date_added = entity.date_added;
                    }

                    objtran_pre_costing.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.List_detl_style_color)).ToString();
                    objtran_pre_costing.item_detl_list = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_detail_entity>>(entity.List_PreCostingDet))).ToString();
                    objtran_pre_costing.embellishment_det_listl = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_embellishment_detail_entity>>(entity.List_EmbellishmentDetl))).ToString();
                    objtran_pre_costing.subcontract_det_list = JArray.Parse(JsonConvert.SerializeObject(_mapper.Map<List<tran_pre_costing_item_subcontract_detail_entity>>(entity.List_SubContractDetl))).ToString();

                   
                    var obj_tran_pre_costing_review = await _TranPreCostingReviewService.GetSingleAsync(entity.pre_costing_review.tran_pre_costing_review_id.Value);

                    tran_pre_costing_entity old_data = JsonConvert.DeserializeObject<List<tran_pre_costing_entity>>(obj_tran_pre_costing_review.old_data).FirstOrDefault();

                    var model = entity.pre_costing_review;
                    model.new_data = JArray.Parse(JsonConvert.SerializeObject(new List<tran_pre_costing_entity>() { objtran_pre_costing })).ToString();
                    model.old_data = JArray.Parse(JsonConvert.SerializeObject(new List<tran_pre_costing_entity>() { old_data })).ToString();

                    using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                var Command = new NpgsqlCommand("tran_pre_costing_review_designer_modification", connection);

                                Command.CommandType = CommandType.StoredProcedure;

                                Command.Parameters.AddWithValue("in_tran_pre_costing_id", NpgsqlDbType.Bigint, model.tran_pre_costing_id == null ? DBNull.Value : model.tran_pre_costing_id);
                                Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, model.is_submitted == null ? DBNull.Value : model.is_submitted);
                                Command.Parameters.AddWithValue("in_is_pre_costing_submitted", NpgsqlDbType.Bigint, entity.is_submitted == null ? DBNull.Value : entity.is_submitted);
                                Command.Parameters.AddWithValue("in_is_approved_by_designer", NpgsqlDbType.Bigint, entity.pre_costing_review.is_approved_by_designer == null ? DBNull.Value : entity.pre_costing_review.is_approved_by_designer);

                                Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, model.submitted_by == null ? DBNull.Value : model.submitted_by);
                                Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, model.added_by == null ? DBNull.Value : model.added_by);
                                Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, model.date_added == null ? DBNull.Value : model.date_added);

                                Command.Parameters.AddWithValue("in_version_no", NpgsqlDbType.Text, model.version_no == null ? DBNull.Value : model.version_no);
                                Command.Parameters.AddWithValue("in_old_data", NpgsqlDbType.Text, model.old_data == null ? DBNull.Value : model.old_data);
                                Command.Parameters.AddWithValue("in_new_data", NpgsqlDbType.Text, model.new_data == null ? DBNull.Value : model.new_data);

                                
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
                else if (entity.pre_costing_review.is_approved_by_designer == (long?)Enum_PreCostingReviewApproval_Status.AckByDesigner)
                {

                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {

                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                string query = @"update tran_pre_costing
                                        set is_submitted=2,is_approved=null,approved_by=null,approve_date=null,approve_remarks=null
                                        where tran_pre_costing_id=@Id;";

                                connection.Execute(query,
                                    new
                                    {
                                        @Id = entity.tran_pre_costing_id
                                    });

                                query = @"update tran_pre_costing_review
                                    set is_approved_by_designer=@is_approved_by_designer,
                                    designer_approve_remarks=@designer_approve_remarks,
                                    designer_approved_by=@designer_approved_by,
                                    designer_approve_date=@designer_approve_date,
                                    merchant_ack_remarks=null, merchant_ack_date=null,merchant_ack_by=null,is_ack_by_merchant=null
                                    where tran_pre_costing_review_id in
                                    (select tran_pre_costing_review_id from tran_pre_costing_review where tran_pre_costing_id=@Id order by tran_pre_costing_review_id desc limit 1);";

                                connection.Execute(query,
                                    new
                                    {
                                        is_approved_by_designer = entity.pre_costing_review.is_approved_by_designer,
                                        designer_approve_remarks=entity.pre_costing_review.designer_approve_remarks,
                                        designer_approved_by=entity.pre_costing_review.designer_approved_by,
                                        designer_approve_date=entity.pre_costing_review.designer_approve_date,
                                        Id = entity.tran_pre_costing_id
                                    });

                                transaction.Commit();
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

                else if (entity.pre_costing_review.is_approved_by_designer == (long?)Enum_PreCostingReviewApproval_Status.RejectByDesigner)
                {

                    var obj_tran_pre_costing_review = await _TranPreCostingReviewService.GetSingleAsync(entity.pre_costing_review.tran_pre_costing_review_id.Value);

                    tran_pre_costing_entity old_data = JsonConvert.DeserializeObject<List<tran_pre_costing_entity>>(obj_tran_pre_costing_review.old_data).FirstOrDefault();

                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {

                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                string query = @"update tran_pre_costing
                                        set is_submitted=2,is_approved=@is_approved,approved_by=@approved_by,approve_date=@approve_date,approve_remarks=@approve_remarks
                                        where tran_pre_costing_id=@Id;";

                                connection.Execute(query,
                                    new
                                    {
                                        is_approved=old_data.is_approved,
                                        approved_by=old_data.approved_by,
                                        approve_date=old_data.approve_date,
                                        approve_remarks=old_data.approve_remarks,
                                        Id = entity.tran_pre_costing_id
                                    });

                                query = @"update tran_pre_costing_review
                                    set is_approved_by_designer=@is_approved_by_designer,merchant_ack_remarks=null,
                                    merchant_ack_date=null,merchant_ack_by=null,is_ack_by_merchant=null
                                    where tran_pre_costing_review_id in
                                    (select tran_pre_costing_review_id from tran_pre_costing_review where tran_pre_costing_id=@Id order by tran_pre_costing_review_id desc limit 1);";

                                connection.Execute(query,
                                    new
                                    {
                                        is_approved_by_designer = entity.pre_costing_review.is_approved_by_designer,
                                        Id = entity.tran_pre_costing_id
                                    });

                                transaction.Commit();
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


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> tran_pre_costing_approve_reject(tran_pre_costing_DTO objtran_pre_costing)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_pre_costing_approve_reject", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_pre_costing_id", NpgsqlDbType.Bigint, objtran_pre_costing.tran_pre_costing_id == null ? DBNull.Value : objtran_pre_costing.tran_pre_costing_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_pre_costing.is_submitted == null ? DBNull.Value : objtran_pre_costing.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_pre_costing.is_approved == null ? DBNull.Value : objtran_pre_costing.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_pre_costing.approved_by == null ? DBNull.Value : objtran_pre_costing.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_pre_costing.approve_date == null ? DBNull.Value : objtran_pre_costing.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_pre_costing.approve_remarks == null ? DBNull.Value : objtran_pre_costing.approve_remarks);

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

        public async Task<List<tran_pre_costing_DTO>> GetAllAsync()
        {
            List<tran_pre_costing_entity> list = new List<tran_pre_costing_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_pre_costing_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<tran_pre_costing_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_pre_costing_DTO> GetAsync(long Id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_pre_costing( @tran_pre_costing_id_filter)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_pre_costing_DTO>(query,
                          new
                          {

                              tran_pre_costing_id_filter = Id
                          }
                         ).AsList().FirstOrDefault();

                    var ret_precosting = JsonConvert.DeserializeObject<tran_pre_costing_DTO>(JsonConvert.SerializeObject(dataList));

                    if (dataList != null)
                    {

                        ret_precosting.current_state = 2;

                        var ret_detail_list = JsonConvert.DeserializeObject<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>>(dataList.tran_pre_costing_item_detail_list);
                        var ret_subcontractdetail_list = JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_entity>>(dataList.subcontract_det_list);
                        var ret_embellishmentdetail_list = JsonConvert.DeserializeObject<List<tran_pre_costing_item_embellishment_detail_entity>>(dataList.embellishment_det_listl);

                        if (ret_detail_list.Count > 0)
                        {
                            ret_detail_list.ForEach(item => { item.current_state = 2; });
                            ret_precosting.List_PreCostingDet_rpc = ret_detail_list;
                        }
                        if (ret_subcontractdetail_list.Count > 0)
                        {
                            ret_precosting.List_SubContractDetl = _mapper.Map<List<tran_pre_costing_item_subcontract_detail_DTO>>(ret_subcontractdetail_list);
                            ret_precosting.List_SubContractDetl.ForEach(item => { item.current_state = 2; });
                        }
                        if (ret_embellishmentdetail_list.Count > 0)
                        {
                            ret_precosting.List_EmbellishmentDetl = _mapper.Map<List<tran_pre_costing_item_embellishment_detail_DTO>>(ret_embellishmentdetail_list);
                            ret_precosting.List_EmbellishmentDetl.ForEach(item => { item.current_state = 2; });
                        }

                        ret_precosting.List_detl_style_color = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(dataList.color_wise_size_quantity);

                    }

                    return ret_precosting;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> tran_pre_costing_insert_sp(tran_pre_costing_entity objtran_pre_costing)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_pre_costing_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_sample_order_id", NpgsqlDbType.Bigint, objtran_pre_costing.tran_sample_order_id == null ? DBNull.Value : objtran_pre_costing.tran_sample_order_id);
                        Command.Parameters.AddWithValue("in_pre_costing_date", NpgsqlDbType.Date, objtran_pre_costing.pre_costing_date == null ? DBNull.Value : objtran_pre_costing.pre_costing_date);
                        Command.Parameters.AddWithValue("in_total_raw_material_cost", NpgsqlDbType.Numeric, objtran_pre_costing.total_raw_material_cost == null ? DBNull.Value : objtran_pre_costing.total_raw_material_cost);
                        Command.Parameters.AddWithValue("in_total_raw_material_percentage", NpgsqlDbType.Numeric, objtran_pre_costing.total_raw_material_percentage == null ? DBNull.Value : objtran_pre_costing.total_raw_material_percentage);
                        Command.Parameters.AddWithValue("in_factory_overhead_cost", NpgsqlDbType.Numeric, objtran_pre_costing.factory_overhead_cost == null ? DBNull.Value : objtran_pre_costing.factory_overhead_cost);
                        Command.Parameters.AddWithValue("in_sales_marketing_distribution_cost", NpgsqlDbType.Numeric, objtran_pre_costing.sales_marketing_distribution_cost == null ? DBNull.Value : objtran_pre_costing.sales_marketing_distribution_cost);
                        Command.Parameters.AddWithValue("in_depreciation_amortization_cost", NpgsqlDbType.Numeric, objtran_pre_costing.depreciation_amortization_cost == null ? DBNull.Value : objtran_pre_costing.depreciation_amortization_cost);
                        Command.Parameters.AddWithValue("in_total_overhead_cost", NpgsqlDbType.Numeric, objtran_pre_costing.total_overhead_cost == null ? DBNull.Value : objtran_pre_costing.total_overhead_cost);
                        Command.Parameters.AddWithValue("in_total_production_cost", NpgsqlDbType.Numeric, objtran_pre_costing.total_production_cost == null ? DBNull.Value : objtran_pre_costing.total_production_cost);
                        Command.Parameters.AddWithValue("in_floor_price_percentage", NpgsqlDbType.Numeric, objtran_pre_costing.floor_price_percentage == null ? DBNull.Value : objtran_pre_costing.floor_price_percentage);
                        Command.Parameters.AddWithValue("in_floor_price_per_pc", NpgsqlDbType.Numeric, objtran_pre_costing.floor_price_per_pc == null ? DBNull.Value : objtran_pre_costing.floor_price_per_pc);
                        Command.Parameters.AddWithValue("in_desired_markup_percentage", NpgsqlDbType.Numeric, objtran_pre_costing.desired_markup_percentage == null ? DBNull.Value : objtran_pre_costing.desired_markup_percentage);
                        Command.Parameters.AddWithValue("in_estimated_markup_price", NpgsqlDbType.Numeric, objtran_pre_costing.estimated_markup_price == null ? DBNull.Value : objtran_pre_costing.estimated_markup_price);
                        Command.Parameters.AddWithValue("in_desired_markup_price", NpgsqlDbType.Numeric, objtran_pre_costing.desired_markup_price == null ? DBNull.Value : objtran_pre_costing.desired_markup_price);
                        Command.Parameters.AddWithValue("in_final_mrp", NpgsqlDbType.Numeric, objtran_pre_costing.final_mrp == null ? DBNull.Value : objtran_pre_costing.final_mrp);
                        Command.Parameters.AddWithValue("in_total_style_quantity_mrp", NpgsqlDbType.Numeric, objtran_pre_costing.total_style_quantity_mrp == null ? DBNull.Value : objtran_pre_costing.total_style_quantity_mrp);
                        Command.Parameters.AddWithValue("in_suggested_mrp_with_cost", NpgsqlDbType.Numeric, objtran_pre_costing.suggested_mrp_with_cost == null ? DBNull.Value : objtran_pre_costing.suggested_mrp_with_cost);
                        Command.Parameters.AddWithValue("in_smv", NpgsqlDbType.Text, objtran_pre_costing.smv == null ? DBNull.Value : objtran_pre_costing.smv);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_pre_costing.remarks == null ? DBNull.Value : objtran_pre_costing.remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_pre_costing.added_by == null ? DBNull.Value : objtran_pre_costing.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_pre_costing.date_added == null ? DBNull.Value : objtran_pre_costing.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_pre_costing.updated_by == null ? DBNull.Value : objtran_pre_costing.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_pre_costing.date_updated == null ? DBNull.Value : objtran_pre_costing.date_updated);
                        Command.Parameters.AddWithValue("in_color_wise_size_quantity", NpgsqlDbType.Text, objtran_pre_costing.color_wise_size_quantity == null ? DBNull.Value : objtran_pre_costing.color_wise_size_quantity);
                        Command.Parameters.AddWithValue("in_pre_costing_quantity", NpgsqlDbType.Bigint, objtran_pre_costing.pre_costing_quantity == null ? DBNull.Value : objtran_pre_costing.pre_costing_quantity);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_pre_costing.is_submitted == null ? DBNull.Value : objtran_pre_costing.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_pre_costing.submitted_by == null ? DBNull.Value : objtran_pre_costing.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_pre_costing.is_approved == null ? DBNull.Value : objtran_pre_costing.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_pre_costing.approved_by == null ? DBNull.Value : objtran_pre_costing.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_pre_costing.approve_date == null ? DBNull.Value : objtran_pre_costing.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_pre_costing.approve_remarks == null ? DBNull.Value : objtran_pre_costing.approve_remarks);
                        Command.Parameters.AddWithValue("in_item_detl_list", NpgsqlDbType.Text, objtran_pre_costing.item_detl_list == null ? DBNull.Value : objtran_pre_costing.item_detl_list);
                        Command.Parameters.AddWithValue("in_embellishment_det_listl", NpgsqlDbType.Text, objtran_pre_costing.embellishment_det_listl == null ? DBNull.Value : objtran_pre_costing.embellishment_det_listl);
                        Command.Parameters.AddWithValue("in_subcontract_det_list", NpgsqlDbType.Text, objtran_pre_costing.subcontract_det_list == null ? DBNull.Value : objtran_pre_costing.subcontract_det_list);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_pre_costing.fiscal_year_id == null ? DBNull.Value : objtran_pre_costing.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_pre_costing.event_id == null ? DBNull.Value : objtran_pre_costing.event_id);


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
        public async Task<bool> tran_pre_costing_update_sp(tran_pre_costing_entity objtran_pre_costing)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_pre_costing_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_pre_costing_id", NpgsqlDbType.Bigint, objtran_pre_costing.tran_pre_costing_id);
                        Command.Parameters.AddWithValue("in_tran_sample_order_id", NpgsqlDbType.Bigint, objtran_pre_costing.tran_sample_order_id == null ? DBNull.Value : objtran_pre_costing.tran_sample_order_id);
                        Command.Parameters.AddWithValue("in_pre_costing_date", NpgsqlDbType.Date, objtran_pre_costing.pre_costing_date == null ? DBNull.Value : objtran_pre_costing.pre_costing_date);
                        Command.Parameters.AddWithValue("in_total_raw_material_cost", NpgsqlDbType.Numeric, objtran_pre_costing.total_raw_material_cost == null ? DBNull.Value : objtran_pre_costing.total_raw_material_cost);
                        Command.Parameters.AddWithValue("in_total_raw_material_percentage", NpgsqlDbType.Numeric, objtran_pre_costing.total_raw_material_percentage == null ? DBNull.Value : objtran_pre_costing.total_raw_material_percentage);
                        Command.Parameters.AddWithValue("in_factory_overhead_cost", NpgsqlDbType.Numeric, objtran_pre_costing.factory_overhead_cost == null ? DBNull.Value : objtran_pre_costing.factory_overhead_cost);
                        Command.Parameters.AddWithValue("in_sales_marketing_distribution_cost", NpgsqlDbType.Numeric, objtran_pre_costing.sales_marketing_distribution_cost == null ? DBNull.Value : objtran_pre_costing.sales_marketing_distribution_cost);
                        Command.Parameters.AddWithValue("in_depreciation_amortization_cost", NpgsqlDbType.Numeric, objtran_pre_costing.depreciation_amortization_cost == null ? DBNull.Value : objtran_pre_costing.depreciation_amortization_cost);
                        Command.Parameters.AddWithValue("in_total_overhead_cost", NpgsqlDbType.Numeric, objtran_pre_costing.total_overhead_cost == null ? DBNull.Value : objtran_pre_costing.total_overhead_cost);
                        Command.Parameters.AddWithValue("in_total_production_cost", NpgsqlDbType.Numeric, objtran_pre_costing.total_production_cost == null ? DBNull.Value : objtran_pre_costing.total_production_cost);
                        Command.Parameters.AddWithValue("in_floor_price_percentage", NpgsqlDbType.Numeric, objtran_pre_costing.floor_price_percentage == null ? DBNull.Value : objtran_pre_costing.floor_price_percentage);
                        Command.Parameters.AddWithValue("in_floor_price_per_pc", NpgsqlDbType.Numeric, objtran_pre_costing.floor_price_per_pc == null ? DBNull.Value : objtran_pre_costing.floor_price_per_pc);
                        Command.Parameters.AddWithValue("in_desired_markup_percentage", NpgsqlDbType.Numeric, objtran_pre_costing.desired_markup_percentage == null ? DBNull.Value : objtran_pre_costing.desired_markup_percentage);
                        Command.Parameters.AddWithValue("in_estimated_markup_price", NpgsqlDbType.Numeric, objtran_pre_costing.estimated_markup_price == null ? DBNull.Value : objtran_pre_costing.estimated_markup_price);
                        Command.Parameters.AddWithValue("in_desired_markup_price", NpgsqlDbType.Numeric, objtran_pre_costing.desired_markup_price == null ? DBNull.Value : objtran_pre_costing.desired_markup_price);
                        Command.Parameters.AddWithValue("in_final_mrp", NpgsqlDbType.Numeric, objtran_pre_costing.final_mrp == null ? DBNull.Value : objtran_pre_costing.final_mrp);
                        Command.Parameters.AddWithValue("in_total_style_quantity_mrp", NpgsqlDbType.Numeric, objtran_pre_costing.total_style_quantity_mrp == null ? DBNull.Value : objtran_pre_costing.total_style_quantity_mrp);
                        Command.Parameters.AddWithValue("in_suggested_mrp_with_cost", NpgsqlDbType.Numeric, objtran_pre_costing.suggested_mrp_with_cost == null ? DBNull.Value : objtran_pre_costing.suggested_mrp_with_cost);
                        Command.Parameters.AddWithValue("in_smv", NpgsqlDbType.Text, objtran_pre_costing.smv == null ? DBNull.Value : objtran_pre_costing.smv);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_pre_costing.remarks == null ? DBNull.Value : objtran_pre_costing.remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_pre_costing.added_by == null ? DBNull.Value : objtran_pre_costing.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_pre_costing.date_added == null ? DBNull.Value : objtran_pre_costing.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_pre_costing.updated_by == null ? DBNull.Value : objtran_pre_costing.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_pre_costing.date_updated == null ? DBNull.Value : objtran_pre_costing.date_updated);
                        Command.Parameters.AddWithValue("in_color_wise_size_quantity", NpgsqlDbType.Text, objtran_pre_costing.color_wise_size_quantity == null ? DBNull.Value : objtran_pre_costing.color_wise_size_quantity);
                        Command.Parameters.AddWithValue("in_pre_costing_quantity", NpgsqlDbType.Bigint, objtran_pre_costing.pre_costing_quantity == null ? DBNull.Value : objtran_pre_costing.pre_costing_quantity);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_pre_costing.is_submitted == null ? DBNull.Value : objtran_pre_costing.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_pre_costing.submitted_by == null ? DBNull.Value : objtran_pre_costing.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_pre_costing.is_approved == null ? DBNull.Value : objtran_pre_costing.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_pre_costing.approved_by == null ? DBNull.Value : objtran_pre_costing.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_pre_costing.approve_date == null ? DBNull.Value : objtran_pre_costing.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_pre_costing.approve_remarks == null ? DBNull.Value : objtran_pre_costing.approve_remarks);
                        Command.Parameters.AddWithValue("in_item_detl_list", NpgsqlDbType.Text, objtran_pre_costing.item_detl_list == null ? DBNull.Value : objtran_pre_costing.item_detl_list);
                        Command.Parameters.AddWithValue("in_embellishment_det_listl", NpgsqlDbType.Text, objtran_pre_costing.embellishment_det_listl == null ? DBNull.Value : objtran_pre_costing.embellishment_det_listl);
                        Command.Parameters.AddWithValue("in_subcontract_det_list", NpgsqlDbType.Text, objtran_pre_costing.subcontract_det_list == null ? DBNull.Value : objtran_pre_costing.subcontract_det_list);

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
    }

}

