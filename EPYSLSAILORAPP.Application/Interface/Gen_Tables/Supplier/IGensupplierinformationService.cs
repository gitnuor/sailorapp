using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenSupplierInformationService
    {
        Task<bool> SaveAsync(gen_supplier_information_DTO objgen_supplier_information);

        Task<bool> UpdateAsync(gen_supplier_information_DTO objgen_supplier_information);

		Task<List<gen_supplier_information_DTO>> GetAllAsync();

        Task<List<gen_supplier_information_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<gen_supplier_information_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);


        

    }
}

