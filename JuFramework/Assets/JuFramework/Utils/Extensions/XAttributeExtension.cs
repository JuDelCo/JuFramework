using System.Globalization;
using System.Xml.Linq;

namespace JuFramework
{
	internal static class XAttributeExtension
	{
		public static bool ToBoolValue(this XAttribute attribute, bool defaultValue)
		{
			if (attribute == null)
			{
				return defaultValue;
			}

			bool value;

			if (bool.TryParse(attribute.Value, out value))
			{
				return value;
			}

			return int.Parse(attribute.Value, CultureInfo.InvariantCulture) == 1;
		}

		public static uint ToUIntValue(this XAttribute attribute)
		{
			return ToUIntValue(attribute, 0);
		}

		public static uint ToUIntValue(this XAttribute attribute, uint defaultValue)
		{
			if (attribute == null)
			{
				return defaultValue;
			}

			return uint.Parse(attribute.Value, CultureInfo.InvariantCulture);
		}

		public static int ToIntValue(this XAttribute attribute)
		{
			return ToIntValue(attribute, 0);
		}

		public static int ToIntValue(this XAttribute attribute, int defaultValue)
		{
			if (attribute == null)
			{
				return defaultValue;
			}

			return int.Parse(attribute.Value, CultureInfo.InvariantCulture);
		}

		public static float ToFloatValue(this XAttribute attribute)
		{
			return ToFloatValue(attribute, 0);
		}

		public static float ToFloatValue(this XAttribute attribute, float defaultValue)
		{
			if (attribute == null)
			{
				return defaultValue;
			}

			return float.Parse(attribute.Value, CultureInfo.InvariantCulture);
		}

		public static string ToStringValue(this XAttribute attribute)
		{
			return ToStringValue(attribute, string.Empty);
		}

		public static string ToStringValue(this XAttribute attribute, string defaultValue)
		{
			if (attribute == null)
			{
				return defaultValue;
			}

			return attribute.Value;
		}
	}
}
