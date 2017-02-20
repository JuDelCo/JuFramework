using JuFramework.TileMapData;

namespace JuFramework.TileMapRenderer
{
	public class TileLayerRenderer //: GameObject
	{
		public TileMap map;
		public TileLayer tileLayer;
		public TileSet tileSet;

		public const int chunkThreshold = 100;
		public int chunkColumns;
		public int chunkRows;
		public TileLayerChunkRenderer[] chunks;
		public int pixelsPerUnit;

		/*public override void Despawn()
		{
			for (int i = 0; i < chunks.Length; ++i)
			{
				chunks[i].Despawn();
			}

			base.Despawn();
		}*/

		public void RebuildLayer(TileMap map, TileLayer tileLayer, TileSet tileSet, int pixelsPerUnit, bool removeEmptyTiles)
		{
			this.map = map;
			this.tileLayer = tileLayer;
			this.tileSet = tileSet;
			this.pixelsPerUnit = pixelsPerUnit;

			RebuildTileLayerChunks(removeEmptyTiles);
		}

		private void RebuildTileLayerChunks(bool removeEmptyTiles)
		{
			chunkColumns = Math.Floor(map.width / chunkThreshold) + 1;
			chunkRows = Math.Floor(map.height / chunkThreshold) + 1;

			chunks = new TileLayerChunkRenderer[(chunkRows * chunkColumns)];

			for (int y = 0; y < chunkRows; ++y)
			{
				for (int x = 0; x < chunkColumns; ++x)
				{
					var chunkTilesX = (map.width - (x * chunkThreshold)) > chunkThreshold ? chunkThreshold : (map.width - (x * chunkThreshold));
					var chunkTilesY = (map.height - (y * chunkThreshold)) > chunkThreshold ? chunkThreshold : (map.height - (y * chunkThreshold));
					var chunkTileOffsetX = x * chunkThreshold;
					var chunkTileOffsetY = y * chunkColumns * chunkThreshold;

					var chunk = CreateNewTileLayerChunk((x + (y * chunkColumns)));

					chunk.Initialize(map, tileLayer, tileSet, pixelsPerUnit, removeEmptyTiles);
					chunk.RebuildLayerChunk(chunkTilesX, chunkTilesY, chunkTileOffsetX, chunkTileOffsetY);

					chunks[x + (y * chunkColumns)] = chunk;
				}
			}
		}

		private TileLayerChunkRenderer CreateNewTileLayerChunk(int index)
		{
			var chunk = new TileLayerChunkRenderer();
			//chunk.SetName("TileLayer Chunk " + index);
			//chunk.SetParent(this);

			return chunk;
		}
	}
}
