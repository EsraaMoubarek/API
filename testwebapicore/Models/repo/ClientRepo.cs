using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class ClientRepo
    {
        WasteAppDbContext _db;
        public ClientRepo(WasteAppDbContext db)
        {
            this._db = db;
        }
        public void RegisterApartment(Client client)
        {
            client.CategoryId = 1;
            _db.Client.Add(client);
            _db.SaveChanges();
        }
        public void RegisterRestrnt(Client client)
        {
            client.CategoryId = 2;
            _db.Client.Add(client);
            _db.SaveChanges();
        }
        public Client FindClient(string phonenumber, string Password)
        {
            Client client = _db.Client.SingleOrDefault(u => u.Mobile == phonenumber && u.Password == Password);
            return client;
        }
        public User FindUser(string phonenumber, string Password)
        {
            User user = _db.User.SingleOrDefault(u => u.PhoneNumber == phonenumber && u.Password == Password);
            return user;
        }
        public List<ClientCategory> AllCategory()
        {
            List<ClientCategory> category = _db.ClientCategory.Select(a => new ClientCategory { Id = a.Id, Name = a.Name }).ToList();
            return category;
        }
    }
}
