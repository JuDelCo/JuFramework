using System.Collections.Generic;
using Identifier = System.String;
using InputActionId = JuFramework.Tuple<System.String, JuFramework.PlayerId>;

namespace JuFramework
{
	public delegate void InputActionPressed(Identifier action, PlayerId player);
	public delegate void InputActionReleased(Identifier action, PlayerId player);

	public class InputAction
	{
		private Input input;
		private Dictionary<InputActionId, List<KeyboardKeyInfo>> keyboardKeyBindings;
		private Dictionary<InputActionId, List<MouseButtonInfo>> mouseButtonBindings;
		private Dictionary<InputActionId, List<GamePadButtonInfo>> gamePadButtonBindings;
		private Dictionary<InputActionId, List<GamePadAxisInfo>> gamePadAxisBindings;
		private Dictionary<InputActionId, List<GamePadTriggerInfo>> gamePadTriggerBindings;
		private float gamePadAxisDeadZone;
		private float gamePadTriggerDeadZone;

		public event InputActionPressed OnActionPressed;
		public event InputActionReleased OnActionReleased;

		public InputAction(Input input)
		{
			this.input = input;

			keyboardKeyBindings = new Dictionary<InputActionId, List<KeyboardKeyInfo>>();
			mouseButtonBindings = new Dictionary<InputActionId, List<MouseButtonInfo>>();
			gamePadButtonBindings = new Dictionary<InputActionId, List<GamePadButtonInfo>>();
			gamePadAxisBindings = new Dictionary<InputActionId, List<GamePadAxisInfo>>();
			gamePadTriggerBindings = new Dictionary<InputActionId, List<GamePadTriggerInfo>>();
			gamePadAxisDeadZone = 0.001f;
			gamePadTriggerDeadZone = 0.001f;
		}

		public void SetGamePadAxisDeadZone(float deadZone)
		{
			gamePadAxisDeadZone = Math.Max(0f, deadZone);
		}

		public void SetGamePadTriggerDeadZone(float deadZone)
		{
			gamePadTriggerDeadZone = Math.Max(0f, deadZone);
		}

