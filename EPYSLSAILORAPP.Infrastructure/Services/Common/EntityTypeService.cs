using Dapper;
using EPYSLSAILORAPP.Application.DTO.General;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.Entity.General;
using EPYSLSAILORAPP.Domain.Statics;
using System.Data.SqlClient;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public class EntityTypeService : IEntityTypeService
    {
        private readonly IDapperCRUDService<EntityType> _service;
        private readonly ISignatureService _signatureService;


        public EntityTypeService(IDapperCRUDService<EntityType> service
            , ISignatureService signatureService)
        {
            _service = service;
            _signatureService = signatureService;
            _service.Connection = service.GetConnection(AppConstants.DB_CONNECTION);
        }
        public async Task<List<EntityTypeDTO>> GetEntityTypeAsync(PaginationInfo paginationInfo)
        {
            string orderBy = paginationInfo.OrderBy.NullOrEmpty() ? "Order By EntityTypeID Desc" : paginationInfo.OrderBy;
            var sql = $@"select *, Count(*) Over() TotalRows from  EntityType
                        {paginationInfo.FilterBy}
                        {orderBy}
                        {paginationInfo.PageBy}";

            return await _service.GetDataAsync<EntityTypeDTO>(sql);
        }
        public async Task<EntityTypeDTO> GetAsync(int EntityTypeID)
        {
            var sql = $@"--Master Information
                  Select * From  EntityType Where EntityTypeID = {EntityTypeID}
	             Select * From EntityTypeValue Where EntityTypeID = {EntityTypeID} ";

            try
            {
                await _service.Connection.OpenAsync();
                var records = await _service.Connection.QueryMultipleAsync(sql);
                EntityTypeDTO data = await records.ReadFirstOrDefaultAsync<EntityTypeDTO>();
                if (data == null)
                {
                    data = new EntityTypeDTO();
                    data.Childs.Add(new EntityTypeValue());
                }
                else
                {
                    data.Childs = records.Read<EntityTypeValue>().ToList();
                }
                data.EntityState = EntityState.Unchanged;
                data.Childs.SetUnchanged();
                return data;
                //Guard.Against.NullObject(data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _service.Connection.Close();
            }
        }
        public async Task SaveEntityTypeAsync(EntityTypeDTO entity)
        {
            SqlTransaction transaction = null;
            try
            {
                await _service.Connection.OpenAsync();
                transaction = _service.Connection.BeginTransaction();

                int maxChildId = 0;
                List<EntityTypeValue> childRecords = entity.Childs;

                switch (entity.EntityState)
                {
                    case EntityState.Added:
                        entity.EntityTypeID = await _signatureService.GetMaxIdAsync(TableNames._EntityType);

                        maxChildId = await _signatureService.GetMaxIdAsync(TableNames._EntityTypeValue, entity.Childs.Count);
                        foreach (var item in entity.Childs)
                        {
                            item.ValueID = maxChildId++;
                            item.EntityTypeID = entity.EntityTypeID;
                        }
                        break;

                    case EntityState.Modified:
                        var addedChilds = entity.Childs.FindAll(x => x.EntityState == EntityState.Added);
                        maxChildId = await _signatureService.GetMaxIdAsync(TableNames._EntityTypeValue, addedChilds.Count);

                        foreach (var item in addedChilds)
                        {
                            item.ValueID = maxChildId++;
                            item.EntityTypeID = entity.EntityTypeID;
                        }
                        break;
                }

                await _service.SaveSingleAsync(entity, transaction);
                await _service.SaveAsync(entity.Childs, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                _service.Connection.Close();
            }
        }
    }
}
