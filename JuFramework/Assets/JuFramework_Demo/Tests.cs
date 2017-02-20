using System.Collections.Generic;
using JuFramework;
using JuFramework.Drawing;

public static class Tests
{
	private static List<Texture> textures;
	private static Material shapesMaterial;

	public static void Initialize()
	{
		textures = new List<Texture>(new Texture[5]
		{
			Core.resource.LoadTexture("Sprites/Blender_UV"),
			Core.resource.LoadTexture("Sprites/Sizes"),
			Core.resource.LoadTexture("Sprites/TestTexture"),
			Core.resource.LoadTexture("Sprites/UV_Grid_Lrg"),
			Core.resource.LoadTexture("Sprites/UV_Grid_Sm")
		});

		shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
	}

	public static void Points()
	{
		shapesMaterial.Use();

		for (int y = 0; y < Core.screen.height; y+=20)
		{
			for (int x = 0; x < Core.screen.width; x+=20)
			{
				Shapes.DrawPixel(new Vector2i(x, y), Random.Color());
			}
		}
	}

	public static void Lines()
	{
		shapesMaterial.Use();

		for (int i = 0; i < 10; ++i)
		{
			var color = Random.Color();
			var line1 = new Vector2f(Random.Range(0, Core.screen.width / 2), Random.Range(0, Core.screen.height / 2));
			var line2 = new Vector2f(Random.Range(Core.screen.width / 2, Core.screen.width), Random.Range(Core.screen.height / 2, Core.screen.height));
			Shapes.DrawLine(line1, line2, color);
		}
	}

	public static void Polygons()
	{
		shapesMaterial.Use();

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

	public static void Circles()
	{
		shapesMaterial.Use();

		for (int i = 0; i < 1; ++i)
		{
			var color = Random.Color();
			var position = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var radius = Random.Range(50, 200);

			Shapes.DrawCircleFill(position, radius, color, 36);
		}
	}

	public static void Rectangles()
	{
		shapesMaterial.Use();

		for (int i = 0; i < 1; ++i)
		{
			var color = Random.Color();
			var position = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var width = Random.Range(50, 200);
			var height = Random.Range(50, 200);

			Shapes.DrawRectangle(position, width, height, color);
		}
	}

	public static void Triangles()
	{
		shapesMaterial.Use();

		for (int i = 0; i < 1; ++i)
		{
			var color = Random.Color();
			var v1 = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var v2 = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var v3 = new Vector2f(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));

			Shapes.DrawTriangleFill(v1, v2, v3, color);
		}
	}

	public static void Texture_Positions()
	{
		var texture = Core.resource.LoadTexture("Sprites/TestTexture");

		for (int i = 0; i < 9; ++i)
		{
			for (int j = 0; j < 5; ++j)
			{
				// TODO: Material + Use() (Sprites/Default)
				Sprites.DrawQuad(texture.GetSize(), new IntRect(i * 100 + 25, j * 100 + 25, 100, 100));
			}
		}
	}

	public static void Textures_SourceRect()
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

			// TODO: Material + Use() (Sprites/Default)
			Sprites.DrawQuad(textures[textureId].GetSize(), position, sourceRectangle, color);
		}
	}

	public static void Textures_DestinationRect()
	{
		for (int i = 0; i < 10; ++i)
		{
			var color = Random.Color();
			var textureId = Random.Range(0, textures.Count);
			var destinationRectBottomLeft = new Vector2i(Random.Range(0, Core.screen.width), Random.Range(0, Core.screen.height));
			var destinationRectangle = new IntRect(destinationRectBottomLeft.x, destinationRectBottomLeft.y,
				Random.Range(0, Core.screen.width - destinationRectBottomLeft.x),
				Random.Range(0, Core.screen.height - destinationRectBottomLeft.y));

			// TODO: Material + Use() (Sprites/Default)
			Sprites.DrawQuad(textures[textureId].GetSize(), destinationRectangle, color);
		}
	}

	public static void Textures_SourceDestinationRect()
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

			// TODO: Material + Use() (Sprites/Default)
			Sprites.DrawQuad(textures[textureId].GetSize(), destinationRectangle, sourceRectangle, color);
		}
	}
}