		public void Update()
		{
			var keyboardKeyActionEnumerator = keyboardKeyBindings.GetEnumerator();

			while(keyboardKeyActionEnumerator.MoveNext())
			{
				var keyboardKeyAction = keyboardKeyActionEnumerator.Current.Key;

				for (int i = 0; i < keyboardKeyBindings[keyboardKeyAction].Count; ++i)
				{
					var keyboardKeyInfo = keyboardKeyBindings[keyboardKeyAction][i];

					keyboardKeyInfo.prevPressed = keyboardKeyInfo.pressed;
					keyboardKeyInfo.pressed = input.IsKeyHeld(keyboardKeyInfo.key);

					keyboardKeyBindings[keyboardKeyAction][i] = keyboardKeyInfo;

					if(OnActionPressed != null && !keyboardKeyInfo.prevPressed && keyboardKeyInfo.pressed)
					{
						OnActionPressed(keyboardKeyAction.first, keyboardKeyAction.second);
					}

					if(OnActionReleased != null && keyboardKeyInfo.prevPressed && !keyboardKeyInfo.pressed)
					{
						OnActionReleased(keyboardKeyAction.first, keyboardKeyAction.second);
					}
				}
			}

			var mouseButtonActionEnumerator = mouseButtonBindings.GetEnumerator();

			while(mouseButtonActionEnumerator.MoveNext())
			{
				var mouseButtonAction = mouseButtonActionEnumerator.Current.Key;

				for (int i = 0; i < mouseButtonBindings[mouseButtonAction].Count; ++i)
				{
					var mouseButtonInfo = mouseButtonBindings[mouseButtonAction][i];

					mouseButtonInfo.prevPressed = mouseButtonInfo.pressed;
					mouseButtonInfo.pressed = input.IsMouseButtonHeld(mouseButtonInfo.button);

					mouseButtonBindings[mouseButtonAction][i] = mouseButtonInfo;

					if(OnActionPressed != null && !mouseButtonInfo.prevPressed && mouseButtonInfo.pressed)
					{
						OnActionPressed(mouseButtonAction.first, mouseButtonAction.second);
					}

					if(OnActionReleased != null && mouseButtonInfo.prevPressed && !mouseButtonInfo.pressed)
					{
						OnActionReleased(mouseButtonAction.first, mouseButtonAction.second);
					}
				}
			}

			var gamePadButtonActionEnumerator = gamePadButtonBindings.GetEnumerator();

			while(gamePadButtonActionEnumerator.MoveNext())
			{
				var gamePadButtonAction = gamePadButtonActionEnumerator.Current.Key;

				for (int i = 0; i < gamePadButtonBindings[gamePadButtonAction].Count; ++i)
				{
					var gamePadButtonInfo = gamePadButtonBindings[gamePadButtonAction][i];

					gamePadButtonInfo.prevPressed = gamePadButtonInfo.pressed;
					gamePadButtonInfo.pressed = input.IsGamePadButtonHeld(gamePadButtonInfo.button, gamePadButtonInfo.controller);

					gamePadButtonBindings[gamePadButtonAction][i] = gamePadButtonInfo;

					if(OnActionPressed != null && !gamePadButtonInfo.prevPressed && gamePadButtonInfo.pressed)
					{
						OnActionPressed(gamePadButtonAction.first, gamePadButtonAction.second);
					}

					if(OnActionReleased != null && gamePadButtonInfo.prevPressed && !gamePadButtonInfo.pressed)
					{
						OnActionReleased(gamePadButtonAction.first, gamePadButtonAction.second);
					}
				}
			}

			var gamePadAxisActionEnumerator = gamePadAxisBindings.GetEnumerator();

			while(gamePadAxisActionEnumerator.MoveNext())
			{
				var gamePadAxisAction = gamePadAxisActionEnumerator.Current.Key;

				for (int i = 0; i < gamePadAxisBindings[gamePadAxisAction].Count; ++i)
				{
					var gamePadAxisInfo = gamePadAxisBindings[gamePadAxisAction][i];
					var gamePadAxisValue = input.GetGamePadAxis(gamePadAxisInfo.axis, gamePadAxisInfo.controller);

					if(Math.Abs(gamePadAxisValue.x) < gamePadAxisDeadZone && Math.Abs(gamePadAxisValue.y) < gamePadAxisDeadZone)
					{
						gamePadAxisValue = Vector2f.zero;
					}

					gamePadAxisInfo.value = gamePadAxisValue;
					gamePadAxisInfo.prevPressed = gamePadAxisInfo.pressed;
					gamePadAxisInfo.pressed = gamePadAxisInfo.value != Vector2f.zero;

					gamePadAxisBindings[gamePadAxisAction][i] = gamePadAxisInfo;

					if(OnActionPressed != null && !gamePadAxisInfo.prevPressed && gamePadAxisInfo.pressed)
					{
						OnActionPressed(gamePadAxisAction.first, gamePadAxisAction.second);
					}

					if(OnActionReleased != null && gamePadAxisInfo.prevPressed && !gamePadAxisInfo.pressed)
					{
						OnActionReleased(gamePadAxisAction.first, gamePadAxisAction.second);
					}
				}
			}

			var gamePadTriggerActionEnumerator = gamePadTriggerBindings.GetEnumerator();

			while(gamePadTriggerActionEnumerator.MoveNext())
			{
				var gamePadTriggerAction = gamePadTriggerActionEnumerator.Current.Key;

				for (int i = 0; i < gamePadTriggerBindings[gamePadTriggerAction].Count; ++i)
				{
					var gamePadTriggerInfo = gamePadTriggerBindings[gamePadTriggerAction][i];
					var gamePadTriggerValue = input.GetGamePadTrigger(gamePadTriggerInfo.trigger, gamePadTriggerInfo.controller);

					if(Math.Abs(gamePadTriggerValue) < gamePadTriggerDeadZone)
					{
						gamePadTriggerValue = 0f;
					}

					gamePadTriggerInfo.value = gamePadTriggerValue;
					gamePadTriggerInfo.prevPressed = gamePadTriggerInfo.pressed;
					gamePadTriggerInfo.pressed = gamePadTriggerInfo.value != 0f;

					gamePadTriggerBindings[gamePadTriggerAction][i] = gamePadTriggerInfo;

					if(OnActionPressed != null && !gamePadTriggerInfo.prevPressed && gamePadTriggerInfo.pressed)
					{
						OnActionPressed(gamePadTriggerAction.first, gamePadTriggerAction.second);
					}

					if(OnActionReleased != null && gamePadTriggerInfo.prevPressed && !gamePadTriggerInfo.pressed)
					{
						OnActionReleased(gamePadTriggerAction.first, gamePadTriggerAction.second);
					}
				}
			}
		}

		public void ResetBindings()
		{
			keyboardKeyBindings.Clear();
			mouseButtonBindings.Clear();
			gamePadButtonBindings.Clear();
			gamePadAxisBindings.Clear();
			gamePadTriggerBindings.Clear();
		}

		public void ResetBind(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);

			if(keyboardKeyBindings.ContainsKey(actionId))
			{
				keyboardKeyBindings.Remove(actionId);
			}

			if(mouseButtonBindings.ContainsKey(actionId))
			{
				mouseButtonBindings.Remove(actionId);
			}

			if(gamePadButtonBindings.ContainsKey(actionId))
			{
				gamePadButtonBindings.Remove(actionId);
			}

