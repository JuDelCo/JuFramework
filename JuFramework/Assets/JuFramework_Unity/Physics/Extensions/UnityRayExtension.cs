
namespace JuFramework
{
	public partial struct Ray
	{
		public static implicit operator UnityEngine.Ray(Ray r)
		{
			return new UnityEngine.Ray(r.origin, r.direction);
		}

		public static explicit operator Ray(UnityEngine.Ray r)
		{
			return new Ray((Vector3f)r.origin, (Vector3f)r.direction);
		}
	}
}
