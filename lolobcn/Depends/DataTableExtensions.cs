using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Diagnostics;

namespace lolobcn.Depends
{
    /// <summary>
    /// 
    /// Title: fetch datarow to c# object
    /// 
    /// class DataTableExtensions
    /// 参考自: http://stackoverflow.com/questions/4593663/fetch-datarow-to-c-sharp-object
    /// 
    /// </summary>
    public static class DataTableExtensions
    {
        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        public static IList<T> ToList<T>(this DataTable table, Dictionary<string, string> mappings) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties, mappings);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                try
                {
                    if (property.Name != "EntityKey" && property.Name != "EntityState")
                        property.SetValue(item, row[property.Name], null);
                }
                catch (Exception ex) {
                    /* "EntityKey" and "EntityState" is not in database rows */
                    System.Console.WriteLine(ex.ToString());
                    Trace.Write(ex);
                }
            }
            return item;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties, Dictionary<string, string> mappings) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                try
                {
                    if (mappings.ContainsKey(property.Name))
                        property.SetValue(item, row[mappings[property.Name]], null);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    Trace.Write(ex);
                }
            }
            return item;
        }
    }

    public class DataTableHelper
    {
        /*
        public static List<T> ConvertRowsToList<T>( DataTable input, Convert<DataRow, T> conversion) {
            List<T> retval = new List<T>();
            foreach (DataRow dr in input.Rows)
            {
                retval.Add(conversion(dr));
            }
            return retval;
        }
        //*/

        #region "getobject filled object with property reconized"

        public List<T> ConvertToList<T>(DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                {
                    columnsNames.Add(DataColumn.ColumnName);
                }
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => GetObjectFromRow<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }

        }
        public T GetObjectFromRow<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }

        #endregion
    }
}
