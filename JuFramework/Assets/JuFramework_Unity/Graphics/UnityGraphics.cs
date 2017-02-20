
// TODO Graphics:
// 		Triangulizar (en métodos DrawPolygon) https://en.wikipedia.org/wiki/Two_ears_theorem

// https://docs.unity3d.com/ScriptReference/UI.VertexHelper.html

// UnityEngine.GL.GetGPUProjectionMatrix ?

// Mirror Test:
//		camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(Vector3 (1, -1, 1));
//		SetCullingMode(true)
//		render
//		SetCullingMode(false)

namespace JuFramework
{
	public class UnityGraphics : Graphics
	{
		private bool isDrawing;
		private Matrix4 projectionMatrixCache;
		private Matrix4 viewMatrixCache;
		private Matrix4 modelMatrixCache;
		private FloatRect viewportCache;

		private UnityEngine.Camera unityCamera;

		public UnityGraphics(UnityEngine.GameObject bootstrapGameObject) : base()
		{
			this.isDrawing = false;
			this.unityCamera = InitializeUnityCamera(bootstrapGameObject);
		}

		private UnityEngine.Camera InitializeUnityCamera(UnityEngine.GameObject bootstrapGameObject)
		{
			unityCamera = bootstrapGameObject.GetComponent<UnityEngine.Camera>();

			if(unityCamera == null)
			{
				unityCamera = bootstrapGameObject.AddComponent<UnityEngine.Camera>();
			}

			unityCamera.cameraType = UnityEngine.CameraType.Game;
			unityCamera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
			unityCamera.cullingMask = ~0; // == -1
			unityCamera.orthographic = false;
			unityCamera.orthographicSize = 10.0f;
			unityCamera.fieldOfView = 45f;
			unityCamera.nearClipPlane = 0.1f;
			unityCamera.farClipPlane = 1000f;
			unityCamera.rect = new UnityEngine.Rect(0f, 0f, 1f, 1f);
			unityCamera.depth = 0;
			unityCamera.renderingPath = UnityEngine.RenderingPath.Forward;
			unityCamera.targetTexture = null;
			unityCamera.useOcclusionCulling = false;
			unityCamera.hdr = false;
			unityCamera.targetDisplay = 0;
			//unityCamera.depthTextureMode = UnityEngine.DepthTextureMode.Depth;

			unityCamera.hideFlags = UnityEngine.HideFlags.NotEditable;

			return unityCamera;
		}

		private void UpdateUnityCamera(Camera camera, Vector3f position, Quat rotation)
		{
			if(camera == null)
			{
				return;
			}

			unityCamera.transform.position = position;
			unityCamera.transform.rotation = rotation;
			unityCamera.nearClipPlane = camera.GetNearDistance();
			unityCamera.farClipPlane = camera.GetFarDistance();
			unityCamera.rect = camera.GetViewport();
			unityCamera.orthographic = camera.IsOrthographic();

			if(camera.IsOrthographic())
			{
				// TODO: Add PixelPerUnit scaling ?
				unityCamera.orthographicSize = (Core.screen.height / 2) / camera.GetZoom();
			}
			else
			{
				unityCamera.fieldOfView = camera.GetFov();
			}
		}

		public override void Begin()
		{
			UnityEngine.GL.PushMatrix();

			projectionMatrixCache = Matrix4.identity;
			viewMatrixCache = Matrix4.identity;
			modelMatrixCache = Matrix4.identity;
			viewportCache = new FloatRect(0f, 0f, 1f, 1f);

			// GL.PushMatrix() resets config too ?
			UnityEngine.GL.modelview = Matrix4.identity;
			UnityEngine.GL.LoadProjectionMatrix(Matrix4.identity);
			//UnityEngine.GL.Viewport(viewportCache);

			isDrawing = true;
		}

		public override void End()
		{
			UnityEngine.GL.PopMatrix();

			isDrawing = false;
		}

		public override void Flush()
		{
			UnityEngine.GL.Flush();
		}

		public override void SetCamera(Camera camera, Vector3f position, Quat rotation)
		{
			UpdateUnityCamera(camera, position, rotation);
		}

		public override void SetProjectionMatrix(Matrix4 projectionMatrix)
		{
			if(isDrawing && projectionMatrixCache != projectionMatrix)
			{
				UnityEngine.GL.LoadProjectionMatrix(projectionMatrix);
				projectionMatrixCache = projectionMatrix;
			}
		}

		public override void SetWorldToViewMatrix(Matrix4 viewMatrix)
		{
			if(isDrawing && viewMatrixCache != viewMatrix)
			{
				UpdateModelToViewMatrix(viewMatrix * modelMatrixCache);
				viewMatrixCache = viewMatrix;
			}
		}

		public override void SetModelToWorldMatrix(Matrix4 modelMatrix)
		{
			if(isDrawing && modelMatrixCache != modelMatrix)
			{
				UpdateModelToViewMatrix(viewMatrixCache * modelMatrix);
				modelMatrixCache = modelMatrix;
			}
		}

		private void UpdateModelToViewMatrix(Matrix4 modelToViewMatrix)
		{
			// NOTE: DrawMeshNow overrides this value
			UnityEngine.GL.modelview = modelToViewMatrix;
		}

		public override void SetViewport(FloatRect viewport)
		{
			if(isDrawing && viewportCache != viewport)
			{
				UnityEngine.GL.Viewport(viewport);
				viewportCache = viewport;
			}
		}

		public override void SetCullingMode(bool inverted)
		{
			// false => FrontFace ==  CW & BackFace == CCW
			// true  => FrontFace == CCW & BackFace ==  CW
			UnityEngine.GL.invertCulling = inverted;
		}

		public override void SetWireframe(bool active)
		{
			UnityEngine.GL.wireframe = active;
		}

		public override void ClearAuto(bool enabled, Color color)
		{
			if(enabled)
			{
				unityCamera.clearFlags = UnityEngine.CameraClearFlags.Color & UnityEngine.CameraClearFlags.Depth;
				unityCamera.backgroundColor = color;
			}
			else
			{
				unityCamera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
			}
		}

		public override void ClearColor(Color color)
		{
			UnityEngine.GL.Clear(false, true, color, 1.0f);
		}

		public override void ClearDepth(float depth)
		{
			UnityEngine.GL.Clear(true, false, Color.clear, depth);
		}

		public override void Clear(Color color, float depth)
		{
			UnityEngine.GL.Clear(true, true, color, depth);
		}

		public override void DrawMesh(Mesh mesh)
		{
			UnityEngine.Graphics.DrawMeshNow((UnityMesh)mesh, modelMatrixCache);
		}
	}
}
