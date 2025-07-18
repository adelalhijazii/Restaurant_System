namespace Restaurant.Models.Repository
{
    public class MasterContactUsInformationRepository : IRepository<MasterContactUsInformation>
    {
        public MasterContactUsInformationRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterContactUsInformation entity)
        {
            MasterContactUsInformation data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterContactUsInformation entity)
        {
            entity.IsActive = true;
            Db.MasterContactUsInformation.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterContactUsInformation entity)
        {
            MasterContactUsInformation data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterContactUsInformation Find(int id)
        {
            var data = Db.MasterContactUsInformation.SingleOrDefault(x => x.MasterContactUsInformationId == id);
            return data;
        }

        public void Update(int id, MasterContactUsInformation entity)
        {
            Db.MasterContactUsInformation.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterContactUsInformation> View()
        {
            return Db.MasterContactUsInformation.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterContactUsInformation> ViewFormClient()
        {
            return Db.MasterContactUsInformation.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
