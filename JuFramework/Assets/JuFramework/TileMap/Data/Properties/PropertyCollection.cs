using System.Collections;
using System.Collections.Generic;

namespace JuFramework.TileMapData
{
	public class PropertyCollection : IEnumerable<Property>
	{
		public readonly Dictionary<string, Property> properties = new Dictionary<string, Property>();

		public IEnumerator<Property> GetEnumerator()
		{
			return properties.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return properties.Values.GetEnumerator();
		}
	}
}
