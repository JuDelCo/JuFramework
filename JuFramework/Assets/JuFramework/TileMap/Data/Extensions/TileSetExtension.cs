
namespace JuFramework.TileMapData
{
	public static class TileSetExtension
	{
		public static bool ContainsTile(this TileSet tileSet, uint gid)
		{
			var id = (gid - tileSet.firstGid);

			return ((id >= 0) && (id < tileSet.tileCount));
		}

		public static AnimationCollection GetTileAnimation(this TileSet tileSet, uint gid)
		{
			AnimationCollection animation;

			if (!tileSet.tileAnimations.TryGetValue(gid, out animation))
			{
				animation = new AnimationCollection();
			}

			return animation;
		}

		public static PropertyCollection GetTileProperties(this TileSet tileSet, uint gid)
		{
			PropertyCollection properties;

			if (!tileSet.tileProperties.TryGetValue(gid, out properties))
			{
				properties = new PropertyCollection();
			}

			return properties;
		}
	}
}
