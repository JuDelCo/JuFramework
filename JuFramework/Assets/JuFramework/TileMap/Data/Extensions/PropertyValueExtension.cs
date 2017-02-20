using System;
using System.Globalization;

namespace JuFramework.TileMapData
{
	public static class PropertyValueExtension
	{
		public static T ToEnum<T>(this PropertyValue propertyValue)
		{
			return (T)Enum.Parse(typeof(T), propertyValue.value, true);
		}

		public static bool ToBoolValue(this PropertyValue propertyValue)
		{
			return bool.Parse(propertyValue.value);
		}

		public static uint ToUIntValue(this PropertyValue propertyValue)
		{
			if (propertyValue.value.Contains("#"))
			{
				string color = propertyValue.value.Substring(1);

				if (color.Length != 8)
				{
					color = "FF" + color;
				}

				return uint.Parse(color, NumberStyles.HexNumber);
			}

			return uint.Parse(propertyValue.value, CultureInfo.InvariantCulture);
		}

		public static int ToIntValue(this PropertyValue propertyValue)
		{
			return int.Parse(propertyValue.value, CultureInfo.InvariantCulture);
		}

		public static float ToFloatValue(this PropertyValue propertyValue)
		{
			return float.Parse(propertyValue.value, CultureInfo.InvariantCulture);
		}

		public static string ToStringValue(this PropertyValue propertyValue)
		{
			return propertyValue.value;
		}
	}
}
