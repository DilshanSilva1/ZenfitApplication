﻿using Draft2.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Draft2.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel user)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection=GetConnection()) 
                using (var command=new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                    //collation changed for password to take into account upper and lower case passwords but not usernames.
                command.CommandText = "select * from USERX where Username = @username and [Password] = @password COLLATE SQL_Latin1_General_CP1_CS_AS";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value=credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {

            UserModel user =null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from USERX where Username = @username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) 
                        {
                            user = new UserModel()
                            {
                                Id = reader[0].ToString(),
                                Username = reader[1].ToString(),
                                Password = string.Empty,
                                Name = reader[3].ToString(),
                                LastName = reader[4].ToString(),
                                email = reader[5].ToString(),

                            };
                        }
                }
               
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
