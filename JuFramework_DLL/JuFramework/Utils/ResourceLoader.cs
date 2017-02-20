
namespace JuFramework
{
	public abstract class ResourceLoader
	{
		public abstract Mesh CreateMesh(bool isDynamic = false);
		public abstract Mesh LoadMesh(string path);
		public abstract Shader LoadShader(string name);
		public abstract Material CreateMaterial(Shader shader);
		public abstract Material CreateMaterial(string shaderName);
		public abstract Texture CreateTexture(int width, int height);
		public abstract Texture LoadTexture(string path);
		public abstract Binary LoadFile(string path);
		public abstract Binary LoadText(string path);
	}
}
