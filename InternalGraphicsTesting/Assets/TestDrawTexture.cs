using UnityEngine;

public class TestDrawTexture : MonoBehaviour
{
	private Camera cam;
	public Rect viewport;
	private RenderTexture rt;

	public Shader shader;
	private Material material;
	public Texture2D texture;

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
		shader = Shader.Find("Unlit/Texture");
		material = new Material(shader);
		material.mainTexture = texture;
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

	private void DrawTexture()
	{
		GL.PushMatrix();

		// For rendering in the local space of another object:
		//GL.LoadProjectionMatrix(GL.GetGPUProjectionMatrix(mainCamera.projectionMatrix, true));
		//GL.LoadProjectionMatrix(camera.projectionMatrix);
		//GL.modelview = camera.worldToCameraMatrix * someObject.localToWorldMatrix;

		//GL.LoadPixelMatrix(0, 1, 0, 1); // would equal GL.LoadOrtho();
		//GL.LoadPixelMatrix(0, 1, 1, 0); // same as above but y-reversed so 0,0 is top left
		GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);

		//var cameraViewRect = new Rect(cam.rect.xMin * Screen.width, Screen.height - cam.rect.yMax * Screen.height, cam.pixelWidth, cam.pixelHeight);
		//                     new Rect (posX - stampTexture.width / 2, (rt.height - posY) - stampTexture.height / 2, stampTexture.width, stampTexture.height);
		var cameraViewRect = new Rect((Screen.width / 2) + (Mathf.Sin(Time.fixedTime) * 100) - (Screen.width / 8), (Screen.height / 2) + (Mathf.Cos(Time.fixedTime) * 100) - (Screen.height / 8), (Screen.width / 4), (Screen.height / 4));

		Graphics.DrawTexture(cameraViewRect, texture, material);

		GL.PopMatrix();
	}

	// ======================================================

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ExportRenderTexture(rt, "_rt_testdrawtexture");
		}
	}

	private void OnPostRender() // Usar OnRenderObject si no tiene una cámara el gameobject
	{
		Graphics.SetRenderTarget(rt);

		GL.Clear(true, false, new Color(0.2f, 0.2f, 0.2f), 1f);
		DrawTexture();

		Graphics.SetRenderTarget(null);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(rt, dest);
	}
}
