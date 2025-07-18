namespace Restaurant.Models.Repository
{
    public class SystemSettingRepository : IRepository<SystemSetting>
    {
        public SystemSettingRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, SystemSetting entity)
        {
            SystemSetting data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(SystemSetting entity)
        {
            entity.IsActive = true;
            Db.SystemSetting.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, SystemSetting entity)
        {
            SystemSetting data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public SystemSetting Find(int id)
        {
            var data = Db.SystemSetting.SingleOrDefault(x => x.SystemSettingId == id);
            return data;
        }

        public void Update(int id, SystemSetting entity)
        {
            Db.SystemSetting.Update(entity);
            Db.SaveChanges();
        }

        public IList<SystemSetting> View()
        {
            return Db.SystemSetting.Where(data => data.IsDelete == false).ToList();
        }

        public IList<SystemSetting> ViewFormClient()
        {
            return Db.SystemSetting.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
