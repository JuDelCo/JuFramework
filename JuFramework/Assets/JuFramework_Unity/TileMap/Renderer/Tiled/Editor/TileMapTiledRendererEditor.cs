using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using JuFramework.TileMapTiledImporter;

namespace JuFramework
{
	[CustomEditor(typeof(TileMapTiledRenderer))]
	public class TileMapTiledRendererEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if(GUILayout.Button("Generate TileMap"))
			{
				var tileMapTiledRenderer = (TileMapTiledRenderer)target;

				if(tileMapTiledRenderer.tiledFileObject == null || tileMapTiledRenderer.tiledFileObject.type != TiledFileObjectType.TileMap)
				{
					return;
				}

				DeleteChildObjects(tileMapTiledRenderer.transform);

				var tileMap = (new TiledImporter()).ParseMap(tileMapTiledRenderer.tiledFileObject.data);
				var layers = (new TileMapMeshGenerator()).GenerateLayers(tileMap);

				var assetPath = AssetDatabase.GetAssetPath(tileMapTiledRenderer.tiledFileObject);
				assetPath = assetPath.Substring(0, assetPath.Length - Path.GetExtension(assetPath).Length); // .asset
				assetPath = assetPath.Substring(0, assetPath.Length - Path.GetExtension(assetPath).Length); // .tilemap

				for (int layerIndex = 0; layerIndex < layers.Count; ++layerIndex)
				{
					var layer = layers[layerIndex];

					for (int meshIndex = 0; meshIndex < layer.chunks.Count; ++meshIndex)
					{
						if(!Directory.Exists(assetPath))
						{
							Directory.CreateDirectory(assetPath);
						}

						AssetDatabase.CreateAsset((UnityMesh)layer.chunks[meshIndex], assetPath + "/Mesh_L" + layerIndex + "_C" + meshIndex + ".asset");
					}
				}

				AssetDatabase.SaveAssets();

				tileMapTiledRenderer.GenerateTileMap(layers);
				tileMapTiledRenderer.generated = true;
			}
			else if(GUILayout.Button("Delete TileMap"))
			{
				var tileMapTiledRenderer = (TileMapTiledRenderer)target;

				DeleteChildObjects(tileMapTiledRenderer.transform);
				tileMapTiledRenderer.generated = false;
			}
		}

		private void DeleteChildObjects(Transform parent)
		{
			var childObjects = parent.Cast<Transform>().ToList();

			foreach(Transform child in childObjects)
			{
				Object.DestroyImmediate(child.gameObject);
			}
		}
	}
}
