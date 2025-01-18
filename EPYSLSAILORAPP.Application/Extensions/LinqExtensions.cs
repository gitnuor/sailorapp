using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.Interface;

namespace System.Linq
{
    public static class LinqExtensions
    {
     

        public static void SetUnchanged(this IEnumerable<IDapperBaseEntity> list)
        {
            foreach (var item in list)
            {
                item.EntityState = EntityState.Unchanged;
            }
        }

        public static void SetModified(this IEnumerable<IDapperBaseEntity> list)
        {
            foreach (var item in list)
            {
                item.EntityState = EntityState.Modified;
            }
        }

        public static void SetDeleted(this IEnumerable<IDapperBaseEntity> list)
        {
            foreach (var item in list)
            {
                item.EntityState = EntityState.Deleted;
            }
        }

    }
}
