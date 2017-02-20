using UnityEngine;
using UnityEngine.Rendering;

public class TestCommandBuffer : MonoBehaviour
{
	private Camera cam;
	public Rect viewport;
	private RenderTexture rt;

	public Shader shader;
	private Material material;
	public Texture2D texture;
	public Mesh meshObject;

	private CommandBuffer cbClearRenderTarget;
	private CommandBuffer cbTest;
	private Matrix4x4 cbTestMatrix;

	// ======================================================

	private void Start()
	{
		//Application.targetFrameRate = 30; // Need vsync to be deactivated

		InitializeCamera();
		InitializeRenderTexture();
		InitializeMaterial();
		InitializeClearScreenCommandBuffer();
		InitializeTestCommandBuffer();
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

	private void InitializeClearScreenCommandBuffer()
	{
		cbClearRenderTarget = new CommandBuffer();
		cbClearRenderTarget.name = "ClearScreenCommandBuffer";

		cbClearRenderTarget.SetRenderTarget(rt);
		cbClearRenderTarget.ClearRenderTarget(true, false, new Color(0.2f, 0.2f, 0.2f), 1f);
	}

	private void InitializeTestCommandBuffer()
	{
		cbTest = new CommandBuffer();
		cbTest.name = "TestCommandBuffer";

		cbTestMatrix = Matrix4x4.identity;
		cbTestMatrix.m23 = 5;

		UpdateTestCommandBuffer();
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
	
	private void UpdateTestCommandBuffer()
	{
		cbTest.Clear();
		cbTest.SetRenderTarget(rt);

		cbTestMatrix.m03 = Mathf.Sin(Time.fixedTime);
		cbTestMatrix.m13 = Mathf.Cos(Time.fixedTime);

		cbTest.DrawMesh(meshObject, cbTestMatrix, material);
		// cbTest.Blit(texture, new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive));
	}
	
	// ======================================================

	private void Update()
	{
		UpdateTestCommandBuffer();
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ExportRenderTexture(rt, "_rt_testcommandbuffer");
		}
	}

	private void OnPostRender() // Usar OnRenderObject si no tiene una cámara el gameobject
	{
		Graphics.ExecuteCommandBuffer(cbClearRenderTarget);
		Graphics.ExecuteCommandBuffer(cbTest);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(rt, dest);
	}
}
