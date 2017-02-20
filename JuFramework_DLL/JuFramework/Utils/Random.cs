
namespace JuFramework
{
	public class Random
	{
		public static System.Random random = new System.Random();

		public static void SetSeed(int seed)
		{
			random = new System.Random(seed);
		}

		public static int Int(int max)
		{
			return random.Next(max);
		}

		public static float Float(float max)
		{
			return (float)random.NextDouble() * max;
		}

		public static int Range(int min, int max)
		{
			return random.Next(min, max);
		}

		public static float Range(float min, float max)
		{
			return min + Float(max - min);
		}

		public static Vector2i Range(Vector2i min, Vector2i max)
		{
			return min + new Vector2i(Int(max.x - min.x), Int(max.y - min.y));
		}

		public static Vector2f Range(Vector2f min, Vector2f max)
		{
			return min + new Vector2f(Float(max.x - min.x), Float(max.y - min.y));
		}

		public static Color Color()
		{
			return Color(0f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f);
		}

		public static Color Color(float hueMin, float hueMax)
		{
			return Color(hueMin, hueMax, 1f, 1f, 0.5f, 0.5f, 1f, 1f);
		}

		public static Color Color(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return Color(hueMin, hueMax, saturationMin, saturationMax, 0.5f, 0.5f, 1f, 1f);
		}

		public static Color Color(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return Color(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
		}

		public static Color Color(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			float hue = Math.Clamp01(Range(hueMin, hueMax));
			float saturation = Math.Clamp01(Range(saturationMin, saturationMax));
			float brightness = Math.Clamp01(Range(valueMin, valueMax));
			float alpha = Math.Clamp01(Range(alphaMin, alphaMax));

			return JuFramework.Color.FromHSV(hue, saturation, brightness, alpha);
		}
	}
}
