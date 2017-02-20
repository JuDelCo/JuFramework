
namespace JuFramework
{
	public enum PlayerId
	{
		Player1 = 1,
		Player2 = 2,
		Player3 = 3,
		Player4 = 4,
		Player5 = 5,
		Player6 = 6,
		Player7 = 7,
		Player8 = 8
	}

	public enum MouseButton
	{
		None = 0,
		Button1 = 323,
		Button2 = 324,
		Button3 = 325,
		Button4 = 326,
		Button5 = 327,
		Button6 = 328,
		Button7 = 329,
		Left = Button1,
		Right = Button2,
		Middle = Button3
	}

	public enum MouseLockMode
	{
		None = 0,
		Confined,
		Locked
	}

	public enum KeyboardKey
	{
		None = 0,
		Backspace = 8,
		Tab = 9,
		Clear = 12,
		Return = 13,
		Pause = 19,
		Escape = 27,
		Space = 32,
		Exclaim = 33,       // !
		DoubleQuote = 34,   // "
		Hash = 35,          // #
		Dollar = 36,        // $
		Ampersand = 38,     // &
		Quote = 39,         // ' (Apostrophe)
		LeftParen = 40,     // (
		RightParen = 41,    // )
		Asterisk = 42,      // *
		Plus = 43,          // +
		Comma = 44,         // ,
		Minus = 45,         // -
		Period = 46,        // .
		Slash = 47,         // /
		Colon = 58,         // :
		Semicolon = 59,     // ;
		Less = 60,          // <
		Equals = 61,        // =
		Greater = 62,       // >
		Question = 63,      // ?
		At = 64,            // @
		LeftBracket = 91,   // [
		Backslash = 92,     // \
		RightBracket = 93,  // ]
		Caret = 94,         // ^
		Underscore = 95,    // _
		BackQuote = 96,     // ` (Grave accent)
		Num0 = 48,
		Num1 = 49,
		Num2 = 50,
		Num3 = 51,
		Num4 = 52,
		Num5 = 53,
		Num6 = 54,
		Num7 = 55,
		Num8 = 56,
		Num9 = 57,
		A = 97,
		B = 98,
		C = 99,
		D = 100,
		E = 101,
		F = 102,
		G = 103,
		H = 104,
		I = 105,
		J = 106,
		K = 107,
		L = 108,
		M = 109,
		N = 110,
		O = 111,
		P = 112,
		Q = 113,
		R = 114,
		S = 115,
		T = 116,
		U = 117,
		V = 118,
		W = 119,
		X = 120,
		Y = 121,
		Z = 122,
		Keypad0 = 256,
		Keypad1 = 257,
		Keypad2 = 258,
		Keypad3 = 259,
		Keypad4 = 260,
		Keypad5 = 261,
		Keypad6 = 262,
		Keypad7 = 263,
		Keypad8 = 264,
		Keypad9 = 265,
		KeypadPeriod = 266,     // .
		KeypadDivide = 267,     // /
		KeypadMultiply = 268,   // *
		KeypadMinus = 269,      // -
		KeypadPlus = 270,       // +
		KeypadEquals = 272,     // =
		KeypadEnter = 271,
		UpArrow = 273,
		DownArrow = 274,
		RightArrow = 275,
		LeftArrow = 276,
		Delete = 127,
		Insert = 277,
		Home = 278,
		End = 279,
		PageUp = 280,
		PageDown = 281,
		F1 = 282,
		F2 = 283,
		F3 = 284,
		F4 = 285,
		F5 = 286,
		F6 = 287,
		F7 = 288,
		F8 = 289,
		F9 = 290,
		F10 = 291,
		F11 = 292,
		F12 = 293,
		F13 = 294,
		F14 = 295,
		F15 = 296,
		Numlock = 300,
		CapsLock = 301,
		ScrollLock = 302,
		RightShift = 303,
		LeftShift = 304,
		RightControl = 305,
		LeftControl = 306,
		RightAlt = 307,
		LeftAlt = 308,
		RightCommand = 309,
		LeftCommand = 310,
		LeftWindows = 311,
		RightWindows = 312,
		AltGr = 313,
		Help = 315,
		Print = 316,
		SysReq = 317,
		Break = 318,
		Menu = 319
	}

