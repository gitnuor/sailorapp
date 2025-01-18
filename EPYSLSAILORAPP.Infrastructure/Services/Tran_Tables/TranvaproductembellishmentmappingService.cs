
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Supabase;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;


namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranvaproductembellishmentmappingService : ITranvaproductembellishmentmappingService
    {

        private readonly IConfiguration _configuration;
        private readonly Client? _connectionSupabse;
        private readonly IMapper _mapper;

        public TranvaproductembellishmentmappingService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
            HttpClient httpClient = new HttpClient();
            var supabaseKey = _configuration.GetValue<string>("supabaseKey");
            var supabaseUrl = _configuration.GetValue<string>("supabaseUrl");
            httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
            _connectionSupabse = new Client(supabaseUrl, supabaseKey, new SupabaseOptions { AutoConnectRealtime = false });
}

        public async Task<bool> SaveAsync( tran_va_product_embellishment_mapping_entity entity)
        {
            try
            {
                await _connectionSupabse.InitializeAsync();
                await _connectionSupabse.From<tran_va_product_embellishment_mapping_entity>().Insert(entity);

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(tran_va_product_embellishment_mapping_entity entity)
        {
            try
            {
                await _connectionSupabse.InitializeAsync();
                await _connectionSupabse.From<tran_va_product_embellishment_mapping_entity>().Update(entity);

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_va_product_embellishment_mapping_entity>> GetAllAsync()
        {
            try
            {
                await _connectionSupabse.InitializeAsync();

                var response =await _connectionSupabse.From<tran_va_product_embellishment_mapping_entity>().Select("*").Get(); 

                return  JsonConvert.DeserializeObject<List<tran_va_product_embellishment_mapping_entity>>(response.Content);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_va_product_embellishment_mapping_entity>> GetAsync(Int64 Id)
        {
            try
            {
                await _connectionSupabse.InitializeAsync();

                var response =await _connectionSupabse.From<tran_va_product_embellishment_mapping_entity>().Select("*").Get(); 

                return  JsonConvert.DeserializeObject<List<tran_va_product_embellishment_mapping_entity>>(response.Content);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

