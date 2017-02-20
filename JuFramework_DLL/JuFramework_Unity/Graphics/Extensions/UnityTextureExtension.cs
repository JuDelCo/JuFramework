
namespace JuFramework
{
	public partial class UnityTexture
	{
		public UnityEngine.Texture2D GetUnityTexture2D()
		{
			return unityTexture;
		}

		public static implicit operator UnityEngine.Texture2D(UnityTexture texture)
		{
			return texture.GetUnityTexture2D();
		}
	}
}
