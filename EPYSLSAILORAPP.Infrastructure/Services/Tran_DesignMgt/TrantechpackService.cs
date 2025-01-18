using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using Postgrest;
using Serilog.Context;
using ServiceStack;
using System.Data;
using System.Drawing.Printing;
using Web.Core.Frame.Helpers;
using static Dapper.SqlMapper;
using static Postgrest.Constants;
using static Postgrest.QueryOptions;

namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{
    public class TrantechpackService : ITrantechpackService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly ICommonService _CommonService;

        public TrantechpackService(IConfiguration configuration, IMapper mapper, IHostingEnvironment hostingEnvironment, ICommonService CommonService)
        {

            _mapper = mapper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _CommonService = CommonService;
        }

        public async Task<bool> SaveAsync(tran_tech_pack_DTO entity)
        {
            string folderPath = _configuration.GetValue<string>("UploadFolderPath");

            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder"), "techpack");

            try
            {
                var model = _mapper.Map<tran_tech_pack_entity>(entity);

                model.product_composition = JsonConvert.SerializeObject(entity.obj_product_composition);
                model.sleeve_details = JsonConvert.SerializeObject(entity.obj_sleeve_details);

                if (entity.List_base64String_Techpack_File.Count > 0)
                {

                    foreach (var singleimage in entity.List_base64String_Techpack_File)
                    {

                        if (singleimage.current_state == 1) //add
                        {
                            singleimage.server_filename = Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);

                            string base64String = singleimage.base64string;

                            byte[] byteArray = Convert.FromBase64String(base64String);

                            try
                            {

                                if (!Directory.Exists(filePath))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                                }

                                File.WriteAllBytes(Path.Combine(filePath, singleimage.server_filename), byteArray);
                            }
                            catch (Exception es)
                            {

                            }

                            singleimage.base64string = "";
                        }
                        else if (singleimage.current_state == 3) //delete
                        {

                            if (File.Exists(Path.Combine(filePath, singleimage.server_filename)))
                            {
                                File.Delete(Path.Combine(filePath, singleimage.server_filename));
                            }
                        }

                    }


                    model.photos = JArray.Parse(JsonConvert.SerializeObject(entity.List_base64String_Techpack_File)).ToString();

                }

                model.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.TechPack_ColorList)).ToString();

                var techpack_embelishment_List = _mapper.Map<List<tran_tech_pack_embellishment_info_entity>>(entity.TechPack_EmbellishmentList);

                for (var index = 0; index < techpack_embelishment_List.Count; index++)
                {
                    techpack_embelishment_List[index].date_added = DateTime.Now;
                    techpack_embelishment_List[index].added_by = entity.added_by;
                    techpack_embelishment_List[index].supplier_info = JsonConvert.SerializeObject(entity.TechPack_EmbellishmentList[index].ddlsupplier_info);
                    techpack_embelishment_List[index].embellishment_details = JArray.Parse(JsonConvert.SerializeObject(entity.TechPack_EmbellishmentList[index].EmbelshmentDet_List)).ToString();
                }


                model.embellishment_detl = JArray.Parse(JsonConvert.SerializeObject(techpack_embelishment_List)).ToString();

                return await tran_tech_pack_insert_sp(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> UpdateAsync(tran_tech_pack_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_tech_pack_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Update(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<bool> UpdateAck(long Id, long userid)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"UPDATE tran_tech_pack SET is_ack=1,ack_date=now(),date_updated=now(),updated_by=@updated_by
                                    where tran_techpack_id=@Id";

                    var dataList = connection.Execute(query,
                        new { updated_by = userid, Id = Id });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync_Extended(tran_tech_pack_DTO entity)
        {
            string folderPath = _configuration.GetValue<string>("UploadFolderPath");

            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder"), "techpack");

            try
            {

                var model = _mapper.Map<tran_tech_pack_entity>(entity);

                if (entity.List_base64String_Techpack_File.Count > 0)
                {

                    foreach (var singleimage in entity.List_base64String_Techpack_File)
                    {

                        if (singleimage.current_state == 1) //add
                        {
                            singleimage.server_filename = Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);

                            string base64String = singleimage.base64string;

                            byte[] byteArray = Convert.FromBase64String(base64String);

                            try
                            {

                                if (!Directory.Exists(filePath))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                                }

                                File.WriteAllBytes(Path.Combine(filePath, singleimage.server_filename), byteArray);
                            }
                            catch (Exception es)
                            {

                            }

                            singleimage.base64string = "";
                        }
                        else if (singleimage.current_state == 3) //delete
                        {

                            if (File.Exists(Path.Combine(filePath, singleimage.server_filename)))
                            {

                                File.Delete(Path.Combine(filePath, singleimage.server_filename));
                            }
                        }

                    }

                }

                model.photos = JArray.Parse(JsonConvert.SerializeObject(entity.List_base64String_Techpack_File.Where(p => p.current_state == 1 || p.current_state == 2).ToList())).ToString();

                model.product_composition = JsonConvert.SerializeObject(entity.obj_product_composition);
                model.sleeve_details = JsonConvert.SerializeObject(entity.obj_sleeve_details);

                model.color_wise_size_quantity = JArray.Parse(JsonConvert.SerializeObject(entity.TechPack_ColorList)).ToString();

                var techpack_embelishment_List = _mapper.Map<List<tran_tech_pack_embellishment_info_entity>>(entity.TechPack_EmbellishmentList);

                for (var index = 0; index < techpack_embelishment_List.Count; index++)
                {
                    techpack_embelishment_List[index].date_added = DateTime.Now;
                    techpack_embelishment_List[index].added_by = entity.added_by;
                    techpack_embelishment_List[index].date_updated = DateTime.Now;
                    techpack_embelishment_List[index].updated_by = entity.added_by;
                    techpack_embelishment_List[index].supplier_info = JsonConvert.SerializeObject(entity.TechPack_EmbellishmentList[index].ddlsupplier_info);
                    techpack_embelishment_List[index].embellishment_details = JsonConvert.SerializeObject(entity.TechPack_EmbellishmentList[index].EmbelshmentDet_List);
                }

                model.embellishment_detl = JsonConvert.SerializeObject(techpack_embelishment_List);

                return await tran_tech_pack_update_sp(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_tech_pack_entity>> GetAllAsync()
        {
            List<tran_tech_pack_entity> list = new List<tran_tech_pack_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_tech_pack_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_tech_pack_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<tran_tech_pack_entity>> GetTechPackListWithAvlFabric(DtParameters filter)
        {
            List<tran_tech_pack_entity> list = new List<tran_tech_pack_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (select ttp.tran_techpack_id,
                                            ttp.techpack_number || '(' || ttp.teckpack_style_code || ')' as techpack_number,
                                           tdr.tran_designer_review_id,
                                           (SELECT string_agg(plc.server_filename, '|')
                                            FROM public.tran_plan_allocate pa
                                                     INNER JOIN public.tran_sample_order so ON so.tran_va_plan_detl_id = pa.tran_va_plan_detl_id
                                                     INNER JOIN public.tran_pre_costing pc ON pc.tran_sample_order_id = so.tran_sample_order_id
                                                     INNER JOIN public.tran_designer_review dr ON dr.tran_pre_costing_id = pc.tran_pre_costing_id
                                                     CROSS JOIN LATERAL
                                                jsonb_array_elements(so.sample_photos::jsonb) AS placement_element
                                                     JOIN LATERAL
                                                json_populate_record(null::gen_file_upload, placement_element::json) AS plc ON true
                                            WHERE dr.tran_designer_review_id = tdr.tran_designer_review_id
                                              and plc.imagetype = 'Front') AS photos
                                    from tran_pre_costing_item_detail pd
                                             inner join tran_pre_costing p on p.tran_pre_costing_id = pd.tran_pre_costing_id
                                             inner join tran_designer_review tdr on p.tran_pre_costing_id = tdr.tran_pre_costing_id
                                             inner join tran_tech_pack ttp on tdr.tran_designer_review_id = ttp.tran_designer_review_id
                                            inner join gen_item_stock_master m on m.item_master_id=pd.gen_item_master_id and m.tran_techpack_id=ttp.tran_techpack_id
                                    where pd.gen_item_master_id = @gen_item_master_id and m.tran_techpack_id is not null
                                     and ttp.fiscal_year_id=@fiscal_year_id and ttp.event_id=@event_id
                                    group by ttp.tran_techpack_id, ttp.techpack_number,tdr.tran_designer_review_id)


                            SELECT cte_saved.*,
                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                            FROM cte_saved
                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_tech_pack_entity>(query,
                          new { 
                              gen_item_master_id= filter.MasterID,
                              fiscal_year_id= filter.fiscal_year_id,
                              event_id= filter.event_id,
                              row_index=filter.Start,
                              page_size=filter.Length
                          }
                         ).AsList();


                    foreach (var obj in dataList)
                    {
                        var image_source = "";

                        if (!string.IsNullOrWhiteSpace(obj.photos))
                        {
                            try
                            {
                                file_upload objfile = new file_upload();
                                objfile.server_filename = obj.photos;
                                objfile.filePath = "sample_order";

                                var objFile = await _CommonService.LoadSingleFiles(objfile, "purchase_order");

                                image_source = objfile.base64string;
                            }
                            catch (Exception ex)
                            {

                            }

                            obj.photos = @"<span><img style='display: inline-block;height:80px;width:80px;' src=" + image_source + " /> " + obj.techpack_number + "</span>";
                        }
                    }

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<tran_tech_pack_entity> GetAsync(long Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_tech_pack m   where m.tran_techpack_id=@Id";

                    var dataList = connection.Query<tran_tech_pack_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_tech_pack_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<tran_tech_pack_DTO> GetTechpackByID(long Id)
        {
            List<file_upload> objfile_list = new List<file_upload>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_tech_pack_by_techpackid( @tran_tech_pack_id_filter)";

                    var objData = connection.Query<tran_tech_pack_entity>(query,
                        new { @tran_tech_pack_id_filter = Id }).ToList().FirstOrDefault();

                    tran_tech_pack_DTO retobj = JsonConvert.DeserializeObject<tran_tech_pack_DTO>(JsonConvert.SerializeObject(objData));

                    retobj.TechPack_ColorList = JsonConvert.DeserializeObject<List<tran_tech_pack_color_DTO>>(objData.color_wise_size_quantity.ToString());

                    retobj.TechPack_EmbellishmentList = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_info_DTO>>(objData.embellishment_detl);

                    var TechPack_EmbellishmentListIndex = 0;

                    foreach (var singleembellish in retobj.TechPack_EmbellishmentList)
                    {
                        singleembellish.EmbelshmentDet_List = JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_det_DTO>>(clsUtil.CleanJsonString(singleembellish.embellishment_details));

                        singleembellish.ddlsupplier_info = JsonConvert.DeserializeObject<dropdown_entity>(clsUtil.CleanJsonString(retobj.TechPack_EmbellishmentList[TechPack_EmbellishmentListIndex].supplier_info));

                        TechPack_EmbellishmentListIndex++;
                    }

                    if (!string.IsNullOrEmpty(objData.photos))
                    {
                        var photo_list = JsonConvert.DeserializeObject<List<file_upload>>(objData.photos);

                        foreach (var file in photo_list)
                        {
                            try
                            {
                                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/techpack/" + file.server_filename;

                                if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                {
                                    var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                    string base64String = Convert.ToBase64String(bytes);

                                    var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                    if (file_extension == "pdf")
                                    {
                                        file.base64string = base64String;
                                    }
                                    else
                                    {
                                        file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                    }

                                    file.current_state = 2;

                                    objfile_list.Add(file);
                                }

                            }
                            catch (Exception ex)
                            {

                                using (LogContext.PushProperty("SpecialLogType", true))
                                {
                                    // _logger.LogError(ex.ToString());
                                }
                            }
                        }
                    }

                    retobj.List_base64String_Techpack_File = objfile_list;

                    return retobj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_tech_pack_entity>> GetPagedData(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_tech_pack m
                                           where 
                                                     m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id
                                                     and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.techpack_number ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_tech_pack_entity>(query,
                        new
                        {
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id,
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<tran_tech_pack_entity>> GetPagedDataForSelect2(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT ttp.tran_techpack_id,
                                       'Techpack No - '||ttp.techpack_number || '<br/>' ||
                                       'Style No - '|| ttp.teckpack_style_code || '<br/>' ||
                                        'Date - '|| trim(TO_CHAR(ttp.techpack_date, 'DD-MM-YYYY')) || '<br/>' ||
                                       'Product-<br/>'|| vp.master_category_name|| ' - '|| vp.style_item_product_name  ||'<br/>' ||
                                       'Designer-<br/>'  || tm_designer.name || '<br/>' ||
                                       'Merchant-<br/>' || tm_merchant.name
                                           as techpack_number,
                                             (
                                             SELECT string_agg(plc.server_filename, '|')
                                             FROM public.tran_plan_allocate pa
                                             INNER JOIN public.tran_sample_order so ON so.tran_va_plan_detl_id = pa.tran_va_plan_detl_id
                                             INNER JOIN public.tran_pre_costing pc ON pc.tran_sample_order_id = so.tran_sample_order_id
                                             INNER JOIN public.tran_designer_review dr ON dr.tran_pre_costing_id = pc.tran_pre_costing_id
                                             CROSS JOIN LATERAL
                                             jsonb_array_elements(so.sample_photos::jsonb) AS placement_element
                                             JOIN LATERAL
                                             json_populate_record(null::gen_file_upload, placement_element::json) AS plc ON true
                                             WHERE dr.tran_designer_review_id = ttp.tran_designer_review_id and plc.imagetype='Front'
                                         ) AS photos
                                        FROM vw_style_item_product vp
                                         INNER JOIN public.style_product_size_group spsg
                                                    ON spsg.style_product_size_group_id = vp.style_product_size_group_id
                                         INNER JOIN tran_range_plan_details rpd ON rpd.style_item_product_id = vp.style_item_product_id
                                         INNER JOIN tran_range_plan rp ON rp.range_plan_id = rpd.range_plan_id

                                         INNER JOIN public.tran_plan_allocate tvpd ON tvpd.style_item_product_id = vp.style_item_product_id
                                    AND tvpd.range_plan_detail_id = rpd.range_plan_detail_id

                                         INNER JOIN public.tran_sample_order tso ON tso.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                                         INNER JOIN public.tran_pre_costing tpc ON tso.tran_sample_order_id = tpc.tran_sample_order_id
                                         INNER JOIN public.tran_plan_allocate_style tvapds
                                                    on tvapds.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                                                        and tso.tran_va_plan_detl_style_id = tvapds.tran_va_plan_detl_style_id
                                         INNER JOIN public.owin_user tm ON tvapds.designer_member_id = tm.userid
                                         inner join public.gen_unit gu on tso.delivery_unit_id = gu.gen_unit_id
                                         inner join public.tran_designer_review tdr on tdr.tran_pre_costing_id = tpc.tran_pre_costing_id
                                         inner join public.tran_tech_pack ttp on ttp.tran_designer_review_id = tdr.tran_designer_review_id

                                         INNER JOIN public.owin_user tm_designer
                                                    ON tvapds.designer_member_id = tm_designer.userid
                                         INNER JOIN public.owin_user tm_merchant ON ttp.merchandiser_id = tm_merchant.userid

                                 where
                                           ttp.fiscal_year_id=@fiscal_year_id and ttp.event_id=@event_id
                                           and case
                                           when @search_text is null or length(@search_text)=0 then true
                                           when @search_text is not null and length(@search_text)>0 and (
                                                  ttp.techpack_number ilike '%' || @search_text || '%' or
                                                  tso.tran_sample_order_number ilike '%' || @search_text || '%'
                                               ) then true
                                           else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_tech_pack_entity>(query,
                        new
                        {
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id,
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    foreach (var obj in dataList)
                    {
                        var image_source = "";

                        if (!string.IsNullOrWhiteSpace(obj.photos))
                        {
                            try
                            {
                                file_upload objfile = new file_upload();
                                objfile.server_filename = obj.photos;
                                objfile.filePath = "sample_order";

                                var objFile = await _CommonService.LoadSingleFiles(objfile, "purchase_order");

                                image_source = objfile.base64string;
                            }
                            catch (Exception ex)
                            {

                            }

                            obj.photos = @"<table><tr><td style='width:90px;'><img style='display: inline-block;height:80px;width:80px;' src=" + image_source + " /></td><td> " + obj.techpack_number + "</td></tr></table>";
                        }
                    }

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_tech_pack_entity>> GetPagedDataForSelectFinishProductionProcess(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT distinct ttp.tran_techpack_id,
                                       'Techpack No - '||ttp.techpack_number || '<br/>' ||
                                       'Style No - '|| ttp.teckpack_style_code || '<br/>' ||
                                        'Date - '|| trim(TO_CHAR(ttp.techpack_date, 'DD-MM-YYYY')) || '<br/>' ||
                                       'Product-<br/>'|| vp.master_category_name|| ' - '|| vp.style_item_product_name  ||'<br/>' ||
                                       'Designer-<br/>'  || tm_designer.name || '<br/>' ||
                                       'Merchant-<br/>' || tm_merchant.name
                                           as techpack_number,
                                             (
                                             SELECT string_agg(plc.server_filename, '|')
                                             FROM public.tran_plan_allocate pa
                                             INNER JOIN public.tran_sample_order so ON so.tran_va_plan_detl_id = pa.tran_va_plan_detl_id
                                             INNER JOIN public.tran_pre_costing pc ON pc.tran_sample_order_id = so.tran_sample_order_id
                                             INNER JOIN public.tran_designer_review dr ON dr.tran_pre_costing_id = pc.tran_pre_costing_id
                                             CROSS JOIN LATERAL
                                             jsonb_array_elements(so.sample_photos::jsonb) AS placement_element
                                             JOIN LATERAL
                                             json_populate_record(null::gen_file_upload, placement_element::json) AS plc ON true
                                             WHERE dr.tran_designer_review_id = ttp.tran_designer_review_id and plc.imagetype='Front'
                                         ) AS photos
                                        FROM vw_style_item_product vp
                                         INNER JOIN public.style_product_size_group spsg
                                                    ON spsg.style_product_size_group_id = vp.style_product_size_group_id
                                         INNER JOIN tran_range_plan_details rpd ON rpd.style_item_product_id = vp.style_item_product_id
                                         INNER JOIN tran_range_plan rp ON rp.range_plan_id = rpd.range_plan_id

                                         INNER JOIN public.tran_plan_allocate tvpd ON tvpd.style_item_product_id = vp.style_item_product_id
                                    AND tvpd.range_plan_detail_id = rpd.range_plan_detail_id

                                         INNER JOIN public.tran_sample_order tso ON tso.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                                         INNER JOIN public.tran_pre_costing tpc ON tso.tran_sample_order_id = tpc.tran_sample_order_id
                                         INNER JOIN public.tran_plan_allocate_style tvapds
                                                    on tvapds.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                                                        and tso.tran_va_plan_detl_style_id = tvapds.tran_va_plan_detl_style_id
                                         INNER JOIN public.owin_user tm ON tvapds.designer_member_id = tm.userid
                                         inner join public.gen_unit gu on tso.delivery_unit_id = gu.gen_unit_id
                                         inner join public.tran_designer_review tdr on tdr.tran_pre_costing_id = tpc.tran_pre_costing_id
                                         inner join public.tran_tech_pack ttp on ttp.tran_designer_review_id = tdr.tran_designer_review_id

                                         INNER JOIN public.owin_user tm_designer
                                                    ON tvapds.designer_member_id = tm_designer.userid
                                         INNER JOIN public.owin_user tm_merchant ON ttp.merchandiser_id = tm_merchant.userid

                                         inner join tran_finishing_receive tfr on ttp.tran_techpack_id=tfr.techpack_id

                                 where
                                           tfr.fiscal_year_id=@fiscal_year_id and tfr.event_id=@event_id
                                           and case
                                           when @search_text is null or length(@search_text)=0 then true
                                           when @search_text is not null and length(@search_text)>0 and (
                                                  ttp.techpack_number ilike '%' || @search_text || '%' or
                                                  tso.tran_sample_order_number ilike '%' || @search_text || '%'
                                               ) then true
                                           else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_tech_pack_entity>(query,
                        new
                        {
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id,
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    foreach (var obj in dataList)
                    {
                        var image_source = "";

                        if (!string.IsNullOrWhiteSpace(obj.photos))
                        {
                            try
                            {
                                file_upload objfile = new file_upload();
                                objfile.server_filename = obj.photos;
                                objfile.filePath = "sample_order";

                                var objFile = await _CommonService.LoadSingleFiles(objfile, "purchase_order");

                                image_source = objfile.base64string;
                            }
                            catch (Exception ex)
                            {

                            }

                            obj.photos = @"<table><tr><td style='width:90px;'><img style='display: inline-block;height:80px;width:80px;' src=" + image_source + " /></td><td> " + obj.techpack_number + "</td></tr></table>";
                        }
                    }

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<tran_tech_pack_entity>> TechPackDetailListForPPMeeting(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.tran_techpack_id,
                                       m.techpack_number || '(' || m.teckpack_style_code || ')' as techpack_number,
                                       (SELECT string_agg(plc.server_filename, '|')
                                        FROM public.tran_plan_allocate pa
                                                 INNER JOIN public.tran_sample_order so ON so.tran_va_plan_detl_id = pa.tran_va_plan_detl_id
                                                 INNER JOIN public.tran_pre_costing pc ON pc.tran_sample_order_id = so.tran_sample_order_id
                                                 INNER JOIN public.tran_designer_review dr ON dr.tran_pre_costing_id = pc.tran_pre_costing_id
                                                 CROSS JOIN LATERAL
                                            jsonb_array_elements(so.sample_photos::jsonb) AS placement_element
                                                 JOIN LATERAL
                                            json_populate_record(null::gen_file_upload, placement_element::json) AS plc ON true
                                        WHERE dr.tran_designer_review_id = m.tran_designer_review_id
                                          and plc.imagetype = 'Front')                          AS photos
                                FROM public.tran_tech_pack m
                                 inner join (SELECT distinct tppd.techpack_id
                                             FROM tran_production_process_define tppd
                                                      LEFT JOIN tran_pp_meeting tppm ON tppd.techpack_id = tppm.techpack_id
                                             WHERE tppm.techpack_id IS NULL) a   on a.techpack_id=m.tran_techpack_id
                                            where 
                                             m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id
                                             and case
                                             when @search_text is null or length(@search_text)=0 then true
                                             when @search_text is not null and length(@search_text)>0 and (
                                                    m.techpack_number ilike '%' || @search_text || '%'
                                                 ) then true
                                             else false end)


                                    SELECT cte_saved.*,
                                           (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                    FROM cte_saved
                                    OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_tech_pack_entity>(query,
                        new
                        {
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id,
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    foreach (var obj in dataList)
                    {
                        var image_source = "";

                        if (!string.IsNullOrWhiteSpace(obj.photos))
                        {
                            try
                            {
                                file_upload objfile = new file_upload();
                                objfile.server_filename = obj.photos;
                                objfile.filePath = "sample_order";

                                var objFile = await _CommonService.LoadSingleFiles(objfile, "purchase_order");

                                image_source = objfile.base64string;
                            }
                            catch (Exception ex)
                            {

                            }

                            obj.photos = @"<span><img style='display: inline-block;height:80px;width:80px;' src=" + image_source + " /> " + obj.techpack_number + "</span>";
                        }
                    }

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<bool> tran_tech_pack_insert_sp(tran_tech_pack_entity objtran_tech_pack)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_tech_pack_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_designer_review_id", NpgsqlDbType.Bigint, objtran_tech_pack.tran_designer_review_id == null ? DBNull.Value : objtran_tech_pack.tran_designer_review_id);
                        Command.Parameters.AddWithValue("in_techpack_number", NpgsqlDbType.Text, objtran_tech_pack.techpack_number == null ? DBNull.Value : objtran_tech_pack.techpack_number);
                        Command.Parameters.AddWithValue("in_techpack_date", NpgsqlDbType.Date, objtran_tech_pack.techpack_date == null ? DBNull.Value : objtran_tech_pack.techpack_date);
                        Command.Parameters.AddWithValue("in_costing_smv", NpgsqlDbType.Numeric, objtran_tech_pack.costing_smv == null ? DBNull.Value : objtran_tech_pack.costing_smv);
                        Command.Parameters.AddWithValue("in_teckpack_style_code", NpgsqlDbType.Text, objtran_tech_pack.teckpack_style_code == null ? DBNull.Value : objtran_tech_pack.teckpack_style_code);
                        Command.Parameters.AddWithValue("in_aop_style", NpgsqlDbType.Text, objtran_tech_pack.aop_style == null ? DBNull.Value : objtran_tech_pack.aop_style);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_tech_pack.merchandiser_id == null ? DBNull.Value : objtran_tech_pack.merchandiser_id);
                        Command.Parameters.AddWithValue("in_production_availability_path", NpgsqlDbType.Text, objtran_tech_pack.production_availability_path == null ? DBNull.Value : objtran_tech_pack.production_availability_path);
                        Command.Parameters.AddWithValue("in_vat", NpgsqlDbType.Text, objtran_tech_pack.vat == null ? DBNull.Value : objtran_tech_pack.vat);
                        Command.Parameters.AddWithValue("in_photoshoot", NpgsqlDbType.Text, objtran_tech_pack.photoshoot == null ? DBNull.Value : objtran_tech_pack.photoshoot);
                        Command.Parameters.AddWithValue("in_e_com", NpgsqlDbType.Text, objtran_tech_pack.e_com == null ? DBNull.Value : objtran_tech_pack.e_com);
                        Command.Parameters.AddWithValue("in_sample_ok", NpgsqlDbType.Text, objtran_tech_pack.sample_ok == null ? DBNull.Value : objtran_tech_pack.sample_ok);
                        Command.Parameters.AddWithValue("in_follow_style", NpgsqlDbType.Text, objtran_tech_pack.follow_style == null ? DBNull.Value : objtran_tech_pack.follow_style);
                        Command.Parameters.AddWithValue("in_need_production_approval", NpgsqlDbType.Text, objtran_tech_pack.need_production_approval == null ? DBNull.Value : objtran_tech_pack.need_production_approval);
                        Command.Parameters.AddWithValue("in_iron", NpgsqlDbType.Text, objtran_tech_pack.iron == null ? DBNull.Value : objtran_tech_pack.iron);
                        Command.Parameters.AddWithValue("in_fabric_allocation", NpgsqlDbType.Text, objtran_tech_pack.fabric_allocation == null ? DBNull.Value : objtran_tech_pack.fabric_allocation);
                        Command.Parameters.AddWithValue("in_additional_comments", NpgsqlDbType.Text, objtran_tech_pack.additional_comments == null ? DBNull.Value : objtran_tech_pack.additional_comments);
                        Command.Parameters.AddWithValue("in_photos", NpgsqlDbType.Text, objtran_tech_pack.photos == null ? DBNull.Value : objtran_tech_pack.photos);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_tech_pack.approved_by == null ? DBNull.Value : objtran_tech_pack.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_tech_pack.approve_date == null ? DBNull.Value : objtran_tech_pack.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_tech_pack.approve_remarks == null ? DBNull.Value : objtran_tech_pack.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_tech_pack.added_by == null ? DBNull.Value : objtran_tech_pack.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_tech_pack.updated_by == null ? DBNull.Value : objtran_tech_pack.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_tech_pack.date_added == null ? DBNull.Value : objtran_tech_pack.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_tech_pack.date_updated == null ? DBNull.Value : objtran_tech_pack.date_updated);
                        Command.Parameters.AddWithValue("in_color_wise_size_quantity", NpgsqlDbType.Text, objtran_tech_pack.color_wise_size_quantity == null ? DBNull.Value : objtran_tech_pack.color_wise_size_quantity);
                        Command.Parameters.AddWithValue("in_tech_pack_costing_quantity", NpgsqlDbType.Bigint, objtran_tech_pack.tech_pack_costing_quantity == null ? DBNull.Value : objtran_tech_pack.tech_pack_costing_quantity);
                        Command.Parameters.AddWithValue("in_is_ack", NpgsqlDbType.Bigint, objtran_tech_pack.is_ack == null ? DBNull.Value : objtran_tech_pack.is_ack);
                        Command.Parameters.AddWithValue("in_ack_date", NpgsqlDbType.Date, objtran_tech_pack.ack_date == null ? DBNull.Value : objtran_tech_pack.ack_date);
                        Command.Parameters.AddWithValue("in_product_composition", NpgsqlDbType.Text, objtran_tech_pack.product_composition == null ? DBNull.Value : objtran_tech_pack.product_composition);
                        Command.Parameters.AddWithValue("in_spi", NpgsqlDbType.Text, objtran_tech_pack.spi == null ? DBNull.Value : objtran_tech_pack.spi);
                        Command.Parameters.AddWithValue("in_sleeve_details", NpgsqlDbType.Text, objtran_tech_pack.sleeve_details == null ? DBNull.Value : objtran_tech_pack.sleeve_details);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_tech_pack.fiscal_year_id == null ? DBNull.Value : objtran_tech_pack.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_tech_pack.event_id == null ? DBNull.Value : objtran_tech_pack.event_id);
                        Command.Parameters.AddWithValue("in_embellishment_detl", NpgsqlDbType.Text, objtran_tech_pack.embellishment_detl == null ? DBNull.Value : objtran_tech_pack.embellishment_detl);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_tech_pack.is_submitted == null ? DBNull.Value : objtran_tech_pack.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_tech_pack.is_approved == null ? DBNull.Value : objtran_tech_pack.is_approved);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_tech_pack.delivery_date == null ? DBNull.Value : objtran_tech_pack.delivery_date);

                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_tech_pack.style_item_product_id);
                        Command.Parameters.AddWithValue("in_style_item_product_name", NpgsqlDbType.Text, objtran_tech_pack.style_item_product_name);

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
        public async Task<bool> tran_tech_pack_update_sp(tran_tech_pack_entity objtran_tech_pack)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_tech_pack_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_techpack_id", NpgsqlDbType.Bigint, objtran_tech_pack.tran_techpack_id == null ? DBNull.Value : objtran_tech_pack.tran_techpack_id);
                        Command.Parameters.AddWithValue("in_tran_designer_review_id", NpgsqlDbType.Bigint, objtran_tech_pack.tran_designer_review_id == null ? DBNull.Value : objtran_tech_pack.tran_designer_review_id);
                        Command.Parameters.AddWithValue("in_techpack_number", NpgsqlDbType.Text, objtran_tech_pack.techpack_number == null ? DBNull.Value : objtran_tech_pack.techpack_number);
                        Command.Parameters.AddWithValue("in_techpack_date", NpgsqlDbType.Date, objtran_tech_pack.techpack_date == null ? DBNull.Value : objtran_tech_pack.techpack_date);
                        Command.Parameters.AddWithValue("in_costing_smv", NpgsqlDbType.Numeric, objtran_tech_pack.costing_smv == null ? DBNull.Value : objtran_tech_pack.costing_smv);
                        Command.Parameters.AddWithValue("in_teckpack_style_code", NpgsqlDbType.Text, objtran_tech_pack.teckpack_style_code == null ? DBNull.Value : objtran_tech_pack.teckpack_style_code);
                        Command.Parameters.AddWithValue("in_aop_style", NpgsqlDbType.Text, objtran_tech_pack.aop_style == null ? DBNull.Value : objtran_tech_pack.aop_style);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_tech_pack.merchandiser_id == null ? DBNull.Value : objtran_tech_pack.merchandiser_id);
                        Command.Parameters.AddWithValue("in_production_availability_path", NpgsqlDbType.Text, objtran_tech_pack.production_availability_path == null ? DBNull.Value : objtran_tech_pack.production_availability_path);
                        Command.Parameters.AddWithValue("in_vat", NpgsqlDbType.Text, objtran_tech_pack.vat == null ? DBNull.Value : objtran_tech_pack.vat);
                        Command.Parameters.AddWithValue("in_photoshoot", NpgsqlDbType.Text, objtran_tech_pack.photoshoot == null ? DBNull.Value : objtran_tech_pack.photoshoot);
                        Command.Parameters.AddWithValue("in_e_com", NpgsqlDbType.Text, objtran_tech_pack.e_com == null ? DBNull.Value : objtran_tech_pack.e_com);
                        Command.Parameters.AddWithValue("in_sample_ok", NpgsqlDbType.Text, objtran_tech_pack.sample_ok == null ? DBNull.Value : objtran_tech_pack.sample_ok);
                        Command.Parameters.AddWithValue("in_follow_style", NpgsqlDbType.Text, objtran_tech_pack.follow_style == null ? DBNull.Value : objtran_tech_pack.follow_style);
                        Command.Parameters.AddWithValue("in_need_production_approval", NpgsqlDbType.Text, objtran_tech_pack.need_production_approval == null ? DBNull.Value : objtran_tech_pack.need_production_approval);
                        Command.Parameters.AddWithValue("in_iron", NpgsqlDbType.Text, objtran_tech_pack.iron == null ? DBNull.Value : objtran_tech_pack.iron);
                        Command.Parameters.AddWithValue("in_fabric_allocation", NpgsqlDbType.Text, objtran_tech_pack.fabric_allocation == null ? DBNull.Value : objtran_tech_pack.fabric_allocation);
                        Command.Parameters.AddWithValue("in_additional_comments", NpgsqlDbType.Text, objtran_tech_pack.additional_comments == null ? DBNull.Value : objtran_tech_pack.additional_comments);
                        Command.Parameters.AddWithValue("in_photos", NpgsqlDbType.Text, objtran_tech_pack.photos == null ? DBNull.Value : objtran_tech_pack.photos);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_tech_pack.approved_by == null ? DBNull.Value : objtran_tech_pack.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_tech_pack.approve_date == null ? DBNull.Value : objtran_tech_pack.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_tech_pack.approve_remarks == null ? DBNull.Value : objtran_tech_pack.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_tech_pack.added_by == null ? DBNull.Value : objtran_tech_pack.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_tech_pack.updated_by == null ? DBNull.Value : objtran_tech_pack.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_tech_pack.date_added == null ? DBNull.Value : objtran_tech_pack.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_tech_pack.date_updated == null ? DBNull.Value : objtran_tech_pack.date_updated);
                        Command.Parameters.AddWithValue("in_color_wise_size_quantity", NpgsqlDbType.Text, objtran_tech_pack.color_wise_size_quantity == null ? DBNull.Value : objtran_tech_pack.color_wise_size_quantity);
                        Command.Parameters.AddWithValue("in_tech_pack_costing_quantity", NpgsqlDbType.Bigint, objtran_tech_pack.tech_pack_costing_quantity == null ? DBNull.Value : objtran_tech_pack.tech_pack_costing_quantity);
                        Command.Parameters.AddWithValue("in_is_ack", NpgsqlDbType.Bigint, objtran_tech_pack.is_ack == null ? DBNull.Value : objtran_tech_pack.is_ack);
                        Command.Parameters.AddWithValue("in_ack_date", NpgsqlDbType.Date, objtran_tech_pack.ack_date == null ? DBNull.Value : objtran_tech_pack.ack_date);
                        Command.Parameters.AddWithValue("in_product_composition", NpgsqlDbType.Text, objtran_tech_pack.product_composition == null ? DBNull.Value : objtran_tech_pack.product_composition);
                        Command.Parameters.AddWithValue("in_spi", NpgsqlDbType.Text, objtran_tech_pack.spi == null ? DBNull.Value : objtran_tech_pack.spi);
                        Command.Parameters.AddWithValue("in_sleeve_details", NpgsqlDbType.Text, objtran_tech_pack.sleeve_details == null ? DBNull.Value : objtran_tech_pack.sleeve_details);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_tech_pack.fiscal_year_id == null ? DBNull.Value : objtran_tech_pack.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_tech_pack.event_id == null ? DBNull.Value : objtran_tech_pack.event_id);
                        Command.Parameters.AddWithValue("in_embellishment_detl", NpgsqlDbType.Text, objtran_tech_pack.embellishment_detl == null ? DBNull.Value : objtran_tech_pack.embellishment_detl);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_tech_pack.is_submitted == null ? DBNull.Value : objtran_tech_pack.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_tech_pack.is_approved == null ? DBNull.Value : objtran_tech_pack.is_approved);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_tech_pack.delivery_date == null ? DBNull.Value : objtran_tech_pack.delivery_date);

                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_tech_pack.style_item_product_id);
                        Command.Parameters.AddWithValue("in_style_item_product_name", NpgsqlDbType.Text, objtran_tech_pack.style_item_product_name);

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

        public async Task<bool> tran_tech_pack_approve_reject(tran_tech_pack_DTO objtran_tech_pack)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new NpgsqlCommand("tran_tech_pack_approve_reject", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("in_tran_techpack_id", NpgsqlDbType.Bigint, objtran_tech_pack.tran_techpack_id);
                        command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_tech_pack.is_submitted);
                        command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_tech_pack.is_approved);
                        command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_tech_pack.approved_by);
                        command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_tech_pack.approve_date);
                        command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_tech_pack.approve_remarks);

                        command.ExecuteNonQuery();

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

        public async Task<rpc_proc_sp_get_techapack_details_for_sewing_DTO> GetAllproc_sp_get_techapack_details_for_sewingAsync(Int64? p_techpack_id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_techapack_details_for_sewing( @p_techpack_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_techapack_details_for_sewing_DTO>(query,
                          new { p_techpack_id }
                         ).FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<rpc_proc_sp_get_techapack_details_for_final_inspection_DTO> GetAllproc_sp_get_techapack_details_for_final_inspection(Int64? p_techpack_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_techapack_details_for_final_inspection( @p_techpack_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_techapack_details_for_final_inspection_DTO>(query,
                          new { p_techpack_id }
                         ).FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public List<rpc_proc_sp_get_colors_by_techpack_DTO> GetAllproc_sp_get_colors_by_techpackAsync(Int64? p_techpack_id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_colors_by_techpack( @p_techpack_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_colors_by_techpack_DTO>(query,
                          new { p_techpack_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_tech_pack_DTO> GetEmbellishmentByTechpackAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_serviceorder_by_techpackid( @p_techpack_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_colors_by_techpack_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              query_type = query_type,
                              p_techpack_id = string.IsNullOrEmpty(Convert.ToString(receivedID)) ? (long?)null : Convert.ToInt64(receivedID),
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id
                          }
                         ).AsList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<tran_tech_pack_DTO>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public List<sizes> GetTechpackColorWiseSizeList(long p_techpack_id, string p_color_code)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_teckpack_wise_colour_wise_size(@p_techpack_id,@p_color_code)";

                    var dataList = connection.Query<sizes>(query,
                          new
                          {

                              p_techpack_id = p_techpack_id,
                              p_color_code = p_color_code
                          }).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<tran_tech_pack_DTO> GetAllproc_sp_get_techpack_data_for_packing_list(long p_techpack_id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_techpack_data_for_packing_list( @p_techpack_id)";

                    var dataList = connection.Query<tran_tech_pack_DTO>(query,
                          new
                          {
                              p_techpack_id = p_techpack_id
                          }).SingleOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<List<rpc_proc_sp_get_data_techpack_for_distribution_DTO>> GetAllproc_sp_get_data_techpack_for_distributionAsync(Int64? p_fiscal_year_id, Int64? p_event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_techpack_for_distribution( @p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_techpack_for_distribution_DTO>(query,
                          new { p_fiscal_year_id, p_event_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

    }

}

