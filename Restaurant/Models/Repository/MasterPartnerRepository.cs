namespace Restaurant.Models.Repository
{
    public class MasterPartnerRepository : IRepository<MasterPartner>
    {
        public MasterPartnerRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterPartner entity)
        {
            MasterPartner data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterPartner entity)
        {
            entity.IsActive = true;
            Db.MasterPartner.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterPartner entity)
        {
            MasterPartner data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterPartner Find(int id)
        {
            var data = Db.MasterPartner.SingleOrDefault(x => x.MasterPartnerId == id);
            return data;
        }

        public void Update(int id, MasterPartner entity)
        {
            Db.MasterPartner.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterPartner> View()
        {
            return Db.MasterPartner.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterPartner> ViewFormClient()
        {
            return Db.MasterPartner.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
