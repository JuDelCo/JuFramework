using IntPtr = System.IntPtr;

// TODO: https://docs.unity3d.com/ScriptReference/Texture.html
//		void GenerateMipMaps();
//		void SetTextureWrapping(const TextureWrappingMode mode);
//		void SetTextureWrappingBorder(const vec4 color);
//		void SetTextureMinFiltering(const TextureFilteringMode mode);
//		void SetTextureMagFiltering(const TextureFilteringMode mode);

namespace JuFramework
{
	public abstract class Texture
	{
		public abstract void Resize(int width, int height);
		public abstract int GetWidth();
		public abstract int GetHeight();
		public abstract Vector2i GetSize();
		public abstract IntPtr GetNativePtr();
		public abstract Color GetPixel(int x, int y);
		public abstract Color[] GetPixels();
		public abstract Color[] GetPixels(Vector2i position, Vector2i size);
		public abstract void SetPixel(int x, int y, Color color);
		public abstract void SetPixels(Color[] pixels);
		public abstract void SetPixels(Vector2i position, Vector2i size, Color[] pixels);
		public abstract void Apply();
	}
}
