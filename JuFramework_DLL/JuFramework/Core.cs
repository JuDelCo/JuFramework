
namespace JuFramework
{
	public class Core
	{
		public static Debug debug = null;
		public static Input input = null;
		public static Graphics graphics = null;
		public static ResourceLoader resource = null;
		public static Screen screen = null;
		public static Camera mainCamera = null;
		public static Time fixedTime { get; private set; }
		public static Time deltaTime { get; private set; }

		private App app;
		private Clock clock;

		public Core(App app)
		{
			this.app = app;
			this.clock = new Clock();
		}

		public void Initialize()
		{
			app.Initialize();
		}

		public void Update(Time dt)
		{
			fixedTime = clock.GetTimeElapsed();
			deltaTime = dt;

			if (Core.screen.hasFocus)
			{
				input.Update();
			}

			app.Update();
		}

		public void Draw(Graphics graphics)
		{
			graphics.Begin();

			app.Draw(graphics);

			graphics.End();
			graphics.Flush();
		}
	}
}
