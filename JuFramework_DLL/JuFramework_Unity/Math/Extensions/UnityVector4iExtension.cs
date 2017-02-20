
namespace JuFramework
{
	public partial struct Vector4i
	{
		public static implicit operator UnityEngine.Vector4(Vector4i v)
		{
			return new UnityEngine.Vector4(v.x, v.y, v.z, v.w);
		}

		public static explicit operator Vector4i(UnityEngine.Vector4 v)
		{
			return v.FloorToVector4i();
		}
	}

	public static class UnityVector4iExtension
	{
		public static Vector4i RoundToVector4i(this UnityEngine.Vector4 v)
		{
			return new Vector4i(Math.Round(v.x), Math.Round(v.y), Math.Round(v.z), Math.Round(v.w));
		}

		public static Vector4i FloorToVector4i(this UnityEngine.Vector4 v)
		{
			return new Vector4i(Math.Floor(v.x), Math.Floor(v.y), Math.Floor(v.z), Math.Floor(v.w));
		}

		public static Vector4i CeilToVector4i(this UnityEngine.Vector4 v)
		{
			return new Vector4i(Math.Ceil(v.x), Math.Ceil(v.y), Math.Ceil(v.z), Math.Ceil(v.w));
		}
	}
}
