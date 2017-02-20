using System.Collections.Generic;
using JuFramework.TileMapData;

namespace JuFramework
{
	public class TileMapMeshChunkGenerator
	{
		private TileMap tileMap;
		private TileLayer tileLayer;
		private TileSet tileSet;

		private Vector2i chunkTileSize;
		private Vector2i chunkTileOffset;
		private int pixelsPerUnit;
		private bool removeEmptyTiles;

		private Mesh mesh;
		private int numTiles;

		public TileMapMeshChunkGenerator(TileMap tileMap, TileLayer tileLayer, TileSet tileSet)
		{
			this.tileMap = tileMap;
			this.tileLayer = tileLayer;
			this.tileSet = tileSet;
		}

		public Mesh GenerateMeshChunk(Vector2i chunkTileSize, Vector2i chunkTileOffset, int pixelsPerUnit, bool removeEmptyTiles)
		{
			this.chunkTileSize = chunkTileSize;
			this.chunkTileOffset = chunkTileOffset;
			this.pixelsPerUnit = pixelsPerUnit;
			this.removeEmptyTiles = removeEmptyTiles;

			mesh = new UnityMesh(true);
			((UnityMesh)mesh).GetUnityMesh().name = "Mesh chunk";

			numTiles = CountTiles();

			RebuildMeshVertices();
			RebuildMeshTriangles();
			RebuildMeshUV();

			mesh.ApplyMeshData(false);

			return mesh;
		}

		private void RebuildMeshVertices()
		{
			int numVertex = numTiles * 4;
			var quads = new List<Vector3f>(numVertex);

			float pixelPerUnitScaleX = tileSet.tileWidth / (float)pixelsPerUnit;
			float pixelPerUnitScaleY = tileSet.tileHeight / (float)pixelsPerUnit;
			float tileUnitScaleX = ((float)tileSet.tileWidth / tileMap.tileWidth) * pixelPerUnitScaleX;
			float tileUnitScaleY = ((float)tileSet.tileHeight / tileMap.tileHeight) * pixelPerUnitScaleY;

			for (int y = 0; y < chunkTileSize.y; ++y)
			{
				for (int x = 0; x < chunkTileSize.x; ++x)
				{
					if(removeEmptyTiles && tileLayer.GetTile(x + chunkTileOffset.x, y + chunkTileOffset.y).gid == 0)
					{
						continue;
					}

					float xPos = (x + chunkTileOffset.x) * pixelPerUnitScaleX;
					float yPos = (y + chunkTileOffset.y) * pixelPerUnitScaleY;

					quads.Add(new Vector3f(xPos, -yPos + tileUnitScaleY, 0));
					quads.Add(new Vector3f(xPos + tileUnitScaleX, -yPos + tileUnitScaleY, 0));
					quads.Add(new Vector3f(xPos + tileUnitScaleX, -yPos, 0));
					quads.Add(new Vector3f(xPos, -yPos, 0));
				}
			}

			mesh.SetVertices(quads.ToArray());
		}

		private void RebuildMeshTriangles()
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

			mesh.SetIndices(triangles, Mesh.Topology.Triangles);
		}

		private void RebuildMeshUV()
		{
			int[] tilePositionVertexMapping;

			if(removeEmptyTiles)
			{
				numTiles = 0;

				for (int y = 0; y < chunkTileSize.y; ++y)
				{
					for (int x = 0; x < chunkTileSize.x; ++x)
					{
						if(tileLayer.GetTile(x + chunkTileOffset.x, y + chunkTileOffset.y).gid > 0)
						{
							++numTiles;
						}
					}
				}

				tilePositionVertexMapping = new int[(chunkTileSize.x * chunkTileSize.y)];

				int counter = 0;
				int tileCounter = 0;

				for (int y = 0; y < chunkTileSize.y; ++y)
				{
					for (int x = 0; x < chunkTileSize.x; ++x)
					{
						tilePositionVertexMapping[counter++] = (tileLayer.GetTile(x + chunkTileOffset.x, y + chunkTileOffset.y).gid > 0 ? tileCounter++ : 0);
					}
				}
			}
			else
			{
				tilePositionVertexMapping = null;
			}

			int numVertex = numTiles * 4;

			var meshTextureCoordinates = new List<Vector2f>(numVertex);

			for (int i = 0; i < numVertex; ++i)
			{
				meshTextureCoordinates.Add(new Vector2f());
			}

			for (int y = 0; y < chunkTileSize.y; ++y)
			{
				for (int x = 0; x < chunkTileSize.x; ++x)
				{
					var mapTile = tileLayer.GetTile(x + chunkTileOffset.x, y + chunkTileOffset.y);

					if(mapTile.gid > 0)
					{
						var flipX = mapTile.flipHorizontal;
						var flipY = mapTile.flipVertical;
						var flipDiagonal = mapTile.flipDiagonal;

						uint id = mapTile.gid - tileSet.firstGid;
						float left = (float)(tileSet.margin + ((id % tileSet.textureColumns) * (tileSet.tileWidth + tileSet.spacing))) / tileSet.textureWidth;
						float right = left + ((float)tileSet.tileWidth / tileSet.textureWidth);
						float top = (tileSet.margin + (Math.Floor(id / (float)tileSet.textureColumns) * (tileSet.tileHeight + tileSet.spacing))) / tileSet.textureHeight;
						float bottom = top + ((float)tileSet.tileHeight / tileSet.textureHeight);

						int tileStartIndex = ((y * chunkTileSize.x) + x) * 4;

						if(removeEmptyTiles && tilePositionVertexMapping != null)
						{
							tileStartIndex = tilePositionVertexMapping[(tileStartIndex / 4)] * 4;
						}

						meshTextureCoordinates[tileStartIndex + 0] = !flipDiagonal ? new Vector2f((flipX ? right : left), 1 - (flipY ? bottom : top)) : new Vector2f((flipX ? left : right), 1 - (flipY ? top : bottom));
						meshTextureCoordinates[tileStartIndex + 1] = new Vector2f((flipX ? left : right), 1 - (flipY ? bottom : top));
						meshTextureCoordinates[tileStartIndex + 2] = !flipDiagonal ? new Vector2f((flipX ? left : right), 1 - (flipY ? top : bottom)) : new Vector2f((flipX ? right : left), 1 - (flipY ? bottom : top));
						meshTextureCoordinates[tileStartIndex + 3] = new Vector2f((flipX ? right : left), 1 - (flipY ? top : bottom));
					}
				}
			}

			mesh.SetTextureCoordinates(meshTextureCoordinates.ToArray());
		}

		private int CountTiles()
		{
			int numTiles = chunkTileSize.x * chunkTileSize.y;

			if(removeEmptyTiles)
			{
				numTiles = 0;

				for (int y = 0; y < chunkTileSize.y; ++y)
				{
					for (int x = 0; x < chunkTileSize.x; ++x)
					{
						if(tileLayer.GetTile(x + chunkTileOffset.x, y + chunkTileOffset.y).gid > 0)
						{
							++numTiles;
						}
					}
				}
			}

			return numTiles;
		}
	}
}
