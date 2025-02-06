using Barber.Colocho.App.Models.Authenticate;
using SQLite;

namespace Barber.Colocho.App.Db
{
    public class DbContext
    {
        private const string DatabaseFilename = "colocho.db3";
        private const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        #region Singleton
        private static DbContext _instance = null;
        public static DbContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbContext();
                }
                return _instance;
            }
        }
        #endregion

        #region Constructor
        private readonly SQLiteConnection connection;
        public DbContext()
        {
            try
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string path = Path.Combine(basePath, DatabaseFilename);
                connection = new SQLiteConnection(path, Flags);
                connection.CreateTable<TokenResponseModel>();
            }
            catch (System.Exception ex)
            {

            }
        }
        #endregion

        public void InsertToken(TokenResponseModel tokenResponseModel)
        {
            try
            {
                DeleteToken();
                connection.Insert(tokenResponseModel);
            }
            catch(Exception ex)
            {

            }
        }

        public TokenResponseModel GetToken()
        {
            try
            {
                var result = connection.Table<TokenResponseModel>().FirstOrDefault();
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public void DeleteToken()
        {
            try
            {
                connection.DeleteAll<TokenResponseModel>();
            }
            catch(Exception ex)
            {

            }
        }

        public void UpdateToken(int companyId)
        {
            try
            {
                var result = connection.Table<TokenResponseModel>().FirstOrDefault();
                if (result != null) 
                {
                    result.CompanyId = companyId;
                    connection.Update(result);
                }              
            }
            catch(Exception ex)
            {

            }
        }
    }
}
