
namespace JuFramework
{
	[UnityEngine.DisallowMultipleComponent]
	public class UnityBootstrap<T> : UnityEngine.MonoBehaviour where T : App, new()
	{
		private Core core;

		private void Awake()
		{
			core = new Core(new T());
		}

		private void Start()
		{
			//this.gameObject.hideFlags = UnityEngine.HideFlags.NotEditable;
			this.transform.hideFlags = UnityEngine.HideFlags.NotEditable;
			this.hideFlags = UnityEngine.HideFlags.NotEditable;

			Core.resource = new UnityResourceLoader();
			Core.graphics = new UnityGraphics(this.gameObject);
			Core.debug = new UnityDebug(Debug.LogLevel.Debug);
			Core.screen = new UnityScreen();
			Core.input = new UnityInput();

			core.Initialize();
		}

		private void Update()
		{
			core.Update(Time.Seconds(UnityEngine.Time.deltaTime));

			if(Core.mainCamera != null)
			{
				// Fix Unity position rendering (or else will use the previous frame camera position)
				// Useful if you use Unity gameObject too (meshRenderer, spriteRenderer, UI in world space, ...)
				Core.mainCamera.Use();
			}
		}

		private void OnPostRender()
		{
			core.Draw(Core.graphics);
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if(Core.screen != null)
			{
				Core.screen.hasFocus = hasFocus;
			}
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if(Core.screen != null)
			{
				Core.screen.hasFocus = !pauseStatus;
			}
		}
	}
}
