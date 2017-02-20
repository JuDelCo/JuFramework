
namespace JuFramework.TileMapRenderer
{
	public static class TileMapRendererExtension
	{
		public static void SetTileID(this TileMapRenderer mapRenderer, int layerIndex, int x, int y, uint gid, bool flipX = false, bool flipY = false)
		{
			mapRenderer.tileLayers[layerIndex].SetTileID(x, y, gid, flipX, flipY);
		}

		public static void ApplyMeshTileChanges(this TileMapRenderer mapRenderer, int layerIndex)
		{
			mapRenderer.tileLayers[layerIndex].ApplyMeshTileChanges();
		}

		public static void ReplaceMapTiles(this TileMapRenderer mapRenderer, uint mapGid, uint gid)
		{
			var tiles = mapRenderer.cache.GetTiles(mapGid);

			foreach (var kvp in tiles)
			{
				foreach (var tile in kvp.Value)
				{
					mapRenderer.SetTileID(kvp.Key, tile.x, tile.y, gid);
				}
			}
		}
	}
}
