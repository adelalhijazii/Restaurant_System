namespace Restaurant.Models.Repository
{
    public class WhatPeopleSayRepository : IRepository<WhatPeopleSay>
    {
        public WhatPeopleSayRepository(AppDbContext _db)
        {
            Db = _db;
        }

        public AppDbContext Db { get; }

        public void Active(int id, WhatPeopleSay entity)
        {
            WhatPeopleSay data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(WhatPeopleSay entity)
        {
            entity.IsActive = true;
            Db.WhatPeopleSay.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(int id, WhatPeopleSay entity)
        {
            WhatPeopleSay data = Find(id);
            data.IsActive = false;
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public WhatPeopleSay Find(int id)
        {
            var data = Db.WhatPeopleSay.SingleOrDefault(x => x.WhatPeopleSayId == id);
            return data;
        }

        public void Update(int id, WhatPeopleSay entity)
        {
            Db.WhatPeopleSay.Update(entity);
            Db.SaveChanges();
        }

        public IList<WhatPeopleSay> View()
        {
            return Db.WhatPeopleSay.Where(data => data.IsDelete == false).ToList();
        }

        public IList<WhatPeopleSay> ViewFormClient()
        {
            return Db.WhatPeopleSay.Where(data => data.IsDelete == false && data.IsActive == true).ToList();
        }
    }
}
