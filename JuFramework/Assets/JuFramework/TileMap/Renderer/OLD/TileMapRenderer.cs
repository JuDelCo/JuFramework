using System.Collections.Generic;
using JuFramework.TileMapData;

namespace JuFramework.TileMapRenderer
{
	public class TileMapRenderer //: GameObject
	{
		public TileMap map;
		public TileCache cache;
		public readonly Dictionary<int, TileLayerRenderer> tileLayers = new Dictionary<int, TileLayerRenderer>();
		private int pixelsPerUnit = 32;

		/*public override void Despawn()
		{
			for (int i = 0; i < tileLayers.Count; ++i)
			{
				tileLayers[i].Despawn();
			}

			base.Despawn();
		}*/

		public void SetMap(TileMap map)
		{
			this.map = map;
			cache = new TileCache(map);
		}

		public void SetPixelsPerUnit(int pixelsPerUnit)
		{
			this.pixelsPerUnit = pixelsPerUnit;
		}

		public void RebuildMap(bool removeEmptyTiles = true)
		{
			cache.ClearCache();
			RebuildTileLayers(removeEmptyTiles);
		}

		private void RebuildTileLayers(bool removeEmptyTiles)
		{
			tileLayers.Clear();

			int tileLayerCount = 0;

			for (int layerIndex = 0; layerIndex < map.tileLayers.Count; ++layerIndex)
			{
				var tileLayer = map.tileLayers[layerIndex];
				var layerTileSet = tileLayer.GetTileSet();
				var tileLayerRenderer = CreateNewTileLayer(tileLayerCount, tileLayer.name, tileLayer.visible);

				tileLayerRenderer.RebuildLayer(map, tileLayer, layerTileSet, pixelsPerUnit, removeEmptyTiles);
				tileLayerRenderer.RefreshAllTiles();
				tileLayerRenderer.ApplyMeshTileChanges();

				tileLayers.Add(tileLayerCount, tileLayerRenderer);

				++tileLayerCount;
			}
		}

		private TileLayerRenderer CreateNewTileLayer(int index, string name, bool visible)
		{
			var tileLayerRenderer = new TileLayerRenderer();
			//tileLayerRenderer.position = new Vector3f(0, 0, (map.tileLayers.Count - 1) - index);
			//tileLayerRenderer.SetName("Layer_" + index + "_" + name);
			//tileLayerRenderer.SetParent(this);

			/*if (! visible)
			{
				tileLayerRenderer.gameObject.SetActive(false);
			}*/

			return tileLayerRenderer;
		}
	}
}
