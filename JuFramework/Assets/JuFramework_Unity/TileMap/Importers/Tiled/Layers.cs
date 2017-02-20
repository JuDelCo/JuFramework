using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using JuFramework.TileMapData;

namespace JuFramework.TileMapTiledImporter
{
	public partial class TiledImporter
	{
		private Layer CreateLayer(XmlNode node)
		{
			if (node.GetName() == "objectgroup")
			{
				return CreateObjectGroup(node);
			}

			if (node.GetName() == "layer")
			{
				return CreateTileLayer(node);
			}

			throw new ArgumentException("Element is not a valid objectgroup or layer element");
		}

		private ObjectGroup CreateObjectGroup(XmlNode node)
		{
			var objectGroup = new ObjectGroup();

			foreach (var objectNode in node.GetNodes("object"))
			{
				objectGroup.objects.Add(CreateObject(objectNode));
			}

			SetLayerProperties(objectGroup, node);

			return objectGroup;
		}

		private TileLayer CreateTileLayer(XmlNode node)
		{
			var width = node.GetInt("width");
			var height = node.GetInt("height");

			var tileLayer = new TileLayer(width, height);

			var dataNode = node.GetNode("data");

			string encoding = dataNode.GetString("encoding");

			if (encoding == "base64")
			{
				var base64data = Convert.FromBase64String(dataNode.GetRawContent());
				Stream stream = new MemoryStream(base64data, false);

				string compression = dataNode.GetString("compression", "none");

				if (compression == "gzip")
				{
					stream = new GZipStream(stream, CompressionMode.Decompress, false);
				}
				else if (compression == "zlib")
				{
					throw new Exception("Unsupported compression (zlib)");
				}
				else if (compression != "none")
				{
					throw new Exception("Unsupported compression");
				}

				using (stream)
				{
					using (var reader = new BinaryReader(stream))
					{
						for (int y = 0; y < height; ++y)
						{
							for (int x = 0; x < width; ++x)
							{
								tileLayer.tiles[x, y] = CreateTile(reader.ReadUInt32());
							}
						}
					}
				}
			}
			else if (encoding == "csv")
			{
				int index = 0;

				foreach (string token in dataNode.GetRawContent().Split(','))
				{
					uint gid = uint.Parse(token.Trim(), CultureInfo.InvariantCulture);
					int x = index % width;
					int y = index / width;

					tileLayer.tiles[x, y] = CreateTile(gid);
					++index;
				}
			}
			else if (encoding == string.Empty) // XML
			{
				int index = 0;

				foreach (var tileNode in dataNode.GetNodes("tile"))
				{
					uint gid = tileNode.GetUInt("gid");
					int x = index % width;
					int y = index / width;

					tileLayer.tiles[x, y] = CreateTile(gid);
					++index;
				}
			}
			else
			{
				throw new Exception("Unknown encoding");
			}

			SetLayerProperties(tileLayer, node);

			return tileLayer;
		}

		private Tile CreateTile(uint idData)
		{
			const uint FLIPHORIZONTALFLAG = 0x80000000;
			const uint FLIPVERTICALFLAG = 0x40000000;
			const uint FLIPDIAGONALFLAG = 0x20000000;

			var tile = new Tile();

			tile.gid = idData & ~(FLIPHORIZONTALFLAG | FLIPVERTICALFLAG | FLIPDIAGONALFLAG);
			tile.flipHorizontal = (idData & FLIPHORIZONTALFLAG) != 0;
			tile.flipVertical = (idData & FLIPVERTICALFLAG) != 0;
			tile.flipDiagonal = (idData & FLIPDIAGONALFLAG) != 0;

			return tile;
		}

		private Layer SetLayerProperties(Layer layer, XmlNode node)
		{
			layer.name = node.GetString("name");
			layer.width = node.GetInt("width");
			layer.height = node.GetInt("height");
			layer.visible = node.GetBool("visible", true);
			layer.opacity = node.GetFloat("opacity", 1.0f);
			layer.offsetX = node.GetInt("offsetX");
			layer.offsetY = node.GetInt("offsetY");
			layer.properties.SetProperties(node.GetNode("properties"));

			return layer;
		}
	}
}
