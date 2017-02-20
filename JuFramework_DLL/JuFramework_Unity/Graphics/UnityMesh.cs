using System.Collections.Generic;

namespace JuFramework
{
	public partial class UnityMesh : Mesh
	{
		private UnityEngine.Mesh unityMesh;

		private Topology topologyCache;
		private int numIndicesCache;
		private int numVerticesCache;

		private List<UnityEngine.Vector3> unityVertices;
		private List<UnityEngine.Vector3> unityNormals;
		private List<UnityEngine.Vector2> unityTextureCoordinates;
		private List<UnityEngine.Vector4> unityTangents;
		private List<UnityEngine.Color> unityColors;
		private List<UnityEngine.Color32> unityColors32;

		public UnityMesh(bool isDynamic) : base()
		{
			unityMesh = new UnityEngine.Mesh();

			SetDynamic(isDynamic);

			Initialize();
		}

		public UnityMesh(UnityEngine.Mesh mesh)
		{
			unityMesh = mesh;
			Initialize();
		}

		private void Initialize()
		{
			unityMesh.hideFlags = UnityEngine.HideFlags.HideAndDontSave ^ UnityEngine.HideFlags.DontSaveInBuild;

			topologyCache = Topology.Triangles;
			numIndicesCache = 0;
			numVerticesCache = 0;

			unityVertices = new List<UnityEngine.Vector3>();
			unityNormals = new List<UnityEngine.Vector3>();
			unityTextureCoordinates = new List<UnityEngine.Vector2>();
			unityTangents = new List<UnityEngine.Vector4>();
			unityColors = new List<UnityEngine.Color>();
			unityColors32 = new List<UnityEngine.Color32>();
		}

		private UnityEngine.MeshTopology GetUnityMeshTopology()
		{
			UnityEngine.MeshTopology unityMeshTopology = UnityEngine.MeshTopology.Triangles;

			switch (GetTopology())
			{
				case Topology.Triangles:
					break;
				case Topology.Lines:
					unityMeshTopology = UnityEngine.MeshTopology.Lines;
					break;
			}

			return unityMeshTopology;
		}

		public override void ApplyMeshData(bool removeFromMemory)
		{
			if(topologyCache != GetTopology() || numIndicesCache != indexCount || numVerticesCache != vertexCount)
			{
				unityMesh.Clear();

				topologyCache = GetTopology();
				numIndicesCache = indexCount;
				numVerticesCache = vertexCount;
			}

			if(IsDynamic())
			{
				unityMesh.MarkDynamic();
			}

			ApplyVertices();
			ApplyNormals();
			ApplyTextureCoordinates();
			ApplyTangents();
			ApplyColors();
			ApplyIndices();

			// TODO: Revisar si esto no afecta para DrawMeshNow
			//unityMesh.RecalculateBounds();

			if(removeFromMemory)
			{
				Clear();
			}

			unityMesh.UploadMeshData(removeFromMemory);
		}

		private void ApplyVertices()
		{
			var vertices = GetVertices();

			unityVertices.Clear();

			for (int i = 0; i < vertices.Length; ++i)
			{
				unityVertices.Add(vertices[i]);
			}

			unityMesh.SetVertices(unityVertices);
		}

		private void ApplyNormals()
		{
			var normals = GetNormals();

			if(normals != null)
			{
				unityNormals.Clear();

				for (int i = 0; i < normals.Length; ++i)
				{
					unityNormals.Add(normals[i]);
				}

				unityMesh.SetNormals(unityNormals);
			}
		}

		private void ApplyTextureCoordinates()
		{
			for (int channelId = 0; channelId < 4; ++channelId)
			{
				var textureCoordinates = GetTextureCoordinates(channelId);

				if(textureCoordinates != null)
				{
					unityTextureCoordinates.Clear();

					for (int i = 0; i < textureCoordinates.Length; ++i)
					{
						unityTextureCoordinates.Add(textureCoordinates[i]);
					}

					unityMesh.SetUVs(channelId, unityTextureCoordinates);
				}
			}
		}

		private void ApplyTangents()
		{
			var tangents = GetTangents();

			if(tangents != null)
			{
				unityTangents.Clear();

				for (int i = 0; i < tangents.Length; ++i)
				{
					unityTangents.Add(tangents[i]);
				}

				unityMesh.SetTangents(unityTangents);
			}
		}

		private void ApplyColors()
		{
			var colors32 = GetColors32();

			if(colors32 != null)
			{
				unityColors32.Clear();

				for (int i = 0; i < colors32.Length; ++i)
				{
					unityColors32.Add(colors32[i]);
				}

				unityMesh.SetColors(unityColors32);

				return;
			}

			var colors = GetColors();

			if(colors != null)
			{
				unityColors.Clear();

				for (int i = 0; i < colors.Length; ++i)
				{
					unityColors.Add(colors[i]);
				}

				unityMesh.SetColors(unityColors);

				return;
			}
		}

		private void ApplyIndices()
		{
			unityMesh.SetIndices(GetIndices(), GetUnityMeshTopology(), 0);
		}
	}
}
