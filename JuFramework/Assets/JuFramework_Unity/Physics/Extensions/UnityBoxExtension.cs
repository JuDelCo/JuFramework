
namespace JuFramework
{
	public partial struct Box
	{
		public static implicit operator UnityEngine.Bounds(Box b)
		{
			return new UnityEngine.Bounds(b.center, b.size);
		}

		public static explicit operator Box(UnityEngine.Bounds b)
		{
			return new Box((Vector3f)b.center, (Vector3f)b.size);
		}
	}
}
