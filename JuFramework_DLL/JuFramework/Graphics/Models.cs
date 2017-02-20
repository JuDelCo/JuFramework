
// TODO: Mover Torus & Grid de Tests
// TODO: IMPORTANT: Optimize (GC Allocations) (start with sphere)

namespace JuFramework
{
	public static partial class Models
	{
		private static Mesh meshCache = Core.resource.CreateMesh(true);

		//private static Vector3f[] lineVerticesCache = new Vector3f[2];
		//private static int[] lineIndicesCache = new int[2] { 0, 1 };
		//private static Color[] lineColorsCache = new Color[2];

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
			// TODO
		}

		public static void DrawSphere(Vector3f position, float radius, Color colour, int circleSegments = 36)
		{
			float thetaInc = (Math.Pi * 2.0f) / (float)circleSegments;
			var lastPointHoriz = position + new Vector3f(0, 0, radius);
			var lastPointVert = position + new Vector3f(0, radius, 0);
			var lastPointForw = position + new Vector3f(0, 0, radius);

			for (int i = 1; i <= circleSegments; ++i)
			{
				float theta = thetaInc * i;

				Vector3f point = position + (new Vector3f(Math.Sin(theta), 0, Math.Cos(theta)) * radius);
				DrawLine(lastPointHoriz, point, colour);
				lastPointHoriz = point;

				point = position + (new Vector3f(Math.Sin(theta), Math.Cos(theta), 0) * radius);
				DrawLine(lastPointVert, point, colour);
				lastPointVert = point;

				point = position + (new Vector3f(0, Math.Sin(theta), Math.Cos(theta)) * radius);
				DrawLine(lastPointForw, point, colour);
				lastPointForw = point;
			}
		}

		public static void DrawAxisGizmo(Transform3D transform)
		{
			float length = 10.0f;

			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Forward() * length, Color.blue);
			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Right() * length, Color.red);
			DrawLine(transform.GetPosition(), transform.GetPosition() + transform.Up() * length, Color.green);
		}
	}
}

/*
void DrawCircle(Vector3 center, float radius, Vector3 rotationAxis, float rotationAngle, Color color);                                                // Draw a circle in 3D world space
void DrawCube(Vector3 position, float width, float height, float lenght, Color color);              // Draw cube
void DrawCubeV(Vector3 position, Vector3 size, Color color);                                        // Draw cube (Vector version)
void DrawCubeWires(Vector3 position, float width, float height, float lenght, Color color);         // Draw cube wires
void DrawCubeTexture(Texture2D texture, Vector3 position, float width, float height, float lenght, Color color);                                      // Draw cube textured
void DrawSphere(Vector3 centerPos, float radius, Color color);                                      // Draw sphere
void DrawSphereEx(Vector3 centerPos, float radius, int rings, int slices, Color color);             // Draw sphere with extended parameters
void DrawSphereWires(Vector3 centerPos, float radius, int rings, int slices, Color color);          // Draw sphere wires
void DrawCylinder(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, Color color);                                           // Draw a cylinder/cone
void DrawCylinderWires(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, Color color);                                      // Draw a cylinder/cone wires
void DrawPlane(Vector3 centerPos, Vector2 size, Color color);                                       // Draw a plane XZ
void DrawRay(Ray ray, Color color);                                                                 // Draw a ray line
void DrawGrid(int slices, float spacing);                                                           // Draw a grid (centered at (0, 0, 0))
void DrawGizmo(Vector3 position);                                                                   // Draw simple gizmo
void DrawLight(Light light);                                                                        // Draw light in 3D world
void DrawBoundingBox(Box box, Color color);                                                 // Draw bounding box (wires)

void DrawBillboard(Camera camera, Texture2D texture, Vector3 center, float size, Color tint);       // Draw a billboard texture
void DrawBillboardRec(Camera camera, Texture2D texture, Rectangle sourceRec, Vector3 center, float size, Color tint);                                      // Draw a billboard texture defined by sourceRec
*/
