namespace Restaurant.Models.Repository
{
    public class TransactionNewsletterRepository : IRepository<TransactionNewsletter>
    {
        public TransactionNewsletterRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, TransactionNewsletter entity)
        {
            TransactionNewsletter data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(TransactionNewsletter entity)
        {
            entity.IsActive = true;
            Db.TransactionNewsletter.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, TransactionNewsletter entity)
        {
            TransactionNewsletter data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public TransactionNewsletter Find(int id)
        {
            var data = Db.TransactionNewsletter.SingleOrDefault(x => x.TransactionNewsletterId == id);
            return data;
        }

        public void Update(int id, TransactionNewsletter entity)
        {
            Db.TransactionNewsletter.Update(entity);
            Db.SaveChanges();
        }

        public IList<TransactionNewsletter> View()
        {
            return Db.TransactionNewsletter.Where(data => data.IsDelete == false).ToList();
        }

        public IList<TransactionNewsletter> ViewFormClient()
        {
            return Db.TransactionNewsletter.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
