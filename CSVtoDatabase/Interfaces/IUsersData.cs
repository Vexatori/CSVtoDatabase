using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSVtoDatabase.Data;

namespace CSVtoDatabase.Interfaces
{
    public interface IUsersData
    {
        IEnumerable<User> GetAll();

        Task<IEnumerable<User>> GetAllAsync();

        User GetById( int id );

        Task<User> GetByIdAsync( int id );

        void AddNew( User newItem );

        Task AddNewAsync( User newItem );

        void DeleteById( int id );

        Task DeleteByIdAsync( int id );

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
