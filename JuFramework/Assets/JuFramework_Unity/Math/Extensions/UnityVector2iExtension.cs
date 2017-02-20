
namespace JuFramework
{
	public partial struct Vector2i
	{
		public static implicit operator UnityEngine.Vector2(Vector2i v)
		{
			return new UnityEngine.Vector2(v.x, v.y);
		}

		public static explicit operator Vector2i(UnityEngine.Vector2 v)
		{
			return v.FloorToVector2i();
		}
	}

	public static class UnityVector2iExtension
	{
		public static Vector2i RoundToVector2i(this UnityEngine.Vector2 v)
		{
			return new Vector2i(Math.Round(v.x), Math.Round(v.y));
		}

		public static Vector2i FloorToVector2i(this UnityEngine.Vector2 v)
		{
			return new Vector2i(Math.Floor(v.x), Math.Floor(v.y));
		}

		public static Vector2i CeilToVector2i(this UnityEngine.Vector2 v)
		{
			return new Vector2i(Math.Ceil(v.x), Math.Ceil(v.y));
		}
	}
}
