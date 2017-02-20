
namespace JuFramework
{
	public class UnityInput : Input
	{
		public override bool IsAnyPressed()
		{
			return UnityEngine.Input.anyKeyDown;
		}

		public override bool IsAnyHeld()
		{
			return UnityEngine.Input.anyKey;
		}

		public override bool IsKeyPressed(KeyboardKey key)
		{
			return UnityEngine.Input.GetKeyDown((UnityEngine.KeyCode)key);
		}

		public override bool IsKeyHeld(KeyboardKey key)
		{
			return UnityEngine.Input.GetKey((UnityEngine.KeyCode)key);
		}

		public override bool IsKeyReleased(KeyboardKey key)
		{
			return UnityEngine.Input.GetKeyUp((UnityEngine.KeyCode)key);
		}

		public override string GetInputString()
		{
			// "\b" == Backspace
			// "\n" == Return | Enter
			return UnityEngine.Input.inputString;
		}

		public override bool IsMouseButtonPressed(MouseButton button)
		{
			return UnityEngine.Input.GetKeyDown((UnityEngine.KeyCode)button);
		}

		public override bool IsMouseButtonHeld(MouseButton button)
		{
			return UnityEngine.Input.GetKey((UnityEngine.KeyCode)button);
		}

		public override bool IsMouseButtonReleased(MouseButton button)
		{
			return UnityEngine.Input.GetKeyUp((UnityEngine.KeyCode)button);
		}

		public override Vector2i GetMousePosition()
		{
			return (Vector2i)UnityEngine.Input.mousePosition.FloorToVector3i();
		}

		public override Vector2f GetMousePositionDelta()
		{
			var delta = Vector2f.zero;

			try
			{
				delta = new Vector2f(
					UnityEngine.Input.GetAxis("Mouse X"),
					UnityEngine.Input.GetAxis("Mouse Y")
				);
			}
			catch (System.Exception e)
			{
				Core.debug.Error(e.Message);
			}

			return delta;
		}

		public override int GetMouseWheelDelta()
		{
			return (int)UnityEngine.Input.mouseScrollDelta.y;
		}

		public override void SetMouseLockMode(MouseLockMode mode)
		{
			var lockMode = UnityEngine.CursorLockMode.None;

			if(mode == MouseLockMode.Confined)
			{
				lockMode = UnityEngine.CursorLockMode.Confined;
			}
			else if(mode == MouseLockMode.Locked)
			{
				lockMode = UnityEngine.CursorLockMode.Locked;
			}

			UnityEngine.Cursor.lockState = lockMode;
		}

		public override void SetMouseCursorVisible(bool visible)
		{
			UnityEngine.Cursor.visible = visible;
		}

		public override bool IsGamePadButtonPressed(GamePadButton button, GamePadController controller)
		{
			return UnityEngine.Input.GetKeyDown((UnityEngine.KeyCode)GetGamePadKeyCode(controller, button));
		}

		public override bool IsGamePadButtonHeld(GamePadButton button, GamePadController controller)
		{
			return UnityEngine.Input.GetKey((UnityEngine.KeyCode)GetGamePadKeyCode(controller, button));
		}

		public override bool IsGamePadButtonReleased(GamePadButton button, GamePadController controller)
		{
			return UnityEngine.Input.GetKeyUp((UnityEngine.KeyCode)GetGamePadKeyCode(controller, button));
		}

		public override Vector2f GetGamePadAxis(GamePadAxis axis, GamePadController controller, bool rawValue)
		{
			if(controller == GamePadController.None || axis == GamePadAxis.None)
			{
				return Vector2f.zero;
			}

			string xAxisName = "";
			string yAxisName = "";
			int controllerIndex = (int)controller;

			if(controller == GamePadController.Any)
			{
				controllerIndex = 0;
			}

			switch (axis)
			{
				case GamePadAxis.Dir_Pad:
					xAxisName = "DPad_XAxis_" + controllerIndex;
					yAxisName = "DPad_YAxis_" + controllerIndex;
					break;
				case GamePadAxis.L_Stick:
					xAxisName = "L_XAxis_" + controllerIndex;
					yAxisName = "L_YAxis_" + controllerIndex;
					break;
				case GamePadAxis.R_Stick:
					xAxisName = "R_XAxis_" + controllerIndex;
					yAxisName = "R_YAxis_" + controllerIndex;
					break;
			}

			Vector2f axisValue = Vector2f.zero;

			try
			{
				if (!rawValue)
				{
					axisValue.x = UnityEngine.Input.GetAxis(xAxisName);
					axisValue.y = -UnityEngine.Input.GetAxis(yAxisName);
				}
				else
				{
					axisValue.x = UnityEngine.Input.GetAxisRaw(xAxisName);
					axisValue.y = -UnityEngine.Input.GetAxisRaw(yAxisName);
				}
			}
			catch (System.Exception e)
			{
				Core.debug.Error(e.Message);
			}

			return axisValue;
		}

		public override float GetGamePadTrigger(GamePadTrigger trigger, GamePadController controller, bool rawValue)
		{
			if(controller == GamePadController.None || trigger == GamePadTrigger.None)
			{
				return 0f;
			}

			string triggerName = "";
			int controllerIndex = (int)controller;

			if(controller == GamePadController.Any)
			{
				controllerIndex = 0;
			}

			switch (trigger)
			{
				case GamePadTrigger.Left:
					triggerName = "TriggersL_" + controllerIndex;
					break;
				case GamePadTrigger.Right:
					triggerName = "TriggersR_" + controllerIndex;
					break;
			}

			float triggerValue = 0f;

			try
			{
				if (!rawValue)
				{
					triggerValue = UnityEngine.Input.GetAxis(triggerName);
				}
				else
				{
					triggerValue = UnityEngine.Input.GetAxisRaw(triggerName);
				}
			}
			catch (System.Exception e)
			{
				Core.debug.Error(e.Message);
			}

			return triggerValue;
		}

		private int GetGamePadKeyCode(GamePadController controller, GamePadButton button)
		{
			int index;

			switch (controller)
			{
				case GamePadController.Any:
					index = (int)GamePadButtonInternal.Any_1;
					break;
				case GamePadController.Controller_1:
					index = (int)GamePadButtonInternal.C1_1;
					break;
				case GamePadController.Controller_2:
					index = (int)GamePadButtonInternal.C2_1;
					break;
				case GamePadController.Controller_3:
					index = (int)GamePadButtonInternal.C3_1;
					break;
				case GamePadController.Controller_4:
					index = (int)GamePadButtonInternal.C4_1;
					break;
				case GamePadController.Controller_5:
					index = (int)GamePadButtonInternal.C5_1;
					break;
				case GamePadController.Controller_6:
					index = (int)GamePadButtonInternal.C6_1;
					break;
				case GamePadController.Controller_7:
					index = (int)GamePadButtonInternal.C7_1;
					break;
				case GamePadController.Controller_8:
					index = (int)GamePadButtonInternal.C8_1;
					break;
				default:
					index = (int)GamePadButtonInternal.None;
					break;
			}

			if(controller != GamePadController.None)
			{
				if(button != GamePadButton.None)
				{
					index += (int)button - 1;
				}
				else
				{
					index = (int)GamePadButtonInternal.None;
				}
			}

			return index;
		}
	}
}
