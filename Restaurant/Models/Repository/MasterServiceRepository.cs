namespace Restaurant.Models.Repository
{
    public class MasterServiceRepository : IRepository<MasterService>
    {
        public MasterServiceRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterService entity)
        {
            MasterService data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterService entity)
        {
            entity.IsActive = true;
            Db.MasterService.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterService entity)
        {
            MasterService data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterService Find(int id)
        {
            var data = Db.MasterService.SingleOrDefault(x => x.MasterServiceId == id);
            return data;
        }

        public void Update(int id, MasterService entity)
        {
            Db.MasterService.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterService> View()
        {
            return Db.MasterService.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterService> ViewFormClient()
        {
            return Db.MasterService.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
