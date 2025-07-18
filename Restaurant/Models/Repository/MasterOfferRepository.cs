namespace Restaurant.Models.Repository
{
    public class MasterOfferRepository : IRepository<MasterOffer>
    {
        public MasterOfferRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterOffer entity)
        {
            MasterOffer data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterOffer entity)
        {
            entity.IsActive = true;
            Db.MasterOffer.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterOffer entity)
        {
            MasterOffer data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterOffer Find(int id)
        {
            var data = Db.MasterOffer.SingleOrDefault(x => x.MasterOfferId == id);
            return data;
        }

        public void Update(int id, MasterOffer entity)
        {
            Db.MasterOffer.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterOffer> View()
        {
            return Db.MasterOffer.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterOffer> ViewFormClient()
        {
            return Db.MasterOffer.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
