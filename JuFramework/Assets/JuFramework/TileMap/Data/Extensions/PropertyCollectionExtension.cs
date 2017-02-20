using System.Collections.Generic;
using System.Linq;

namespace JuFramework.TileMapData
{
	public static class PropertyCollectionExtension
	{
		public static bool Contains(this PropertyCollection propertyCollection, string key)
		{
			return propertyCollection.properties.ContainsKey(key);
		}

		public static PropertyValue GetProperty(this PropertyCollection propertyCollection, string key)
		{
			Property property;

			if (!propertyCollection.properties.TryGetValue(key, out property))
			{
				property = new Property();
			}

			return property.value;
		}

		public static Dictionary<string, string> GetDictionary(this PropertyCollection propertyCollection)
		{
		   return propertyCollection.properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.value.ToStringValue());
		}
	}
}
