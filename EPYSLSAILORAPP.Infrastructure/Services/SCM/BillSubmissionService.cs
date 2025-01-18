
//using EPYSLSAILORAPP.Application.Interface;
//using EPYSLSAILORAPP.Domain.Entity;
//using EPYSLSAILORAPP.Application.DTO;
//using AutoMapper;
//using Microsoft.Extensions.Configuration;
//using Supabase;
//using EPYSLSAILORAPP.Domain.Statics;
//using static Dapper.SqlMapper;
//using Newtonsoft.Json;
//using EPYSLSAILORAPP.Domain.DTO;
//using static Postgrest.Constants;
//using Npgsql;
//using Azure;

//namespace EPYSLSAILORAPP.Infrastructure.Services
//{

//    public class BillSubmissionService : IBillSubmissionService
//    {

//        private readonly IConfiguration _configuration;
//        private readonly Client? _connectionSupabse;
//        private readonly IMapper _mapper;

//        public BillSubmissionService(IConfiguration configuration, IMapper mapper)
//        {

//            _mapper = mapper;
//            _configuration = configuration;
//            HttpClient httpClient = new HttpClient();
//            var supabaseKey = _configuration.GetValue<string>("supabaseKey");
//            var supabaseUrl = _configuration.GetValue<string>("supabaseUrl");
//            httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
//            _connectionSupabse = new Client(supabaseUrl, supabaseKey, new SupabaseOptions { AutoConnectRealtime = false });
//}

//        //public async Task<bool> SaveAsync( tran_scm_bill_submission_entity entity)
//        //{
//        //    try
//        //    {
//        //        await _connectionSupabse.InitializeAsync();
//        //        await _connectionSupabse.From<tran_scm_bill_submission_entity>().Insert(entity);

//        //        var response = await _connectionSupabse.From<tran_scm_po_entity>()
//        //   .Where(x => x.po_id == entity.po_id)
//        //   .Select("*").Get();

//        //        tran_scm_po_entity ret = JsonConvert.DeserializeObject<List<tran_scm_po_entity>>(response.Content).FirstOrDefault();
//        //        ret.is_bill_submitted = true;
               

//        //        await _connectionSupabse.From<tran_scm_po_entity>().Update(ret);

//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw (ex);
//        //    }

//        //}

//        //public async Task<bool> UpdateAsync(tran_scm_bill_submission_entity entity)
//        //{
//        //    try
//        //    {
//        //        await _connectionSupabse.InitializeAsync();
//        //        await _connectionSupabse.From<tran_scm_bill_submission_entity>().Update(entity);



//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw (ex);
//        //    }

//        //}

//        //public async Task<List<tran_scm_bill_submission_DTO>> GetAllAsync()
//        //{
//        //    try
//        //    {
//        //        await _connectionSupabse.InitializeAsync();

//        //        var response =await _connectionSupabse.From<tran_scm_bill_submission_entity>().Select("*").Get(); 

//        //        return  JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw (ex);
//        //    }

//        //}

//        //public async Task<List<tran_scm_po_DTO>> GetAllPOApprovaData_Fro_Bill_Submission_Async(DtParameters param)
//        //{
//        //    try
//        //    {

//        //        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
//        //        {
//        //            connection.Open();

//        //            string query = @"WITH cte_saved AS (SELECT m.*
//        //                                   FROM tran_scm_po m
//        //                                   where 
//        //                                            m.is_approved IS NULL  and m.is_bill_submitted=true              
//        //                                            and case
//        //                                             when @search_text is null or length(@search_text)=0 then true
//        //                                             when @search_text is not null and length(@search_text)>0 and (
//        //                                                    m.po_no ilike '%' || @search_text || '%'
//        //                                                 ) then true
//        //                                             else false end)


//        //                                    SELECT cte_saved.*,
//        //                                           (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
//        //                                    FROM cte_saved
//        //                                    OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

//        //            var dataList = connection.Query<tran_scm_po_entity>(query,
//        //                new
//        //                {
//        //                    search_text = param.Search.Value,
//        //                    row_index = param.Start,
//        //                    page_size = param.Length
//        //                }).ToList();

//        //            return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(JsonConvert.SerializeObject(dataList)); ;

//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw (ex);
//        //    }
//            //try
//            //{
//            //    await _connectionSupabse.InitializeAsync();
//            //    long group = 1;
//            //    var sort_column = ""; var sort_order = "";

//            //    if (param.SortOrder.Contains(' '))
//            //    {
//            //        sort_column = param.SortOrder.Split(' ')[0];
//            //        sort_order = param.SortOrder.Split(' ')[1];
//            //    }
//            //    else
//            //    {
//            //        sort_column = param.SortOrder;
//            //        sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
//            //    }

//            //    if (!string.IsNullOrEmpty(param.Search.Value))
//            //    {
//            //        //replace primary column from filter by your required column
//            //        var response = await _connectionSupabse.From<tran_scm_po_entity>()
//            //       .Select("*")
//            //       .Filter(p => p.po_id, Operator.ILike, "%" + param.Search.Value + "%")
//            //       .Order("po_id", Ordering.Descending)
//            //       .Range(param.Start, param.Start + param.Length - 1)
                   
