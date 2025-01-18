using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using static Postgrest.Constants;
using AutoMapper;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Domain.Statics;
using EPYSLSAILORAPP.Application.DTO.Season;

using EPYSLSAILORAPP.Application.DTO;
using System.Linq.Expressions;
using ServiceStack;
using EPYSLSAILORAPP.Domain.Security;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using Npgsql;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using static Dapper.SqlMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Infrastructure.Services.GenServices
{
    public class Tran_BP_YearService : ITran_BP_YearService
    {
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;
        public Tran_BP_YearService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

       
        public async Task<bool> Update(tran_bp_year_dto obj)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_season_event_config>(JsonConvert.SerializeObject(obj));
                
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    connection.Update(objEntity);

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {

            }
        }

        


    }
}
