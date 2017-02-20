using System.Collections.Generic;
using JuFramework.TileMapData;

namespace JuFramework.TileMapRenderer
{
	public class TileLayerChunkRenderer : Mesh
	{
		public bool removeEmptyTiles;

		public TileMap map;
		public TileLayer tileLayer;
		public TileSet tileSet;

		public int chunkTileOffsetX;
		public int chunkTileOffsetY;
		public int chunkTilesX;
		public int chunkTilesY;

		public List<Vector2f> meshTextureCoordinates;
		public bool meshHasChanges;
		public int[] tilePositionVertexMapping;
		public uint[,] cacheTilesRendered;
		public int pixelsPerUnit;

public override void ApplyMeshData(bool removeFromMemory = false) {}

		public void Initialize(TileMap map, TileLayer tileLayer, TileSet tileSet, int pixelsPerUnit, bool removeEmptyTiles)
		{
			this.map = map;
			this.tileLayer = tileLayer;
			this.tileSet = tileSet;
			this.pixelsPerUnit = pixelsPerUnit;

			this.removeEmptyTiles = removeEmptyTiles;

			SetDynamic(true);

			//SetTexture(tileSet.texture);
		}

		public void RebuildLayerChunk(int chunkTilesX, int chunkTilesY, int chunkTileOffsetX, int chunkTileOffsetY)
		{
			this.chunkTilesX = chunkTilesX;
			this.chunkTilesY = chunkTilesY;
			this.chunkTileOffsetX = chunkTileOffsetX;
			this.chunkTileOffsetY = chunkTileOffsetY;

			RebuildMesh();
		}

		private void RebuildMesh()
		{
			Clear();

			RebuildMeshVertices();
			RebuildMeshTriangles();
			RebuildMeshUV();
			RebuildTileCache();

			ApplyMeshData();
		}

		private void RebuildMeshVertices()
		{
			int numTiles = chunkTilesX * chunkTilesY;

			if(removeEmptyTiles)
			{
				numTiles = 0;

				for (int y = 0; y < chunkTilesY; ++y)
				{
					for (int x = 0; x < chunkTilesX; ++x)
					{
						if(tileLayer.GetTile(x + chunkTileOffsetX, y + chunkTileOffsetY).gid > 0)
						{
							++numTiles;
						}
					}
				}
			}

			int numVertex = numTiles * 4;
			var quads = new List<Vector3f>(numVertex);

			float pixelPerUnitScaleX = tileSet.tileWidth / (float)pixelsPerUnit;
			float pixelPerUnitScaleY = tileSet.tileHeight / (float)pixelsPerUnit;
			float tileUnitScaleX = ((float)tileSet.tileWidth / map.tileWidth) * pixelPerUnitScaleX;
			float tileUnitScaleY = ((float)tileSet.tileHeight / map.tileHeight) * pixelPerUnitScaleY;

			for (int y = 0; y < chunkTilesY; ++y)
			{
				for (int x = 0; x < chunkTilesX; ++x)
				{
					if(removeEmptyTiles && tileLayer.GetTile(x + chunkTileOffsetX, y + chunkTileOffsetY).gid == 0)
					{
						continue;
					}

					float xPos = (x + chunkTileOffsetX) * pixelPerUnitScaleX;
					float yPos = (y + chunkTileOffsetY) * pixelPerUnitScaleY;

					quads.Add(new Vector3f(xPos, -yPos + tileUnitScaleY, 0));
					quads.Add(new Vector3f(xPos + tileUnitScaleX, -yPos + tileUnitScaleY, 0));
					quads.Add(new Vector3f(xPos + tileUnitScaleX, -yPos, 0));
					quads.Add(new Vector3f(xPos, -yPos, 0));
				}
			}

			SetVertices(quads.ToArray());
		}

