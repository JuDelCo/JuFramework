
namespace JuFramework
{
	public abstract class Screen
	{
		public abstract Vector2i halfSize { get; }
		public abstract Vector2i size { get; }
		public abstract int width { get; set; }
		public abstract int height { get; set; }
		public abstract bool isFullscreen { get; set; }
		public abstract bool hasFocus { get; set; }
	}
}
