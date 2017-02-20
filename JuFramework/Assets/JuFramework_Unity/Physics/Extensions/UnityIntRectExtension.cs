
namespace JuFramework
{
	public partial struct IntRect
	{
		public static implicit operator UnityEngine.Rect(IntRect r)
		{
			return new UnityEngine.Rect(r.x, r.y, r.width, r.height);
		}

		public static explicit operator IntRect(UnityEngine.Rect r)
		{
			return r.FloorToIntRect();
		}
	}

	public static class UnityIntRectExtension
	{
		public static IntRect RoundToIntRect(this UnityEngine.Rect r)
		{
			return new IntRect(Math.Round(r.x), Math.Round(r.y), Math.Round(r.width), Math.Round(r.height));
		}

		public static IntRect FloorToIntRect(this UnityEngine.Rect r)
		{
			return new IntRect(Math.Floor(r.x), Math.Floor(r.y), Math.Floor(r.width), Math.Floor(r.height));
		}

		public static IntRect CeilToIntRect(this UnityEngine.Rect r)
		{
			return new IntRect(Math.Ceil(r.x), Math.Ceil(r.y), Math.Ceil(r.width), Math.Ceil(r.height));
		}
	}
}
