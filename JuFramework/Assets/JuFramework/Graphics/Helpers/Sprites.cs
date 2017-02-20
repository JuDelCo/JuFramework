
// TODO: Graphics texture flip methods

namespace JuFramework.Drawing
{
	public static class Sprites
	{
		private static Mesh textureMeshCache = Core.resource.CreateMesh(true);

		private static Vector3f[] vertices = new Vector3f[4];
		private static Vector2f[] uv = new Vector2f[4];
		private static int[] indices = new int[6] { 0, 1, 2, 2, 3, 0 };

		private static void DrawMeshRaw(Vector3f[] vertices, Vector2f[] uv, Color color)
		{
			var colors = new Color[vertices.Length];
			for (int i = 0; i < vertices.Length; colors[i++] = color);

			DrawMeshRaw(vertices, uv, colors);
		}

		private static void DrawMeshRaw(Vector3f[] vertices, Vector2f[] uv, Color[] colors)
		{
			textureMeshCache.Clear();
			textureMeshCache.SetVertices(vertices);
			textureMeshCache.SetTextureCoordinates(uv);
			textureMeshCache.SetColors(colors);
			textureMeshCache.SetIndices(indices, Mesh.Topology.Triangles);
			textureMeshCache.ApplyMeshData();

			Core.graphics.DrawMesh(textureMeshCache);
		}

		//-----------------------------------------------------------------------

		public static void DrawQuad(Vector2i size, Vector2f position)
		{
			DrawQuad(size, position, Color.white);
		}

		public static void DrawQuad(Vector2i size, Vector2f position, Color color)
		{
			vertices[0] = position;
			vertices[1] = position + new Vector2f(size.x, 0);
			vertices[2] = position + new Vector2f(size.x, size.y);
			vertices[3] = position + new Vector2f(0, size.y);

			uv[0] = new Vector2f(0f, 0f);
			uv[1] = new Vector2f(1f, 0f);
			uv[2] = new Vector2f(1f, 1f);
			uv[3] = new Vector2f(0f, 1f);

			DrawMeshRaw(vertices, uv, color);
		}

		public static void DrawQuad(Vector2i size, Vector2f position, IntRect source, Color color)
		{
			float coordLeft = (float)source.left / size.x;
			float coordRight = (float)source.right / size.x;
			float coordBottom = (float)source.bottom / size.y;
			float coordTop = (float)source.top / size.y;

			vertices[0] = position;
			vertices[1] = position + new Vector2f(source.width, 0);
			vertices[2] = position + new Vector2f(source.width, source.height);
			vertices[3] = position + new Vector2f(0, source.height);

			uv[0] = new Vector2f(coordLeft, coordTop);
			uv[1] = new Vector2f(coordRight, coordTop);
			uv[2] = new Vector2f(coordRight, coordBottom);
			uv[3] = new Vector2f(coordLeft, coordBottom);

			DrawMeshRaw(vertices, uv, color);
		}

		//public static void DrawQuad(Vector2i size, Vector2f position, IntRect source, Color color, Vector2f origin);

		public static void DrawQuad(Vector2i size, IntRect destination)
		{
			DrawQuad(size, destination, Color.white);
		}

		public static void DrawQuad(Vector2i size, IntRect destination, Color color)
		{
			vertices[0] = new Vector2f(destination.left, destination.top);
			vertices[1] = new Vector2f(destination.right, destination.top);
			vertices[2] = new Vector2f(destination.right, destination.bottom);
			vertices[3] = new Vector2f(destination.left, destination.bottom);

			uv[0] = new Vector2f(0f, 0f);
			uv[1] = new Vector2f(1f, 0f);
			uv[2] = new Vector2f(1f, 1f);
			uv[3] = new Vector2f(0f, 1f);

			DrawMeshRaw(vertices, uv, color);
		}

		public static void DrawQuad(Vector2i size, IntRect destination, IntRect source, Color color)
		{
			float coordLeft = (float)source.left / size.x;
			float coordRight = (float)source.right / size.x;
			float coordBottom = (float)source.bottom / size.y;
			float coordTop = (float)source.top / size.y;

			vertices[0] = new Vector2f(destination.left, destination.top);
			vertices[1] = new Vector2f(destination.right, destination.top);
			vertices[2] = new Vector2f(destination.right, destination.bottom);
			vertices[3] = new Vector2f(destination.left, destination.bottom);

			uv[0] = new Vector2f(coordLeft, coordTop);
			uv[1] = new Vector2f(coordRight, coordTop);
			uv[2] = new Vector2f(coordRight, coordBottom);
			uv[3] = new Vector2f(coordLeft, coordBottom);

			DrawMeshRaw(vertices, uv, color);
		}

		//public void DrawQuad(Vector2i size, IntRect destination, IntRect source, Color color, Vector2f origin);
	}
}
