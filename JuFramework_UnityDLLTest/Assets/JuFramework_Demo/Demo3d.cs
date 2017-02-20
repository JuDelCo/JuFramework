using JuFramework;
using System.Collections.Generic;

public class GameDemo3d : App
{
	private FpsCamera camera;
	private Camera2D hudCamera;

	private Material shapesMaterial;

	private Transform3D testtransform;
	private Mesh testmesh;
	private Material testmaterial;
	private Texture testtexture;
	private List<Ray> rays = new List<Ray>();

	private Transform3D testtransformbillboard;
	private Texture billboard;

	private Texture proceduralTexture;

	public override void Initialize()
	{
		camera = new FpsCamera(Core.screen.size);
		camera.SetPosition(new Vector3f(-20, 10, 20));
		camera.SetRotation(-Math.Pi / 10f, 3f / 4f * Math.Pi);

		Core.mainCamera = camera;

		hudCamera = new Camera2D(Core.screen.size);
		hudCamera.SetZoom(2f);

		shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");

		Core.graphics.ClearAuto(true, new Color32(71));

		testtransform = new Transform3D();
		testtransform.Rotate(new Vector3f(-90f * Math.Deg2Rad, 0f, 0f));
		testmesh = Core.resource.LoadMesh("Models/Test");
		testtexture = Core.resource.LoadTexture("Models/lightmap");
		testmaterial = Core.resource.CreateMaterial("Unlit/Texture");
		testmaterial.Set("_MainTex", testtexture);

		testtransformbillboard = new Transform3D();
		testtransformbillboard.SetPosition(new Vector3f(0, 0, 10));
		testtransformbillboard.SetLocalScale(Vector3f.one * 0.01f);
		billboard = Core.resource.LoadTexture("Sprites/tree_billboard");

		proceduralTexture = Core.resource.CreateTexture(256, 256);

		for (int x = 0; x < proceduralTexture.GetWidth(); ++x)
		{
			for (int y = 0; y < proceduralTexture.GetHeight(); ++y)
			{
				var baseColor = new Color((float)x / proceduralTexture.GetWidth(), 0.5f, (float)y / proceduralTexture.GetHeight());
				var checkerColor = (new Color(1f, 1f, 1f, 0f)) * (y % 32 > 15 ? (x % 32 > 15 ? 0 : 0.2f) : (x % 32 <= 15 ? 0 : 0.2f));

				proceduralTexture.SetPixel(x, y, baseColor - checkerColor);
			}
		}

		proceduralTexture.Apply();
	}

	public override void Update()
	{
		camera.Update();

		hudCamera.SetTargetSize(Core.screen.size);
		hudCamera.SetPosition(Core.screen.halfSize / hudCamera.GetZoom());

		if(Core.input.IsMouseButtonPressed(MouseButton.Left))
		{
			rays.Add(camera.ScreenPixelToWorldRay(Core.input.GetMousePosition()));
		}

		if(Core.input.IsKeyPressed(KeyboardKey.R))
		{
			rays.Clear();
		}

		testtransformbillboard.LookAt(camera.GetPosition());
	}

	public override void Draw(Graphics graphics)
	{
		camera.Use(graphics);
		{
			Tests.Draw(graphics);

			testmaterial.SetPass(0);
			graphics.SetModelToWorldMatrix(testtransform.GetLocalToWorldMatrix());
			graphics.DrawMesh(testmesh);

			shapesMaterial.SetPass(0);
			graphics.SetModelToWorldMatrix(Matrix4.identity);
			for (int i = 0; i < rays.Count; ++i)
			{
				Shapes.DrawLine(rays[i].position, rays[i].GetPoint(100), new Color(1, 0 ,0 , 0.5f));
			}

			testmaterial.SetPass(0); // ???
			graphics.SetModelToWorldMatrix(testtransformbillboard.GetLocalToWorldMatrix());
			graphics.DrawTexture(billboard, new IntRect(new Vector2i(-billboard.GetWidth() / 2, 0), billboard.GetSize()));

			shapesMaterial.SetPass(0);
			graphics.SetModelToWorldMatrix(Matrix4.identity);
			//Shapes.DrawSphere(testtransformbillboard.GetPosition(), 0.1f, Color.green);
		}

		hudCamera.Use(graphics);
		{
			shapesMaterial.SetPass(0);
			Shapes.DrawRectangle((camera.WorldToScreenPixel(Vector2f.zero) / hudCamera.GetZoom()) - new Vector2f(5, 5), 10, 10, Color.blue);

			graphics.DrawTexture(proceduralTexture, new IntRect(Vector2i.zero, Math.Round(proceduralTexture.GetSize() * 0.125f)));
		}
	}
}
