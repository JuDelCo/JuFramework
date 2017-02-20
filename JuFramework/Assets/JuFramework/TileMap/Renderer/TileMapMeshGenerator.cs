using System.Collections.Generic;
using JuFramework.TileMapData;

namespace JuFramework
{
	public class TileMapMeshGenerator
	{
		public List<TileMapMeshLayer> GenerateLayers(TileMap tileMap)
		{
			var layers = new List<TileMapMeshLayer>();

			for (int layerIndex = 0; layerIndex < tileMap.tileLayers.Count; ++layerIndex)
			{
				layers.Add(GenerateMeshLayer(tileMap, layerIndex));
			}

			return layers;
		}

		private TileMapMeshLayer GenerateMeshLayer(TileMap tileMap, int layerIndex)
		{
			var tileLayer = tileMap.tileLayers[layerIndex];
			var tileSet = tileLayer.GetTileSet();

			var meshLayer = new TileMapMeshLayer();
			meshLayer.name = tileLayer.name;
			meshLayer.visible = tileLayer.visible;
			meshLayer.texture = tileSet.texture;
			meshLayer.chunks = GenerateMeshChunks(tileMap, tileLayer, tileSet);

			return meshLayer;
		}

		private List<Mesh> GenerateMeshChunks(TileMap tileMap, TileLayer tileLayer, TileSet tileSet)
		{
			var meshChunks = new List<Mesh>();

			int chunkThreshold = 100; // Max tiles per chunk (in a row or column)
			int chunkColumns = Math.Floor(tileMap.width / chunkThreshold) + 1;
			int chunkRows = Math.Floor(tileMap.height / chunkThreshold) + 1;

			var meshChunkGenerator = new TileMapMeshChunkGenerator(tileMap, tileLayer, tileSet);

			for (int y = 0; y < chunkRows; ++y)
			{
				for (int x = 0; x < chunkColumns; ++x)
				{
					var chunkTilesX = (tileMap.width - (x * chunkThreshold)) > chunkThreshold ? chunkThreshold : (tileMap.width - (x * chunkThreshold));
					var chunkTilesY = (tileMap.height - (y * chunkThreshold)) > chunkThreshold ? chunkThreshold : (tileMap.height - (y * chunkThreshold));
					var chunkTileOffsetX = x * chunkThreshold;
					var chunkTileOffsetY = y * chunkColumns * chunkThreshold;

					var pixelsPerUnit = 32;
					var mesh = meshChunkGenerator.GenerateMeshChunk(new Vector2i(chunkTilesX, chunkTilesY), new Vector2i(chunkTileOffsetX, chunkTileOffsetY), pixelsPerUnit, true);

					meshChunks.Add(mesh);
				}
			}

			return meshChunks;
		}
	}
}
