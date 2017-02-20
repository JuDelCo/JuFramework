
namespace JuFramework.TileMapData
{
	public class TileLayer : Layer
	{
		public readonly Tile[,] tiles;

		public TileLayer(int width, int height)
		{
			tiles = new Tile[width, height];
		}
	}
}