//            //       .Where(x => x.is_approved == 1)
//            //          .Where(x => x.is_bill_submitted == false)
//            //       .Get();

//            //        return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);
//            //    }
//            //    else
//            //    {
//            //        var response = await _connectionSupabse.From<tran_scm_po_entity>()
//            //       .Select("*")
//            //       .Order("po_id", Ordering.Descending)
//            //       .Range(param.Start, param.Start + param.Length - 1)

//            //       .Where(x => x.is_approved == 1)
//            //       .Where(x => x.is_bill_submitted == false)
//            //       .Get();

//            //        return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);
//            //    }

//            //}
//            //catch (Exception ex)
//            //{
//            //    throw (ex);
//            //}

//        }

//        //public async Task<List<tran_scm_bill_submission_DTO>> GetAllPagedDataAsync(DtParameters param)
//        //{
//        //    try
//        //    {
//        //        await _connectionSupabse.InitializeAsync();

//        //        var sort_column = ""; var sort_order = "";

//        //        if (param.SortOrder.Contains(' '))
//        //        {
//        //            sort_column = param.SortOrder.Split(' ')[0];
//        //            sort_order = param.SortOrder.Split(' ')[1];
//        //        }
//        //        else
//        //        {
//        //            sort_column = param.SortOrder;
//        //            sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
//        //        }

//        //        if (!string.IsNullOrEmpty(param.Search.Value))
//        //        {
//        //            //replace primary column from filter by your required column
//        //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>()
//        //           .Select("*")
//        //           .Filter(p => p.bill_submission_id, Operator.ILike, "%" + param.Search.Value + "%")
//        //           .Order("bill_submission_id", Ordering.Descending)
//        //           .Range(param.Start, param.Start + param.Length - 1)
//        //           .Get();

//        //            return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);
//        //        }
//        //        else
//        //        {
//        //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>()
//        //           .Select("*")
//        //           .Order("bill_submission_id", Ordering.Descending)
//        //           .Range(param.Start, param.Start + param.Length - 1)
//        //           .Get();

//        //            return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw (ex);
//        //    }

//        //}



//        //public async Task<List<tran_scm_bill_submission_DTO>> GetAllSubmittedPagedDataAsync(DtParameters param)
//        //{
//        //    try
//        //    {
//        //        await _connectionSupabse.InitializeAsync();

//        //        var sort_column = ""; var sort_order = "";

//        //        if (param.SortOrder.Contains(' '))
//        //        {
//        //            sort_column = param.SortOrder.Split(' ')[0];
//        //            sort_order = param.SortOrder.Split(' ')[1];
//        //        }
//        //        else
//        //        {
//        //            sort_column = param.SortOrder;
//        //            sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
//        //        }

//        //        if (!string.IsNullOrEmpty(param.Search.Value))
//        //        {
//        //            //replace primary column from filter by your required column
//        //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>()
//        //           .Select("*")
//        //           .Filter(p => p.bill_submission_id, Operator.ILike, "%" + param.Search.Value + "%")
//        //           .Order("bill_submission_id", Ordering.Descending)
//        //           .Range(param.Start, param.Start + param.Length - 1)
//        //           .Where(x=>x.is_submitted==true)
//        //           .Where(x=>x.is_send_for_approval==false)
//        //           .Get();

//        //            return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);
//        //        }
//        //        else
//        //        {
//        //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>()
//        //           .Select("*")
//        //           .Order("bill_submission_id", Ordering.Descending)
//        //           .Range(param.Start, param.Start + param.Length - 1)
//        //            .Where(x => x.is_submitted == true)
//        //              .Where(x => x.is_send_for_approval == false)
//        //           .Get();

//        //            return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);
//        //        }

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw (ex);
//        //    }

//        //}


//        //public async Task<tran_scm_bill_submission_DTO> GetSingleAsync(Int64 Id)
//        //{
//        //    try
//        //    {
//        //        await _connectionSupabse.InitializeAsync();

//        //        var response =await _connectionSupabse.From<tran_scm_bill_submission_entity>().Select("*").Where(p=>p.bill_submission_id==Id).Get(); 

//        //        return  JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content).FirstOrDefault();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw (ex);
//        //    }

//        //}

//        //public async Task<bool> DeleteAsync(Int64 Id)
//        //{
//        //   try
//        //   {
//        //       await _connectionSupabse.InitializeAsync();

//        //       var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>().Select("*").Where(p => p.bill_submission_id == Id).Get();

//        //       var objDelete= JsonConvert.DeserializeObject<List<tran_scm_bill_submission_entity>>(response.Content).FirstOrDefault();

//        //       await _connectionSupabse.From<tran_scm_bill_submission_entity>().Delete(objDelete);

//        //       return true;

//        //   }
//        //   catch (Exception ex)
//        //   {
//        //       throw (ex);
//        //   }

//        //}
//    }

//}

