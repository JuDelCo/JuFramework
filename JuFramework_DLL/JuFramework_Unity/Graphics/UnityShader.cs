using System.Collections.Generic;

namespace JuFramework
{
	public partial class UnityShader : Shader
	{
		private bool usingFallback;
		private UnityEngine.Shader shader;
		private Dictionary<string, int> propertyCache;

		public UnityShader(UnityEngine.Shader shader)
		{
			propertyCache = new Dictionary<string, int>();

			if(shader != null)
			{
				this.shader = shader;
				usingFallback = false;
			}
			else
			{
				this.shader = UnityEngine.Shader.Find("Sprites/Default");
				usingFallback = true;
			}

			this.shader.hideFlags = UnityEngine.HideFlags.HideAndDontSave ^ UnityEngine.HideFlags.DontSaveInBuild;
		}

		private int GetPropertyID(string propertyName)
		{
			if(!propertyCache.ContainsKey(propertyName))
			{
				propertyCache.Add(propertyName, UnityEngine.Shader.PropertyToID(propertyName));
			}

			return propertyCache[propertyName];
		}

		public bool IsFallbackShader()
		{
			return usingFallback;
		}

		public override bool IsSupported()
		{
			if(shader != null)
			{
				return shader.isSupported;
			}

			return false;
		}

		public override void SetMaximumLOD(int level)
		{
			if(shader != null)
			{
				shader.maximumLOD = level;
			}
		}

		public override void SetGlobal(string propertyName, int value)
		{
			SetGlobal(GetPropertyID(propertyName), value);
		}

		public override void SetGlobal(int nameID, int value)
		{
			UnityEngine.Shader.SetGlobalInt(nameID, value);
		}

		public override void SetGlobal(string propertyName, float value)
		{
			SetGlobal(GetPropertyID(propertyName), value);
		}

		public override void SetGlobal(int nameID, float value)
		{
			UnityEngine.Shader.SetGlobalFloat(nameID, value);
		}

		public override void SetGlobal(string propertyName, float[] values)
		{
			SetGlobal(GetPropertyID(propertyName), values);
		}

		public override void SetGlobal(int nameID, float[] values)
		{
			UnityEngine.Shader.SetGlobalFloatArray(nameID, values);
		}

		public override void SetGlobal(string propertyName, Vector4f value)
		{
			SetGlobal(GetPropertyID(propertyName), value);
		}

		public override void SetGlobal(int nameID, Vector4f value)
		{
			UnityEngine.Shader.SetGlobalVector(nameID, value);
		}

		public override void SetGlobal(string propertyName, Vector4f[] values)
		{
			SetGlobal(GetPropertyID(propertyName), values);
		}

		public override void SetGlobal(int nameID, Vector4f[] values)
		{
			var unityVectors = new List<UnityEngine.Vector4>(values.Length);

			for (int i = 0; i < values.Length; ++i)
			{
				unityVectors.Add(values[i]);
			}

			UnityEngine.Shader.SetGlobalVectorArray(nameID, unityVectors);
		}

		public override void SetGlobal(string propertyName, Texture value)
		{
			SetGlobal(GetPropertyID(propertyName), value);
		}

		public override void SetGlobal(int nameID, Texture value)
		{
			UnityEngine.Shader.SetGlobalTexture(nameID, (UnityTexture)value);
		}

		public override void SetGlobal(string propertyName, Color value)
		{
			SetGlobal(GetPropertyID(propertyName), value);
		}

		public override void SetGlobal(int nameID, Color value)
		{
			UnityEngine.Shader.SetGlobalColor(nameID, value);
		}

		public override void SetGlobal(string propertyName, Matrix4 value)
		{
			SetGlobal(GetPropertyID(propertyName), value);
		}

		public override void SetGlobal(int nameID, Matrix4 value)
		{
			UnityEngine.Shader.SetGlobalMatrix(nameID, value);
		}

		public override void SetGlobal(string propertyName, Matrix4[] values)
		{
			SetGlobal(GetPropertyID(propertyName), values);
		}

		public override void SetGlobal(int nameID, Matrix4[] values)
		{
			var unityValues = new UnityEngine.Matrix4x4[values.Length];

			for (int i = 0; i < values.Length; ++i)
			{
				unityValues[i] = values[i];
			}

			UnityEngine.Shader.SetGlobalMatrixArray(nameID, unityValues);
		}

		public override void SetGlobalMaximumLOD(int level)
		{
			UnityEngine.Shader.globalMaximumLOD = level;
		}

		public override bool GetGlobalKeyword(string keyword)
		{
			return UnityEngine.Shader.IsKeywordEnabled(keyword);
		}

		public override void SetGlobalKeyword(string keyword, bool enabled)
		{
			if(enabled)
			{
				UnityEngine.Shader.EnableKeyword(keyword);
			}
			else
			{
				UnityEngine.Shader.DisableKeyword(keyword);
			}
		}

		public override void WarmupAllShaders()
		{
			UnityEngine.Shader.WarmupAllShaders();
		}
	}
}
