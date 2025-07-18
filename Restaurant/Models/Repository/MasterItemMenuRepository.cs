using Microsoft.EntityFrameworkCore;

namespace Restaurant.Models.Repository
{
    public class MasterItemMenuRepository : IRepository<MasterItemMenu>
    {
        public MasterItemMenuRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterItemMenu entity)
        {
            MasterItemMenu data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterItemMenu entity)
        {
            entity.IsActive = true;
            Db.MasterItemMenu.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterItemMenu entity)
        {
            MasterItemMenu data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterItemMenu Find(int id)
        {
            var data = Db.MasterItemMenu.SingleOrDefault(x => x.MasterItemMenuId == id);
            return data;
        }

        public void Update(int id, MasterItemMenu entity)
        {
            Db.MasterItemMenu.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterItemMenu> View()
        {
            return Db.MasterItemMenu.Include(x => x.MasterCategoryMenu).Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterItemMenu> ViewFormClient()
        {
            return Db.MasterItemMenu.Include(x => x.MasterCategoryMenu).Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
