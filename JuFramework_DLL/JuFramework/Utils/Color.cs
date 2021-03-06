
namespace JuFramework
{
	public partial struct Color
	{
		private float rValue;
		private float gValue;
		private float bValue;
		private float aValue;

		public Color(float r, float g, float b)
		{
			rValue = Math.Clamp01(r);
			gValue = Math.Clamp01(g);
			bValue = Math.Clamp01(b);
			aValue = 1f;
		}

		public Color(float r, float g, float b, float a)
		{
			rValue = Math.Clamp01(r);
			gValue = Math.Clamp01(g);
			bValue = Math.Clamp01(b);
			aValue = Math.Clamp01(a);
		}

		//-----------------------------------------------------------------------

		public float r
		{
			get { return rValue; }
			set { rValue = Math.Clamp01(value); }
		}

		public float g
		{
			get { return gValue; }
			set { gValue = Math.Clamp01(value); }
		}

		public float b
		{
			get { return bValue; }
			set { bValue = Math.Clamp01(value); }
		}

		public float a
		{
			get { return aValue; }
			set { aValue = Math.Clamp01(value); }
		}

		//-----------------------------------------------------------------------

		public static Color operator + (Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		//-----------------------------------------------------------------------

		public static Color operator - (Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		//-----------------------------------------------------------------------

		public static Color operator * (Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		public static Color operator * (Color c, int v)
		{
			return new Color(c.r * v, c.g * v, c.b * v, c.a * v);
		}

		public static Color operator * (int v, Color c)
		{
			return new Color(c.r * v, c.g * v, c.b * v, c.a * v);
		}

		public static Color operator * (Color c, float v)
		{
			return new Color(c.r * v, c.g * v, c.b * v, c.a * v);
		}

		public static Color operator * (float v, Color c)
		{
			return new Color(c.r * v, c.g * v, c.b * v, c.a * v);
		}

		//-----------------------------------------------------------------------

		public static Color operator / (Color a, Color b)
		{
			return new Color(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
		}

		public static Color operator / (Color c, int v)
		{
			return new Color(c.r / v, c.g / v, c.b / v, c.a / v);
		}

		public static Color operator / (int v, Color c)
		{
			return new Color(v / c.r, v / c.g, v / c.b, v / c.a);
		}

		public static Color operator / (Color c, float v)
		{
			return new Color(c.r / v, c.g / v, c.b / v, c.a / v);
		}

		public static Color operator / (float v, Color c)
		{
			return new Color(v / c.r, v / c.g, v / c.b, v / c.a);
		}

		//-----------------------------------------------------------------------

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + rValue.GetHashCode();
				hash = hash * 23 + gValue.GetHashCode();
				hash = hash * 23 + bValue.GetHashCode();
				hash = hash * 23 + aValue.GetHashCode();
				return hash;
			}
		}

		public override bool Equals(object obj)
		{
			return (obj is Color && (this == (Color)obj));
		}

		public static bool operator == (Color a, Color b)
		{
			return (a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a);
		}

		public static bool operator != (Color a, Color b)
		{
			return !(a == b);
		}

		//-----------------------------------------------------------------------

		public override string ToString()
		{
			return "[ " + r + " , " + g + " , " + b + " , " + a + " ]";
		}
	}
}
