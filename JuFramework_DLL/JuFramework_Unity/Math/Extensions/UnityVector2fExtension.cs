
namespace JuFramework
{
	public partial struct Vector2f
	{
		public static implicit operator UnityEngine.Vector2(Vector2f v)
		{
			return new UnityEngine.Vector2(v.x, v.y);
		}

		public static explicit operator Vector2f(UnityEngine.Vector2 v)
		{
			return new Vector2f(v.x, v.y);
		}
	}
}
