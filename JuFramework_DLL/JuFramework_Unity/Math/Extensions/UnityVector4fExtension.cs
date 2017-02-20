
namespace JuFramework
{
	public partial struct Vector4f
	{
		public static implicit operator UnityEngine.Vector4(Vector4f v)
		{
			return new UnityEngine.Vector4(v.x, v.y, v.z, v.w);
		}

		public static explicit operator Vector4f(UnityEngine.Vector4 v)
		{
			return new Vector4f(v.x, v.y, v.z, v.w);
		}
	}
}
