using System.Data;
using System.Reflection;

namespace DotNetDemo.Service.Helpers
{
    public static class DataTableMapper
    {
        public static List<T> MapDataTableToList<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (DataRow row in dataTable.Rows)
                {
                    T item = new T();
                    foreach (PropertyInfo property in properties)
                    {
                        if (dataTable.Columns.Contains(property.Name))
                        {
                            if (row[property.Name] != DBNull.Value)
                            {
                                property.SetValue(item, row[property.Name]);
                            }
                        }
                    }
                    list.Add(item);
                }
            }
            
            return list;
        }
    }
}
