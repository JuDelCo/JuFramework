using System.Collections.Generic;

// TODO:
//		Lerp
//		CopyPropertiesFromMaterial

namespace JuFramework
{
	public partial class UnityMaterial : Material
	{
		private UnityEngine.Material material;

		public UnityMaterial(Shader shader) : base(shader)
		{
			material = new UnityEngine.Material((UnityShader)shader);
		}

		public override Shader GetShader()
		{
			return shader;
		}

		public override int GetPassCount()
		{
			return material.passCount;
		}

		public override bool Use(int pass = 0)
		{
			return material.SetPass(pass);
		}

		public override bool HasProperty(string propertyName)
		{
			return material.HasProperty(propertyName);
		}

		public override bool HasProperty(int nameID)
		{
			return material.HasProperty(nameID);
		}

		public override void Set(string propertyName, int value)
		{
			material.SetInt(propertyName, value);
		}

		public override void Set(string propertyName, float value)
		{
			material.SetFloat(propertyName, value);
		}

		public override void Set(string propertyName, float[] values)
		{
			material.SetFloatArray(propertyName, values);
		}

		public override void Set(string propertyName, Vector4f value)
		{
			material.SetVector(propertyName, value);
		}

		public override void Set(string propertyName, Vector4f[] values)
		{
			var unityVectors = new List<UnityEngine.Vector4>(values.Length);

			for (int i = 0; i < values.Length; ++i)
			{
				unityVectors.Add(values[i]);
			}

			material.SetVectorArray(propertyName, unityVectors);
		}

		public override void Set(string propertyName, Texture value)
		{
			material.SetTexture(propertyName, (UnityTexture)value);
		}

		public override void SetTextureOffset(string propertyName, Vector2f value)
		{
			material.SetTextureOffset(propertyName, value);
		}

		public override void SetTextureScale(string propertyName, Vector2f value)
		{
			material.SetTextureScale(propertyName, value);
		}

		public override void Set(string propertyName, Color value)
		{
			material.SetColor(propertyName, value);
		}

		public override void Set(string propertyName, Color[] values)
		{
			var colors = new UnityEngine.Color[values.Length];

			for (int i = 0; i < values.Length; ++i)
			{
				colors[i] = values[i];
			}

			material.SetColorArray(propertyName, colors);
		}

		public override void Set(string propertyName, Matrix4 value)
		{
			material.SetMatrix(propertyName, value);
		}

		public override void Set(string propertyName, Matrix4[] values)
		{
			var matrices = new UnityEngine.Matrix4x4[values.Length];

			for (int i = 0; i < values.Length; ++i)
			{
				matrices[i] = values[i];
			}

			material.SetMatrixArray(propertyName, matrices);
		}

		public override bool GetKeyword(string keyword)
		{
			return material.IsKeywordEnabled(keyword);
		}

		public override void SetKeyword(string keyword, bool enabled)
		{
			if(enabled)
			{
				material.EnableKeyword(keyword);
			}
			else
			{
				material.DisableKeyword(keyword);
			}
		}

		public override int GetInt(string propertyName)
		{
			return material.GetInt(propertyName);
		}

		public override float GetFloat(string propertyName)
		{
			return material.GetFloat(propertyName);
		}

		public override float[] GetFloatArray(string propertyName)
		{
			return material.GetFloatArray(propertyName);
		}

		public override Vector4f GetVector(string propertyName)
		{
			return (Vector4f)material.GetVector(propertyName);
		}

		public override Vector4f[] GetVectorArray(string propertyName)
		{
			var vectors = material.GetVectorArray(propertyName);
			var values = new Vector4f[vectors.Length];

			for (int i = 0; i < values.Length; ++i)
			{
				values[i] = (Vector4f)vectors[i];
			}

			return values;
		}

		public override Texture GetTexture(string propertyName)
		{
			return new UnityTexture((UnityEngine.Texture2D)material.GetTexture(propertyName));
		}

		public override Vector2f GetTextureOffset(string propertyName)
		{
			return (Vector2f)material.GetTextureOffset(propertyName);
		}

		public override Vector2f GetTextureScale(string propertyName)
		{
			return (Vector2f)material.GetTextureScale(propertyName);
		}

		public override Color GetColor(string propertyName)
		{
			return (Color)material.GetColor(propertyName);
		}

		public override Color[] GetColorArray(string propertyName)
		{
			var colors = material.GetColorArray(propertyName);
			var values = new Color[colors.Length];

			for (int i = 0; i < values.Length; ++i)
			{
				values[i] = (Color)colors[i];
			}

			return values;
		}

		public override Matrix4 GetMatrix(string propertyName)
		{
			return (Matrix4)material.GetMatrix(propertyName);
		}

		public override Matrix4[] GetMatrixArray(string propertyName)
		{
			var matrices = material.GetMatrixArray(propertyName);
			var values = new Matrix4[matrices.Length];

			for (int i = 0; i < values.Length; ++i)
			{
				values[i] = (Matrix4)matrices[i];
			}

			return values;
		}
	}
}
