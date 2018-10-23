using System;
using System.Reflection;

namespace HSBC.INS.Utils.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static void SetPropertyValue<T>(this PropertyInfo pInfo, T entity, object value)
        {
            switch (pInfo.PropertyType.FullName)
            {
                case "System.DateTime":
                    pInfo.SetValue(entity, Convert.ToDateTime(value));
                    break;

                case "System.Int16":
                    pInfo.SetValue(entity, Convert.ToInt16(value));
                    break;

                case "System.Int32":
                    pInfo.SetValue(entity, Convert.ToInt32(value));
                    break;

                case "System.Int64":
                    pInfo.SetValue(entity, Convert.ToInt64(value));
                    break;

                case "System.UInt16":
                    pInfo.SetValue(entity, Convert.ToUInt16(value));
                    break;

                case "System.UInt32":
                    pInfo.SetValue(entity, Convert.ToUInt32(value));
                    break;

                case "System.UInt64":
                    pInfo.SetValue(entity, Convert.ToUInt64(value));
                    break;

                case "System.Single":
                    pInfo.SetValue(entity, Convert.ToSingle(value));
                    break;

                case "System.Double":
                    pInfo.SetValue(entity, Convert.ToDouble(value));
                    break;

                case "System.Decimal":
                    pInfo.SetValue(entity, Convert.ToDecimal(value));
                    break;

                case "System.Boolean":
                    pInfo.SetValue(entity, Convert.ToBoolean(value));
                    break;

                default:
                    pInfo.SetValue(entity, value);
                    break;
            }
        }
    }
}