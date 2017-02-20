using UnityEngine;
using UnityEditor;
using System.IO;

namespace JuFramework.TileMapTiledImporter
{
	public class UnityTiledAssetImporter : AssetPostprocessor
	{
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			foreach (string asset in deletedAssets)
			{
				if(IsTileMapFile(asset) || IsTileSetFile(asset))
				{
					AssetDatabase.DeleteAsset(ConvertToInternalPath(asset));
				}
			}

			for (int i = 0; i < movedAssets.Length; ++i)
			{
				AssetDatabase.MoveAsset(ConvertToInternalPath(movedFromAssetPaths[i]), ConvertToInternalPath(movedAssets[i]));
			}

			foreach(string asset in importedAssets)
			{
				if(IsTileMapFile(asset) || IsTileSetFile(asset))
				{
					ImportAsset(asset);
				}
			}

			AssetDatabase.SaveAssets();
		}

		private static bool IsTileMapFile(string asset)
		{
			return asset.EndsWith(".tmx", System.StringComparison.OrdinalIgnoreCase);
		}

		private static bool IsTileSetFile(string asset)
		{
			return asset.EndsWith(".tsx", System.StringComparison.OrdinalIgnoreCase);
		}

		private static string ConvertToInternalPath(string asset)
		{
			string left = asset.Substring(0, asset.Length - 4);

			if(IsTileMapFile(asset))
			{
				left += ".tilemap";
			}
			else if(IsTileSetFile(asset))
			{
				left += ".tileset";
			}

			return left + ".asset";
		}

		private static void ImportAsset(string asset)
		{
			string newPath = ConvertToInternalPath(asset);

			var assetFile = AssetDatabase.LoadAssetAtPath(newPath, typeof(TiledFileObject)) as TiledFileObject;

			bool loaded = (assetFile != null);

			if(!loaded)
			{
				assetFile = ScriptableObject.CreateInstance<TiledFileObject>();
			}
			else
			{
				// return; // When the original file is changed, changes are ignored
			}

			assetFile.data = File.ReadAllText(asset);

			if(IsTileMapFile(asset))
			{
				assetFile.type = TiledFileObjectType.TileMap;
			}
			else if(IsTileSetFile(asset))
			{
				assetFile.type = TiledFileObjectType.TileSet;
			}

			if(!loaded)
			{
				AssetDatabase.CreateAsset(assetFile, newPath);
			}
		}
	}
}
