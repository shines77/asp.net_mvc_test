using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
//using System.Data.DataSetExtensions;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;

namespace lolobcn.Depends
{
    /// <summary>
    /// 
    /// Title: fetch datarow to c# object
    /// 
    /// class DataTableExtensions
    /// 参考自: http://stackoverflow.com/questions/4593663/fetch-datarow-to-c-sharp-object
    /// 
    /// Now you can just call
    ///
    ///    var items = dt.ToList<Item>();
    ///
    ///  or
    ///
    ///    var mappings = new Dictionary<string, string>();
    ///    mappings.Add("ItemId", "item_id");
    ///    mappings.Add("ItemName ", "item_name");
    ///    mappings.Add("Price ", "price);
    ///    var items = dt.ToList<Item>(mappings);
    ///    
    /// </summary>
    public static partial class DataTableExtensionsEx
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

    /// <summary>
    /// 
    /// Title: Converting Custom Collections To and From DataTable
    /// 
    /// 来自: http://lozanotek.com/blog/archive/2007/05/09/Converting_Custom_Collections_To_and_From_DataTable.aspx
    /// 
    /// 用法: 
    /// 
    ///     List<Customer> customers = new List<Customer>(); customers.Add(new Customer());
    /// 
    ///     DataTable table = CollectionHelper.ConvertTo<Customer>(customers);
    /// 
    /// </summary>
    public class CollectionHelper
    {
        private CollectionHelper()
        {
        }

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
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

        /// <summary>
        /// Tile: Convert DataTable to List<T>
        /// 来自: http://stackoverflow.com/questions/1427484/convert-datatable-to-listt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datatable"></param>
        /// <returns></returns>
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

        ///
        /// IEnumerable collection To Datatable
        /// 
        #region "New DataTable"
        public DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            DataTable newDataTable = new DataTable();
            Type impliedType = typeof(T);
            PropertyInfo[] _propInfo = impliedType.GetProperties();
            foreach (PropertyInfo pi in _propInfo)
                newDataTable.Columns.Add(pi.Name, pi.PropertyType);

            foreach (T item in collection)
            {
                DataRow newDataRow = newDataTable.NewRow();
                newDataRow.BeginEdit();
                foreach (PropertyInfo pi in _propInfo)
                    newDataRow[pi.Name] = pi.GetValue(item, null);
                newDataRow.EndEdit();
                newDataTable.Rows.Add(newDataRow);
            }
            return newDataTable;
        }

        #endregion
    }
}
