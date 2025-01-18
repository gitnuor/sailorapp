using AutoMapper;
using EPYSLSAILORAPP.Application.Interface;
using Microsoft.Extensions.Configuration;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class BillApprovalService : IBillApprovalService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public BillApprovalService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }


    }
    //    public async Task<bool> UpdateAsync(tran_scm_bill_submission_entity entity)
    //    {
    //        try
    //        {
    //            await _connectionSupabse.InitializeAsync();
    //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>().Select("*").Where(p => p.bill_submission_id == entity.bill_submission_id).Get();

        //            tran_scm_bill_submission_entity model = JsonConvert.DeserializeObject<List<tran_scm_bill_submission_entity>>(response.Content).FirstOrDefault();
        //            model.approved_date = DateTime.Now;
        //            model.approved_by = entity.updated_by;
        //            model.is_approved = true;
        //            await _connectionSupabse.From<tran_scm_bill_submission_entity>().Update(model);



        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }

        //    }



        //    public async Task<List<tran_scm_bill_submission_DTO>> GetAllAsync()
        //    {
        //        try
        //        {
        //            await _connectionSupabse.InitializeAsync();

        //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>().Select("*").Get();

        //            return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);

        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }

        //    }





        //    public async Task<List<tran_scm_bill_submission_DTO>> GetAllPagedDataAsync(DtParameters param)
        //    {
        //        try
        //        {
        //            await _connectionSupabse.InitializeAsync();

        //            var sort_column = ""; var sort_order = "";

        //            if (param.SortOrder.Contains(' '))
        //            {
        //                sort_column = param.SortOrder.Split(' ')[0];
        //                sort_order = param.SortOrder.Split(' ')[1];
        //            }
        //            else
        //            {
        //                sort_column = param.SortOrder;
        //                sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
        //            }

        //            if (!string.IsNullOrEmpty(param.Search.Value))
        //            {
        //                //replace primary column from filter by your required column
        //                var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>()
        //               .Select("*")
        //               .Filter(p => p.bill_submission_id, Operator.ILike, "%" + param.Search.Value + "%")
        //               .Order("bill_submission_id", Ordering.Descending)
        //               .Range(param.Start, param.Start + param.Length - 1)
        //               .Where(x => x.is_send_for_approval == true)
        //               .Where(x => x.is_approved == false)
        //               .Get();

        //                return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);
        //            }
        //            else
        //            {
        //                var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>()
        //               .Select("*")
        //               .Order("bill_submission_id", Ordering.Descending)
        //               .Range(param.Start, param.Start + param.Length - 1)
        //                .Where(x => x.is_send_for_approval == true)
        //                .Where(x => x.is_approved == false)
        //               .Get();

        //                return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }

        //    }


        //    public async Task<tran_scm_bill_submission_DTO> GetSingleAsync(Int64 Id)
        //    {
        //        try
        //        {
        //            await _connectionSupabse.InitializeAsync();

        //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>().Select("*").Where(p => p.bill_submission_id == Id).Get();

        //            return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(response.Content).FirstOrDefault();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }

        //    }

        //    public async Task<bool> DeleteAsync(Int64 Id)
        //    {
        //        try
        //        {
        //            await _connectionSupabse.InitializeAsync();

        //            var response = await _connectionSupabse.From<tran_scm_bill_submission_entity>().Select("*").Where(p => p.bill_submission_id == Id).Get();

        //            var objDelete = JsonConvert.DeserializeObject<List<tran_scm_bill_submission_entity>>(response.Content).FirstOrDefault();

        //            await _connectionSupabse.From<tran_scm_bill_submission_entity>().Delete(objDelete);

        //            return true;

        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }

        //    }
        //}
    
}

