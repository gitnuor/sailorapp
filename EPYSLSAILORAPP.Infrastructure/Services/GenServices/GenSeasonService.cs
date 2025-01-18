using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using ServiceStack;

namespace EPYSLSAILORAPP.Infrastructure.Services.GenServices
{
    public class GenSeasonService : IGenSeasonService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public GenSeasonService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<List<gen_season_dto>> get_fiscal_season_GetAll()
        {
            List<gen_season_dto> list = new List<gen_season_dto>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_season>().ToList();

                    return JsonConvert.DeserializeObject<List<gen_season_dto>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


    }
}
