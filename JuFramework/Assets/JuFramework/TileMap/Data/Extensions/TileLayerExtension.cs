
namespace JuFramework.TileMapData
{
	public static class TileLayerExtension
	{
		public static Tile GetTile(this TileLayer tileLayer, int x, int y)
		{
			return tileLayer.tiles[x, y];
		}

		public static bool IsUsing(this TileLayer tileLayer, TileSet tileSet)
		{
			for (int y = 0; y < tileLayer.height; ++y)
			{
				for (int x = 0; x < tileLayer.width; ++x)
				{
					if(tileLayer.tiles[x, y].gid > 0 && tileSet.ContainsTile(tileLayer.tiles[x, y].gid))
					{
						return true;
					}
				}
			}

			return false;
		}

		public static TileSet GetTileSet(this TileLayer tileLayer)
		{
			foreach (var tileSet in tileLayer.map.tileSets)
			{
				if(tileLayer.IsUsing(tileSet))
				{
					return tileSet;
				}
			}

			return tileLayer.map.tileSets[0];
		}
	}
}
