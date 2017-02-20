using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace JuFramework
{
	public static class Xml
	{
		public static XmlNode Parse(string xml)
		{
			return new XmlNode(XDocument.Parse(xml).Root);
		}
	}

	public class XmlNode
	{
		private XElement node;

		public XmlNode(XElement node)
		{
			this.node = node;
		}

		public string GetName()
		{
			return node.Name.LocalName;
		}

		public string GetRawContent()
		{
			return node.GetRawContent();
		}

		public bool HasNode(string nodeName)
		{
			return node.HasNode(nodeName);
		}

		public XmlNode GetNode(string nodeName)
		{
			var childNode = node.GetNode(nodeName);

			if(childNode != null)
			{
				return new XmlNode(childNode);
			}

			return null;
		}

		public IEnumerable<XmlNode> GetNodes(string nodeName)
		{
			return node.GetNodes(nodeName).Select(node => new XmlNode(node));
		}

		public IEnumerable<XmlNode> GetNodes()
		{
			return node.GetNodes().Select(node => new XmlNode(node));
		}

		public bool HasValue(string attributeName)
		{
			return node.HasValue(attributeName);
		}

		public bool GetBool(string attributeName, bool defaultValue)
		{
			return node.GetBoolValue(attributeName, defaultValue);
		}

		public uint GetUInt(string attributeName)
		{
			return node.GetUIntValue(attributeName);
		}

		public uint GetUInt(string attributeName, uint defaultValue)
		{
			return node.GetUIntValue(attributeName, defaultValue);
		}

		public int GetInt(string attributeName)
		{
			return node.GetIntValue(attributeName);
		}

		public int GetInt(string attributeName, int defaultValue)
		{
			return node.GetIntValue(attributeName, defaultValue);
		}

		public float GetFloat(string attributeName)
		{
			return node.GetFloatValue(attributeName);
		}

		public float GetFloat(string attributeName, float defaultValue)
		{
			return node.GetFloatValue(attributeName, defaultValue);
		}

		public string GetString(string attributeName)
		{
			return node.GetStringValue(attributeName);
		}

		public string GetString(string attributeName, string defaultValue)
		{
			return node.GetStringValue(attributeName, defaultValue);
		}
	}
}