		private void RebuildMeshTriangles()
		{
			int numTiles = chunkTilesX * chunkTilesY;

			if(removeEmptyTiles)
			{
				numTiles = 0;

				for (int y = 0; y < chunkTilesY; ++y)
				{
					for (int x = 0; x < chunkTilesX; ++x)
					{
						if(tileLayer.GetTile(x + chunkTileOffsetX, y + chunkTileOffsetY).gid > 0)
						{
							++numTiles;
						}
					}
				}
			}

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

			SetIndices(triangles, Topology.Triangles);
		}

		private void RebuildMeshUV()
		{
			int numTiles = chunkTilesX * chunkTilesY;

			if(removeEmptyTiles)
			{
				numTiles = 0;

				for (int y = 0; y < chunkTilesY; ++y)
				{
					for (int x = 0; x < chunkTilesX; ++x)
					{
						if(tileLayer.GetTile(x + chunkTileOffsetX, y + chunkTileOffsetY).gid > 0)
						{
							++numTiles;
						}
					}
				}

				tilePositionVertexMapping = new int[(chunkTilesX * chunkTilesY)];

				int counter = 0;
				int tileCounter = 0;

				for (int y = 0; y < chunkTilesY; ++y)
				{
					for (int x = 0; x < chunkTilesX; ++x)
					{
						tilePositionVertexMapping[counter++] = (tileLayer.GetTile(x + chunkTileOffsetX, y + chunkTileOffsetY).gid > 0 ? tileCounter++ : 0);
					}
				}
			}
			else
			{
				tilePositionVertexMapping = null;
			}

			int numVertex = numTiles * 4;

			meshTextureCoordinates = new List<Vector2f>(numVertex);

			for (int i = 0; i < numVertex; ++i)
			{
				meshTextureCoordinates.Add(new Vector2f());
			}

			meshHasChanges = true;

			ApplyMeshTileChanges();
		}

		private void RebuildTileCache()
		{
			cacheTilesRendered = new uint[chunkTilesX, chunkTilesY];
		}

		public void SetTileID(int x, int y, uint gid, bool flipX = false, bool flipY = false, bool flipDiagonal = false)
		{
			if(cacheTilesRendered[x, y] == gid)
			{
				return;
			}

			uint id = gid - tileSet.firstGid;
			float left = (float)(tileSet.margin + ((id % tileSet.textureColumns) * (tileSet.tileWidth + tileSet.spacing))) / tileSet.textureWidth;
			float right = left + ((float)tileSet.tileWidth / tileSet.textureWidth);
			float top = (tileSet.margin + (Math.Floor(id / (float)tileSet.textureColumns) * (tileSet.tileHeight + tileSet.spacing))) / tileSet.textureHeight;
			float bottom = top + ((float)tileSet.tileHeight / tileSet.textureHeight);

			int tileStartIndex = ((y * chunkTilesX) + x) * 4;

			if(removeEmptyTiles && tilePositionVertexMapping != null)
			{
				tileStartIndex = tilePositionVertexMapping[(tileStartIndex / 4)] * 4;
			}

			meshTextureCoordinates[tileStartIndex + 0] = !flipDiagonal ? new Vector2f((flipX ? right : left), 1 - (flipY ? bottom : top)) : new Vector2f((flipX ? left : right), 1 - (flipY ? top : bottom));
			meshTextureCoordinates[tileStartIndex + 1] = new Vector2f((flipX ? left : right), 1 - (flipY ? bottom : top));
			meshTextureCoordinates[tileStartIndex + 2] = !flipDiagonal ? new Vector2f((flipX ? left : right), 1 - (flipY ? top : bottom)) : new Vector2f((flipX ? right : left), 1 - (flipY ? bottom : top));
			meshTextureCoordinates[tileStartIndex + 3] = new Vector2f((flipX ? right : left), 1 - (flipY ? top : bottom));

			cacheTilesRendered[x, y] = gid;
			meshHasChanges = true;
		}

		public void ApplyMeshTileChanges()
		{
			if(! meshHasChanges)
			{
				return;
			}

			SetTextureCoordinates(meshTextureCoordinates.ToArray());
			meshHasChanges = false;
		}
	}
}
