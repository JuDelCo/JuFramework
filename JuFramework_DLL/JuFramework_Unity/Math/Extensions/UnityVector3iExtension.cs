
namespace JuFramework
{
	public partial struct Vector3i
	{
		public static implicit operator UnityEngine.Vector3(Vector3i v)
		{
			return new UnityEngine.Vector3(v.x, v.y, v.z);
		}

		public static explicit operator Vector3i(UnityEngine.Vector3 v)
		{
			return v.FloorToVector3i();
		}
	}

	public static class UnityVector3iExtension
	{
		public static Vector3i RoundToVector3i(this UnityEngine.Vector3 v)
		{
			return new Vector3i(Math.Round(v.x), Math.Round(v.y), Math.Round(v.z));
		}

		public static Vector3i FloorToVector3i(this UnityEngine.Vector3 v)
		{
			return new Vector3i(Math.Floor(v.x), Math.Floor(v.y), Math.Floor(v.z));
		}

		public static Vector3i CeilToVector3i(this UnityEngine.Vector3 v)
		{
			return new Vector3i(Math.Ceil(v.x), Math.Ceil(v.y), Math.Ceil(v.z));
		}
	}
}
