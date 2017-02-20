
// TODO: Cachear "int nameID"

// TODO: Shorthands para:
//		samplerState (pointClamp, ...)			(material, filtro de texturas bilineal, etc)
//		faceCulling (rasterizerState) ->		https://docs.unity3d.com/Manual/SL-CullAndDepth.html
//		depthBuffer ->							https://docs.unity3d.com/Manual/SL-CullAndDepth.html
//		stencilBuffer (depthStencilState) ->	https://docs.unity3d.com/Manual/SL-Stencil.html
//		blendState ->							https://docs.unity3d.com/Manual/SL-Blend.html

namespace JuFramework
{
	public abstract partial class Material
	{
		protected Shader shader;

		public Material(Shader shader)
		{
			this.shader = shader;
		}

		public abstract Shader GetShader();
		public abstract int GetPassCount();
		public abstract bool SetPass(int pass);

		public abstract bool HasProperty(string propertyName);
		public abstract bool HasProperty(int nameID);

		public abstract void Set(string propertyName, int value);
		public abstract void Set(string propertyName, float value);
		public abstract void Set(string propertyName, float[] values);
		public abstract void Set(string propertyName, Vector4f value);
		public abstract void Set(string propertyName, Vector4f[] values);
		public abstract void Set(string propertyName, Texture value);
		public abstract void SetTextureOffset(string propertyName, Vector2f value);
		public abstract void SetTextureScale(string propertyName, Vector2f value);
		public abstract void Set(string propertyName, Color value);
		public abstract void Set(string propertyName, Color[] values);
		public abstract void Set(string propertyName, Matrix4 value);
		public abstract void Set(string propertyName, Matrix4[] values);

		public abstract bool GetKeyword(string keyword);
		public abstract void SetKeyword(string keyword, bool enabled);

		public abstract int GetInt(string propertyName);
		public abstract float GetFloat(string propertyName);
		public abstract float[] GetFloatArray(string propertyName);
		public abstract Vector4f GetVector(string propertyName);
		public abstract Vector4f[] GetVectorArray(string propertyName);
		public abstract Texture GetTexture(string propertyName);
		public abstract Vector2f GetTextureOffset(string propertyName);
		public abstract Vector2f GetTextureScale(string propertyName);
		public abstract Color GetColor(string propertyName);
		public abstract Color[] GetColorArray(string propertyName);
		public abstract Matrix4 GetMatrix(string propertyName);
		public abstract Matrix4[] GetMatrixArray(string propertyName);
	}
}
