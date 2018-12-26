using SampleMVC.Models;
using SampleMVC.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SampleMVC.Extensions;
namespace SampleMVC.Repository
{
    public class UserRepository : IUserRepository
    {
        string _conStr = "Data Source=DESKTOP-U7M344F;Initial Catalog=SampleMVC;Integrated Security=True";
        Logger _logger = new Logger();

        public IEnumerable<User> Get()
        {
            IEnumerable<User> Users = null;
            try
            {
                using (var con = new SqlConnection(_conStr))
                {
                    using (var cmd = new SqlCommand("sp_SelectAllUsers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var adapter = new SqlDataAdapter(cmd);

                        var usersSet = new DataSet();
                        adapter.Fill(usersSet, "Users");

                        Users = usersSet
                            .Tables["Users"]
                            .AsEnumerable()
                            .ToUsers();
                    }
                }
                return Users;
            }
            catch (Exception e)
            {
                _logger.ExceptionLogger(e);
                return Users;
            }

        }
        public User Get(int id)
        {
            User user = null;
            try
            {
                using (var con = new SqlConnection(_conStr))
                {
                    using (var cmd = new SqlCommand("sp_SelectUserById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        var adapter = new SqlDataAdapter(cmd);

                        var usersSet = new DataSet();
                        adapter.Fill(usersSet, "Users");

                        user = usersSet
                            .Tables["Users"]
                            .AsEnumerable()
                            .ToUsers()
                            .FirstOrDefault();
                    }

                }
                return user;
            }
            catch (Exception e)
            {
                _logger.ExceptionLogger(e);

                return user;
            }
        }

        public int UsersCountByName(string username)
        {
            try
            {
                var usersCount = 0;
                using (var con = new SqlConnection(_conStr))
                {
                    using (var cmd = new SqlCommand("sp_CountUserByName", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", username);
                        usersCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return usersCount;
            }
            catch (Exception e)
            {
                _logger.ExceptionLogger(e);

                return -1;
            }
        }


        public bool Add(User user)
        {
            try
            {
                using (var con = new SqlConnection(_conStr))
                {
                    using (var cmd = new SqlCommand("sp_InsertUserOutId", con))
                     {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", user.UserName);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@CompanyId", user.CompanyId);
                        cmd.Parameters.AddWithValue("@AccountId", user.AccountId);
                        cmd.Parameters.AddWithValue("@LicenseKey", user.LicenseKey);
                        cmd.Parameters.AddWithValue("@MasterId", user.MasterId);
                        cmd.Parameters.AddWithValue("@Address1", user.Address1);
                        cmd.Parameters.AddWithValue("@Address2", user.Address2);
                        cmd.Parameters.AddWithValue("@CityCode", user.CityCode);
                        cmd.Parameters.AddWithValue("@RegionCode", user.RegionCode);
                        cmd.Parameters.AddWithValue("@CountryCode", user.CountryCode);
                        cmd.Parameters.AddWithValue("@PostalCode", user.PostalCode);
                        var idParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;
                        var rowsEffected = cmd.ExecuteNonQuery();
                        user.Id = Convert.ToInt32(idParam.Value);
                        return rowsEffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.ExceptionLogger(e);

                return false;

            }


        }

        public bool Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_conStr))
                {
                    using (var cmd = new SqlCommand("sp_DeleteUserById", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        var rowsEffected = cmd.ExecuteNonQuery();
                        return rowsEffected > 0 ;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.ExceptionLogger(e);

                return false;
            }

        }

        public bool Update(User user)
        {
            try
            {
                using (var con = new SqlConnection(_conStr))
                {
                    using (var cmd = new SqlCommand("sp_UpdateUser", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@Username",user.UserName );
                        cmd.Parameters.AddWithValue("@Password",user.Password);
                        cmd.Parameters.AddWithValue("@CompanyId", user.CompanyId);
                        cmd.Parameters.AddWithValue("@AccountId", user.AccountId);
                        cmd.Parameters.AddWithValue("@LicenseKey",user.LicenseKey );
                        cmd.Parameters.AddWithValue("@MasterId",user.MasterId );
                        cmd.Parameters.AddWithValue("@Address1",user.Address1 );
                        cmd.Parameters.AddWithValue("@Address2",user.Address2 );
                        cmd.Parameters.AddWithValue("@CityCode",user.CityCode );
                        cmd.Parameters.AddWithValue("@RegionCode", user.RegionCode);
                        cmd.Parameters.AddWithValue("@CountryCode",user.CountryCode );
                        cmd.Parameters.AddWithValue("@PostalCode", user.PostalCode);
                        var rowsEffected = cmd.ExecuteNonQuery();
                        return rowsEffected > 0 ;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.ExceptionLogger(e);

                return false;

            }
        }

        //public IEnumerable<User> Get()
        //{
        //    List<User> Users = null;
        //    try
        //    {
        //        using (var con = new SqlConnection(_conStr))
        //        {
        //            var queryString = "select * from Users;";

        //            SqlCommand command = new SqlCommand(queryString, con);
        //            con.Open();

        //            SqlDataReader reader = command.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    Users.Add(new User
        //                    {
        //                        Id = Convert.ToInt32(reader["Id"].ToString()),
        //                        UserName = reader["Username"].ToString(),
        //                        Password = reader["Password"].ToString(),
        //                        CompanyId = Convert.ToInt32(reader["CompanyId"].ToString()),
        //                        AccountId = reader["AccountId"].ToString(),
        //                        LicenseKey = reader["LicenseKey"].ToString()
        //                    });
        //                }
        //            }
        //            reader.Close();
        //        }
        //        return Users;

        //    }
        //    catch (Exception e)
        //    {
        //        _logger.ExceptionLogger(e);
        //        return Users;


        //    }


        //}

        //public User Get(int id)
        //{
        //    User user = null;
        //    try
        //    {
        //        using (var con = new SqlConnection(_conStr))
        //        {
        //            con.Open();
        //            var sqlCmd = new SqlCommand($"select * from Users  where id = {id};", con);
        //            var reader = sqlCmd.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    user = new User
        //                    {
        //                        Id = Convert.ToInt32(reader["Id"].ToString()),
        //                        UserName = reader["Username"].ToString(),
        //                        Password = reader["Password"].ToString(),
        //                        CompanyId = Convert.ToInt32(reader["CompanyId"].ToString()),
        //                        AccountId = reader["AccountId"].ToString(),
        //                        LicenseKey = reader["LicenseKey"].ToString()
        //                    };
        //                }
        //            }
        //            reader.Close();
        //            sqlCmd.Dispose();
        //        }
        //        return user;
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.ExceptionLogger(e);

        //        return user;

        //    }

        //}


    }
}