			if(gamePadAxisBindings.ContainsKey(actionId))
			{
				gamePadAxisBindings.Remove(actionId);
			}

			if(gamePadTriggerBindings.ContainsKey(actionId))
			{
				gamePadTriggerBindings.Remove(actionId);
			}
		}

		public void AddKeyboardKey(Identifier action, KeyboardKey key, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var keyboardKeyInfo = new KeyboardKeyInfo(key, false, false);

			if(keyboardKeyBindings.ContainsKey(actionId))
			{
				keyboardKeyBindings[actionId].Add(keyboardKeyInfo);
			}
			else
			{
				var newList = new List<KeyboardKeyInfo>() { keyboardKeyInfo };
				keyboardKeyBindings.Add(actionId, newList);
			}
		}

		public void AddMouseButton(Identifier action, MouseButton button, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var mouseButtonInfo = new MouseButtonInfo(button, false, false);

			if(mouseButtonBindings.ContainsKey(actionId))
			{
				mouseButtonBindings[actionId].Add(mouseButtonInfo);
			}
			else
			{
				var newList = new List<MouseButtonInfo>() { mouseButtonInfo };
				mouseButtonBindings.Add(actionId, newList);
			}
		}

		public void AddGamePadButton(Identifier action, GamePadButton button, GamePadController controller = GamePadController.Any, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var gamePadButtonInfo = new GamePadButtonInfo(controller, button, false, false);

			if(gamePadButtonBindings.ContainsKey(actionId))
			{
				gamePadButtonBindings[actionId].Add(gamePadButtonInfo);
			}
			else
			{
				var newList = new List<GamePadButtonInfo>() { gamePadButtonInfo };
				gamePadButtonBindings.Add(actionId, newList);
			}
		}

		public void AddGamePadAxis(Identifier action, GamePadAxis axis, GamePadController controller = GamePadController.Any, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var gamePadAxisInfo = new GamePadAxisInfo(controller, axis, Vector2f.zero, false, false);

			if(gamePadAxisBindings.ContainsKey(actionId))
			{
				gamePadAxisBindings[actionId].Add(gamePadAxisInfo);
			}
			else
			{
				var newList = new List<GamePadAxisInfo>() { gamePadAxisInfo };
				gamePadAxisBindings.Add(actionId, newList);
			}
		}

		public void AddGamePadTrigger(Identifier action, GamePadTrigger trigger, GamePadController controller = GamePadController.Any, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var gamePadTriggerInfo = new GamePadTriggerInfo(controller, trigger, 0f, false, false);

			if(gamePadTriggerBindings.ContainsKey(actionId))
			{
				gamePadTriggerBindings[actionId].Add(gamePadTriggerInfo);
			}
			else
			{
				var newList = new List<GamePadTriggerInfo>() { gamePadTriggerInfo };
				gamePadTriggerBindings.Add(actionId, newList);
			}
		}

		public bool IsPressed(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var isPressed = false;

			if(!isPressed && keyboardKeyBindings.ContainsKey(actionId))
			{
				var keys = keyboardKeyBindings[actionId];

				for (int i = 0; i < keys.Count; ++i)
				{
					isPressed = (keys[i].pressed && ! keys[i].prevPressed);

					if(isPressed)
					{
						break;
					}
				}
			}

			if(!isPressed && mouseButtonBindings.ContainsKey(actionId))
			{
				var buttons = mouseButtonBindings[actionId];

				for (int i = 0; i < buttons.Count; ++i)
				{
					isPressed = (buttons[i].pressed && ! buttons[i].prevPressed);

					if(isPressed)
					{
						break;
					}
				}
			}

			if(!isPressed && gamePadButtonBindings.ContainsKey(actionId))
			{
				var buttons = gamePadButtonBindings[actionId];

				for (int i = 0; i < buttons.Count; ++i)
				{
					isPressed = (buttons[i].pressed && ! buttons[i].prevPressed);

					if(isPressed)
					{
						break;
					}
				}
			}

			if(!isPressed && gamePadAxisBindings.ContainsKey(actionId))
			{
				var axisList = gamePadAxisBindings[actionId];

				for (int i = 0; i < axisList.Count; ++i)
				{
					isPressed = (axisList[i].pressed && ! axisList[i].prevPressed);

					if(isPressed)
					{
						break;
					}
				}
			}

			if(!isPressed && gamePadTriggerBindings.ContainsKey(actionId))
			{
				var triggers = gamePadTriggerBindings[actionId];

				for (int i = 0; i < triggers.Count; ++i)
				{
					isPressed = (triggers[i].pressed && ! triggers[i].prevPressed);

					if(isPressed)
					{
						break;
					}
				}
			}

			return isPressed;
		}

