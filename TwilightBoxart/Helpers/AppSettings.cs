using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace KirovAir.Core.Config
{
    /// <summary>
    /// Base class for key/value application/web config files. Inherit this class and add config properties where needed.
    /// </summary>
    public abstract class AppSettings
    {
        /// <summary>
        /// Appends the name/value collection on to the currently initialized config class properties.
        /// </summary>
        /// <param name="values"></param>
        public void Load(NameValueCollection values)
        {
            var properties = GetType().GetProperties();

            foreach (var keyName in values.AllKeys)
            {
                var property = properties.FirstOrDefault(c => c.Name.Equals(keyName, StringComparison.OrdinalIgnoreCase));
                if (property == null)
                    continue;

                var value = values[keyName];

                try
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(this, value);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(this, Convert.ToInt32(value));
                    }
                    else if (property.PropertyType == typeof(bool))
                    {
                        property.SetValue(this, Convert.ToBoolean(value));
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        property.SetValue(this, DateTime.Parse(value));
                    }
                    else if (property.PropertyType == typeof(List<string>))
                    {
                        var list = new List<string>();
                        if (!string.IsNullOrEmpty(value))
                        {
                            list = value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList();
                        }

                        property.SetValue(this, list);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Impossible de charger la configuration de l'application : {keyName} avec la valeur {value}. Le type de la propriété est-il correct ?", e);
                }
            }
        }
    }
}
