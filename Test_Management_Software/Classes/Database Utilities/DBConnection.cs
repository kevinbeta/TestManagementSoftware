using System;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using System.Data;
using System.Threading;

namespace Test_Management_Software
{
    
    class DBConnection
    {
        /// <summary>
        /// This class is used in conjunction with the DBCommand. It is 
        /// used to create the actual connection to the database. By using
        /// this class, the developer does not have to type out the connection
        /// string each time they wish to access the database. 
        /// This idea was suggested by Eric toulson.
        /// Partially coded by Matthew Mills.
        /// </summary>
        private static DBConnection connection = null;
        private SqlCeConnection dbConnection = new SqlCeConnection();
        private static string connectionString = @"Data Source=|DataDirectory|\MainDatabase.sdf";
        private Semaphore semaphore = new Semaphore(1,1);

        #region Private Methods
        private DBConnection() 
        {
            dbConnection = new SqlCeConnection(DBConnection.connectionString);
            try
            {
                dbConnection.Open();
            }
            catch (SqlCeException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private static SqlCeConnection getConnection()
        {
            lock (typeof(DBConnection))
            {
                if (DBConnection.connection == null)
                {
                    DBConnection.connection = new DBConnection();
                }
                return DBConnection.connection.dbConnection;
            }
        }
        #endregion

        #region Public Methods
        public static DBCommand makeCommand(string query)
        {
            if (DBConnection.connection == null)
            {
                DBConnection.getConnection();
            }
            return new DBCommand(new SqlCeCommand(query, DBConnection.connection.dbConnection));
        }
        #endregion
    }
}