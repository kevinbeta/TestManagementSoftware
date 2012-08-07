using System;
using System.Data.SqlServerCe;
using System.Threading;
using System.Windows.Forms;

namespace Test_Management_Software
{
    /// <summary>
    /// Wrapper Class used to connect to the database.
    /// This will make connecting and manipulating a database more efficient.
    /// This idea was suggested by Eric toulson.
    /// Partially coded by Matthew Mills.
    /// </summary>
    class DBCommand
    {
        private SqlCeCommand sqlCommand;

        private bool DBBusy = false;

        public DBCommand(SqlCeCommand sqlCommand)
        {
            this.sqlCommand = sqlCommand;
        }

        #region Public Methods
       
        public SqlCeCommand Command
        {
            get
            {
                return this.sqlCommand;
            }
        }

        public void RunNoReturnQuery()
        {
            if(!this.DBBusy)
            {
                try
                {
                    this.sqlCommand.ExecuteNonQuery();
                }
                catch (SqlCeException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            }
        }

        public SqlCeDataReader Start()
        {
            if (!this.DBBusy)
            {
                this.DBBusy = true;

                try
                {
                    return this.sqlCommand.ExecuteReader();
                }
                catch (SqlCeException sqlEx)
                {
                    System.Windows.Forms.MessageBox.Show(sqlEx.Message);
                    System.Console.Error.WriteLine("Error Executing SELECT operation: " + sqlEx.Message);
                }
            }
            return null;
        }

        public void Stop()
        {
            if (this.DBBusy)
            {
                this.DBBusy = false;
            }
        }

        public void Dispose()
        {
            if (this.sqlCommand != null)
            {
                this.sqlCommand.Dispose();
                this.sqlCommand = null;
            }
        }
        #endregion
    }
}