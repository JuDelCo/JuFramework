using UnityEngine;

public class TestDrawGL : MonoBehaviour
{
	private Camera cam;
	public Rect viewport;
	private RenderTexture rt;

	public Shader shader;
	private Material material;

	// ======================================================

	private void Start()
	{
		//Application.targetFrameRate = 30; // Need vsync to be deactivated

		InitializeCamera();
		InitializeRenderTexture();
		InitializeMaterial();
	}

	private void InitializeCamera()
	{
		if(Camera.current == null)
		{
			cam = gameObject.AddComponent<Camera>();
		}
		else
		{
			cam = Camera.current;
		}

		cam.cameraType = CameraType.Game;
		cam.clearFlags = CameraClearFlags.Nothing;
		cam.cullingMask = ~0; // == -1
		cam.orthographic = true;
		cam.orthographicSize = 5.0f;
		cam.nearClipPlane = 0.3f;
		cam.farClipPlane = 1000f;
		cam.rect = viewport;
		cam.depth = 0;
		cam.renderingPath = RenderingPath.Forward;
		cam.targetTexture = null;
		cam.useOcclusionCulling = false;
		cam.hdr = false;
		cam.targetDisplay = 0;
	}

	private void InitializeRenderTexture()
	{
		//rt = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default); // ReleaseTemporary !
		
		rt = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
		rt.Create();
	}

	private void InitializeMaterial()
	{
		// Sprites/Default
		// Particles/Alpha Blended
		// Mobile/Particles/Alpha Blended
		// GUI/Text Shader
		// Hidden/GIDebug/VertexColors

		shader = Shader.Find("Sprites/Default");
		material = new Material(shader);
	}

	// ======================================================
	
	private void ExportRenderTexture(RenderTexture rt, string name = "")
	{
		if(rt == null)
		{
			Debug.Log("No valid rt to export (" + name + ")");
			return;
		}

		Graphics.SetRenderTarget(rt);

		var exportTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false, true);
		exportTexture.ReadPixels(new Rect(0, 0, RenderTexture.active.width, RenderTexture.active.height), 0, 0);
        exportTexture.Apply();

        var data = exportTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Export_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + name + ".png", data);

		Graphics.SetRenderTarget(null);
	}

	// ======================================================

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ExportRenderTexture(rt, "_rt_testdrawmesh");
		}
	}

	private void OnPostRender() // Usar OnRenderObject si no tiene una cámara el gameobject
	{
		Graphics.SetRenderTarget(rt);

		GL.Clear(true, false, new Color(0.2f, 0.2f, 0.2f), 1f);

		GL.PushMatrix();

		// For rendering in the local space of another object:
		//GL.LoadProjectionMatrix(GL.GetGPUProjectionMatrix(cam.projectionMatrix, true));
		//GL.LoadProjectionMatrix(cam.projectionMatrix);
		//GL.modelview = cam.worldToCameraMatrix * someObject.localToWorldMatrix;

		//GL.LoadPixelMatrix(0, 1, 0, 1); // would equal GL.LoadOrtho();
		//GL.LoadPixelMatrix(0, 1, 1, 0); // same as above but y-reversed so 0,0 is top left
		GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);

		material.SetPass(0);
		
		GL.Begin(GL.TRIANGLE_STRIP);
		for (int i = 0; i < 3; ++i)
		{
			GL.Color(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
			GL.Vertex3(Random.Range(0, Screen.width / 2), Random.Range(0, Screen.height / 2), 0f);
			GL.Vertex3(Random.Range(Screen.width / 2, Screen.width), Random.Range(Screen.height / 2, Screen.height), 0f);
		}
		GL.End();

		GL.PopMatrix();

		Graphics.SetRenderTarget(null);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(rt, dest);
	}
}
