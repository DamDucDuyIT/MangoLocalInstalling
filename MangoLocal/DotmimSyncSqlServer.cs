using Dapper;
using Dotmim.Sync;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoLocal
{
    public static class DotmimSyncSqlServer
    {
        public static async Task<string> Initialize
            (List<string> tableNames,
            string serverConnectionString,
            string clientConnectionString)
        {
            Dotmim.Sync.SqlServer.SqlSyncProvider serverProvider = new Dotmim.Sync.SqlServer.SqlSyncProvider(serverConnectionString);

            // Sqlite Client provider acting as the "client"
            Dotmim.Sync.SqlServer.SqlSyncProvider clientProvider = new Dotmim.Sync.SqlServer.SqlSyncProvider(clientConnectionString);

            //#region First run
            //var remoteOrchestrator = new RemoteOrchestrator(serverProvider);
            //await remoteOrchestrator.DropAllAsync();

            //var localOrchestrator = new LocalOrchestrator(clientProvider);
            //await localOrchestrator.DropAllAsync();
            //#endregion

            // Tables involved in the sync process:
            var setup = new SyncSetup(tableNames);

            foreach(var tableName in tableNames)
            {
                setup.Tables[tableName].Columns.AddRange(await GetListColumnsNames(serverConnectionString, tableName));
            } 

            //var productFilter = new SetupFilter(table);
            //// Add a column as parameter. This column will be automaticaly added in the tracking table
            //productFilter.AddParameter("RVCNo", "Product");
            //// add the side where expression, mapping the parameter to the column
            //productFilter.AddWhere("RVCNo", table, "RVCNo");
            //// add this filter to setup
            //setup.Filters.Add(productFilter);

            var agent = new SyncAgent(clientProvider, serverProvider);
            //var parameters = new SyncParameters(("RVCNo", 1));

            //var localOrchestrator = new LocalOrchestrator(clientProvider);
            //await localOrchestrator.CreateScopeInfoTableAsync();

            //localOrchestrator = new LocalOrchestrator(serverProvider);
            //await localOrchestrator.CreateScopeInfoTableAsync();
            ////var scopeInfo = await localOrchestrator.GetScopeInfoAsync();
            //var localOrchestrator = new LocalOrchestrator(clientProvider);
            //await localOrchestrator.DropAllAsync();
            //await localOrchestrator.CreateScopeInfoTableAsync();

            //var remoteOrchestrator = new LocalOrchestrator(serverProvider);
            //await remoteOrchestrator.DropAllAsync();
            //await remoteOrchestrator.CreateScopeInfoTableAsync();

            var s1 = await agent.SynchronizeAsync(setup);
            return s1.ToString();
        }

        private static async Task<List<string>> GetListColumnsNames(string connectionString, string tableName)
        {
            var columnsNames = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    columnsNames = (await conn.QueryAsync<string>(@"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('" + tableName + @"') and system_type_id != 34"))
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

            return columnsNames;
        }
    }
}
