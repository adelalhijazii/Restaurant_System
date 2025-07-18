namespace Restaurant.Models.Repository
{
    public class MasterMenuRepository : IRepository<MasterMenu>
    {
        public MasterMenuRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterMenu entity)
        {
            MasterMenu data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterMenu entity)
        {
            entity.IsActive = true;
            Db.MasterMenu.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterMenu entity)
        {
            MasterMenu data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterMenu Find(int id)
        {
            var data = Db.MasterMenu.SingleOrDefault(x => x.MasterMenuId == id);
            return data;
        }

        public void Update(int id, MasterMenu entity)
        {
            Db.MasterMenu.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterMenu> View()
        {
            return Db.MasterMenu.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterMenu> ViewFormClient()
        {
            return Db.MasterMenu.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
