
namespace JuFramework
{
	public partial struct FloatRect
	{
		public static implicit operator UnityEngine.Rect(FloatRect r)
		{
			return new UnityEngine.Rect(r.x, r.y, r.width, r.height);
		}

		public static explicit operator FloatRect(UnityEngine.Rect r)
		{
			return new FloatRect(r.x, r.y, r.width, r.height);
		}
	}
}
