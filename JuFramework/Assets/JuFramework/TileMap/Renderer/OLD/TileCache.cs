using System.Collections.Generic;
using JuFramework.TileMapData;
using TileGID = System.UInt32;
using LayerID = System.Int32;

// Esta clase solo es un contenedor para cachear consultas de listas de un tipo de tile (ej: "dame todos los tiles con id #")

namespace JuFramework.TileMapRenderer
{
	public struct CacheTileInfo
	{
		public int x;
		public int y;
		public Tile tile;
	}

	public class TileCache
	{
		private TileMap map;
		private Dictionary<TileGID, Dictionary<LayerID, List<CacheTileInfo>>> cache;

		public TileCache(TileMap map)
		{
			this.map = map;
			cache = new Dictionary<TileGID, Dictionary<LayerID, List<CacheTileInfo>>>();
		}

		public Dictionary<LayerID, List<CacheTileInfo>> GetTiles(uint gid)
		{
			if(cache.ContainsKey(gid))
			{
				return cache[gid];
			}

			var tileCache = new Dictionary<LayerID, List<CacheTileInfo>>();

			for (int layerIndex = 0; layerIndex < map.tileLayers.Count; ++layerIndex)
			{
				var layerTiles = new List<CacheTileInfo>();

				for (int y = 0; y < map.tileLayers[layerIndex].height; ++y)
				{
					for (int x = 0; x < map.tileLayers[layerIndex].width; ++x)
					{
						if(map.tileLayers[layerIndex].GetTile(x, y).gid == gid)
						{
							var tileInfo = new CacheTileInfo();
							tileInfo.x = x;
							tileInfo.y = y;
							tileInfo.tile = map.tileLayers[layerIndex].GetTile(x, y);

							layerTiles.Add(tileInfo);
						}
					}
				}

				if(layerTiles.Count > 0)
				{
					tileCache.Add(layerIndex, layerTiles);
				}
			}

			cache[gid] = tileCache;

			return tileCache;
		}

		public void ClearCache()
		{
			cache.Clear();
		}
	}
}
