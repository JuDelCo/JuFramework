
namespace JuFramework
{
	public partial struct Color
	{
		public static implicit operator UnityEngine.Color(Color color)
		{
			return new UnityEngine.Color(color.r, color.g, color.b, color.a);
		}

		public static explicit operator Color(UnityEngine.Color color)
		{
			return new Color(color.r, color.g, color.b, color.a);
		}
	}
}
