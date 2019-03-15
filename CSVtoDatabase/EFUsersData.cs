using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSVtoDatabase.Context;
using CSVtoDatabase.Data;
using CSVtoDatabase.Interfaces;

namespace CSVtoDatabase
{
    public class EFUsersData : IUsersData
    {
        private readonly UsersDatabaseContext _db;

        public EFUsersData( UsersDatabaseContext db ) => _db = db;

        public IEnumerable<User> GetAll()
        {
            return _db.Users.AsEnumerable();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await Task.Yield();
            return _db.Users.AsEnumerable();
        }

        public User GetById( int id )
        {
            return _db.Users.FirstOrDefault( u => u.Id == id );
        }

        public Task<User> GetByIdAsync( int id )
        {
            return _db.Users.FirstOrDefaultAsync( u => u.Id == id );
        }

        public void AddNew( User newItem )
        {
            _db.Users.Add( newItem );
        }

        public async Task AddNewAsync( User newItem )
        {
            await Task.Yield();
            _db.Users.Add( newItem );
        }

        public void DeleteById( int id )
        {
            var user = GetById( id );
            if ( user is null ) return;
            _db.Users.Remove( user );
        }

        public async Task DeleteByIdAsync( int id )
        {
            var user = await GetByIdAsync( id );
            if ( user is null ) return;
            _db.Users.Remove( user );
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}