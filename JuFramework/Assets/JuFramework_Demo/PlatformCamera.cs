using JuFramework;

public class PlatformCamera : Camera2D
{
	public PlatformCamera(Vector2i targetSize) : base(targetSize.x, targetSize.y)
	{
	}

	public PlatformCamera(int targetWidth, int targetHeight) : base(targetWidth, targetHeight)
	{
	}

	public PlatformCamera(Transform2D transform, Vector2i targetSize) : base(transform, targetSize.x, targetSize.y)
	{
	}

	public PlatformCamera(Transform2D transform, int targetWidth, int targetHeight) : base(transform, targetWidth, targetHeight)
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
		float cameraMovSpeed = 1f;
		if(Core.input.IsKeyHeld(KeyboardKey.LeftShift))		cameraMovSpeed = 0.1f;
		if(Core.input.IsKeyHeld(KeyboardKey.LeftControl))	cameraMovSpeed = 10f;
		if(Core.input.IsKeyHeld(KeyboardKey.D))				Translate(Vector3f.right / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.A))				Translate(-Vector3f.right / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.W))				Translate(Vector3f.up / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.S))				Translate(-Vector3f.up / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.RightArrow))	Translate(Vector3f.right / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.LeftArrow))		Translate(-Vector3f.right / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.UpArrow))		Translate(Vector3f.up / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.DownArrow))		Translate(-Vector3f.up / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.E))				Rotate(0.05f / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.Q))				Rotate(-0.05f / cameraMovSpeed);
		if(Core.input.IsKeyHeld(KeyboardKey.Space))
		{
			SetPosition(Vector2f.zero);
			SetRotation(0f);
			SetZoom(1f);
		}

		// Mouse wheel behaviour
		if(Core.input.GetMouseWheelDelta() != 0)
		{
			if(IsOrthographic())
			{
				var oldZoom = GetZoom();

				if(oldZoom <= 1f)
				{
					var modifier = Math.Sign(Core.input.GetMouseWheelDelta()) == 1 ? 2f : 0.5f;

					if(oldZoom >= 0.0002f) // x13 zoom-out levels
					{
						SetZoom(oldZoom * modifier);
					}

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

		// Mouse movement (Middle click)
		if(Core.input.IsMouseButtonPressed(MouseButton.Middle))
		{
			Core.input.SetMouseCursorVisible(false);
			Core.input.SetMouseLockMode(MouseLockMode.Locked);
		}
		else if(Core.input.IsMouseButtonReleased(MouseButton.Middle) && !(Core.input.IsMouseButtonHeld(MouseButton.Middle)))
		{
			Core.input.SetMouseLockMode(MouseLockMode.None);
			Core.input.SetMouseCursorVisible(true);
		}

		if(Core.input.IsMouseButtonHeld(MouseButton.Middle))
		{
			var mouseDiff = Core.input.GetMousePositionDelta();
			Translate((Vector2f.one / cameraMovSpeed * 10f) * -mouseDiff / GetZoom());
		}
	}
}
