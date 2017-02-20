
// TODO: IMPORTANT: Optimize (GC Allocations) (start with circle)

// TODO: Better lines
//		http://www.codeproject.com/Articles/199525/Drawing-nearly-perfect-D-line-segments-in-OpenGL
//		https://www.mapbox.com/blog/drawing-antialiased-lines

namespace JuFramework
{
	public static partial class Shapes
	{
		private static Mesh meshCache = Core.resource.CreateMesh(true);

		private static Vector3f[] lineVerticesCache = new Vector3f[2];
		private static int[] lineIndicesCache = new int[2] { 0, 1 };
		private static Color[] lineColorsCache = new Color[2];

		private static void RebuildMesh(Vector3f[] vertices, int[] indices, Color[] colors, Mesh.Topology topology)
		{
			meshCache.Clear();
			meshCache.SetVertices(vertices);
			meshCache.SetColors(colors);
			meshCache.SetIndices(indices, topology);
			meshCache.ApplyMeshData();
		}

		//-----------------------------------------------------------------------

		public static void DrawPixel(Vector2i position, Color color)
		{
			DrawPoint((Vector3i)position, color);
		}

		public static void DrawPoint(Vector2f position, Color color)
		{
			DrawPoint((Vector3f)position, color);
		}

		public static void DrawPoint(Vector3f position, Color color)
		{
			var vertices = new Vector3f[4]
			{
				position,
				position + new Vector3f(1, 0, 0),
				position + new Vector3f(1, 1, 0),
				position + new Vector3f(0, 1, 0)
			};
			var indices = new int[6] { 0, 1, 2, 2, 3, 0 };
			var colors = new Color[4] { color, color, color, color };

			RebuildMesh(vertices, indices, colors, Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawLine(Vector2f start, Vector2f end, Color color)
		{
			DrawLine((Vector3f)start, (Vector3f)end, color);
		}

		public static void DrawLine(Vector3f start, Vector3f end, Color color)
		{
			lineVerticesCache[0] = start;
			lineVerticesCache[1] = end;
			lineColorsCache[0] = color;
			lineColorsCache[1] = color;

			RebuildMesh(lineVerticesCache, lineIndicesCache, lineColorsCache, Mesh.Topology.Lines);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawCircle(Vector2f center, float radius, Color color, int circleSegments = 36)
		{
			var vertices = new System.Collections.Generic.List<Vector3f>();
			var indices = new System.Collections.Generic.List<int>();
			var colors = new System.Collections.Generic.List<Color>();
			var percentage = Math.Round(360f / circleSegments);

			for (int i = 0; i < 360; i += percentage)
			{
				vertices.Add(new Vector2f(center.x + Math.Sin(Math.Deg2Rad * i) * radius, center.y + Math.Cos(Math.Deg2Rad * i) * radius));
				vertices.Add(new Vector2f(center.x + Math.Sin(Math.Deg2Rad * (i + percentage)) * radius, center.y + Math.Cos(Math.Deg2Rad * (i + percentage)) * radius));

				indices.Add(Math.Round(i / percentage) * 2);
				indices.Add(Math.Round(i / percentage) * 2 + 1);

				colors.Add(color);
				colors.Add(color);
			}

			RebuildMesh(vertices.ToArray(), indices.ToArray(), colors.ToArray(), Mesh.Topology.Lines);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawCircleFill(Vector2f center, float radius, Color color, int circleSegments = 36)
		{
			var vertices = new System.Collections.Generic.List<Vector3f>();
			var indices = new System.Collections.Generic.List<int>();
			var colors = new System.Collections.Generic.List<Color>();
			var percentage = Math.Round(360f / circleSegments);

			for (int i = 0; i < 360; i += percentage)
			{
				vertices.Add(center);
				vertices.Add(new Vector2f(center.x + Math.Sin(Math.Deg2Rad * i) * radius, center.y + Math.Cos(Math.Deg2Rad * i) * radius));
				vertices.Add(new Vector2f(center.x + Math.Sin(Math.Deg2Rad * (i + percentage)) * radius, center.y + Math.Cos(Math.Deg2Rad * (i + percentage)) * radius));

				indices.Add(Math.Round(i / percentage) * 3);
				indices.Add(Math.Round(i / percentage) * 3 + 1);
				indices.Add(Math.Round(i / percentage) * 3 + 2);

				colors.Add(color);
				colors.Add(color);
				colors.Add(color);
			}

			RebuildMesh(vertices.ToArray(), indices.ToArray(), colors.ToArray(), Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawRectangle(Vector2f position, float width, float height, Color color)
		{
			var vertices = new Vector3f[8]
			{
				position,
				position + new Vector2f(width, 0),
				position + new Vector2f(width, 0),
				position + new Vector2f(width, height),
				position + new Vector2f(width, height),
				position + new Vector2f(0, height),
				position + new Vector2f(0, height),
				position
			};
			var indices = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
			var colors = new Color[8] { color, color, color, color, color, color, color, color };

			RebuildMesh(vertices, indices, colors, Mesh.Topology.Lines);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawRectangle(FloatRect rect, Color color)
		{
			DrawRectangle(rect.position, rect.width, rect.height, color);
		}

		public static void DrawRectangleFill(Vector2f position, float width, float height, Color color)
		{
			var vertices = new Vector3f[4]
			{
				position,
				position + new Vector2f(width, 0),
				position + new Vector2f(width, height),
				position + new Vector2f(0, height)
			};
			var indices = new int[6] { 0, 1, 2, 2, 3, 0 };
			var colors = new Color[4] { color, color, color, color };

			RebuildMesh(vertices, indices, colors, Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawRectangleFill(FloatRect rect, Color color)
		{
			DrawRectangleFill(rect.position, rect.width, rect.height, color);
		}

		public static void DrawTriangle(Vector2f v1, Vector2f v2, Vector2f v3, Color color)
		{
			DrawTriangle((Vector3f)v1, (Vector3f)v2, (Vector3f)v3, color);
		}

		public static void DrawTriangle(Vector3f v1, Vector3f v2, Vector3f v3, Color color)
		{
			var vertices = new Vector3f[6]
			{
				v1, v2,
				v2, v3,
				v3, v1,
			};
			var indices = new int[6] { 0, 1, 2, 3, 4, 5 };
			var colors = new Color[6] { color, color, color, color, color, color };

			RebuildMesh(vertices, indices, colors, Mesh.Topology.Lines);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawTriangleFill(Vector2f v1, Vector2f v2, Vector2f v3, Color color)
		{
			DrawTriangleFill((Vector3f)v1, (Vector3f)v2, (Vector3f)v3, color);
		}

		public static void DrawTriangleFill(Vector3f v1, Vector3f v2, Vector3f v3, Color color)
		{
			var vertices = new Vector3f[3]
			{
				v1, v2, v3,
			};
			var indices = new int[3] { 0, 1, 2 };
			var colors = new Color[3] { color, color, color };

			RebuildMesh(vertices, indices, colors, Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawPolygon(Vector2f[] points, Color color)
		{
			var vertices = new System.Collections.Generic.List<Vector3f>();
			var indices = new System.Collections.Generic.List<int>();
			var colors = new System.Collections.Generic.List<Color>();

			for (int i = 1; i < points.Length; i+=2)
			{
				vertices.Add(points[0]);
				vertices.Add(points[i]);
				vertices.Add(points[i+1]);

				indices.Add(0);
				indices.Add(Math.Round((i - 1) / 2) * 3 + 1);
				indices.Add(Math.Round((i - 1) / 2) * 3 + 2);

				colors.Add(color);
				colors.Add(color);
				colors.Add(color);
			}

			RebuildMesh(vertices.ToArray(), indices.ToArray(), colors.ToArray(), Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}
	}
}
