using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Services
{
    public class MosaikRepository : IMosaikRepository
    {
        private List<MosaikItem> _items;

        public MosaikRepository()
        {
            InitializeData();
        }

        public IEnumerable<MosaikItem> All
        {
            get { return _items; }
        }

        public bool IsAccountValid(MosaikItem mosaikItem)
        {
            return _items.Any(item => item.Email == mosaikItem.Email
            && item.Password == mosaikItem.Password);
        }

        public void InsertAccount(MosaikItem mosaikItem)
        {
            if (!CheckUsernameUsed(mosaikItem.Email))
            {
                _items.Add(mosaikItem);
            }
        }

        public bool CheckUsernameUsed(string Email)
        {
            return _items.Any(item => item.Email == Email);
        }
        private void InitializeData()
        {
            _items = new List<MosaikItem>();

            var item1 = new MosaikItem
            {
                FullName = "Learn app development",
                Email = "Take Microsoft Learn Courses",
                Password = "password"
            };

            _items.Add(item1);
        }
    }
}
