using JuFramework;
using JuFramework.Drawing;

public class GameDemo2d : App
{
	private PlatformCamera camera;
	private Texture testtexture;
	private Material testmaterial;
	private Material testmaterial2;
	private JuFramework.TileMap tilemap;

	public override void Initialize()
	{
		camera = new PlatformCamera(Core.screen.size);
		Core.mainCamera = camera; // See "UnityBootstrap.Update()" method

		Core.graphics.ClearAuto(true, Color32.black);

		testtexture = Core.resource.LoadTexture("Sprites/Blender_UV");

		testmaterial = Core.resource.CreateMaterial("Sprites/Default");
		testmaterial.Set("_MainTex", testtexture);

		testmaterial2 = Core.resource.CreateMaterial("Lines/Colored Blended");

		tilemap = Core.resource.LoadTileMap("Tilemaps/Test");
	}

	public override void Update()
	{
		camera.Update();
		camera.SetPosition(Math.Round(camera.GetPosition()));
	}

	public override void Draw()
	{
		camera.Use();
		{
			//testmaterial2.Use();
			//Shapes.DrawRectangleFill(new Vector2f(-5, -5), 100, 100, Random.Color());

			//testmaterial.Use();
			//Sprites.DrawQuad(testtexture.GetSize(), Vector2f.zero);

			//for (int i = 0; i < 10; i++)
			{
				JuFramework.Drawing.TileMap.DrawMap(tilemap, 0);
				JuFramework.Drawing.TileMap.DrawMap(tilemap, 1);
			}
		}
	}
}
