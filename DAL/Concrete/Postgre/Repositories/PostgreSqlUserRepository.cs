using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DAL.Concrete.Postgre.Repositories
{
    public class PostgreSqlUserRepository :BaseRepository<User>
    {
        public PostgreSqlUserRepository(string ConString) : base(ConString)
        {

        }
        public override Task<bool> CreateAsync(User Entity)
        {
            using (var conn = new NpgsqlConnection(ConString))
            {
                conn.Open();
                using (var com = new NpgsqlCommand("CreateUser", conn))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@providedusername", NpgsqlTypes.NpgsqlDbType.Varchar, Entity.Username);
                    com.Parameters.AddWithValue("@providedpassword", NpgsqlTypes.NpgsqlDbType.Varchar, Entity.Password);
                    bool res = com.ExecuteNonQuery() == 1;
                    return Task.FromResult(res);
                }
            }
        }

        public override Task<bool> DeleteAsync(int id)
        {
            
            using (var conn = new NpgsqlConnection(ConString))
            {
                conn.Open();
                using (var com = new NpgsqlCommand("delete", conn))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@providedid", NpgsqlTypes.NpgsqlDbType.Integer, id);
                    
                    bool res = com.ExecuteNonQuery() == 1;
                    return Task.FromResult(res);
                }
            }
        }

        public override Task<List<User>> GetAllAsync()
        {
            List<User> users = new List<User>();
            using (var conn = new NpgsqlConnection(ConString))
            {
                
                conn.Open();
                using (var com = new NpgsqlCommand("getall", conn))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = com.ExecuteReader();
                    
                    while (reader.Read())
                    {

                        
                        User user = new User
                        {
                            Id = (int)reader["id"],
                            Username = (string)reader["username"],
                            Password = (string)reader["password"]
                        };
                        users.Add(user);

                    }
                }
            }
            return Task.FromResult(users.Count == 0 ? null : users);
        }

        public override Task<User> GetByIdAsync(int id)
        {
            User user = null;
            using (var conn = new NpgsqlConnection(ConString))
            {
                conn.Open();
                using (var com = new NpgsqlCommand("getbyid", conn))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@providedid", NpgsqlTypes.NpgsqlDbType.Integer, id);
                    var reader = com.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        user = new User 
                        { 
                            Id = (int)reader["id"],
                            Username = (string)reader["username"],
                            Password = (string)reader["password"]
                        };
                        
                    }
                }
            }
            return Task.FromResult(user);
        }
    }
}
