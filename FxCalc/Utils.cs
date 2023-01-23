using FxCalc.CustomAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace FxCalc
{


    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' is numeric.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if numeric, false if not.</returns>
        public static bool IsNumeric(this string @this)
        {
            return Regex.IsMatch(@this, "^[0-9]\\d*(\\,\\d+)?$");
        }
    }
    public static class Utils
    {
        #region Converter
        public static byte[] ToBtyeArray(this Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static Object ToObject(this byte[] arrBytes)
        {
            if (arrBytes == null)
                return null;
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }
        public static byte[] Byte8(string deger)
        {
            char[] arrayChar = deger.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }
        public static int ToInt(this object deger)
        {
            if (deger != null)
            {
                return Convert.ToInt32(deger);
            }
            return 0;
        }
        public static double ToDouble(this object deger)
        {
            if (deger != null)
            {
                return Convert.ToDouble(deger);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deger"></param>
        /// <param name="ReplaceComma">Virgülleri noktaya çevirmek için True değeri Gönder</param>
        /// <returns></returns>
        public static new string ToString(this object deger, bool ReplaceComma = false)
        {
            return deger.ToString().Replace(",", ".");
        }
        public static decimal ToDecimal(this object deger)
        {
            if (deger == null || deger == string.Empty)
            {
                return 0;
            }
            return Convert.ToDecimal(deger);
        }
        public static DateTime ToDateTime(this object deger)
        {
            if (deger != null)
            {
                return Convert.ToDateTime(deger);
            }
            return DateTime.Now;
        }
        public static string ReadFile(this string Path)
        {
            return File.ReadAllText(Path);
        }
        public static List<String> GetEnums(this Type enumType)
        {
            if (!enumType.IsEnum || string.IsNullOrEmpty(enumType.FullName))
                return null;

            var ret = new List<string>();
            foreach (var val in Enum.GetValues(enumType))
            {
                var definition = Enum.GetName(enumType, val);
                if (string.IsNullOrEmpty(definition))
                    continue;

                // can't use (int)val 
                var code = Convert.ToInt32(val);
                var description = GetDescription(enumType, definition);

                ret.Add(description);
            }

            return ret;
        }
        public static List<UnitData> GetUnitEnums(this Type enumType)
        {
            if (!enumType.IsEnum || string.IsNullOrEmpty(enumType.FullName))
                return null;

            var ret = new List<UnitData>();
            foreach (var val in Enum.GetValues(enumType))
            {
                var definition = Enum.GetName(enumType, val);
                if (string.IsNullOrEmpty(definition))
                    continue;

                // can't use (int)val 
                var code = Convert.ToInt32(val);
                var description = GetUnitAttribute(val as Enum).AsUnitData();

                ret.Add(description);
            }

            return ret;
        }
        public static string ToName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        public static int IntValidate(this object value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                return value.ToInt();
            }
        }
        public static string StringValidate(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }
        static string GetDescription(Type enumType, string field)
        {
            FieldInfo fieldInfo = enumType.GetField(field);
            if (fieldInfo == null)
                return string.Empty;

            foreach (var attribute in fieldInfo.GetCustomAttributes())
            {
                if (attribute == null)
                    continue;
                if (attribute is DescriptionAttribute descAtt)
                    return descAtt.Description;
                else if (attribute.ToString().IndexOf("Display", StringComparison.Ordinal) > 0)
                {
                    var prop = attribute.GetType().GetProperty("Name");
                    if (prop == null)
                        continue;
                    return Convert.ToString(prop.GetValue(attribute));
                }
            }

            return null;
        }
        public static T NameToEnum<T>(this string name) where T : Enum
        {
            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(UnitAttribute)) is UnitAttribute attribute)
                {
                    if (attribute.Description == name)
                    {
                        return (T)field.GetValue(null);
                    }
                }

                if (field.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(name));
        }
        #endregion
        #region Custom Converter
        public static UnitAttribute GetUnitAttribute(Enum enumValue)
        {
            // Get instance of the attribute.
            UnitAttribute r = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<UnitAttribute>();
            if (r == null)
            {
                return null;
            }
            else
            {
                return r;
            }

        }
        #endregion

        #region Value
        public static decimal EUR = 1;
        public static decimal USD = 1;
        #endregion
    }
}
