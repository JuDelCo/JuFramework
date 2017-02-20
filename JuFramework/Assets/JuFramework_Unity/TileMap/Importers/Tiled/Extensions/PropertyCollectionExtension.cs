using System;
using JuFramework.TileMapData;

namespace JuFramework.TileMapTiledImporter
{
	public static class PropertyCollectionExtension
	{
		public static void SetProperties(this PropertyCollection propertyCollection, XmlNode node)
		{
			if (node == null)
			{
				return;
			}

			foreach (var propertyNode in node.GetNodes("property"))
			{
				var newProperty = new Property();
				newProperty.name = propertyNode.GetString("name");
				newProperty.type = (PropertyType)Enum.Parse(typeof(PropertyType), propertyNode.GetString("type", "string"), true);
				newProperty.value = new PropertyValue(propertyNode.GetString("value"));

				propertyCollection.properties[propertyNode.GetString("name")] = newProperty;
			}
		}
	}
}
