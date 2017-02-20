
namespace JuFramework
{
	[UnityEngine.DisallowMultipleComponent]
	public class UnityBootstrap<T> : UnityEngine.MonoBehaviour where T : App, new()
	{
		private Core core;

		public void Awake()
		{
			core = new Core(new T());
		}

		public void Start()
		{
			this.transform.hideFlags = UnityEngine.HideFlags.NotEditable;

			Core.resource = new UnityResourceLoader();
			Core.graphics = new UnityGraphics(this.gameObject);
			Core.debug = new UnityDebug(UnityDebug.LogLevel.Debug);
			Core.screen = new UnityScreen();
			Core.input = new UnityInput();

			core.Initialize();
		}

		public void Update()
		{
			core.Update(Time.Seconds(UnityEngine.Time.deltaTime));

			if(Core.mainCamera != null)
			{
				// Fix Unity position rendering (or else will use the previous frame camera position)
				Core.mainCamera.Use(Core.graphics);
			}
		}

		public void OnPostRender()
		{
			core.Draw(Core.graphics);
		}

		public void OnApplicationFocus(bool hasFocus)
		{
			if(Core.screen != null)
			{
				Core.screen.hasFocus = hasFocus;
			}
		}

		public void OnApplicationPause(bool pauseStatus)
		{
			if(Core.screen != null)
			{
				Core.screen.hasFocus = !pauseStatus;
			}
		}
	}
}
