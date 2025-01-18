namespace EPYSLSAILORAPP.Application.Interface.Employee
{
    public interface IEmployeeService
    {
        Task<IEnumerable<string>>  GetById(int param);
    }
}
