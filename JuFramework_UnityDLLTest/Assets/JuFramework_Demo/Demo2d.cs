using JuFramework;

public class GameDemo2d : App
{
	private PlatformCamera camera;
	private Texture testtexture;

	public override void Initialize()
	{
		camera = new PlatformCamera(Core.screen.size);

		Core.mainCamera = camera;

		Core.graphics.ClearAuto(true, Color32.black);

		testtexture = Core.resource.LoadTexture("Sprites/Blender_UV");
	}

	public override void Update()
	{
		camera.Update();
		camera.SetPosition(Math.Round(camera.GetPosition()));

		//Core.debug.Log(camera.WorldToScreenPoint(Vector2f.zero));
		//Core.debug.Log(camera.ScreenToWorldPoint(Vector2f.zero));
	}

	public override void Draw(Graphics graphics)
	{
		camera.Use(graphics);
		{
			//graphics.SetModelToWorldMatrix(Matrix4.identity);
			//Core.debug.DrawRectangle(Vector2f.zero, 959, 539, Random.Color());
			//Core.debug.DrawRectangle(new Vector2f(960 - 20 - 100, 540 - 20 - 100), 100, 100, Random.Color(0f, 1f, 0.5f, 1f, 0.5f, 1f));

			//Tests.Draw(graphics);

			graphics.DrawTexture(testtexture, Vector2f.zero);
		}
	}
}
