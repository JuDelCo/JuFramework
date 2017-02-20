
// TODO:
// 		Gamepad extended support (ex: vibration) (https://github.com/RecentNewcomer/XInputDotNet)
//		Touch support (at least a "tap" in a screen with position, multitouch/swipe/etc not needed atm)

namespace JuFramework
{
	public abstract class Input
	{
		public InputAction action;

		public Input()
		{
			action = new InputAction(this);
		}

		public void Update()
		{
			action.Update();
		}

		public abstract bool IsAnyPressed();
		public abstract bool IsAnyHeld();

		public abstract bool IsKeyPressed(KeyboardKey key);
		public abstract bool IsKeyHeld(KeyboardKey key);
		public abstract bool IsKeyReleased(KeyboardKey key);

		public abstract string GetInputString();

		public abstract bool IsMouseButtonPressed(MouseButton button);
		public abstract bool IsMouseButtonHeld(MouseButton button);
		public abstract bool IsMouseButtonReleased(MouseButton button);
		public abstract Vector2i GetMousePosition();
		public abstract Vector2f GetMousePositionDelta();
		public abstract int GetMouseWheelDelta();
		public abstract void SetMouseLockMode(MouseLockMode mode);
		public abstract void SetMouseCursorVisible(bool visible);

		public abstract bool IsGamePadButtonPressed(GamePadButton button, GamePadController controller = GamePadController.Any);
		public abstract bool IsGamePadButtonHeld(GamePadButton button, GamePadController controller = GamePadController.Any);
		public abstract bool IsGamePadButtonReleased(GamePadButton button, GamePadController controller = GamePadController.Any);
		public abstract Vector2f GetGamePadAxis(GamePadAxis axis, GamePadController controller = GamePadController.Any, bool rawValue = false);
		public abstract float GetGamePadTrigger(GamePadTrigger trigger, GamePadController controller = GamePadController.Any, bool rawValue = false);
	}
}
