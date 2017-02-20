using System.Collections.Generic;
using JuFramework;

public static class Tests
{
	private static Mesh gridMesh;
	private static List<Texture> textures;
	private static Material gridMaterial;
	private static Material shapesMaterial;

	public static void Draw(Graphics graphics)
	{
		if(textures == null)
		{
			textures = new List<Texture>(new Texture[5]
			{
				Core.resource.LoadTexture("Sprites/Blender_UV"),
				Core.resource.LoadTexture("Sprites/Sizes"),
				Core.resource.LoadTexture("Sprites/TestTexture"),
				Core.resource.LoadTexture("Sprites/UV_Grid_Lrg"),
				Core.resource.LoadTexture("Sprites/UV_Grid_Sm")
			});
		}

		//Graphics_Test_Points(graphics); // TODO: Fix bug cuando la cámara está rotada (o con perspectiva)
		//Graphics_Test_Lines(graphics);
		//Graphics_Test_Polygons(graphics);
		//Graphics_Test_Circles(graphics);
		//Graphics_Test_Rectangles(graphics);
		//Graphics_Test_Triangles(graphics);
		//Graphics_Test_Textures_1(graphics);
		//Graphics_Test_Textures_2(graphics);
		//Graphics_Test_Textures_3(graphics);
		//Graphics_Test_Meshes_1(graphics);
		Graphics_Test_Meshes_2(graphics);
		//Graphics_Test_TexturePositions(graphics);
	}

	private static void Graphics_Test_Points(Graphics graphics)
	{
		if(shapesMaterial == null)
		{
			shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		}

		shapesMaterial.SetPass(0);

		for (int y = 0; y < Core.screen.height; y+=20)
		{
			for (int x = 0; x < Core.screen.width; x+=20)
			{
				Shapes.DrawPixel(new Vector2i(x, y), Random.Color());
			}
		}
	}

	private static void Graphics_Test_Lines(Graphics graphics)
	{
		if(shapesMaterial == null)
		{
			shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		}

		shapesMaterial.SetPass(0);

		for (int i = 0; i < 10; ++i)
		{
			var color = Random.Color();
			var line1 = new Vector2f(Random.Range(0, Core.screen.width / 2), Random.Range(0, Core.screen.height / 2));
			var line2 = new Vector2f(Random.Range(Core.screen.width / 2, Core.screen.width), Random.Range(Core.screen.height / 2, Core.screen.height));
			Shapes.DrawLine(line1, line2, color);
		}
	}

	private static void Graphics_Test_Polygons(Graphics graphics)
	{
		if(shapesMaterial == null)
		{
			shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		}

		shapesMaterial.SetPass(0);

		for (int i = 0; i < 1; ++i)
		{
			var color = Random.Color();
			var points = new Vector2f[5];

			for (int j = 0; j < 5; ++j)
			{
				points[j] = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			}

			Shapes.DrawPolygon(points, color);
		}
	}

	private static void Graphics_Test_Circles(Graphics graphics)
	{
		if(shapesMaterial == null)
		{
			shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		}

		shapesMaterial.SetPass(0);

		for (int i = 0; i < 1; ++i)
		{
			var color = Random.Color();
			var position = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var radius = Random.Range(50, 200);

			Shapes.DrawCircleFill(position, radius, color, 36);
		}
	}

	private static void Graphics_Test_Rectangles(Graphics graphics)
	{
		if(shapesMaterial == null)
		{
			shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		}

		shapesMaterial.SetPass(0);

		for (int i = 0; i < 1; ++i)
		{
			var color = Random.Color();
			var position = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var width = Random.Range(50, 200);
			var height = Random.Range(50, 200);

			Shapes.DrawRectangle(position, width, height, color);
		}
	}

	private static void Graphics_Test_Triangles(Graphics graphics)
	{
		if(shapesMaterial == null)
		{
			shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		}

		shapesMaterial.SetPass(0);

		for (int i = 0; i < 1; ++i)
		{
			var color = Random.Color();
			var v1 = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var v2 = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var v3 = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));

