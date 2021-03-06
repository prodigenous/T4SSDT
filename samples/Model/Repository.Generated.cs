//------------------------------------------------------------------------------
// <auto-generated>
// T4SSDT Data access layer code generator.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.CodeDom.Compiler;

namespace Model
{
    [GeneratedCode("T4SSDT", "1.0")]
    public partial interface IRepository
    {
        int Create(User obj);
        int Update(User obj);
        int DeleteUser(short userId);
        bool GetUserById(short userId, out User obj);
        IEnumerable<UserRole> GetUserRole();
        int TestProcedure(int IntNotNullable, int? IntNullable, ref int? IntOut);
    }

    [GeneratedCode("T4SSDT", "1.0")]
    public partial class SqlRepository : IRepository
    {
        private readonly string _connectionString;
        private readonly string _readOnlyConnectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
            _readOnlyConnectionString = connectionString;
        }

        public SqlRepository(string connectionString, string readOnlyConnectionString)
        {
            _connectionString = connectionString;
            _readOnlyConnectionString = readOnlyConnectionString;
        }

        public int Create(User obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string cmdText = @"INSERT INTO User VALUES (@UserName, @Email, @HashedPassword, @Created)";
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@UserName", obj.UserName),
                    new SqlParameter("@Email", obj.Email),
                    new SqlParameter("@HashedPassword", obj.HashedPassword),
                    new SqlParameter("@Created", obj.Created),
                });

                using (cmd)
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Update(User obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string cmdText = @"UPDATE User
SET UserName = @UserName, Email = @Email, HashedPassword = @HashedPassword, Created = @Created
WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@UserName", obj.UserName),
                    new SqlParameter("@Email", obj.Email),
                    new SqlParameter("@HashedPassword", obj.HashedPassword),
                    new SqlParameter("@Created", obj.Created),
                });

                using (cmd)
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeleteUser(short userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string cmdText = @"DELETE FROM User WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@UserId", userId),
                });

                using (cmd)
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public bool GetUserById(short userId, out User obj)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string cmdText = @"
SELECT UserId, UserName, Email, HashedPassword, Created
FROM User
WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@UserId", userId),
                });

                using (cmd)
                {
                    obj = new User();
                    IDataReader reader = cmd.ExecuteReader();
                    if (reader.NextResult())
                    {
                        obj.UserId = (short)reader["UserId"];
                        obj.UserName = (string)reader["UserName"];
                        obj.Email = (string)reader["Email"];
                        obj.HashedPassword = (byte[])reader["HashedPassword"];
                        obj.Created = (DateTime)reader["Created"];
                    }
                    else
                        return false;

                    return true;
                }
            }
        }
        public IEnumerable<UserRole> GetUserRole()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string cmdText = @"SELECT UserName, Created, RoleId FROM UserRole";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                using (cmd)
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.NextResult())
                    {
                        UserRole obj = new UserRole();
                        obj.UserName = (string)reader["UserName"];
                        obj.Created = (DateTime)reader["Created"];
                        obj.RoleId = (short)reader["RoleId"];
                        yield return obj;
                    }
                }
            }
        }
        public int TestProcedure(int intNotNullable, int? intNullable, ref int? intOut)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("TestProcedure", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@IntNotNullable", intNotNullable),
                    new SqlParameter("@IntNullable", intNullable),
                    new SqlParameter("@IntOut", intOut) { Direction = ParameterDirection.InputOutput },
                });

                using (cmd)
                {
                    int retVal = cmd.ExecuteNonQuery();
                    intOut = (int?)cmd.Parameters["@IntOut"].Value;

                    return retVal;
                }
            }
        }
    }
}
