
// TODO: Graphics texture rotation, scale, flip methods

namespace JuFramework
{
	public abstract partial class Graphics
	{
		private Mesh textureMeshCache;
		private Material textureMaterialCache;

		private Vector3f[] vertices = new Vector3f[4];
		private Vector2f[] uv = new Vector2f[4];
		private int[] indices;

		public void DrawTexture(Texture texture, Vector2f position)
		{
			DrawTexture(texture, position, Color.white);
		}

		public void DrawTexture(Texture texture, Vector2f position, Color color)
		{
			vertices[0] = position;
			vertices[1] = position + new Vector2f(texture.GetWidth(), 0);
			vertices[2] = position + new Vector2f(texture.GetWidth(), texture.GetHeight());
			vertices[3] = position + new Vector2f(0, texture.GetHeight());

			uv[0] = new Vector2f(0f, 0f);
			uv[1] = new Vector2f(1f, 0f);
			uv[2] = new Vector2f(1f, 1f);
			uv[3] = new Vector2f(0f, 1f);

			DrawTextureRaw(texture, vertices, uv, color);
		}

		public void DrawTexture(Texture texture, Vector2f position, IntRect source, Color color)
		{
			float coordLeft = (float)source.left / texture.GetWidth();
			float coordRight = (float)source.right / texture.GetWidth();
			float coordBottom = (float)source.bottom / texture.GetHeight();
			float coordTop = (float)source.top / texture.GetHeight();

			vertices[0] = position;
			vertices[1] = position + new Vector2f(source.width, 0);
			vertices[2] = position + new Vector2f(source.width, source.height);
			vertices[3] = position + new Vector2f(0, source.height);

			uv[0] = new Vector2f(coordLeft, coordTop);
			uv[1] = new Vector2f(coordRight, coordTop);
			uv[2] = new Vector2f(coordRight, coordBottom);
			uv[3] = new Vector2f(coordLeft, coordBottom);

			DrawTextureRaw(texture, vertices, uv, color);
		}

		//public void DrawTexture(Texture texture, Vector2f position, IntRect source, Color color, float rotation, Vector2f scale, float layerDepth);
		//public void DrawTexture(Texture texture, Vector2f position, IntRect source, Color color, float rotation, Vector2f origin, Vector2f scale, float layerDepth);

		public void DrawTexture(Texture texture, IntRect destination)
		{
			DrawTexture(texture, destination, Color.white);
		}

		public void DrawTexture(Texture texture, IntRect destination, Color color)
		{
			vertices[0] = new Vector2f(destination.left, destination.top);
			vertices[1] = new Vector2f(destination.right, destination.top);
			vertices[2] = new Vector2f(destination.right, destination.bottom);
			vertices[3] = new Vector2f(destination.left, destination.bottom);

			uv[0] = new Vector2f(0f, 0f);
			uv[1] = new Vector2f(1f, 0f);
			uv[2] = new Vector2f(1f, 1f);
			uv[3] = new Vector2f(0f, 1f);

			DrawTextureRaw(texture, vertices, uv, color);
		}

		public void DrawTexture(Texture texture, IntRect destination, IntRect source, Color color)
		{
			float coordLeft = (float)source.left / texture.GetWidth();
			float coordRight = (float)source.right / texture.GetWidth();
			float coordBottom = (float)source.bottom / texture.GetHeight();
			float coordTop = (float)source.top / texture.GetHeight();

			vertices[0] = new Vector2f(destination.left, destination.top);
			vertices[1] = new Vector2f(destination.right, destination.top);
			vertices[2] = new Vector2f(destination.right, destination.bottom);
			vertices[3] = new Vector2f(destination.left, destination.bottom);

			uv[0] = new Vector2f(coordLeft, coordTop);
			uv[1] = new Vector2f(coordRight, coordTop);
			uv[2] = new Vector2f(coordRight, coordBottom);
			uv[3] = new Vector2f(coordLeft, coordBottom);

			DrawTextureRaw(texture, vertices, uv, color);
		}

		//public void DrawTexture(Texture texture, IntRect destination, IntRect source, Color color, float rotation, float layerDepth);
		//public void DrawTexture(Texture texture, IntRect destination, IntRect source, Color color, float rotation, Vector2f origin, float layerDepth);

		public void DrawTextureRaw(Texture texture, Vector3f[] vertices, Vector2f[] uv, Color color)
		{
			var colors = new Color[vertices.Length];
			for (int i = 0; i < vertices.Length; colors[i++] = color);

			DrawTextureRaw(texture, vertices, uv, colors);
		}

		public void DrawTextureRaw(Texture texture, Vector3f[] vertices, Vector2f[] uv, Color[] colors)
		{
			if(indices == null)
			{
				indices = new int[6] { 0, 1, 2, 2, 3, 0 };
			}

			if(textureMeshCache == null)
			{
				this.textureMeshCache = Core.resource.CreateMesh(true);
			}

			textureMeshCache.Clear();
			textureMeshCache.SetVertices(vertices);
			textureMeshCache.SetTextureCoordinates(uv);
			textureMeshCache.SetColors(colors);
			textureMeshCache.SetIndices(indices, Mesh.Topology.Triangles);
			textureMeshCache.ApplyMeshData();

			if(textureMaterialCache == null)
			{
				textureMaterialCache = Core.resource.CreateMaterial("Sprites/Default");
			}

			textureMaterialCache.Set("_MainTex", texture);
			textureMaterialCache.SetPass(0);

			DrawMesh(textureMeshCache);
		}
	}
}
