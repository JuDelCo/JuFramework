using System.Collections.Generic;

// TODO: IMPORTANT: Optimize (GC Allocations)

// TODO: duplicate some methods to allow drawing and creating standalone meshes (only solid versions) ?

// TODO: duplicate some methods to draw wireframe alone and solid versions of some primitives (GL.Lines vs Triangles)

// TODO: cubicmap

// TODO: polysphere

// TODO: billboards
//			void DrawBillboard(Camera camera, Texture2D texture, Vector3 center, float size, Color tint)
//			void DrawBillboardRec(Camera camera, Texture2D texture, Rectangle sourceRec, Vector3 center, float size, Color tint)

// TODO: heightmaps
//			static Mesh GenMeshHeightmap(Image heightmap, Vector3 size)

// TODO: Add texture coordinates to: Cube, Cylinder, Billboard, HeightMap

namespace JuFramework
{
	public static partial class Models
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

		public static void DrawLine(Vector3f start, Vector3f end, Color color)
		{
			lineVerticesCache[0] = start;
			lineVerticesCache[1] = end;
			lineColorsCache[0] = color;
			lineColorsCache[1] = color;

			RebuildMesh(lineVerticesCache, lineIndicesCache, lineColorsCache, Mesh.Topology.Lines);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawRay(Ray ray, Color color, float distance = 1000f)
		{
			lineVerticesCache[0] = ray.position;
			lineVerticesCache[1] = (ray.position + (ray.direction * distance));
			lineColorsCache[0] = color;
			lineColorsCache[1] = color;

			RebuildMesh(lineVerticesCache, lineIndicesCache, lineColorsCache, Mesh.Topology.Lines);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawCircle(Vector3f center, float radius, Color color, int circleSegments = 36)
		{
			var vertices = new List<Vector3f>();
			var indices = new List<int>();
			var colors = new List<Color>();
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

		public static void DrawAxisGizmo(Transform2D transform)
		{
			float length = 10.0f;

			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Right() * length, Color.red);
			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Up() * length, Color.green);
		}

		public static void DrawAxisGizmo(Transform3D transform)
		{
			float length = 10.0f;

			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Forward() * length, Color.blue);
			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Right() * length, Color.red);
			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Up() * length, Color.green);
		}

		//-----------------------------------------------------------------------

		public static void DrawCube(Vector3f position, Vector3f size, Color color)
		{
			var vertices = new List<Vector3f>();
			var indices = new List<int>();
			var colors = new List<Color>();

			for (int i = 0; i < 36; ++i)
			{
				indices.Add(i);
				colors.Add(color);
			}

			// Front --------------------------------------------------
			vertices.Add(new Vector3f(-0.5f, -0.5f,  0.5f) * size + position); // Bottom Left
			vertices.Add(new Vector3f( 0.5f, -0.5f,  0.5f) * size + position); // Bottom Right
			vertices.Add(new Vector3f(-0.5f,  0.5f,  0.5f) * size + position); // Top Left

			vertices.Add(new Vector3f( 0.5f,  0.5f,  0.5f) * size + position); // Top Right
			vertices.Add(new Vector3f(-0.5f,  0.5f,  0.5f) * size + position); // Top Left
			vertices.Add(new Vector3f( 0.5f, -0.5f,  0.5f) * size + position); // Bottom Right

			// Back ---------------------------------------------------
			vertices.Add(new Vector3f(-0.5f, -0.5f, -0.5f) * size + position); // Bottom Left
			vertices.Add(new Vector3f(-0.5f,  0.5f, -0.5f) * size + position); // Top Left
			vertices.Add(new Vector3f( 0.5f, -0.5f, -0.5f) * size + position); // Bottom Right

			vertices.Add(new Vector3f( 0.5f,  0.5f, -0.5f) * size + position); // Top Right
			vertices.Add(new Vector3f( 0.5f, -0.5f, -0.5f) * size + position); // Bottom Right
			vertices.Add(new Vector3f(-0.5f,  0.5f, -0.5f) * size + position); // Top Left

			// Top ----------------------------------------------------
			vertices.Add(new Vector3f(-0.5f,  0.5f, -0.5f) * size + position); // Top Left
			vertices.Add(new Vector3f(-0.5f,  0.5f,  0.5f) * size + position); // Bottom Left
			vertices.Add(new Vector3f( 0.5f,  0.5f,  0.5f) * size + position); // Bottom Right

			vertices.Add(new Vector3f( 0.5f,  0.5f, -0.5f) * size + position); // Top Right
			vertices.Add(new Vector3f(-0.5f,  0.5f, -0.5f) * size + position); // Top Left
			vertices.Add(new Vector3f( 0.5f,  0.5f,  0.5f) * size + position); // Bottom Right

			// Bottom -------------------------------------------------
			vertices.Add(new Vector3f(-0.5f, -0.5f, -0.5f) * size + position); // Top Left
			vertices.Add(new Vector3f( 0.5f, -0.5f,  0.5f) * size + position); // Bottom Right
			vertices.Add(new Vector3f(-0.5f, -0.5f,  0.5f) * size + position); // Bottom Left

			vertices.Add(new Vector3f( 0.5f, -0.5f, -0.5f) * size + position); // Top Right
			vertices.Add(new Vector3f( 0.5f, -0.5f,  0.5f) * size + position); // Bottom Right
			vertices.Add(new Vector3f(-0.5f, -0.5f, -0.5f) * size + position); // Top Left

			// Right --------------------------------------------------
			vertices.Add(new Vector3f( 0.5f, -0.5f, -0.5f) * size + position); // Bottom Right
			vertices.Add(new Vector3f( 0.5f,  0.5f, -0.5f) * size + position); // Top Right
			vertices.Add(new Vector3f( 0.5f,  0.5f,  0.5f) * size + position); // Top Left

			vertices.Add(new Vector3f( 0.5f, -0.5f,  0.5f) * size + position); // Bottom Left
			vertices.Add(new Vector3f( 0.5f, -0.5f, -0.5f) * size + position); // Bottom Right
			vertices.Add(new Vector3f( 0.5f,  0.5f,  0.5f) * size + position); // Top Left

			// Left ---------------------------------------------------
			vertices.Add(new Vector3f(-0.5f, -0.5f, -0.5f) * size + position); // Bottom Right
			vertices.Add(new Vector3f(-0.5f,  0.5f,  0.5f) * size + position); // Top Left
			vertices.Add(new Vector3f(-0.5f,  0.5f, -0.5f) * size + position); // Top Right

			vertices.Add(new Vector3f(-0.5f, -0.5f,  0.5f) * size + position); // Bottom Left
			vertices.Add(new Vector3f(-0.5f,  0.5f,  0.5f) * size + position); // Top Left
			vertices.Add(new Vector3f(-0.5f, -0.5f, -0.5f) * size + position); // Bottom Right

			RebuildMesh(vertices.ToArray(), indices.ToArray(), colors.ToArray(), Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawCube(Box box, Color color)
		{
			DrawCube(box.center, box.size, color);
		}

		public static void DrawSphere(Vector3f position, float radius, Color color, int rings = 8, int slices = 8)
		{
			var numVertices = 6 * slices * (rings + 4);
			var vertices = new List<Vector3f>(numVertices);
			var indices = new List<int>(numVertices);
			var colors = new List<Color>(numVertices);

			for (int i = 0; i < 6 * slices * (rings + 4); ++i)
			{
				indices.Add(i);
				colors.Add(color);
			}

			var sliceStep = (360f / slices) * Math.Deg2Rad;
			var ringStep = 180f / (rings + 1);

			for (int i = 0; i < (rings + 4); ++i)
			{
				for (int j = 0; j < slices; ++j)
				{
					vertices.Add(new Vector3f(
						Math.Cos(Math.Deg2Rad * (270 + ringStep * i)) * Math.Sin(j * sliceStep),
						Math.Sin(Math.Deg2Rad * (270 + ringStep * i)),
						Math.Cos(Math.Deg2Rad * (270 + ringStep * i)) * Math.Cos(j * sliceStep)
					) * radius + position);
					vertices.Add(new Vector3f(
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i + 1))) * Math.Sin((j + 1) * sliceStep),
						Math.Sin(Math.Deg2Rad * (270 + ringStep * (i + 1))),
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i + 1))) * Math.Cos((j + 1) * sliceStep)
					) * radius + position);
					vertices.Add(new Vector3f(
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i + 1))) * Math.Sin(j * sliceStep),
						Math.Sin(Math.Deg2Rad * (270 + ringStep * (i + 1))),
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i + 1))) * Math.Cos(j * sliceStep)
					) * radius + position);

					vertices.Add(new Vector3f(
						Math.Cos(Math.Deg2Rad * (270 + ringStep * i)) * Math.Sin(j * sliceStep),
						Math.Sin(Math.Deg2Rad * (270 + ringStep * i)),
						Math.Cos(Math.Deg2Rad * (270 + ringStep * i)) * Math.Cos(j * sliceStep)
					) * radius + position);
					vertices.Add(new Vector3f(
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i))) * Math.Sin((j + 1) * sliceStep),
						Math.Sin(Math.Deg2Rad * (270 + ringStep * (i))),
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i))) * Math.Cos((j + 1) * sliceStep)
					) * radius + position);
					vertices.Add(new Vector3f(
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i + 1))) * Math.Sin((j + 1) * sliceStep),
						Math.Sin(Math.Deg2Rad * (270 + ringStep * (i + 1))),
						Math.Cos(Math.Deg2Rad * (270 + ringStep * (i + 1))) * Math.Cos((j + 1) * sliceStep)
					) * radius + position);
				}
			}

			RebuildMesh(vertices.ToArray(), indices.ToArray(), colors.ToArray(), Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}

		public static void DrawSphere(Sphere sphere, Color color)
		{
			DrawSphere(sphere.position, sphere.radius, color);
		}

		public static void DrawCone(Vector3f position, float radiusBottom, float height, int sides, Color color)
		{
			DrawCylinder(position, 0f, radiusBottom, height, sides, color);
		}

		public static void DrawCylinder(Vector3f position, float radiusTop, float radiusBottom, float height, int sides, Color color)
		{
			var vertices = new List<Vector3f>();
			var indices = new List<int>();
			var colors = new List<Color>();

			if (sides < 3)
			{
				sides = 3;
			}

			if (radiusTop > 0)
			{
				// Body
				for (int i = 0; i < sides; ++i)
				{
					var step = i * (360f / sides);

					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * step) * radiusBottom, 0, Math.Cos(Math.Deg2Rad * step) * radiusBottom) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * (step + 360f / sides)) * radiusBottom, 0, Math.Cos(Math.Deg2Rad * (step + 360f / sides)) * radiusBottom) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * (step + 360f / sides)) * radiusTop, height, Math.Cos(Math.Deg2Rad * (step + 360f / sides)) * radiusTop) + position);

					indices.Add(vertices.Count - 3);
					indices.Add(vertices.Count - 2);
					indices.Add(vertices.Count - 1);

					colors.Add(color);
					colors.Add(color);
					colors.Add(color);

					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * step) * radiusTop, height, Math.Cos(Math.Deg2Rad * step) * radiusTop) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * step) * radiusBottom, 0, Math.Cos(Math.Deg2Rad * step) * radiusBottom) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * (step + 360f / sides)) * radiusTop, height, Math.Cos(Math.Deg2Rad * (step + 360f / sides)) * radiusTop) + position);

					indices.Add(vertices.Count - 3);
					indices.Add(vertices.Count - 2);
					indices.Add(vertices.Count - 1);

					colors.Add(color);
					colors.Add(color);
					colors.Add(color);
				}

				// Cap
				for (int i = 0; i < sides; ++i)
				{
					var step = i * (360f / sides);

					vertices.Add(new Vector3f(0, height, 0) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * step) * radiusTop, height, Math.Cos(Math.Deg2Rad * step) * radiusTop) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * (step + 360f / sides)) * radiusTop, height, Math.Cos(Math.Deg2Rad * (step + 360f / sides)) * radiusTop) + position);

					indices.Add(vertices.Count - 3);
					indices.Add(vertices.Count - 2);
					indices.Add(vertices.Count - 1);

					colors.Add(color);
					colors.Add(color);
					colors.Add(color);
				}
			}
			else
			{
				// Cone
				for (int i = 0; i < sides; ++i)
				{
					var step = i * (360f / sides);

					vertices.Add(new Vector3f(0, height, 0) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * step) * radiusBottom, 0, Math.Cos(Math.Deg2Rad * step) * radiusBottom) + position);
					vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * (step + 360f / sides)) * radiusBottom, 0, Math.Cos(Math.Deg2Rad * (step + 360f / sides)) * radiusBottom) + position);

					indices.Add(vertices.Count - 3);
					indices.Add(vertices.Count - 2);
					indices.Add(vertices.Count - 1);

					colors.Add(color);
					colors.Add(color);
					colors.Add(color);
				}
			}

			// Base
			for (int i = 0; i < sides; ++i)
			{
				var step = i * (360f / sides);

				vertices.Add(new Vector3f(0, 0, 0) + position);
				vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * (step + 360f / sides)) * radiusBottom, 0, Math.Cos(Math.Deg2Rad * (step + 360f / sides)) * radiusBottom) + position);
				vertices.Add(new Vector3f(Math.Sin(Math.Deg2Rad * step) * radiusBottom, 0, Math.Cos(Math.Deg2Rad * step) * radiusBottom) + position);

				indices.Add(vertices.Count - 3);
				indices.Add(vertices.Count - 2);
				indices.Add(vertices.Count - 1);

				colors.Add(color);
				colors.Add(color);
				colors.Add(color);
			}

			RebuildMesh(vertices.ToArray(), indices.ToArray(), colors.ToArray(), Mesh.Topology.Triangles);

			Core.graphics.DrawMesh(meshCache);
		}

		//-----------------------------------------------------------------------

		public static Mesh CreateGrid(int size, float spacing = 1f)
		{
			var thinLineColor = new Color32(80);
			var thickLineColor = new Color32(100);

			var vertices = new List<Vector3f>();
			var colors = new List<Color32>();
			var offset = -(size / 2);

			for (int x = 0; x <= size; ++x)
			{
				if(x == size / 2)
				{
					vertices.Add(new Vector3f(offset + x, 0, offset) * spacing);
					vertices.Add(new Vector3f(offset + x, 0, offset + size / 2) * spacing);
					vertices.Add(new Vector3f(offset + x, 0, offset + size / 2) * spacing);
					vertices.Add(new Vector3f(offset + x, 0, offset + size) * spacing);

					colors.Add(thickLineColor);
					colors.Add(thickLineColor);
					colors.Add(Color32.blue);
					colors.Add(Color32.blue);

					continue;
				}

				vertices.Add(new Vector3f(offset + x, 0, offset) * spacing);
				vertices.Add(new Vector3f(offset + x, 0, offset + size) * spacing);

				if(x % 10 == 0)
				{
					colors.Add(thickLineColor);
					colors.Add(thickLineColor);
				}
				else
				{
					colors.Add(thinLineColor);
					colors.Add(thinLineColor);
				}
			}

			for (int y = 0; y <= size; ++y)
			{
				if(y == size / 2)
				{
					vertices.Add(new Vector3f(offset, 0, offset + y) * spacing);
					vertices.Add(new Vector3f(offset + size / 2, 0, offset + y) * spacing);
					vertices.Add(new Vector3f(offset + size / 2, 0, offset + y) * spacing);
					vertices.Add(new Vector3f(offset + size, 0, offset + y) * spacing);

					colors.Add(thickLineColor);
					colors.Add(thickLineColor);
					colors.Add(Color32.red);
					colors.Add(Color32.red);

					continue;
				}

				vertices.Add(new Vector3f(offset, 0, offset + y) * spacing);
				vertices.Add(new Vector3f(offset + size, 0, offset + y) * spacing);

				if(y % 10 == 0)
				{
					colors.Add(thickLineColor);
					colors.Add(thickLineColor);
				}
				else
				{
					colors.Add(thinLineColor);
					colors.Add(thinLineColor);
				}
			}

			vertices.Add(new Vector3f(0, 0, 0) * spacing);
			vertices.Add(new Vector3f(0, 10, 0) * spacing);
			colors.Add(Color.green);
			colors.Add(Color.green);

			var indices = new int[vertices.Count];
			for (int i = 0; i < indices.Length; indices[i] = i++);

			var mesh = Core.resource.CreateMesh();
			mesh.SetVertices(vertices.ToArray());
			mesh.SetColors(colors.ToArray());
			mesh.SetIndices(indices, Mesh.Topology.Lines);
			mesh.ApplyMeshData();

			return mesh;
		}

		public static Mesh CreateTorus(float radius, float insideRadius, int segments = 16, int sides = 16)
		{
			var numVertices = (segments + 1) * (sides + 1);
			var vertices = new List<Vector3f>(numVertices);
			var normals = new List<Vector3f>(numVertices);
			var uv = new List<Vector2f>(numVertices);
			var indices = new List<int>(numVertices * 2 * 3);

			for (int segment = 0; segment <= segments; ++segment)
			{
				int currentSegment = (segment == segments ? 0 : segment);

				float t1 = (float)currentSegment / segments * Math.Pi * 2f;
				var r1 = new Vector3f(Math.Cos(t1) * radius, 0f, Math.Sin(t1) * radius);

				for (int side = 0; side <= sides; ++side)
				{
					int currentSide = (side == sides ? 0 : side);

					float t2 = (float)currentSide / sides * Math.Pi * 2f;
					//var r2 = Quat.AngleAxis(-t1, Vector3f.up)          * new Vector3f(Math.Sin(t2) * radius2, Math.Cos(t2) * radius2, 0f);
					var r2 = Math.EulerAngles(new Vector3f(0f, -t1, 0f)) * new Vector3f(Math.Sin(t2) * insideRadius, Math.Cos(t2) * insideRadius, 0f);

					vertices.Add(r1 + r2);
				}
			}

			for (int segment = 0; segment <= segments; ++segment)
			{
				int currentSegment = (segment == segments ? 0 : segment);

				float t1 = (float)currentSegment / segments * Math.Pi * 2f;
				var r1 = new Vector3f(Math.Cos(t1) * radius, 0f, Math.Sin(t1) * radius);

				for (int side = 0; side <= sides; ++side)
				{
					normals.Add(Math.Normalize(vertices[side + segment * (sides + 1)] - r1));
				}
			}

			for (int segment = 0; segment <= segments; ++segment)
			{
				for (int side = 0; side <= sides; ++side)
				{
					uv.Add(new Vector2f((float)segment / segments, (float)side / sides));
				}
			}

			for (int segment = 0, i = 0; segment <= segments; ++segment)
			{
				for (int side = 0; side <= (sides - 1); ++side)
				{
					int current = side + segment * (sides + 1);
					int next = side + (segment < segments ? (segment + 1) * (sides + 1) : 0);

					if (i < indices.Capacity - 6)
					{
						indices.Add(current);
						indices.Add(next);
						indices.Add(next + 1);

						indices.Add(current);
						indices.Add(next + 1);
						indices.Add(current + 1);

						i += 6;
					}
				}
			}

			var mesh = Core.resource.CreateMesh();
			mesh.SetVertices(vertices.ToArray());
			mesh.SetTextureCoordinates(uv.ToArray());
			mesh.SetNormals(normals.ToArray());
			mesh.SetIndices(indices.ToArray(), Mesh.Topology.Triangles);
			mesh.ApplyMeshData();

			return mesh;
		}
	}
}
