using System.Collections.Generic;
using JuFramework.TileMapData;

namespace JuFramework
{
	public class TileMap
	{
		public int width;
		public int height;
		public int tileWidth;
		public int tileHeight;
		public Orientation orientation;
		public RenderOrder renderOrder;
		public Color backgroundColor;
		public readonly PropertyCollection properties = new PropertyCollection();
		public readonly List<TileSet> tileSets = new List<TileSet>();
		public readonly List<Layer> layers = new List<Layer>();
		public readonly List<TileLayer> tileLayers = new List<TileLayer>();
		public readonly List<ObjectGroup> objectGroups = new List<ObjectGroup>();
	}
}
