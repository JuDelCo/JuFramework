
namespace JuFramework
{
	public partial struct Color32
	{
		public static Color32 beige      = new Color32( 211, 176, 131, 255 );
		public static Color32 black      = new Color32(   0,   0,   0, 255 );
		public static Color32 blue       = new Color32(   0, 121, 241, 255 );
		public static Color32 brown      = new Color32( 127, 106,  79, 255 );
		public static Color32 clear      = new Color32(   0,   0,   0,   0 );
		public static Color32 cyan       = new Color32(   0, 255, 255, 255 );
		public static Color32 darkblue   = new Color32(   0,  82, 172, 255 );
		public static Color32 darkbrown  = new Color32(  76,  63,  47, 255 );
		public static Color32 darkgray   = new Color32(  80,  80,  80, 255 );
		public static Color32 darkgreen  = new Color32(   0, 117,  44, 255 );
		public static Color32 darkpurple = new Color32( 112,  31, 126, 255 );
		public static Color32 gold       = new Color32( 255, 203,   0, 255 );
		public static Color32 gray       = new Color32( 130, 130, 130, 255 );
		public static Color32 green      = new Color32(   0, 228,  48, 255 );
		public static Color32 lightgray  = new Color32( 200, 200, 200, 255 );
		public static Color32 lime       = new Color32(   0, 158,  47, 255 );
		public static Color32 magenta    = new Color32( 255,   0, 255, 255 );
		public static Color32 maroon     = new Color32( 190,  33,  55, 255 );
		public static Color32 orange     = new Color32( 255, 161,   0, 255 );
		public static Color32 pink       = new Color32( 255, 109, 194, 255 );
		public static Color32 purple     = new Color32( 200, 122, 255, 255 );
		public static Color32 red        = new Color32( 230,  41,  55, 255 );
		public static Color32 skyblue    = new Color32( 102, 191, 255, 255 );
		public static Color32 violet     = new Color32( 135,  60, 190, 255 );
		public static Color32 white      = new Color32( 255, 255, 255, 255 );
		public static Color32 yellow     = new Color32( 253, 249,   0, 255 );

		//-----------------------------------------------------------------------

		public Color32(byte brightness)
		{
			rValue = (byte)Math.Clamp(brightness, 0, 255);
			gValue = (byte)Math.Clamp(brightness, 0, 255);
			bValue = (byte)Math.Clamp(brightness, 0, 255);
			aValue = 255;
		}

		public Color32(int r, int g, int b, int a)
		{
			rValue = (byte)Math.Clamp(r, 0, 255);
			gValue = (byte)Math.Clamp(g, 0, 255);
			bValue = (byte)Math.Clamp(b, 0, 255);
			aValue = (byte)Math.Clamp(a, 0, 255);
		}

		public Color32(float r, float g, float b, float a)
		{
			rValue = (byte)Math.Clamp(r, 0, 255);
			gValue = (byte)Math.Clamp(g, 0, 255);
			bValue = (byte)Math.Clamp(b, 0, 255);
			aValue = (byte)Math.Clamp(a, 0, 255);
		}

		public static Color32 FromHSV(float hue, float saturation, float value, float alpha)
		{
			if (saturation == 0f)
			{
				return new Color(value, value, value, alpha);
			}

			float max = value < 0.5f ? value * (1f + saturation) : (value + saturation) - (value * saturation);
			float min = (value * 2f) - max;

			return new Color32(
				ColorExtension.RGBChannelFromHue(min, max, hue + (1 / 3f)) * 255f,
				ColorExtension.RGBChannelFromHue(min, max, hue) * 255f,
				ColorExtension.RGBChannelFromHue(min, max, hue - (1 / 3f)) * 255f,
				alpha * 255f
			);
		}

		public static implicit operator Color32(Color color)
		{
			return new Color32(color.r * 255, color.g * 255, color.b * 255, color.a * 255);
		}

		public static implicit operator Color(Color32 color)
		{
			return new Color(color.r / 255f, color.g / 255f, color.b / 255f, color.a / 255f);
		}

		public static explicit operator Color32(Vector4i vector)
		{
			return new Color32(vector.x, vector.y, vector.z, vector.w);
		}

		public static implicit operator Vector4i(Color32 color)
		{
			return new Vector4i(color.r, color.g, color.b, color.a);
		}

		//-----------------------------------------------------------------------

		private const string hexCharacters = "0123456789ABCDEF";

		private static byte HexToByte(char c)
		{
			return (byte)hexCharacters.IndexOf(char.ToUpper(c));
		}

