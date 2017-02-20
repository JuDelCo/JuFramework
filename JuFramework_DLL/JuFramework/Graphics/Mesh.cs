
// TODO:
//		Bounds (squared and sphere)
//		Recalculate normals

// TODO: Get native pointers (vertex & index buffer)

namespace JuFramework
{
	public abstract class Mesh
	{
		public enum Topology
		{
			Lines,
			Triangles
		}

		private Topology topology;
		private bool isDynamic;
		private Vector3f[] vertices;
		private int[] indices;
		private Vector2f[][] uv;
		private Vector3f[] normals;
		private Vector4f[] tangents;
		private Color[] colors;
		private Color32[] colors32;

		//public box bounds // vec3 center + vec3 size
		//public Sphere boundingSphere // Center, Radius

		public int vertexCount
		{
			get
			{
				if(vertices != null)
				{
					return vertices.Length;
				}

				return 0;
			}
		}

		public int indexCount
		{
			get
			{
				if(indices != null)
				{
					return indices.Length;
				}

				return 0;
			}
		}

		public Mesh()
		{
			uv = new Vector2f[4][];
		}

		public bool IsDynamic()
		{
			return isDynamic;
		}

		public void SetDynamic(bool isDynamic)
		{
			this.isDynamic = isDynamic;
		}

		/*public void RecalculateBounds()
		{
		}*/

		public void Clear()
		{
			vertices = null;
			indices = null;
			normals = null;
			tangents = null;
			colors = null;
			colors32 = null;

			for (int i = 0; i < uv.Length; ++i)
			{
				uv[i] = null;
			}
		}

		public Vector3f[] GetVertices()
		{
			return vertices;
		}

		public void SetVertices(Vector2f[] vertices, float depth = 0f)
		{
			var values = new Vector3f[vertices.Length];

			for (int i = 0; i < vertices.Length; ++i)
			{
				values[i] = new Vector3f(vertices[i], depth);
			}

			SetVertices(values);
		}

		public void SetVertices(Vector3f[] vertices)
		{
			if(vertices != null)
			{
				if(this.vertices != null && vertices.Length != vertexCount)
				{
					Clear();
				}

				this.vertices = vertices;
			}
		}

		public Topology GetTopology()
		{
			return topology;
		}

		public int[] GetIndices()
		{
			return indices;
		}

		public void SetIndices(int[] indices, Topology topology = Topology.Triangles)
		{
			if(indices != null)
			{
				this.topology = topology;
				this.indices = indices;
			}
		}

		public Vector2f[] GetTextureCoordinates(int channel = 0)
		{
			if(Math.Between(channel, 0, 3))
			{
				return uv[channel];
			}

			return null;
		}

		public void SetTextureCoordinates(Vector2f[] uv, int channel = 0)
		{
			if(Math.Between(channel, 0, 3) && uv != null && uv.Length == vertexCount)
			{
				this.uv[channel] = uv;
			}
		}

		public Vector3f[] GetNormals()
		{
			return normals;
		}

		public void SetNormals(Vector3f[] normals)
		{
			if(normals != null && normals.Length == vertexCount)
			{
				this.normals = normals;
			}
		}

		/*public void RecalculateNormals()
		{
		}*/

		public Vector4f[] GetTangents()
		{
			return tangents;
		}

		public void SetTangents(Vector4f[] tangents)
		{
			if(tangents != null && tangents.Length == vertexCount)
			{
				this.tangents = tangents;
			}
		}

		public Color[] GetColors()
		{
			return colors;
		}

		public Color32[] GetColors32()
		{
			return colors32;
		}

		public void SetColors(Color[] colors)
		{
			if(colors != null && colors.Length == vertexCount)
			{
				this.colors = colors;
			}
		}

		public void SetColors(Color32[] colors)
		{
			if(colors != null && colors.Length == vertexCount)
			{
				this.colors32 = colors;
			}
		}

		public void SetColor(Color color)
		{
			if(vertexCount > 0)
			{
				if(colors == null)
				{
					colors = new Color[vertexCount];
				}

				for (int i = 0; i < colors.Length; ++i)
				{
					colors[i] = color;
				}
			}
		}

		public void SetColor(Color32 color)
		{
			if(vertexCount > 0)
			{
				if(colors32 == null)
				{
					colors32 = new Color32[vertexCount];
				}

				for (int i = 0; i < colors32.Length; ++i)
				{
					colors32[i] = color;
				}
			}
		}

		public void SetColor(int vertIndex, Color color)
		{
			if(Math.Between(vertIndex, 0, (vertexCount - 1)))
			{
				if(colors == null)
				{
					colors = new Color[vertexCount];
				}

				colors[vertIndex] = color;
			}
		}

		public void SetColor(int vertIndex, Color32 color)
		{
			if(Math.Between(vertIndex, 0, (vertexCount - 1)))
			{
				if(colors32 == null)
				{
					colors32 = new Color32[vertexCount];
				}

				colors32[vertIndex] = color;
			}
		}

		public abstract void ApplyMeshData(bool removeFromMemory = false);
	}
}
