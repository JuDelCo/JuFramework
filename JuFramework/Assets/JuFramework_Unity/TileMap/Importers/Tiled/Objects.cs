using System;
using System.Globalization;
using JuFramework.TileMapData;

namespace JuFramework.TileMapTiledImporter
{
	public partial class TiledImporter
	{
		private TileMapData.Object CreateObject(XmlNode node)
		{
			if (node.HasValue("gid"))
			{
				return CreateTileObject(node);
			}

			if (node.HasNode("polygon"))
			{
				return CreatePolygonObject(node);
			}

			if (node.HasNode("polyline"))
			{
				return CreatePolyLineObject(node);
			}

			if (node.HasNode("ellipse"))
			{
				return CreateEllipseObject(node);
			}

			return CreateRectangleObject(node);
		}

		private TileObject CreateTileObject(XmlNode node)
		{
			var tileObject = new TileObject();

			tileObject.gid = node.GetUInt("gid");

			SetObjectProperties(tileObject, node);

			return tileObject;
		}

		private PolygonObject CreatePolygonObject(XmlNode node)
		{
			var polygonObject = new PolygonObject();

			string pointsData = node.GetNode("polygon").GetString("points");

			foreach (string p in pointsData.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
			{
				var pSplit = p.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				polygonObject.points.Add(new Vector2f(float.Parse(pSplit[0], CultureInfo.InvariantCulture), float.Parse(pSplit[1], CultureInfo.InvariantCulture)));
			}

			SetObjectProperties(polygonObject, node);

			return polygonObject;
		}

		private PolyLineObject CreatePolyLineObject(XmlNode node)
		{
			var polyLineObject = new PolyLineObject();

			string pointsData = node.GetNode("polyline").GetString("points");

			foreach (string p in pointsData.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
			{
				var pSplit = p.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				polyLineObject.points.Add(new Vector2f(float.Parse(pSplit[0], CultureInfo.InvariantCulture), float.Parse(pSplit[1], CultureInfo.InvariantCulture)));
			}

			SetObjectProperties(polyLineObject, node);

			return polyLineObject;
		}

		private EllipseObject CreateEllipseObject(XmlNode node)
		{
			var ellipseObject = new EllipseObject();

			SetObjectProperties(ellipseObject, node);

			return ellipseObject;
		}

		private RectangleObject CreateRectangleObject(XmlNode node)
		{
			var rectangleObject = new RectangleObject();

			SetObjectProperties(rectangleObject, node);

			return rectangleObject;
		}

		private TileMapData.Object SetObjectProperties(TileMapData.Object baseObject, XmlNode node)
		{
			baseObject.id = node.GetUInt("id");
			baseObject.name = node.GetString("name");
			baseObject.type = node.GetString("type");
			baseObject.x = node.GetFloat("x");
			baseObject.y = node.GetFloat("y");
			baseObject.width = node.GetFloat("width");
			baseObject.height = node.GetFloat("height");
			baseObject.rotation = node.GetFloat("rotation");
			baseObject.visible = node.GetBool("visible", true);
			baseObject.properties.SetProperties(node.GetNode("properties"));

			return baseObject;
		}
	}
}
