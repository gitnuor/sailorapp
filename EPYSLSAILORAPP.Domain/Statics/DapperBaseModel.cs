namespace EPYSLSAILORAPP.Domain.Statics
{
    public abstract class DapperBaseModel : IDapperBaseModel
    {
        public DapperBaseModel()
        {
            EntityState = EntityState.Added;
        }

        public int TotalRows { get; set; }
        public EntityState EntityState { get; set; }
    }

    public interface IDapperBaseModel
    {
        int TotalRows { get; set; }
        EntityState EntityState { get; set; }
    }
}
