using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangoLocal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void buttonChecking_Click(object sender, EventArgs e)
        {
            //var mySettings = Program.Configuration.GetValue<string>("ConnectionStrings:ENRICHRCPConnection");
            //if (mySettings.Contains("|DataDirectory|"))
            //{
            //    mySettings = mySettings.Replace("|DataDirectory|", System.IO.Directory.GetCurrentDirectory());
            //}
            textBoxMessage.Visible = true;
            textBoxMessage.Text = "Logging...";

            var clientConnectionString = textBoxClientConnectionString.Text.Trim();
            var serverConnectionString = textBoxServerConnectionString.Text.Trim();

            if (String.IsNullOrEmpty(clientConnectionString))
            {
                textBoxMessage.ForeColor = Color.Red;
                textBoxMessage.Text = "Client ConnectionString is Empty";
                return;
            }
            else if (String.IsNullOrEmpty(serverConnectionString))
            {
                textBoxMessage.ForeColor = Color.Red;
                textBoxMessage.Text = "Server ConnectionString is Empty";
                return;
            }

            if (!checkingConnectionStringIsValid(clientConnectionString))
            {
                textBoxMessage.ForeColor = Color.Red;
                textBoxMessage.Text = "Client ConnectionString is Invalid";
                return;
            }
            else if (!checkingConnectionStringIsValid(serverConnectionString))
            {
                textBoxMessage.ForeColor = Color.Red;
                textBoxMessage.Text = "Server ConnectionString is Invalid";
                return;
            }


            textBoxMessage.ForeColor = Color.Green;
            textBoxMessage.Text = $"Tesing successfully!!!";

        }

        private async Task<bool> checkingDatabaseHasTable(string clientConnectionString, string tablename)
        {
            bool exists = false;

            using (var connection = new SqlConnection(clientConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    // ANSI SQL way.  Works in PostgreSQL, MSSQL, MySQL.  
                    exists = await connection.ExecuteScalarAsync<bool>(
                      "select case when exists((select * from information_schema.tables where table_name = '" + tablename + "')) then 1 else 0 end");
                }
                catch
                {
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return exists;
        }

        private async Task<List<string>> GetListTableNames(string serverConnectionString)
        {
            var tableNames = new List<string>();
            var syncTableNames = Program.Configuration.GetSection("SyncTables").Get<List<string>>();
            using (SqlConnection conn = new SqlConnection(serverConnectionString))
            {
                try
                {
                    conn.Open();
                    tableNames = (await conn.QueryAsync<string>(@"SELECT '[' + SCHEMA_NAME(schema_id) + '].[' + name +']' 
                                                                    FROM sys.Tables
                                                                    WHERE name IN @syncTableNames and name ='EMP_EMPLOYEE'", new { syncTableNames }))
                                                                .ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Dispose();
                }
            }

            return tableNames;
        }

        private bool checkingConnectionStringIsValid(string clientConnectionString)
        {
            try
            {
                DbConnectionStringBuilder csb = new DbConnectionStringBuilder();
                csb.ConnectionString = clientConnectionString;
                using (SqlConnection conn = new SqlConnection(clientConnectionString))
                {
                    conn.Open();
                    conn.Dispose();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async void syncButton_Click(object sender, EventArgs e)
        {
            try
            {
                string errors = String.Empty;
                textBoxMessage.Visible = true;
                textBoxMessage.Text = "Syncing...";

                var clientConnectionString = textBoxClientConnectionString.Text.Trim();
                var serverConnectionString = textBoxServerConnectionString.Text.Trim();
                if (String.IsNullOrEmpty(clientConnectionString))
                {
                    clientConnectionString = Program.Configuration.GetValue<string>("ConnectionStrings:ClientConnection");
                }
                if (String.IsNullOrEmpty(serverConnectionString))
                {
                    serverConnectionString = Program.Configuration.GetValue<string>("ConnectionStrings:ServerConnection");
                }

                var tableNames = new List<string>();

                if (clientConnectionString.Contains("|DataDirectory|"))
                {
                    clientConnectionString = clientConnectionString.Replace("|DataDirectory|", System.IO.Directory.GetCurrentDirectory());
                }

                if (serverConnectionString.Contains("|DataDirectory|"))
                {
                    serverConnectionString = serverConnectionString.Replace("|DataDirectory|", System.IO.Directory.GetCurrentDirectory());
                }

                tableNames = await GetListTableNames(serverConnectionString);

                try
                {
                    var res = await DotmimSyncSqlServer.Initialize(tableNames, serverConnectionString, clientConnectionString);
                    textBoxMessage.ForeColor = Color.Green;
                    textBoxMessage.Text = res;
                }
                catch (Exception ex)
                {
                    errors += ex.Message + "\n";
                    textBoxError.Visible = true;
                    textBoxError.ForeColor = Color.Red;
                    textBoxError.Text = errors;
                }

                //foreach (var tablename in tableNames)
                //{
                //    try
                //    {
                //        textBoxMessage.ForeColor = Color.Blue;
                //        textBoxMessage.Text = "Start to sync " + tablename;
                //        //DataSynchronizer.Synchronize(tablename, serverConnectionString, clientConnectionString);
                //        //DataSynchronizerWithFilter.Synchronize(tablename, serverConnectionString, clientConnectionString);
                //        //textBoxMessage.Text = "Sync Successfully: " + tablename;
                //        var res = await DotmimSyncSqlServer.Initialize(tablename, serverConnectionString, clientConnectionString);
                //        textBoxMessage.ForeColor = Color.Green;
                //        textBoxMessage.Text = res;
                //    }
                //    catch (Exception ex)
                //    {
                //        errors += ex.Message + "\n";
                //        textBoxError.Visible = true;
                //        textBoxError.ForeColor = Color.Red;
                //        textBoxError.Text = errors;
                //    }
                //}

                if (String.IsNullOrEmpty(errors))
                {
                    textBoxError.Visible = false;
                }
                textBoxMessage.ForeColor = Color.Green;
                textBoxMessage.Text = "Sync Successfully All!";
            }
            catch (Exception ex)
            {
                textBoxMessage.Visible = true;
                textBoxMessage.ForeColor = Color.Red;
                textBoxMessage.Text = ex.Message;
            }
        }
    }
}
