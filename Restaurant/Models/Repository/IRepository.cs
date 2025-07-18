namespace Restaurant.Models.Repository
{
    public interface IRepository<TEntity>
    {
        void Active (int id, TEntity entity);

        void Add (TEntity entity);

        void Delete(int id, TEntity entity);

        TEntity Find(int id);

        void Update (int id, TEntity entity);

        IList<TEntity> View();

        IList<TEntity> ViewFormClient();
    }
}
