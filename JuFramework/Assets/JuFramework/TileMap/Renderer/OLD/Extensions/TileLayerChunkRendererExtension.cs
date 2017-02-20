using JuFramework.TileMapData;

namespace JuFramework.TileMapRenderer
{
	public static class TileLayerChunkRendererExtension
	{
		public static void RefreshAllTiles(this TileLayerChunkRenderer chunk)
		{
			for (int y = 0; y < chunk.chunkTilesY; ++y)
			{
				for (int x = 0; x < chunk.chunkTilesX; ++x)
				{
					var mapTile = chunk.tileLayer.GetTile(x + chunk.chunkTileOffsetX, y + chunk.chunkTileOffsetY);

					if(mapTile.gid > 0)
					{
						chunk.SetTileID(x, y, mapTile.gid, mapTile.flipHorizontal, mapTile.flipVertical, mapTile.flipDiagonal);
					}
				}
			}
		}
	}
}
