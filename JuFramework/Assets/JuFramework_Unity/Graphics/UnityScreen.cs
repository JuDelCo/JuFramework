
// TODO: Orientation / Autorotate

namespace JuFramework
{
	public class UnityScreen : Screen
	{
		private bool windowHasFocus = true;

		public override Vector2i halfSize
		{
			get
			{
				return (size / 2);
			}
		}

		public override Vector2i size
		{
			get
			{
				return new Vector2i(width, height);
			}
		}

		public override int width
		{
			get
			{
				return UnityEngine.Screen.width;
			}
			set
			{
				UnityEngine.Screen.SetResolution(value, height, isFullscreen);
			}
		}

		public override int height
		{
			get
			{
				return UnityEngine.Screen.height;
			}
			set
			{
				UnityEngine.Screen.SetResolution(width, value, isFullscreen);
			}
		}

		public override bool isFullscreen
		{
			get
			{
				return UnityEngine.Screen.fullScreen;
			}
			set
			{
				UnityEngine.Screen.fullScreen = value;
			}
		}

		public override bool hasFocus
		{
			get
			{
				return windowHasFocus;
			}
			set
			{
				windowHasFocus = value;
			}
		}
	}
}
