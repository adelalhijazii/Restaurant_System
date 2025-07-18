namespace Restaurant.Models.Repository
{
    public class MasterCategoryMenuRepository : IRepository<MasterCategoryMenu>
    {
        public MasterCategoryMenuRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterCategoryMenu entity)
        {
            MasterCategoryMenu data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterCategoryMenu entity)
        {
            entity.IsActive = true;
            Db.MasterCategoryMenu.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterCategoryMenu entity)
        {
            MasterCategoryMenu data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterCategoryMenu Find(int id)
        {
            var data = Db.MasterCategoryMenu.SingleOrDefault(x => x.MasterCategoryMenuId == id);
            return data;
        }

        public void Update(int id, MasterCategoryMenu entity)
        {
            Db.MasterCategoryMenu.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterCategoryMenu> View()
        {
            return Db.MasterCategoryMenu.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterCategoryMenu> ViewFormClient()
        {
            return Db.MasterCategoryMenu.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
