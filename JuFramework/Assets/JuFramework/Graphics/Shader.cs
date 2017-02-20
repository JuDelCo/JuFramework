
// TODO: Global Getters

namespace JuFramework
{
	public abstract partial class Shader
	{
		public abstract bool IsSupported();
		public abstract void SetMaximumLOD(int level);

		public abstract void SetGlobal(string propertyName, int value);
		public abstract void SetGlobal(int nameID, int value);
		public abstract void SetGlobal(string propertyName, float value);
		public abstract void SetGlobal(int nameID, float value);
		public abstract void SetGlobal(string propertyName, float[] values);
		public abstract void SetGlobal(int nameID, float[] values);
		public abstract void SetGlobal(string propertyName, Vector4f value);
		public abstract void SetGlobal(int nameID, Vector4f value);
		public abstract void SetGlobal(string propertyName, Vector4f[] values);
		public abstract void SetGlobal(int nameID, Vector4f[] values);
		public abstract void SetGlobal(string propertyName, Texture value);
		public abstract void SetGlobal(int nameID, Texture value);
		public abstract void SetGlobal(string propertyName, Color value);
		public abstract void SetGlobal(int nameID, Color value);
		public abstract void SetGlobal(string propertyName, Matrix4 value);
		public abstract void SetGlobal(int nameID, Matrix4 value);
		public abstract void SetGlobal(string propertyName, Matrix4[] values);
		public abstract void SetGlobal(int nameID, Matrix4[] values);

		public abstract void SetGlobalMaximumLOD(int level);
		public abstract bool GetGlobalKeyword(string keyword);
		public abstract void SetGlobalKeyword(string keyword, bool enabled);

		public abstract void WarmupAllShaders();
	}
}
