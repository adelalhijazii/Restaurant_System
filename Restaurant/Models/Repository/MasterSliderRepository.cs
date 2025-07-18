namespace Restaurant.Models.Repository
{
    public class MasterSliderRepository : IRepository<MasterSlider>
    {
        public MasterSliderRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterSlider entity)
        {
            MasterSlider data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterSlider entity)
        {
            entity.IsActive = true;
            Db.MasterSlider.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterSlider entity)
        {
            MasterSlider data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterSlider Find(int id)
        {
            var data = Db.MasterSlider.SingleOrDefault(x => x.MasterSliderId == id);
            return data;
        }

        public void Update(int id, MasterSlider entity)
        {
            Db.MasterSlider.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterSlider> View()
        {
            return Db.MasterSlider.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterSlider> ViewFormClient()
        {
            return Db.MasterSlider.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
