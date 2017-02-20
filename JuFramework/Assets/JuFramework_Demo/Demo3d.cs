using JuFramework;
using JuFramework.Drawing;
using System.Collections.Generic;

public class GameDemo3d : App
{
	private FpsCamera camera;
	private Camera2D hudCamera;

	private Material shapesMaterial;
	private Material spritesMaterial;

	private Transform3D testtransform;
	private Mesh testmesh;
	private Material testmaterial;
	private Texture testtexture;
	private List<Ray> rays = new List<Ray>();

	private Mesh gridMesh;

	private Transform3D testtransformbillboard;
	private Texture billboard;

	private Texture proceduralTexture;

	public override void Initialize()
	{
		// camera 3D init
		camera = new FpsCamera(Core.screen.size);
		camera.SetPosition(new Vector3f(-20, 10, 20));
		camera.SetRotation(-Math.Pi / 10f, 3f / 4f * Math.Pi);

		// core init
		Core.mainCamera = camera; // See "UnityBootstrap.Update()" method
		Core.graphics.ClearAuto(true, new Color32(71));

		// camera 2D init
		hudCamera = new Camera2D(Core.screen.size);
		hudCamera.SetZoom(2f);

		// materials init
		shapesMaterial = Core.resource.CreateMaterial("Lines/Colored Blended");
		spritesMaterial = Core.resource.CreateMaterial("Unlit/Transparent");

		// grid init
		gridMesh = Models.CreateGrid(100);

		// model test init
		testtransform = new Transform3D();
		testtransform.Rotate(new Vector3f(-90f * Math.Deg2Rad, 0f, 0f));
		testmesh = Core.resource.LoadMesh("Models/Test");
		testtexture = Core.resource.LoadTexture("Models/lightmap");
		testmaterial = Core.resource.CreateMaterial("Unlit/Texture");
		testmaterial.Set("_MainTex", testtexture);

		// billboard init
		testtransformbillboard = new Transform3D();
		testtransformbillboard.SetPosition(new Vector3f(0, 0, 10));
		testtransformbillboard.SetLocalScale(Vector3f.one * 0.01f);
		billboard = Core.resource.LoadTexture("Sprites/tree_billboard");

		// procedural texture init
		{
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
	}

	public override void Update()
	{
		// camera 3D update
		camera.Update();

		// camera 2D update
		hudCamera.SetTargetSize(Core.screen.size);
		hudCamera.SetPosition(Core.screen.halfSize / hudCamera.GetZoom());

		// ray test update
		if(Core.input.IsMouseButtonPressed(MouseButton.Left))
		{
			rays.Add(camera.ScreenPixelToWorldRay(Core.input.GetMousePosition()));
		}
		else if(Core.input.IsKeyPressed(KeyboardKey.R))
		{
			rays.Clear();
		}

		// billboard update
		testtransformbillboard.LookAt(camera.GetPosition());
	}

	public override void Draw()
	{
		camera.Use();
		{
			// grid draw
			shapesMaterial.Use();
			Core.graphics.DrawMesh(gridMesh);

			// model test draw
			testmaterial.Use();
			Core.graphics.SetModelToWorldMatrix(testtransform.GetLocalToWorldMatrix());
			Core.graphics.DrawMesh(testmesh);

			// model test bounding box draw
			shapesMaterial.Use();
			Core.graphics.SetWireframe(true);
			Models.DrawCube(testmesh.boundingBox, Color.red);
			Core.graphics.SetWireframe(false);

			// ray test draw
			shapesMaterial.Use();
			Core.graphics.SetModelToWorldMatrix(Matrix4.identity);
			for (int i = 0; i < rays.Count; ++i)
			{
				Shapes.DrawLine(rays[i].position, rays[i].GetPoint(100), new Color(1, 0 ,0 , 0.5f));
			}

			// model draw test
			//shapesMaterial.Use();
			//Core.graphics.SetModelToWorldMatrix(Matrix4.identity);
			//Models.DrawCylinder(new Vector3f(10, 1, 10), 5f, 5f, 8f, 32, Color.green);
			//Core.graphics.SetWireframe(true);
			//Models.DrawCylinder(new Vector3f(10, 1, 10), 5f, 5f, 8f, 32, Color.maroon);
			//Core.graphics.SetWireframe(false);

			// billboard draw (note: last because is transparent object)
			spritesMaterial.Set("_MainTex", billboard);
			spritesMaterial.Use();
			Core.graphics.SetModelToWorldMatrix(testtransformbillboard.GetLocalToWorldMatrix());
			Sprites.DrawQuad(billboard.GetSize(), new IntRect(new Vector2i(-billboard.GetWidth() / 2, 0), billboard.GetSize()));
		}

		hudCamera.Use();
		{
			// square indicator test draw
			shapesMaterial.Use();
			Shapes.DrawRectangle((camera.WorldToScreenPixel(Vector2f.zero) / hudCamera.GetZoom()) - new Vector2f(5, 5), 10, 10, Color.blue);

			// procedural texture draw
			spritesMaterial.Set("_MainTex", proceduralTexture);
			spritesMaterial.Use();
			Sprites.DrawQuad(proceduralTexture.GetSize(), new IntRect(Vector2i.zero, Math.Round(proceduralTexture.GetSize() * 0.125f)));
		}
	}
}
