using JuFramework.TileMapData;
using System.Collections.Generic;

namespace JuFramework.Drawing
{
	public static class TileMap
	{
		private static Material mapMaterial = Core.resource.CreateMaterial("Sprites/Default");
		private static Mesh meshCache = Core.resource.CreateMesh(true);
		private static int numTiles = 0;

		private static int CountTiles(TileLayer layer, IntRect cells)
		{
			int numTiles = 0;

			int left = cells.left;
			int right = cells.right;
			int top = cells.top;
			int bottom = cells.bottom;

			for (int y = top; y < bottom; ++y)
			{
				for (int x = left; x < right; ++x)
				{
					if(layer.tiles[x, y].gid > 0)
					{
						++numTiles;
					}
				}
			}

			return numTiles;
		}

		private static void RebuildMesh(TileLayer layer, IntRect cells, Vector2f position, TileSet tileSet)
		{
			numTiles = CountTiles(layer, cells);

			meshCache.Clear();

			RebuildMeshVertices(layer, cells, position, new Vector2i(tileSet.tileWidth, tileSet.tileHeight), new Vector2i(layer.map.tileWidth, layer.map.tileHeight));
			RebuildMeshTriangles();
			RebuildMeshUV(layer, cells, tileSet);

			meshCache.ApplyMeshData();
		}

		private static void RebuildMeshVertices(TileLayer layer, IntRect cells, Vector2f position, Vector2i tileSize, Vector2i mapTileSize)
		{
			int numVertex = numTiles * 4;
			var quads = new List<Vector3f>(numVertex);

			float xPos, yPos;

			for (int y = 0; y < cells.height; ++y)
			{
				for (int x = 0; x < cells.width; ++x)
				{
					if(layer.GetTile(x + cells.left, y + cells.top).gid == 0)
					{
						continue;
					}

					xPos = (x * mapTileSize.x) + position.x;
					yPos = (y * mapTileSize.y) + position.y;

					quads.Add(new Vector3f(xPos, -yPos + tileSize.y, 0));
					quads.Add(new Vector3f(xPos + tileSize.x, -yPos + tileSize.y, 0));
					quads.Add(new Vector3f(xPos + tileSize.x, -yPos, 0));
					quads.Add(new Vector3f(xPos, -yPos, 0));
				}
			}

			meshCache.SetVertices(quads.ToArray());
		}

		private static void RebuildMeshTriangles()
		{
			int numTriangleIndices = numTiles * 6;
			int[] triangles = new int[numTriangleIndices];

			uint triangleVertexIndex = 0;
			int tileVertexIndex = 0;

			for (uint tileIndex = 0; tileIndex < numTiles; ++tileIndex)
			{
				triangles[triangleVertexIndex + 0] = (tileVertexIndex + 0);
				triangles[triangleVertexIndex + 1] = (tileVertexIndex + 1);
				triangles[triangleVertexIndex + 2] = (tileVertexIndex + 2);

				triangles[triangleVertexIndex + 3] = (tileVertexIndex + 0);
				triangles[triangleVertexIndex + 4] = (tileVertexIndex + 2);
				triangles[triangleVertexIndex + 5] = (tileVertexIndex + 3);

				tileVertexIndex += 4;
				triangleVertexIndex += 6;
			}

			meshCache.SetIndices(triangles, Mesh.Topology.Triangles);
		}

		private static void RebuildMeshUV(TileLayer layer, IntRect cells, TileSet tileSet)
		{
			var meshTextureCoordinates = new List<Vector2f>(numTiles * 4);

			Tile tile;
			float left, right, top, bottom;
			bool flipX, flipY, flipDiagonal;

			for (int y = 0; y < cells.height; ++y)
			{
				for (int x = 0; x < cells.width; ++x)
				{
					tile = layer.GetTile(x + cells.left, y + cells.top);

					if(tile.gid == 0)
					{
						continue;
					}

					left = (float)(tileSet.margin + (((tile.gid - tileSet.firstGid) % tileSet.textureColumns) * (tileSet.tileWidth + tileSet.spacing))) / tileSet.textureWidth;
					right = left + ((float)tileSet.tileWidth / tileSet.textureWidth);
					top = (tileSet.margin + ((float)Math.Floor((tile.gid - tileSet.firstGid) / tileSet.textureColumns) * (tileSet.tileHeight + tileSet.spacing))) / tileSet.textureHeight;
					bottom = top + ((float)tileSet.tileHeight / tileSet.textureHeight);

					flipX = tile.flipHorizontal;
					flipY = tile.flipVertical;
					flipDiagonal = tile.flipDiagonal;

					meshTextureCoordinates.Add( !flipDiagonal ? new Vector2f((flipX ? right : left), 1 - (flipY ? bottom : top)) : new Vector2f((flipX ? left : right), 1 - (flipY ? top : bottom)) );
					meshTextureCoordinates.Add( new Vector2f((flipX ? left : right), 1 - (flipY ? bottom : top)) );
					meshTextureCoordinates.Add( !flipDiagonal ? new Vector2f((flipX ? left : right), 1 - (flipY ? top : bottom)) : new Vector2f((flipX ? right : left), 1 - (flipY ? bottom : top)) );
					meshTextureCoordinates.Add( new Vector2f((flipX ? right : left), 1 - (flipY ? top : bottom)) );
				}
			}

			meshCache.SetTextureCoordinates(meshTextureCoordinates.ToArray());
		}

		//-----------------------------------------------------------------------

		public static void DrawMap(JuFramework.TileMap tileMap, int layerId)
		{
			DrawMap(tileMap, layerId, Vector2f.zero);
		}

		public static void DrawMap(JuFramework.TileMap tileMap, int layerId, Vector2f position)
		{
			var tileLayer = tileMap.tileLayers[layerId];

			DrawMap(tileMap, layerId, new IntRect(0, 0, tileLayer.width, tileLayer.height), position);
		}

		public static void DrawMap(JuFramework.TileMap tileMap, int layerId, IntRect cells, Vector2f position)
		{
			var tileLayer = tileMap.tileLayers[layerId];
			var tileSet = tileLayer.GetTileSet();

			RebuildMesh(tileLayer, cells, position, tileSet);

			mapMaterial.Set("_MainTex", tileSet.texture);
			mapMaterial.Use();

			Core.graphics.DrawMesh(meshCache);
		}
	}
}

/*for (int y = cells.top; y < cells.bottom; ++y)
{
	for (int x = cells.left; x < (cells.left + cells.width); ++x)
	{
		var tilePosition = new Vector2i(x * tileSet.tileWidth, -y * tileSet.tileHeight);
		var tile = layer.GetTile(x, y);

		if(tile.gid > 0)
		{
			int id = (int)(tile.gid - tileSet.firstGid);
			int left = tileSet.margin + ((id % tileSet.textureColumns) * (tileSet.tileWidth + tileSet.spacing));
			int top = tileSet.tileHeight - tileSet.margin + (Math.Floor(id / (float)tileSet.textureColumns) * (tileSet.tileHeight + tileSet.spacing));

			// todo: tile flips
			Sprites.DrawQuad(tileSet.texture.GetSize(), tilePosition, new IntRect(left, top, tileSet.tileWidth, tileSet.tileHeight), Color.white);
		}
	}
}*/
