using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CSVtoDatabase.Data;
using CSVtoDatabase.Interfaces;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;

namespace CSVtoDatabase.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUsersData _usersData;

        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

        public ObservableCollection<User> UsersFromDB { get; } = new ObservableCollection<User>();

        public MainWindowViewModel( IUsersData usersData )
        {
            _usersData = usersData;

            ReceiveFromCsvCommand = new RelayCommand( OnReceiveFromCSVCommandExecuted, CanReceiveFromCSVCommandExecuted );

            SendToDB = new RelayCommand( OnSendToDBExecuted, CanSendToDBExecuted );

            var usersFromDB = _usersData.GetAll();

            if ( usersFromDB != null )
            {
                foreach ( var user in usersFromDB )
                {
                    UsersFromDB.Add( user );
                }
            }
        }

        #region Считывание пользователей из CSV

        private string _filePath = String.Empty;

        public string FilePath { get => _filePath; set => Set( ref _filePath, value ); }

        private User _currentUser;

        public User CurrentUser { get => _currentUser; set => Set( ref _currentUser, value ); }

        public ICommand ReceiveFromCsvCommand { get; }

        private bool CanReceiveFromCSVCommandExecuted() => true;

        private void OnReceiveFromCSVCommandExecuted()
        {
            using ( StreamReader reader = new StreamReader( _filePath, Encoding.Default ) )
            {
                while ( !reader.EndOfStream )
                {
                    Task<string> readTask = new Task<string>( () => reader.ReadLine() );
                    readTask.Start();

                    string[] temp = readTask.Result.Split( new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries );
                    Users.Add( new User() { FullName = temp[ 0 ], Email = temp[ 1 ], Phone = temp[ 2 ] } );
                }
            }
        }

        #endregion

        #region Запись пользователей в БД

        private User _currentUserFromDB;

        public User CurrentUserFromDB { get => _currentUserFromDB; set => Set( ref _currentUserFromDB, value ); }

        public ICommand SendToDB { get; }

        private bool CanSendToDBExecuted() => true;

        private void OnSendToDBExecuted()
        {
            if ( Users.Any() )
            {
                foreach ( var user in Users )
                {
                    if ( !UsersFromDB.Contains( user ) )
                    {
                        _usersData.AddNew( user );
                    }
                }

                _usersData.SaveChanges();

                foreach ( var user in _usersData.GetAll() )
                {
                    if ( !UsersFromDB.Contains( user ) )
                    {
                        UsersFromDB.Add( user );
                    }
                }
            }
        }

        #endregion
    }
}