using System.Reflection;
using System.Text;

namespace Serealizer.Classes
{
    public class Serealizer
    {
        public static string Serealize<T>(T obj) where T : class
        {
            StringBuilder sb = new StringBuilder();
            var props = GetProperties(obj.GetType());
            foreach (var prop in props)
            {
                var propVal = prop.GetValue(obj); // Значение

                //var propType = propVal?.GetType(); // Тип значения
                //var serValue = propType == null || (propType.IsPrimitive && propType != typeof(char)) ? $"{propVal?.ToString().ToLower() ?? "null"}" : $"{propVal}";

                sb.Append($"\"{prop.Name}\": {propVal},");
            }
            if (sb.Length > 0)
                sb.Length--;  // Убираем последнюю запятую
            string result = $"{{{sb}}}";
            return result;
        }

        public static T Deserealise<T>(string jsonObj) where T : class
        {
            if (!IsJSONStrIsCorrect(jsonObj))
                throw new ArgumentException(jsonObj, nameof(jsonObj));

            var resultObj = Activator.CreateInstance<T>();
            var pairs = jsonObj.Trim('{', '}')
                .Split(',')
                .Select(s => s.Split(':'))
                .Where(kvp => kvp.Length == 2)
                .ToDictionary(kvp => kvp[0].Trim('"'), kvp => kvp[1].Trim());

            foreach (var prop in GetProperties(resultObj.GetType()))
            {
                var propVal = TryParse(prop.PropertyType, pairs.SingleOrDefault(f => f.Key == prop.Name).Value);
                prop.SetValue(resultObj, propVal);
            }
            return resultObj;

        }

        private static PropertyInfo[] GetProperties<T>(T obj) where T : Type
        {
            return obj.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        private static object TryParse(Type propertyType, object value)
        {
            try
            {
                return Convert.ChangeType(value, propertyType);
            }
            catch
            {
                return GetDefault(propertyType);
            }
        }
        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
        private static bool IsJSONStrIsCorrect(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr) || !jsonStr.StartsWith('{') || !jsonStr.EndsWith('}'))
                return false;
            return true;
        }
    }
}
