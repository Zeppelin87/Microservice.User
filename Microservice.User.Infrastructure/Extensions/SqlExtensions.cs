using System;
using System.Data;

namespace Microservice.User.Infrastructure.Extensions
{
    public static class SqlExtensions
    {
        public static object ValueOrDbNull(this object @object)
        {
            if (@object == null) { return System.DBNull.Value; }

            if (typeof(string) == @object.GetType() && string.IsNullOrWhiteSpace((string)@object))
            {
                return System.DBNull.Value;
            }

            return @object;
        }

        public static T ValueOrNull<T>(this IDataReader reader, string columnName) where T : class
        {
            if (columnName == null)
            {
                throw new ArgumentException(nameof(columnName));
            }

            object result = reader[columnName];
            if (result == null || reader[columnName] == DBNull.Value)
            {
                return null;
            }

            return (T)result;
        }

        public static T ValueOrNull<T>(this IDataRecord row, int ordinal)
        {
            return (T)(row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal));
        }
    }
}
