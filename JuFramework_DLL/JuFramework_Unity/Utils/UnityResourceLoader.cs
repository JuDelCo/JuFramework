
namespace JuFramework
{
	public partial class UnityResourceLoader : ResourceLoader
	{
		public override Mesh CreateMesh(bool isDynamic = false)
		{
			return new UnityMesh(isDynamic);
		}

		public override Mesh LoadMesh(string path)
		{
			return new UnityMesh(UnityEngine.Resources.Load(path, typeof(UnityEngine.Mesh)) as UnityEngine.Mesh);
		}

		public override Shader LoadShader(string name)
		{
			return new UnityShader(UnityEngine.Shader.Find(name));
		}

		public override Material CreateMaterial(Shader shader)
		{
			var material = new UnityMaterial(shader);

			if(((UnityShader)shader).IsFallbackShader())
			{
				material.Set("_Color", Color.magenta);
			}

			return material;
		}

		public override Material CreateMaterial(string shaderName)
		{
			return CreateMaterial(LoadShader(shaderName));
		}

		public override Texture CreateTexture(int width, int height)
		{
			return new UnityTexture(width, height);
		}

		public override Texture LoadTexture(string path)
		{
			return new UnityTexture(UnityEngine.Resources.Load(path, typeof(UnityEngine.Texture2D)) as UnityEngine.Texture2D);
		}

		public override Binary LoadFile(string path)
		{
			return new Binary(UnityEngine.Resources.Load(path, typeof(UnityEngine.TextAsset)) as UnityEngine.TextAsset);
		}

		public override Binary LoadText(string path)
		{
			return new Binary(UnityEngine.Resources.Load(path, typeof(UnityEngine.TextAsset)) as UnityEngine.TextAsset, true);
		}
	}
}