			Shapes.DrawTriangleFill(v1, v2, v3, color);
		}
	}

	private static void Graphics_Test_Textures_1(Graphics graphics)
	{
		for (int i = 0; i < 10; ++i)
		{
			var position = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var color = Random.Color();
			var textureId = Random.Range(0, textures.Count);
			var sourceRectBottomLeft = new Vector2i(Random.Range(0, textures[textureId].GetWidth()), Random.Range(0, textures[textureId].GetHeight()));
			var sourceRectangle = new IntRect(sourceRectBottomLeft.x, sourceRectBottomLeft.y,
				Random.Range(sourceRectBottomLeft.x, textures[textureId].GetWidth()),
				Random.Range(sourceRectBottomLeft.y, textures[textureId].GetHeight()));

			graphics.DrawTexture(textures[textureId], position, sourceRectangle, color);
		}
	}

	private static void Graphics_Test_Textures_2(Graphics graphics)
	{
		for (int i = 0; i < 10; ++i)
		{
			var color = Random.Color();
			var textureId = Random.Range(0, textures.Count);
			var destinationRectBottomLeft = new Vector2i(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var destinationRectangle = new IntRect(destinationRectBottomLeft.x, destinationRectBottomLeft.y,
				Random.Range(0, Core.screen.width - destinationRectBottomLeft.x),
				Random.Range(0, Core.screen.height - destinationRectBottomLeft.y));

			graphics.DrawTexture(textures[textureId], destinationRectangle, color);
		}
	}

	private static void Graphics_Test_Textures_3(Graphics graphics)
	{
		for (int i = 0; i < 10; ++i)
		{
			var color = Random.Color();
			var textureId = Random.Range(0, textures.Count);
			var sourceRectBottomLeft = new Vector2i(Random.Range(0, textures[textureId].GetWidth()), Random.Range(0, textures[textureId].GetHeight()));
			var sourceRectangle = new IntRect(sourceRectBottomLeft.x, sourceRectBottomLeft.y,
				Random.Range(sourceRectBottomLeft.x, textures[textureId].GetWidth()),
				Random.Range(sourceRectBottomLeft.y, textures[textureId].GetHeight()));
			var destinationRectBottomLeft = new Vector2i(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var destinationRectangle = new IntRect(destinationRectBottomLeft.x, destinationRectBottomLeft.y,
				Random.Range(0, Core.screen.width - destinationRectBottomLeft.x),
				Random.Range(0, Core.screen.height - destinationRectBottomLeft.y));

			graphics.DrawTexture(textures[textureId], destinationRectangle, sourceRectangle, color);
		}
	}

	private static void Graphics_Test_Meshes_1(Graphics graphics)
	{
		// TODO: Optimizar
		var meshMaterial = Core.resource.CreateMaterial("Mobile/VertexLit"); // Standard
		meshMaterial.SetPass(0);

		var transform = new Transform3D();
		transform.Rotate(new Vector3f(Core.fixedTime.seconds, 0f, 0f));
		graphics.SetModelToWorldMatrix(transform.GetLocalToWorldMatrix());
		graphics.DrawMesh(CreateTorus(10f, 3f, 24, 18));
	}

	private static void Graphics_Test_Meshes_2(Graphics graphics)
	{
		if(gridMesh == null)
		{
			gridMesh = CreateGrid(100);
		}

		if(gridMaterial == null)
		{
			gridMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		}

		gridMaterial.SetPass(0);
		graphics.DrawMesh(gridMesh);
	}

	private static void Graphics_Test_TexturePositions(Graphics graphics)
	{
		var texture = Core.resource.LoadTexture("Sprites/TestTexture");

		for (int i = 0; i < 9; ++i)
		{
			for (int j = 0; j < 5; ++j)
			{
				graphics.DrawTexture(texture, new IntRect(i * 100 + 25, j * 100 + 25, 100, 100));
			}
		}
	}

	private static Mesh CreateGrid(int gridSize)
	{
		var thinLineColor = new Color32(80);
		var thickLineColor = new Color32(100);

		var vertices = new List<Vector3f>();
		var colors = new List<Color32>();
		var offset = -(gridSize / 2);

		for (int x = 0; x <= gridSize; ++x)
		{
			if(x == gridSize / 2)
			{
				vertices.Add(new Vector3f(offset + x, 0, offset));
				vertices.Add(new Vector3f(offset + x, 0, offset + gridSize / 2));
				vertices.Add(new Vector3f(offset + x, 0, offset + gridSize / 2));
				vertices.Add(new Vector3f(offset + x, 0, offset + gridSize));

				colors.Add(thickLineColor);
				colors.Add(thickLineColor);
				colors.Add(Color32.blue);
				colors.Add(Color32.blue);

				continue;
			}

			vertices.Add(new Vector3f(offset + x, 0, offset));
			vertices.Add(new Vector3f(offset + x, 0, offset + gridSize));

			if(x % 10 == 0)
			{
				colors.Add(thickLineColor);
				colors.Add(thickLineColor);
			}
			else
			{
				colors.Add(thinLineColor);
				colors.Add(thinLineColor);
			}
		}

		for (int y = 0; y <= gridSize; ++y)
		{
			if(y == gridSize / 2)
			{
				vertices.Add(new Vector3f(offset, 0, offset + y));
				vertices.Add(new Vector3f(offset + gridSize / 2, 0, offset + y));
				vertices.Add(new Vector3f(offset + gridSize / 2, 0, offset + y));
				vertices.Add(new Vector3f(offset + gridSize, 0, offset + y));

				colors.Add(thickLineColor);
				colors.Add(thickLineColor);
				colors.Add(Color32.red);
				colors.Add(Color32.red);

				continue;
			}

			vertices.Add(new Vector3f(offset, 0, offset + y));
			vertices.Add(new Vector3f(offset + gridSize, 0, offset + y));

			if(y % 10 == 0)
			{
				colors.Add(thickLineColor);
				colors.Add(thickLineColor);
			}
			else
			{
				colors.Add(thinLineColor);
				colors.Add(thinLineColor);
			}
		}

		vertices.Add(new Vector3f(0, 0, 0));
		vertices.Add(new Vector3f(0, 10, 0));
		colors.Add(Color.green);
		colors.Add(Color.green);

		var indices = new int[vertices.Count];
		for (int i = 0; i < indices.Length; indices[i] = i++);

		var mesh = Core.resource.CreateMesh();
		mesh.SetVertices(vertices.ToArray());
		mesh.SetColors(colors.ToArray());
		mesh.SetIndices(indices, Mesh.Topology.Lines);
		mesh.ApplyMeshData();

		return mesh;
	}

	private static Mesh CreateTorus(float radius1, float radius2, int nbRadSeg, int nbSides)
	{
		var vertices = new Vector3f[(nbRadSeg+1) * (nbSides+1)];
		float _2pi = Math.Pi * 2f;
		for (int seg = 0; seg <= nbRadSeg; ++seg)
		{
			int currSeg = (seg == nbRadSeg ? 0 : seg);

			float t1 = (float)currSeg / nbRadSeg * _2pi;
			var r1 = new Vector3f(Math.Cos(t1) * radius1, 0f, Math.Sin(t1) * radius1);

			for (int side = 0; side <= nbSides; ++side)
			{
				int currSide = (side == nbSides ? 0 : side);

				float t2 = (float)currSide / nbSides * _2pi;
				//var r2 = Quat.AngleAxis(-t1, Vector3f.up)          * new Vector3f(Math.Sin(t2) * radius2, Math.Cos(t2) * radius2, 0f);
				var r2 = Math.EulerAngles(new Vector3f(0f, -t1, 0f)) * new Vector3f(Math.Sin(t2) * radius2, Math.Cos(t2) * radius2, 0f);

				vertices[side + seg * (nbSides+1)] = r1 + r2;
			}
		}

		var normals = new Vector3f[vertices.Length];
		for (int seg = 0; seg <= nbRadSeg; ++seg)
		{
			int currSeg = (seg == nbRadSeg ? 0 : seg);

			float t1 = (float)currSeg / nbRadSeg * _2pi;
			var r1 = new Vector3f(Math.Cos(t1) * radius1, 0f, Math.Sin(t1) * radius1);

			for (int side = 0; side <= nbSides; ++side)
			{
				normals[side + seg * (nbSides+1)] = Math.Normalize(vertices[side + seg * (nbSides+1)] - r1);
			}
		}

		var uv = new Vector2f[vertices.Length];
		for (int seg = 0; seg <= nbRadSeg; ++seg)
		{
			for (int side = 0; side <= nbSides; ++side)
			{
				uv[side + seg * (nbSides+1)] = new Vector2f((float)seg / nbRadSeg, (float)side / nbSides);
			}
		}

		int nbFaces = vertices.Length;
		int nbTriangles = nbFaces * 2;
		int nbIndexes = nbTriangles * 3;
		var triangles = new int[nbIndexes];

		int i = 0;
		for (int seg = 0; seg <= nbRadSeg; ++seg)
		{
			for (int side = 0; side <= nbSides - 1; ++side)
			{
				int current = side + seg * (nbSides+1);
				int next = side + (seg < (nbRadSeg) ? (seg+1) * (nbSides+1) : 0);

				if (i < triangles.Length - 6)
				{
					triangles[i++] = current;
					triangles[i++] = next;
					triangles[i++] = next+1;

					triangles[i++] = current;
					triangles[i++] = next+1;
					triangles[i++] = current+1;
				}
			}
		}

		var mesh = Core.resource.CreateMesh();
		mesh.SetVertices(vertices);
		mesh.SetTextureCoordinates(uv);
		mesh.SetNormals(normals);
		mesh.SetIndices(triangles, Mesh.Topology.Triangles);
		mesh.ApplyMeshData();

		return mesh;
	}
}