		public static Color32 HexToColor(string hex)
		{
			int r = HexToByte(hex[0]) * 16 + HexToByte(hex[1]);
			int g = HexToByte(hex[2]) * 16 + HexToByte(hex[3]);
			int b = HexToByte(hex[4]) * 16 + HexToByte(hex[5]);

			return new Color32(r, g, b, 255);
		}

		public static Color32 HexToColorAlpha(string hex)
		{
			int r = HexToByte(hex[0]) * 16 + HexToByte(hex[1]);
			int g = HexToByte(hex[2]) * 16 + HexToByte(hex[3]);
			int b = HexToByte(hex[4]) * 16 + HexToByte(hex[5]);
			int a = HexToByte(hex[6]) * 16 + HexToByte(hex[7]);

			return new Color32(r, g, b, a);
		}

		public static Color32 HexToColor(int hex)
		{
			var r = (hex & 0x000000FF);
			var g = (hex & 0x0000FF00) >> 8;
			var b = (hex & 0x00FF0000) >> 16;

			return new Color32(r, g, b, 255);
		}

		public static Color32 HexToColorAlpha(int hex)
		{
			var r = (hex & 0x000000FF);
			var g = (hex & 0x0000FF00) >> 8;
			var b = (hex & 0x00FF0000) >> 16;
			var a = (hex & 0xFF000000) >> 24;

			return new Color32(r, g, b, a);
		}

		public static Color32 Lerp(Color32 a, Color32 b, float alpha, bool extrapolate = false)
		{
			alpha = extrapolate ? alpha : Math.Clamp01(alpha);

			return new Color32(
				(a.r * ( 1f - alpha ) + b.r * alpha),
				(a.g * ( 1f - alpha ) + b.g * alpha),
				(a.b * ( 1f - alpha ) + b.b * alpha),
				(a.a * ( 1f - alpha ) + b.a * alpha)
			);
		}
	}

	public static class Color32Extension
	{
		public static void Clamp(this Color32 color)
		{
			color.r = (byte)Math.Clamp(color.r, 0, 255);
			color.g = (byte)Math.Clamp(color.g, 0, 255);
			color.b = (byte)Math.Clamp(color.b, 0, 255);
			color.a = (byte)Math.Clamp(color.a, 0, 255);
		}

		public static float Brightness(this Color32 color)
		{
			float red = color.r;
			float green = color.g;
			float blue = color.b;
			float max = Math.Max(red, green, blue);

			return max / 255f;
		}

		public static float BrightnessPerceived(this Color32 color)
		{
			// Photometric/digital ITU BT.709
			// return (color.r * 0.2126f + color.g * 0.7152f + color.b * 0.0722f);

			// HSP Color model
			// return Math.Sqrt(Math.Pow(color.r, 2) * 0.299f + Math.Pow(color.g, 2) * 0.587f + Math.Pow(color.b, 2) * 0.114f);

			// Digital ITU BT.601
			return (color.r * 0.299f + color.g * 0.587f + color.b * 0.114f);
		}

		public static Color32 Grayscale(this Color32 color)
		{
			return new Color32(
				(color.r * 0.299f + color.g * 0.587f + color.b * 0.114f),
				(color.r * 0.299f + color.g * 0.587f + color.b * 0.114f),
				(color.r * 0.299f + color.g * 0.587f + color.b * 0.114f),
				color.a
			);
		}

		public static float Hue(this Color32 color)
		{
			float hueValue = 0f;
			float red = color.r / 255f;
			float green = color.g / 255f;
			float blue = color.b / 255f;
			float min = Math.Min(red, green, blue);
			float max = Math.Max(red, green, blue);

			if (red == max && blue == min)
			{
				hueValue = green / 6;
			}
			else if (green == max && blue == min)
			{
				hueValue = (red - 2) / -6;
			}
			else if (red == min && green == max)
			{
				hueValue = (blue + 2) / 6;
			}
			else if (red == min && blue == max)
			{
				hueValue = (green - 4) / -6;
			}
			else if (green == min && blue == max)
			{
				hueValue = (red + 4) / 6;
			}
			else if (red == max && green == min)
			{
				hueValue = (blue - 6) / -6;
			}

			return hueValue;
		}

		public static Color32 Invert(this Color32 color)
		{
			return new Color32(255 - color.r, 255 - color.g, 255 - color.b, color.a);
		}

		public static float Saturation(this Color32 color)
		{
			float saturation = 0f;
			float red = color.r / 255f;
			float green = color.g / 255f;
			float blue = color.b / 255f;
			float min = Math.Min(red, green, blue);
			float max = Math.Max(red, green, blue);

			if(max > 0)
			{
				saturation = (max - min) / max;
			}

			return saturation;
		}
	}
}
