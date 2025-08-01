﻿namespace Restaurant.Models.Repository
{
    public class MasterSocialMediumRepository : IRepository<MasterSocialMedium>
    {
        public MasterSocialMediumRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, MasterSocialMedium entity)
        {
            MasterSocialMedium data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterSocialMedium entity)
        {
            entity.IsActive = true;
            Db.MasterSocialMedium.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, MasterSocialMedium entity)
        {
            MasterSocialMedium data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterSocialMedium Find(int id)
        {
            var data = Db.MasterSocialMedium.SingleOrDefault(x => x.MasterSocialMediumId == id);
            return data;
        }

        public void Update(int id, MasterSocialMedium entity)
        {
            Db.MasterSocialMedium.Update(entity);
            Db.SaveChanges();
        }

        public IList<MasterSocialMedium> View()
        {
            return Db.MasterSocialMedium.Where(data => data.IsDelete == false).ToList();
        }

        public IList<MasterSocialMedium> ViewFormClient()
        {
            return Db.MasterSocialMedium.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
