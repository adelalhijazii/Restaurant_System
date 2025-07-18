namespace Restaurant.Models.Repository
{
    public class MasterWorkingHourRepository : IRepository<MasterWorkingHour>
    {
        public MasterWorkingHourRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterWorkingHour entity)
        {
            MasterWorkingHour data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterWorkingHour entity)
        {
            entity.IsActive = true;
            Db.MasterWorkingHour.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterWorkingHour entity)
        {
            MasterWorkingHour data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterWorkingHour Find(int id)
        {
            var data = Db.MasterWorkingHour.SingleOrDefault(x => x.MasterWorkingHourId == id);
            return data;
        }

        public void Update(int id, MasterWorkingHour entity)
        {
            Db.MasterWorkingHour.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterWorkingHour> View()
        {
            return Db.MasterWorkingHour.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterWorkingHour> ViewFormClient()
        {
            return Db.MasterWorkingHour.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
