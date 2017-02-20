using UnityEngine;

namespace JuFramework.TileMapTiledImporter
{
	public enum TiledFileObjectType
	{
		TileMap,
		TileSet
	}

	public class TiledFileObject : ScriptableObject
	{
		[HideInInspector]
		public string data;

		public TiledFileObjectType type;
	}
}
