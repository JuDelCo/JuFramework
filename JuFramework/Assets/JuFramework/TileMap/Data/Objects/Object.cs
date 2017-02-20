
namespace JuFramework.TileMapData
{
	public abstract class Object
	{
		public uint id;
		public string name;
		public string type;
		public float x;
		public float y;
		public float width;
		public float height;
		public float rotation;
		public bool visible;
		public readonly PropertyCollection properties = new PropertyCollection();
	}
}
