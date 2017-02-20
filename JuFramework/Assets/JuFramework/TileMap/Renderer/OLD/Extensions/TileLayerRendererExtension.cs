
namespace JuFramework.TileMapRenderer
{
	public static class TileLayerRendererExtension
	{
		public static int GetChunkIndex(this TileLayerRenderer mapTileLayerRenderer, int x, int y)
		{
			var chunkColumn = Math.Floor(x / TileLayerRenderer.chunkThreshold);
			var chunkRow = Math.Floor(y / TileLayerRenderer.chunkThreshold);
			var chunkIndex = chunkColumn + (chunkRow * mapTileLayerRenderer.chunkColumns);

			return chunkIndex;
		}

		public static void SetTileID(this TileLayerRenderer mapTileLayerRenderer, int x, int y, uint gid, bool flipX = false, bool flipY = false, bool flipDiagonal = false)
		{
			var chunkIndex = mapTileLayerRenderer.GetChunkIndex(x, y);
			var chunkTileX = x % TileLayerRenderer.chunkThreshold;
			var chunkTileY = y % TileLayerRenderer.chunkThreshold;

			mapTileLayerRenderer.chunks[chunkIndex].SetTileID(chunkTileX, chunkTileY, gid, flipX, flipY, flipDiagonal);
		}

		public static void RefreshAllTiles(this TileLayerRenderer mapTileLayerRenderer)
		{
			foreach (var chunk in mapTileLayerRenderer.chunks)
			{
				chunk.RefreshAllTiles();
			}
		}

		public static void ApplyMeshTileChanges(this TileLayerRenderer mapTileLayerRenderer)
		{
			foreach (var chunk in mapTileLayerRenderer.chunks)
			{
				chunk.ApplyMeshTileChanges();
			}
		}
	}
}
