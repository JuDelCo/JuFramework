using IntPtr = System.IntPtr;

namespace JuFramework
{
	public partial class UnityTexture : Texture
	{
		private UnityEngine.Texture2D unityTexture;

		private Color[] colorsCache;

		public UnityTexture(UnityEngine.Texture2D texture)
		{
			if(texture != null)
			{
				this.unityTexture = texture;

				colorsCache = new Color[GetWidth() * GetHeight()];
			}
			else
			{
				CreateEmptyTexture(1, 1);
			}
		}

		public UnityTexture(int width, int height)
		{
			CreateEmptyTexture(width, height);
		}

		private void CreateEmptyTexture(int width, int height)
		{
			unityTexture = new UnityEngine.Texture2D(width, height, UnityEngine.TextureFormat.RGBA32, false, /* Linear | sRGB */ false);
			unityTexture.wrapMode = UnityEngine.TextureWrapMode.Repeat;
			unityTexture.filterMode = UnityEngine.FilterMode.Point;
			unityTexture.anisoLevel = 1; // No filtering

			colorsCache = new Color[GetWidth() * GetHeight()];
		}

		public override void Resize(int width, int height)
		{
			unityTexture.Resize(width, height, UnityEngine.TextureFormat.RGBA32, false);

			colorsCache = new Color[width * height];
		}

		public override int GetWidth()
		{
			return unityTexture.width;
		}

		public override int GetHeight()
		{
			return unityTexture.height;
		}

		public override Vector2i GetSize()
		{
			return new Vector2i(unityTexture.width, unityTexture.height);
		}

		public override IntPtr GetNativePtr()
		{
			return unityTexture.GetNativeTexturePtr();
		}

		public override Color GetPixel(int x, int y)
		{
			return (Color)unityTexture.GetPixel(x, y);
		}

		public override Color[] GetPixels()
		{
			for (int y = 0; y < unityTexture.height; ++y)
			{
				for (int x = 0; x < unityTexture.width; ++x)
				{
					colorsCache[(y * unityTexture.height) + x] = (Color)unityTexture.GetPixel(x, y);
				}
			}

			return colorsCache;
		}

		public override Color[] GetPixels(Vector2i position, Vector2i size)
		{
			var colors = new Color[size.x * size.y];

			for (int y = position.y; y < (position.y + size.y); ++y)
			{
				for (int x = position.x; x < (position.x + size.x); ++x)
				{
					colors[((y - position.y) * size.y) + (x - position.x)] = (Color)unityTexture.GetPixel(x, y);
				}
			}

			return colors;
		}

		public override void SetPixel(int x, int y, Color color)
		{
			unityTexture.SetPixel(x, y, color);
		}

		public override void SetPixels(Color[] pixels)
		{
			var unityPixels = new UnityEngine.Color[pixels.Length];
			Color pixel;

			for (int i = 0; i < pixels.Length; ++i)
			{
				pixel = pixels[i];
				unityPixels[i].r = pixel.r;
				unityPixels[i].g = pixel.g;
				unityPixels[i].b = pixel.b;
				unityPixels[i].a = pixel.a;
			}

			unityTexture.SetPixels(unityPixels);
		}

		public override void SetPixels(Vector2i position, Vector2i size, Color[] pixels)
		{
			for (int y = position.y; y < (position.y + size.y); ++y)
			{
				for (int x = position.x; x < (position.x + size.x); ++x)
				{
					unityTexture.SetPixel(x, y, pixels[((y - position.y) * size.y) + (x - position.x)]);
				}
			}
		}

		public override void Apply()
		{
			unityTexture.Apply();
		}
	}
}
