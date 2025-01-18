using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.Employee
{
    [Table("Employee")]
    public class EmployeeInfo:DapperBaseEntity
    {
        [Key]
        public int EmployeeID { get; set; }
        [Write(false)]
        public override bool IsModified => EntityState == EntityState.Modified || EmployeeID > 0;
        public EmployeeInfo()
        {
                
        }

    }

}
