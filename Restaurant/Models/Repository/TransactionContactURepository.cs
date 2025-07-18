namespace Restaurant.Models.Repository
{
    public class TransactionContactURepository : IRepository<TransactionContactU>
    {
        public TransactionContactURepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, TransactionContactU entity)
        {
            TransactionContactU data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(TransactionContactU entity)
        {
            entity.IsActive = true;
            Db.TransactionContactU.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, TransactionContactU entity)
        {
            TransactionContactU data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public TransactionContactU Find(int id)
        {
            var data = Db.TransactionContactU.SingleOrDefault(x => x.TransactionContactUId == id);
            return data;
        }

        public void Update(int id, TransactionContactU entity)
        {
            Db.TransactionContactU.Update(entity);
            Db.SaveChanges();
        }

        public IList<TransactionContactU> View()
        {
            return Db.TransactionContactU.Where(data => data.IsDelete == false).ToList();
        }

        public IList<TransactionContactU> ViewFormClient()
        {
            return Db.TransactionContactU.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
