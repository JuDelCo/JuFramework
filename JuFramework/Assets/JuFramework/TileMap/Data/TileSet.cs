using System.Collections.Generic;
using JuFramework.TileMapData;

namespace JuFramework
{
	public class TileSet
	{
		public string name;
		public uint firstGid;
		public string texturePath;
		public int textureWidth;
		public int textureHeight;
		public int textureColumns;
		public Texture texture;
		public int margin;
		public int spacing;
		public int tileCount;
		public int tileWidth;
		public int tileHeight;
		public int tileOffsetX;
		public int tileOffsetY;
		public readonly PropertyCollection properties = new PropertyCollection();
		public readonly Dictionary<uint, AnimationCollection> tileAnimations = new Dictionary<uint, AnimationCollection>();
		public readonly Dictionary<uint, PropertyCollection> tileProperties = new Dictionary<uint, PropertyCollection>();
	}
}
