using System.IO;
using JuFramework.TileMapData;

// TODO: Delay asset loads (TiledFileObject & Texture)

namespace JuFramework.TileMapTiledImporter
{
	public partial class TiledImporter
	{
		private TileSet CreateTileSet(XmlNode node)
		{
			var tileSet = new TileSet();

			tileSet.firstGid = node.GetUInt("firstgid");

			if (node.HasValue("source"))
			{
				var resourcePath = "Tilemaps/" + Path.GetFileNameWithoutExtension(node.GetString("source"));
				var asset = UnityEngine.Resources.Load(resourcePath + ".tileset", typeof(TiledFileObject)) as TiledFileObject;

				node = Xml.Parse(asset.data);
			}

			tileSet.name = node.GetString("name");
			tileSet.margin = node.GetInt("margin");
			tileSet.spacing = node.GetInt("spacing");
			tileSet.tileCount = node.GetInt("tilecount");
			tileSet.tileWidth = node.GetInt("tilewidth");
			tileSet.tileHeight = node.GetInt("tileheight");

			var imageNode = node.GetNode("image");
			tileSet.texturePath = imageNode.GetString("source");
			tileSet.textureWidth = imageNode.GetInt("width");
			tileSet.textureHeight = imageNode.GetInt("height");
			tileSet.textureColumns = Math.Ceil(((tileSet.textureWidth - (tileSet.margin * 2)) + tileSet.spacing) / (tileSet.tileWidth + tileSet.spacing));

			if(tileSet.texturePath != string.Empty)
			{
				var resourcePath = "Tilesets/" + Path.GetFileNameWithoutExtension(tileSet.texturePath);
				tileSet.texture = new UnityTexture(UnityEngine.Resources.Load(resourcePath, typeof(UnityEngine.Texture2D)) as UnityEngine.Texture2D);
			}

			if(tileSet.tileCount == 0)
			{
				tileSet.tileCount = (tileSet.textureWidth / tileSet.tileWidth) * (tileSet.textureHeight / tileSet.tileHeight);
			}

			if(node.HasNode("tileoffset"))
			{
				tileSet.tileOffsetX = node.GetNode("tileoffset").GetInt("x");
				tileSet.tileOffsetY = node.GetNode("tileoffset").GetInt("y");
			}

			tileSet.properties.SetProperties(node.GetNode("properties"));

			foreach (var tileNode in node.GetNodes("tile"))
			{
				if(tileNode.HasNode("animation"))
				{
					var animationCollection = new AnimationCollection();

					foreach (var frameNode in tileNode.GetNode("animation").GetNodes("frame"))
					{
						animationCollection.frames.Add(new AnimationFrame(frameNode.GetUInt("tileid") + tileSet.firstGid, frameNode.GetUInt("duration")));
					}

					tileSet.tileAnimations[tileNode.GetUInt("id") + tileSet.firstGid] = animationCollection;
				}

				if(tileNode.HasNode("properties"))
				{
					tileSet.tileProperties[tileNode.GetUInt("id") + tileSet.firstGid].SetProperties(tileNode.GetNode("properties"));
				}
			}

			return tileSet;
		}
	}
}
