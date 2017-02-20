using System.Collections.Generic;
using System.Xml.Linq;

namespace JuFramework
{
	internal static class XElementExtension
	{
		public static string GetRawContent(this XElement element)
		{
			return element.Value;
		}

		public static bool HasNode(this XElement element, string nodeName)
		{
			return (element.Element(nodeName) != null);
		}

		public static XElement GetNode(this XElement element, string nodeName)
		{
			return element.Element(nodeName);
		}

		public static IEnumerable<XElement> GetNodes(this XElement element, string nodeName)
		{
			return element.Elements(nodeName);
		}

		public static IEnumerable<XElement> GetNodes(this XElement element)
		{
			return element.Elements();
		}

		public static bool HasValue(this XElement element, string attributeName)
		{
			return (element.Attribute(attributeName) != null);
		}

		public static bool GetBoolValue(this XElement element, string attributeName, bool defaultValue)
		{
			return element.Attribute(attributeName).ToBoolValue(defaultValue);
		}

		public static uint GetUIntValue(this XElement element, string attributeName)
		{
			return element.GetUIntValue(attributeName, 0);
		}

		public static uint GetUIntValue(this XElement element, string attributeName, uint defaultValue)
		{
			return element.Attribute(attributeName).ToUIntValue(defaultValue);
		}

		public static int GetIntValue(this XElement element, string attributeName)
		{
			return element.GetIntValue(attributeName, 0);
		}

		public static int GetIntValue(this XElement element, string attributeName, int defaultValue)
		{
			return element.Attribute(attributeName).ToIntValue(defaultValue);
		}

		public static float GetFloatValue(this XElement element, string attributeName)
		{
			return element.GetFloatValue(attributeName, 0f);
		}

		public static float GetFloatValue(this XElement element, string attributeName, float defaultValue)
		{
			return element.Attribute(attributeName).ToFloatValue(defaultValue);
		}

		public static string GetStringValue(this XElement element, string attributeName)
		{
			return element.GetStringValue(attributeName, string.Empty);
		}

		public static string GetStringValue(this XElement element, string attributeName, string defaultValue)
		{
			return element.Attribute(attributeName).ToStringValue(defaultValue);
		}
	}
}
