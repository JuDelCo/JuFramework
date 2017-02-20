
namespace JuFramework.TileMapData
{
	public struct AnimationFrame
	{
		public readonly uint gid;
		public readonly uint duration;

		public AnimationFrame(uint gid, uint duration)
		{
			this.gid = gid;
			this.duration = duration;
		}
	}
}
