using System.Collections.Generic;
using MicroServiceUser.Repository;
using Model.MongoDb;
using MongoDB.Driver;

namespace MicroServiceUser.Service
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IMongoUserDatabase settings)
        {
            var user = new MongoClient(settings.ConnectionString);
            var database = user.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>(settings.UserCollectionName);
        }

        public List<User> Get() =>
            _user.Find(user => true).ToList();

        public User GetId(string id) =>
            _user.Find(user => user.Id == id).FirstOrDefault();

        public User GetLogin(string login) =>
            _user.Find(user => user.Login == login).FirstOrDefault();

        public User Create(User newUser)
        {
            _user.InsertOne(newUser);
            return newUser;
        }

        public void Update(string id, User updateUser) =>
            _user.ReplaceOne(user => user.Id == id, updateUser);

        public void Remove(string id) =>
            _user.DeleteOne(user => user.Id == id);
    }
}
