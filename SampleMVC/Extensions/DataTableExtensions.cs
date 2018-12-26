using SampleMVC.Models;
using SampleMVC.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SampleMVC.Extensions
{
    public static class DataTableExtensions
    {
        public static IEnumerable<User> ToUsers(this EnumerableRowCollection<DataRow> data)
        {

            foreach(DataRow row in data)
            {
                var user = new User
                {
                    Id = row.Field<int>("Id"),
                    UserName = row.Field<string>("Username"),
                    Password = row.Field<string>("Password"),
                    CompanyId = row.Field<int>("CompanyId"),
                    AccountId = row.Field<string>("AccountId"),
                    LicenseKey = row.Field<string>("LicenseKey"),

                    MasterId = row.Field<int>("MasterId"),
                    Address1 = row.Field<string>("Address1"),
                    Address2 = row.Field<string>("Address2"),
                    CityCode = row.Field<string>("CityCode"),
                    RegionCode = row.Field<string>("RegionCode"),
                    CountryCode = row.Field<string>("CountryCode"),
                    PostalCode = row.Field<string>("PostalCode")
                };

                yield return user;
            }
        }

        //public static IEnumerable<T> ToCustomEnumerable<T>(this EnumerableRowCollection<DataRow> data) where T : new()
        //{
        //    Logger _logger = new Logger();

        //    foreach (DataRow row in data)
        //    {
        //        T t = new T();
        //        Type type = typeof(T);
        //        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //        foreach (PropertyInfo property in properties)
        //        {

        //            //t.Id = row.Field<property.PropertyType>($"{ property.Name}");                   
        //            _logger.TestLogger($" Property Name { property.Name} , Property type {property.PropertyType}");
        //        }

        //        yield return t;
        //    }

        //}
    }
}