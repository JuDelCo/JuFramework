using System;
using System.Linq;

namespace JuFramework.TileMapData
{
	public static class MapExtension
	{
		public static PropertyCollection GetProperties(this TileMap map, uint gid)
		{
			var tileSet = map.tileSets.FirstOrDefault(ts => ts.ContainsTile(gid));

			if (tileSet == null)
			{
				throw new Exception("Failed to find properties for tile " + gid);
			}

			return tileSet.GetTileProperties(gid);
		}
	}
}
