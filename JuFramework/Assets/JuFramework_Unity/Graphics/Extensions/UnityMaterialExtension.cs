
namespace JuFramework
{
	public partial class UnityMaterial
	{
		public UnityEngine.Material GetUnityMaterial()
		{
			return material;
		}

		public static implicit operator UnityEngine.Material(UnityMaterial material)
		{
			return material.GetUnityMaterial();
		}
	}
}
