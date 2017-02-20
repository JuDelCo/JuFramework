
namespace JuFramework
{
	public partial class UnityShader
	{
		public UnityEngine.Shader GetUnityShader()
		{
			return shader;
		}

		public static implicit operator UnityEngine.Shader(UnityShader shader)
		{
			return shader.GetUnityShader();
		}
	}
}
