using JuFramework;

public class FpsCamera : Camera3D
{
	public FpsCamera(Vector2i targetSize) : base(targetSize.x, targetSize.y)
	{
	}

	public FpsCamera(int targetWidth, int targetHeight) : base(targetWidth, targetHeight)
	{
	}

	public FpsCamera(Transform3D transform, Vector2i targetSize) : base(transform, targetSize.x, targetSize.y)
	{
	}

	public FpsCamera(Transform3D transform, int targetWidth, int targetHeight) : base(transform, targetWidth, targetHeight)
	{
	}

	public void Update()
	{
		// Update camera on resolution changes
		SetTargetSize(Core.screen.size);

		// Skip camera control when window is not focused
		if(! Core.screen.hasFocus)
		{
			return;
		}

		// Basic keyboard behaviour
		float cameraMovSpeed = 10f;
		if(Core.input.IsKeyHeld(KeyboardKey.LeftShift))		cameraMovSpeed = 2f;
		if(Core.input.IsKeyHeld(KeyboardKey.LeftControl))	cameraMovSpeed = 100f;
		if(Core.input.IsKeyHeld(KeyboardKey.D))				Translate(Vector3f.right / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.A))				Translate(-Vector3f.right / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.E))				Translate(Vector3f.up / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.Q))				Translate(-Vector3f.up / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.W))				Translate(Vector3f.forward / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.S))				Translate(-Vector3f.forward / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.LeftArrow))		Rotate(0f, -0.05f);
		if(Core.input.IsKeyHeld(KeyboardKey.RightArrow))	Rotate(0f, 0.05f);
		if(Core.input.IsKeyHeld(KeyboardKey.UpArrow))		Rotate(-0.05f, 0f);
		if(Core.input.IsKeyHeld(KeyboardKey.DownArrow))		Rotate(0.05f, 0f);
		if(Core.input.IsKeyPressed(KeyboardKey.O))			SetOrthographic(true);
		if(Core.input.IsKeyPressed(KeyboardKey.P))			SetOrthographic(false);

		// Mouse wheel behaviour
		if(Core.input.GetMouseWheelDelta() != 0)
		{
			if(IsOrthographic())
			{
				var oldZoom = GetZoom();

				if(oldZoom <= 1f)
				{
					var modifier = Math.Sign(Core.input.GetMouseWheelDelta()) == 1 ? 2f : 0.5f;

					SetZoom(oldZoom * modifier);

					if(oldZoom < 1f && GetZoom() > 1f)
					{
						SetZoom(1f);
					}
				}
				else
				{
					SetZoom(oldZoom + Math.Sign(Core.input.GetMouseWheelDelta()));
				}
			}
			else
			{
				if(Core.input.IsKeyHeld(KeyboardKey.LeftShift))
				{
					SetFov(GetFov() - Core.input.GetMouseWheelDelta() * 5f);
				}
				else
				{
					Translate(Vector3f.forward / cameraMovSpeed * (float)Core.input.GetMouseWheelDelta() * 10f);
				}
			}
		}

		// Mouse movement (Middle & Right click)
		if(Core.input.IsMouseButtonPressed(MouseButton.Middle) || Core.input.IsMouseButtonPressed(MouseButton.Right))
		{
			Core.input.SetMouseCursorVisible(false);
			Core.input.SetMouseLockMode(MouseLockMode.Locked);
		}
		else if(Core.input.IsMouseButtonReleased(MouseButton.Middle) || Core.input.IsMouseButtonReleased(MouseButton.Right)
			&& !(Core.input.IsMouseButtonHeld(MouseButton.Middle) || Core.input.IsMouseButtonHeld(MouseButton.Right)))
		{
			Core.input.SetMouseLockMode(MouseLockMode.None);
			Core.input.SetMouseCursorVisible(true);
		}

		if(Core.input.IsMouseButtonHeld(MouseButton.Right))
		{
			var mouseDiff = Core.input.GetMousePositionDelta();
			Rotate(-mouseDiff.y / 30f, mouseDiff.x / 30f);
		}
		else if(Core.input.IsMouseButtonHeld(MouseButton.Middle))
		{
			var mouseDiff = Core.input.GetMousePositionDelta();
			Translate((Vector2f.one / cameraMovSpeed) * mouseDiff);
		}
	}

	public void Debug()
	{
		//DebugLog::Write("Camera -> X: %f Y: %f Z: %f Pitch: %f Yaw: %f", newPos.x, newPos.y, newPos.z, GetEulerAngles().x, GetEulerAngles().y);

		// Local to World
		//var res = GetTransform().TransformPoint(vec3(0f,5f,5f));
		//var res = GetTransform().TransformVector(vec3(0f,0f,1f));
		//var res = GetTransform().TransformDirection(vec3(0f,0f,1f));
		// World to Local
		//var res = GetTransform().InverseTransformPoint(vec3(0f,5f,5f));
		//var res = GetTransform().InverseTransformVector(vec3(0f,0f,1f));
		//var res = GetTransform().InverseTransformDirection(vec3(0f,0f,1f));
		//DebugLog::Write("%f %f %f", res.x, res.y, res.z);

		// Core.debug.Log(ScreenToWorldPixel(Core.input.GetMousePosition()));

		/*if(App::Input()->IsHeld("debug"))
		{
			//GetTransform().LookAt(EntityManager_old::Get("modelTest")->GetComponent<Transform_old>()->GetPosition(), Vector3f.up);
		}*/
	}
}