		public bool IsHeld(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var isHeld = false;

			if(!isHeld && keyboardKeyBindings.ContainsKey(actionId))
			{
				var keys = keyboardKeyBindings[actionId];

				for (int i = 0; i < keys.Count; ++i)
				{
					isHeld = keys[i].pressed;

					if(isHeld)
					{
						break;
					}
				}
			}

			if(!isHeld && mouseButtonBindings.ContainsKey(actionId))
			{
				var buttons = mouseButtonBindings[actionId];

				for (int i = 0; i < buttons.Count; ++i)
				{
					isHeld = buttons[i].pressed;

					if(isHeld)
					{
						break;
					}
				}
			}

			if(!isHeld && gamePadButtonBindings.ContainsKey(actionId))
			{
				var buttons = gamePadButtonBindings[actionId];

				for (int i = 0; i < buttons.Count; ++i)
				{
					isHeld = buttons[i].pressed;

					if(isHeld)
					{
						break;
					}
				}
			}

			if(!isHeld && gamePadAxisBindings.ContainsKey(actionId))
			{
				var axisList = gamePadAxisBindings[actionId];

				for (int i = 0; i < axisList.Count; ++i)
				{
					isHeld = axisList[i].pressed;

					if(isHeld)
					{
						break;
					}
				}
			}

			if(!isHeld && gamePadTriggerBindings.ContainsKey(actionId))
			{
				var triggers = gamePadTriggerBindings[actionId];

				for (int i = 0; i < triggers.Count; ++i)
				{
					isHeld = triggers[i].pressed;

					if(isHeld)
					{
						break;
					}
				}
			}

			return isHeld;
		}

		public bool IsReleased(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);
			var isReleased = false;

			if(!isReleased && keyboardKeyBindings.ContainsKey(actionId))
			{
				var keys = keyboardKeyBindings[actionId];

				for (int i = 0; i < keys.Count; ++i)
				{
					isReleased = (! keys[i].pressed && keys[i].prevPressed);

					if(isReleased)
					{
						break;
					}
				}
			}

			if(!isReleased && mouseButtonBindings.ContainsKey(actionId))
			{
				var buttons = mouseButtonBindings[actionId];

				for (int i = 0; i < buttons.Count; ++i)
				{
					isReleased = (! buttons[i].pressed && buttons[i].prevPressed);

					if(isReleased)
					{
						break;
					}
				}
			}

			if(!isReleased && gamePadButtonBindings.ContainsKey(actionId))
			{
				var buttons = gamePadButtonBindings[actionId];

				for (int i = 0; i < buttons.Count; ++i)
				{
					isReleased = (! buttons[i].pressed && buttons[i].prevPressed);

					if(isReleased)
					{
						break;
					}
				}
			}

			if(!isReleased && gamePadAxisBindings.ContainsKey(actionId))
			{
				var axisList = gamePadAxisBindings[actionId];

				for (int i = 0; i < axisList.Count; ++i)
				{
					isReleased = (! axisList[i].pressed && axisList[i].prevPressed);

					if(isReleased)
					{
						break;
					}
				}
			}

			if(!isReleased && gamePadTriggerBindings.ContainsKey(actionId))
			{
				var triggers = gamePadTriggerBindings[actionId];

				for (int i = 0; i < triggers.Count; ++i)
				{
					isReleased = (! triggers[i].pressed && triggers[i].prevPressed);

					if(isReleased)
					{
						break;
					}
				}
			}

			return isReleased;
		}

		public List<KeyboardKeyInfo> GetKeyboardKeys(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);

			if(keyboardKeyBindings.ContainsKey(actionId))
			{
				return keyboardKeyBindings[actionId];
			}

			return new List<KeyboardKeyInfo>();
		}

		public List<MouseButtonInfo> GetMouseButtons(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);

			if(mouseButtonBindings.ContainsKey(actionId))
			{
				return mouseButtonBindings[actionId];
			}

			return new List<MouseButtonInfo>();
		}

		public List<GamePadButtonInfo> GetGamePadButtons(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);

			if(gamePadButtonBindings.ContainsKey(actionId))
			{
				return gamePadButtonBindings[actionId];
			}

			return new List<GamePadButtonInfo>();
		}

		public List<GamePadAxisInfo> GetGamePadAxis(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);

			if(gamePadAxisBindings.ContainsKey(actionId))
			{
				return gamePadAxisBindings[actionId];
			}

			return new List<GamePadAxisInfo>();
		}

		public List<GamePadTriggerInfo> GetGamePadTriggers(Identifier action, PlayerId player = PlayerId.Player1)
		{
			var actionId = new InputActionId(action, player);

			if(gamePadTriggerBindings.ContainsKey(actionId))
			{
				return gamePadTriggerBindings[actionId];
			}

			return new List<GamePadTriggerInfo>();
		}
	}
}
