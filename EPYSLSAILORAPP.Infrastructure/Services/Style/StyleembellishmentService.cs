
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

    public class StyleembellishmentService : IStyleembellishmentService
    {

        private readonly IConfiguration _configuration;
        private readonly Client? _connectionSupabse;
        private readonly IMapper _mapper;

        public StyleembellishmentService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
            HttpClient httpClient = new HttpClient();
            var supabaseKey = _configuration.GetValue<string>("supabaseKey");
            var supabaseUrl = _configuration.GetValue<string>("supabaseUrl");
            httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
            _connectionSupabse = new Client(supabaseUrl, supabaseKey, new SupabaseOptions { AutoConnectRealtime = false });
}

        public async Task<bool> SaveAsync( style_embellishment_entity entity)
        {
            try
            {
                await _connectionSupabse.InitializeAsync();
                await _connectionSupabse.From<style_embellishment_entity>().Insert(entity);

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(style_embellishment_entity entity)
        {
            try
            {
                await _connectionSupabse.InitializeAsync();
                await _connectionSupabse.From<style_embellishment_entity>().Update(entity);

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<style_embellishment_entity>> GetAllAsync()
        {
            try
            {
                await _connectionSupabse.InitializeAsync();

                var response =await _connectionSupabse.From<style_embellishment_entity>().Select("*").Get(); 

                return  JsonConvert.DeserializeObject<List<style_embellishment_entity>>(response.Content);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<style_embellishment_entity>> GetAsync(Int64 Id)
        {
            try
            {
                await _connectionSupabse.InitializeAsync();

                var response =await _connectionSupabse.From<style_embellishment_entity>().Select("*").Get(); 

                return  JsonConvert.DeserializeObject<List<style_embellishment_entity>>(response.Content);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

