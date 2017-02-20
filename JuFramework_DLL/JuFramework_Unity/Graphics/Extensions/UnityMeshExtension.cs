
namespace JuFramework
{
	public partial class UnityMesh
	{
		public UnityEngine.Mesh GetUnityMesh()
		{
			return unityMesh;
		}

		public static implicit operator UnityEngine.Mesh(UnityMesh mesh)
		{
			return mesh.GetUnityMesh();
		}
	}
}
