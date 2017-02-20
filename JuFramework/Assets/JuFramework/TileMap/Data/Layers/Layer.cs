
namespace JuFramework.TileMapData
{
	public abstract class Layer
	{
		public TileMap map;
		public string name;
		public int width;
		public int height;
		public bool visible;
		public float opacity;
		public int offsetX;
		public int offsetY;
		public readonly PropertyCollection properties = new PropertyCollection();
	}
}
