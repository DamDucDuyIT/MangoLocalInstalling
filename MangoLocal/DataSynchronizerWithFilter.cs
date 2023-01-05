using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using System;
using System.Data.SqlClient;

namespace MangoLocal
{
    public static class DataSynchronizerWithFilter
    {
        public static void Initialize
            (string table,
            string serverConnectionString,
            string clientConnectionString)
        {
            using (SqlConnection serverConnection = new
                SqlConnection(serverConnectionString))
            {
                using (SqlConnection clientConnection = new
                    SqlConnection(clientConnectionString))
                {
                    //DbSyncScopeDescription scopeDescription = new
                    //    DbSyncScopeDescription(table);
                    //DbSyncTableDescription tableDescription =
                    //    SqlSyncDescriptionBuilder.GetDescriptionForTable(table,
                    //        serverConnection);
                    //scopeDescription.Tables.Add(tableDescription);
                    //SqlSyncScopeProvisioning serverProvision = new
                    //    SqlSyncScopeProvisioning(serverConnection,
                    //        scopeDescription);
                    //serverProvision.Apply();
                    //SqlSyncScopeProvisioning clientProvision = new
                    //    SqlSyncScopeProvisioning(clientConnection,
                    //       scopeDescription);
                    //clientProvision.Apply();

                    try
                    {
                        CleanUp(table, serverConnectionString, clientConnectionString);
                    }
                    catch { }

                    DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(table);
                    scopeDesc.UserComment = "Template for filtering based on branch id.";
                    DbSyncTableDescription EMPLOYEE = SqlSyncDescriptionBuilder.GetDescriptionForTable(table, serverConnection);
                    scopeDesc.Tables.Add(EMPLOYEE);

                    //creating a provisioning template
                    SqlSyncScopeProvisioning serverProvisionTemplate = new SqlSyncScopeProvisioning(serverConnection, scopeDesc);
                    serverProvisionTemplate.ObjectSchema = "dbo";
                    //determine if this scope already exists on the server and if not go ahead and provision
                    if (!serverProvisionTemplate.ScopeExists(table))
                    {
                        //add the approrpiate tables to this scope
                        serverProvisionTemplate.Tables[table].AddFilterColumn("RVCNo");
                        serverProvisionTemplate.Tables[table].FilterClause = "[base].[RVCNo] = 1";
                        //SqlParameter param = new SqlParameter("@RVCNo", SqlDbType.Int, 1);
                        //serverProvisionTemplate.Tables[table].FilterParameters.Add(param);

                        //note that it is important to call this after the tables have been added to the scope
                        serverProvisionTemplate.PopulateFromScopeDescription(scopeDesc);
                        //indicate that the base table already exists and does not need to be created
                        serverProvisionTemplate.SetCreateTableDefault(DbSyncCreationOption.Skip);
                        serverProvisionTemplate.SetCreateTrackingTableDefault(DbSyncCreationOption.CreateOrUseExisting);
                        //provision the server
                        serverProvisionTemplate.CommandTimeout = 3000;
                        serverProvisionTemplate.Apply();
                    }
                    //serverProvisionTemplate.Apply();


                    SqlSyncScopeProvisioning clientProvisionTemplate = new SqlSyncScopeProvisioning(clientConnection, scopeDesc);
                    clientProvisionTemplate.ObjectSchema = "dbo";
                    //determine if this scope already exists on the server and if not go ahead and provision
                    if (!clientProvisionTemplate.ScopeExists(table))
                    {
                        //add the approrpiate tables to this scope
                        clientProvisionTemplate.Tables[table].AddFilterColumn("RVCNo");
                        clientProvisionTemplate.Tables[table].FilterClause = "[base].[RVCNo] = 1";
                        //SqlParameter param = new SqlParameter("@RVCNo", SqlDbType.Int, 1);
                        //clientProvisionTemplate.Tables[table].FilterParameters.Add(param);

                        //note that it is important to call this after the tables have been added to the scope
                        clientProvisionTemplate.PopulateFromScopeDescription(scopeDesc);
                        //indicate that the base table already exists and does not need to be created
                        clientProvisionTemplate.SetCreateTableDefault(DbSyncCreationOption.Skip);
                        clientProvisionTemplate.SetCreateTrackingTableDefault(DbSyncCreationOption.CreateOrUseExisting);
                        //provision the server
                        clientProvisionTemplate.CommandTimeout = 3000;
                        clientProvisionTemplate.Apply();
                    }
                }
            }
        }

        public static void Synchronize(string tableName,
            string serverConnectionString, string clientConnectionString)
        {
            Initialize(tableName, serverConnectionString, clientConnectionString);
            Synchronize(tableName, serverConnectionString,
                clientConnectionString, SyncDirectionOrder.DownloadAndUpload);
            CleanUp(tableName, serverConnectionString, clientConnectionString);
        }

        private static void Synchronize(string scopeName,
            string serverConnectionString,
            string clientConnectionString, SyncDirectionOrder syncDirectionOrder)
        {
            using (SqlConnection serverConnection = new
                SqlConnection(serverConnectionString))
            {
                using (SqlConnection clientConnection
                    = new SqlConnection(clientConnectionString))
                {
                    var agent = new SyncOrchestrator
                    {
                        LocalProvider = new
                            SqlSyncProvider(scopeName, clientConnection),
                        RemoteProvider = new SqlSyncProvider(scopeName, serverConnection),
                        Direction = syncDirectionOrder
                    };
                    (agent.RemoteProvider as RelationalSyncProvider).SyncProgress +=
                        new EventHandler<DbSyncProgressEventArgs>
                        (dbProvider_SyncProgress);
                    (agent.LocalProvider as RelationalSyncProvider).ApplyChangeFailed +=
                        new EventHandler<DbApplyChangeFailedEventArgs>(dbProvider_SyncProcessFailed);
                    (agent.RemoteProvider as RelationalSyncProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>
                    (dbProvider_SyncProcessFailed);
                    agent.Synchronize();
                }
            }
        }

        private static void CleanUp(string scopeName,
            string serverConnectionString,
            string clientConnectionString)
        {
            using (SqlConnection serverConnection = new
                SqlConnection(serverConnectionString))
            {
                using (SqlConnection clientConnection = new
                    SqlConnection(clientConnectionString))
                {
                    SqlSyncScopeDeprovisioning serverDeprovisioning = new
                         SqlSyncScopeDeprovisioning(serverConnection);
                    SqlSyncScopeDeprovisioning clientDeprovisioning = new
                        SqlSyncScopeDeprovisioning(clientConnection);
                    serverDeprovisioning.DeprovisionScope(scopeName);
                    serverDeprovisioning.DeprovisionStore();
                    clientDeprovisioning.DeprovisionScope(scopeName);
                    clientDeprovisioning.DeprovisionStore();
                }
            }
        }

        private static void dbProvider_SyncProcessFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            //Write your code here
        }

        private static void dbProvider_SyncProgress(object sender, DbSyncProgressEventArgs e)
        {
            //Write your code here
        }
    }

}