	public enum GamePadController
	{
		None = 0,
		Controller_1 = 1,
		Controller_2 = 2,
		Controller_3 = 3,
		Controller_4 = 4,
		Controller_5 = 5,
		Controller_6 = 6,
		Controller_7 = 7,
		Controller_8 = 8,
		Any = 9
	}

	public enum GamePadAxis
	{
		None = 0,
		Dir_Pad = 1,
		L_Stick = 2,
		R_Stick = 3
	}

	public enum GamePadTrigger
	{
		None = 0,
		Left = 1,
		Right = 2
	}

	public enum GamePadButton
	{
		None = 0,
		Button_1 = 1,
		Button_2 = 2,
		Button_3 = 3,
		Button_4 = 4,
		Button_5 = 5,
		Button_6 = 6,
		Button_7 = 7,
		Button_8 = 8,
		Button_9 = 9,
		Button_10 = 10,
		Button_11 = 11,
		Button_12 = 12,
		Button_13 = 13,
		Button_14 = 14,
		Button_15 = 15,
		Button_16 = 16,
		Button_17 = 17,
		Button_18 = 18,
		Button_19 = 19,
		Button_20 = 20,
		A = 1,
		B = 2,
		X = 3,
		Y = 4,
		L = 5,
		R = 6,
		Back = 7,
		Start = 8,
		L_Stick = 9,
		R_Stick = 10
	}

	public enum GamePadButtonInternal
	{
		None = 0,
		Any_1 = 330,
		Any_2 = 331,
		Any_3 = 332,
		Any_4 = 333,
		Any_5 = 334,
		Any_6 = 335,
		Any_7 = 336,
		Any_8 = 337,
		Any_9 = 338,
		Any_10 = 339,
		Any_11 = 340,
		Any_12 = 341,
		Any_13 = 342,
		Any_14 = 343,
		Any_15 = 344,
		Any_16 = 345,
		Any_17 = 346,
		Any_18 = 347,
		Any_19 = 348,
		Any_20 = 349,
		C1_1 = 350,
		C1_2 = 351,
		C1_3 = 352,
		C1_4 = 353,
		C1_5 = 354,
		C1_6 = 355,
		C1_7 = 356,
		C1_8 = 357,
		C1_9 = 358,
		C1_10 = 359,
		C1_11 = 360,
		C1_12 = 361,
		C1_13 = 362,
		C1_14 = 363,
		C1_15 = 364,
		C1_16 = 365,
		C1_17 = 366,
		C1_18 = 367,
		C1_19 = 368,
		C1_20 = 369,
		C2_1 = 370,
		C2_2 = 371,
		C2_3 = 372,
		C2_4 = 373,
		C2_5 = 374,
		C2_6 = 375,
		C2_7 = 376,
		C2_8 = 377,
		C2_9 = 378,
		C2_10 = 379,
		C2_11 = 380,
		C2_12 = 381,
		C2_13 = 382,
		C2_14 = 383,
		C2_15 = 384,
		C2_16 = 385,
		C2_17 = 386,
		C2_18 = 387,
		C2_19 = 388,
		C2_20 = 389,
		C3_1 = 390,
		C3_2 = 391,
		C3_3 = 392,
		C3_4 = 393,
		C3_5 = 394,
		C3_6 = 395,
		C3_7 = 396,
		C3_8 = 397,
		C3_9 = 398,
		C3_10 = 399,
		C3_11 = 400,
		C3_12 = 401,
		C3_13 = 402,
		C3_14 = 403,
		C3_15 = 404,
		C3_16 = 405,
		C3_17 = 406,
		C3_18 = 407,
		C3_19 = 408,
		C3_20 = 409,
		C4_1 = 410,
		C4_2 = 411,
		C4_3 = 412,
		C4_4 = 413,
		C4_5 = 414,
		C4_6 = 415,
		C4_7 = 416,
		C4_8 = 417,
		C4_9 = 418,
		C4_10 = 419,
		C4_11 = 420,
		C4_12 = 421,
		C4_13 = 422,
		C4_14 = 423,
		C4_15 = 424,
		C4_16 = 425,
		C4_17 = 426,
		C4_18 = 427,
		C4_19 = 428,
		C4_20 = 429,
		C5_1 = 430,
		C5_2 = 431,
		C5_3 = 432,
		C5_4 = 433,
		C5_5 = 434,
		C5_6 = 435,
		C5_7 = 436,
		C5_8 = 437,
		C5_9 = 438,
		C5_10 = 439,
		C5_11 = 440,
		C5_12 = 441,
		C5_13 = 442,
		C5_14 = 443,
		C5_15 = 444,
		C5_16 = 445,
		C5_17 = 446,
		C5_18 = 447,
		C5_19 = 448,
		C5_20 = 449,
		C6_1 = 450,
		C6_2 = 451,
		C6_3 = 452,
		C6_4 = 453,
		C6_5 = 454,
		C6_6 = 455,
		C6_7 = 456,
		C6_8 = 457,
		C6_9 = 458,
		C6_10 = 459,
		C6_11 = 460,
		C6_12 = 461,
		C6_13 = 462,
		C6_14 = 463,
		C6_15 = 464,
		C6_16 = 465,
		C6_17 = 466,
		C6_18 = 467,
		C6_19 = 468,
		C6_20 = 469,
		C7_1 = 470,
		C7_2 = 471,
		C7_3 = 472,
		C7_4 = 473,
		C7_5 = 474,
		C7_6 = 475,
		C7_7 = 476,
		C7_8 = 477,
		C7_9 = 478,
		C7_10 = 479,
		C7_11 = 480,
		C7_12 = 481,
		C7_13 = 482,
		C7_14 = 483,
		C7_15 = 484,
		C7_16 = 485,
		C7_17 = 486,
		C7_18 = 487,
		C7_19 = 488,
		C7_20 = 489,
		C8_1 = 490,
		C8_2 = 491,
		C8_3 = 492,
		C8_4 = 493,
		C8_5 = 494,
		C8_6 = 495,
		C8_7 = 496,
		C8_8 = 497,
		C8_9 = 498,
		C8_10 = 499,
		C8_11 = 500,
		C8_12 = 501,
		C8_13 = 502,
		C8_14 = 503,
		C8_15 = 504,
		C8_16 = 505,
		C8_17 = 506,
		C8_18 = 507,
		C8_19 = 508,
		C8_20 = 509
	}

