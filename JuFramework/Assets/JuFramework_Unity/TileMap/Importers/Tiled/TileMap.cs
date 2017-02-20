using System;
using JuFramework.TileMapData;

namespace JuFramework.TileMapTiledImporter
{
	public partial class TiledImporter
	{
		public TileMap ParseMap(string xml)
		{
			var node = Xml.Parse(xml);
			var tileMap = new TileMap();

			tileMap.width = node.GetInt("width");
			tileMap.height = node.GetInt("height");
			tileMap.tileWidth = node.GetInt("tilewidth");
			tileMap.tileHeight = node.GetInt("tileheight");
			tileMap.orientation = (Orientation)Enum.Parse(typeof(Orientation), node.GetString("orientation", "orthogonal"), true);
			tileMap.renderOrder = (RenderOrder)Enum.Parse(typeof(RenderOrder), node.GetString("renderorder", "rightdown").Replace("-", ""), true);
			tileMap.backgroundColor = ParseBackgroundColor(node.GetString("backgroundcolor"));
			tileMap.properties.SetProperties(node.GetNode("properties"));

			foreach (var tileSetNode in node.GetNodes("tileset"))
			{
				tileMap.tileSets.Add(CreateTileSet(tileSetNode));
			}

			foreach (var layerNode in node.GetNodes())
			{
				if(layerNode.GetName() == "layer" || layerNode.GetName() == "objectgroup")
				{
					var newLayer = CreateLayer(layerNode);
					tileMap.layers.Add(newLayer);

					if(layerNode.GetName() == "layer")
					{
						tileMap.tileLayers.Add((TileLayer)newLayer);
					}
					else if (layerNode.GetName() == "objectgroup")
					{
						tileMap.objectGroups.Add((ObjectGroup)newLayer);
					}

					newLayer.map = tileMap;
				}
			}

			return tileMap;
		}

		private Color ParseBackgroundColor(string backgroundColor)
		{
			if(backgroundColor != string.Empty && backgroundColor.Contains("#"))
			{
				string color = backgroundColor.Substring(1);

				if (color.Length != 8)
				{
					color = "FF" + color;
				}

				uint colorUInt = uint.Parse(color, System.Globalization.NumberStyles.HexNumber);

				byte a = (byte)(colorUInt >> 24);
				byte r = (byte)(colorUInt >> 16);
				byte g = (byte)(colorUInt >> 8);
				byte b = (byte)(colorUInt >> 0);

				return new Color(r, g, b, a);
			}

			return Color.clear;
		}
	}
}
