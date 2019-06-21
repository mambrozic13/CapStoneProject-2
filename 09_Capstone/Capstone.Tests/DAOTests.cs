using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    abstract public class DAOTests
    {
        protected string connectionString = "Server=.\\SqlExpress; Database=npcampground; Trusted_Connection=true;";
        private TransactionScope transaction;
        
        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();
            string script = File.ReadAllText("TestSetup.sql");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(script, conn);

                cmd.ExecuteNonQuery();
            }
        }

        protected int GetCount(string tableName)
        {
            string sql = $"SELECT COUNT(*) FROM @tableName";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tableName", tableName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.transaction.Dispose();
        }
    }
}