	public struct KeyboardKeyInfo
	{
		public KeyboardKey key;
		public bool pressed;
		public bool prevPressed;

		public KeyboardKeyInfo(KeyboardKey key, bool pressed, bool prevPressed)
		{
			this.key = key;
			this.pressed = pressed;
			this.prevPressed = prevPressed;
		}
	}

	public struct MouseButtonInfo
	{
		public MouseButton button;
		public bool pressed;
		public bool prevPressed;

		public MouseButtonInfo(MouseButton button, bool pressed, bool prevPressed)
		{
			this.button = button;
			this.pressed = pressed;
			this.prevPressed = prevPressed;
		}
	}

	public struct GamePadButtonInfo
	{
		public GamePadController controller;
		public GamePadButton button;
		public bool pressed;
		public bool prevPressed;

		public GamePadButtonInfo(GamePadController controller, GamePadButton button, bool pressed, bool prevPressed)
		{
			this.controller = controller;
			this.button = button;
			this.pressed = pressed;
			this.prevPressed = prevPressed;
		}
	}

	public struct GamePadAxisInfo
	{
		public GamePadController controller;
		public GamePadAxis axis;
		public Vector2f value;
		public bool pressed;
		public bool prevPressed;

		public GamePadAxisInfo(GamePadController controller, GamePadAxis axis, Vector2f value, bool pressed, bool prevPressed)
		{
			this.controller = controller;
			this.axis = axis;
			this.value = value;
			this.pressed = pressed;
			this.prevPressed = prevPressed;
		}
	}

	public struct GamePadTriggerInfo
	{
		public GamePadController controller;
		public GamePadTrigger trigger;
		public float value;
		public bool pressed;
		public bool prevPressed;

		public GamePadTriggerInfo(GamePadController controller, GamePadTrigger trigger, float value, bool pressed, bool prevPressed)
		{
			this.controller = controller;
			this.trigger = trigger;
			this.value = value;
			this.pressed = pressed;
			this.prevPressed = prevPressed;
		}
	}
}
