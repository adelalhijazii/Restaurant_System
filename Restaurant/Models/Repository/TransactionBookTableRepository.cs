namespace Restaurant.Models.Repository
{
    public class TransactionBookTableRepository : IRepository<TransactionBookTable>
    {
        public TransactionBookTableRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, TransactionBookTable entity)
        {
            TransactionBookTable data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(TransactionBookTable entity)
        {
            entity.IsActive = true;
            Db.TransactionBookTable.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, TransactionBookTable entity)
        {
            TransactionBookTable data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public TransactionBookTable Find(int id)
        {
            var data = Db.TransactionBookTable.SingleOrDefault(x => x.TransactionBookTableId == id);
            return data;
        }

        public void Update(int id, TransactionBookTable entity)
        {
            Db.TransactionBookTable.Update(entity);
            Db.SaveChanges();
        }

        public IList<TransactionBookTable> View()
        {
            return Db.TransactionBookTable.Where(data => data.IsDelete == false).ToList();
        }

        public IList<TransactionBookTable> ViewFormClient()
        {
            return Db.TransactionBookTable.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
