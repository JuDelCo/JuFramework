using UnityEngine;
using UnityEngine.Rendering;

public class TestGraphics : MonoBehaviour
{
	/*
	 * TAREAS:
	 *  - Testear posiciones y tamaños => Testear cam.WorldToScreenPoint(transform.position)
	 *  - Testear Z sorting
	 *  - Testear DLL
	 *  - Blend modes (custom materials) => https://wiki.libsdl.org/SDL_BlendMode
	 *  - Dibujar figuras geométricas simples (punto, línea, rectangulo, circulo)
	 *	- Optimizar (tilemap, particulas, sprites) => http://wiki.unity3d.com/index.php/MeshMerger
	 */
	
	private Camera cam;
	private RenderTexture rt;

	public Shader shader;
	private Material material;

	public Texture2D texture;

	private Mesh meshQuad;
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
		InitializeMesh();
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
		cam.rect = new Rect(0f, 0f, 1f, 1f);
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

	private void InitializeMesh()
	{
		var vertices = new Vector3[4]
		{
			new Vector3(1f, 1f, 0f),
			new Vector3(1f, -1f, 0f),
			new Vector3(-1f, 1f, 0f),
			new Vector3(-1f, -1f, 0f)
		};

		var uvs = new Vector2[4]
		{
			new Vector2(1, 1),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(0, 0),
		};

		var triangles = new int[6]
		{
			0, 1, 2,
			2, 1, 3,
		};

		meshQuad = new Mesh();
		meshQuad.vertices = vertices;
		meshQuad.uv = uvs;
		meshQuad.triangles = triangles;
		meshQuad.RecalculateNormals();
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

	private void UpdateTestCommandBuffer()
	{
		cbTest.Clear();
		cbTest.SetRenderTarget(rt);

		cbTestMatrix.m03 = Mathf.Sin(Time.fixedTime);
		cbTestMatrix.m13 = Mathf.Cos(Time.fixedTime);
		// cbTest.DrawMesh(meshQuad, cbTestMatrix, material);
		cbTest.DrawMesh(meshObject, cbTestMatrix, material);

		// cbTest.Blit(texture, new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive));
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
		var cameraViewRect = new Rect(0, 0, 490, 270);

		Graphics.DrawTexture(cameraViewRect, texture, material);

		GL.PopMatrix();
	}

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
		UpdateTestCommandBuffer();

		// Graphics.DrawMesh(meshObject, new Vector3(Mathf.Sin(Time.fixedTime), Mathf.Cos(Time.fixedTime), 5), Quaternion.Euler(Time.fixedTime * 150, Time.fixedTime * 80, Time.fixedTime * 100), material, 0);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			ExportRenderTexture(rt, "_rt");
		}
	}

	private void OnPostRender() // Usar OnRenderObject si no tiene una cámara el gameobject
	{
		Graphics.SetRenderTarget(rt);

		GL.Clear(true, false, new Color(0.2f, 0.2f, 0.2f), 1f);
		// Graphics.ExecuteCommandBuffer(cbClearRenderTarget);

		// + GL.<methods> (remember push/pop matrix)
		material.SetPass(0);
		// Graphics.DrawMeshNow(meshQuad, new Vector3(Mathf.Sin(Time.fixedTime), Mathf.Cos(Time.fixedTime), 5), Quaternion.identity);
		Graphics.DrawMeshNow(meshObject, new Vector3(Mathf.Sin(Time.fixedTime), Mathf.Cos(Time.fixedTime), 5), Quaternion.Euler(Time.fixedTime * 150, Time.fixedTime * 80, Time.fixedTime * 100));

		// Graphics.ExecuteCommandBuffer(cbTest);

		// DrawTexture();

		Graphics.Blit(rt, cam.targetTexture);
		Graphics.SetRenderTarget(null);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		// Graphics.SetRenderTarget(rt);

		// GL.Clear(true, false, new Color(0.2f, 0.2f, 0.2f), 1f);
		// Graphics.ExecuteCommandBuffer(cbClearRenderTarget);

		// + GL.<methods> (remember push/pop matrix)
		// material.SetPass(0);
		// Graphics.DrawMeshNow(meshQuad, new Vector3(Mathf.Sin(Time.fixedTime), Mathf.Cos(Time.fixedTime), 5), Quaternion.identity);
		// Graphics.DrawMeshNow(meshObject, new Vector3(Mathf.Sin(Time.fixedTime), Mathf.Cos(Time.fixedTime), 5), Quaternion.Euler(Time.fixedTime * 150, Time.fixedTime * 80, Time.fixedTime * 100));

		// Graphics.ExecuteCommandBuffer(cbTest);

		// Graphics.Blit(texture/src, material);
		// Graphics.Blit(texture/src, dest);
		// Graphics.Blit(texture/src, dest, materialEffect);

		// DrawTexture();

		// Graphics.Blit(rt, dest);
		// Graphics.SetRenderTarget(null);
	}
}
