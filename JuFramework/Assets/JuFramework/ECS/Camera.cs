
namespace JuFramework
{
	public abstract class Camera
	{
		protected Graphics graphics;

		private bool isOrthographic;
		private float zoom;
		private float fov;
		private Vector2i targetSize;
		private float nearDistance;
		private float farDistance;
		private FloatRect viewport;

		public Camera(Vector2i targetSize) : this(targetSize.x, targetSize.y)
		{
		}

		public Camera(int targetWidth, int targetHeight)
		{
			graphics = Core.graphics;

			SetOrthographic(false);
			SetZoom(1f);
			SetFov(45f);

			SetTargetSize(targetWidth, targetHeight);
			SetDistanceClipping(0.1f, 1000f);
			SetViewport(new FloatRect(0f, 0f, 1f, 1f));
		}

		public abstract void Use();

		public bool IsOrthographic()
		{
			return isOrthographic;
		}

		public void SetOrthographic(bool isOrthographic)
		{
			this.isOrthographic = isOrthographic;
		}

		public float GetZoom()
		{
			return zoom;
		}

		public void SetZoom(float zoom)
		{
			this.zoom = Math.Clamp(zoom, 0.0001f, 1000f);
		}

		public float GetFov()
		{
			return fov;
		}

		public void SetFov(float degrees)
		{
			this.fov = Math.Clamp(degrees, 0.1f, 175f);
		}

		public Vector2i GetTargetSize()
		{
			return targetSize;
		}

		public void SetTargetSize(int width, int height)
		{
			SetTargetSize(new Vector2i(width, height));
		}

		public void SetTargetSize(Vector2i targetSize)
		{
			this.targetSize = targetSize;
		}

		public float GetNearDistance()
		{
			return nearDistance;
		}

		public float GetFarDistance()
		{
			return farDistance;
		}

		public void SetDistanceClipping(float near, float far)
		{
			nearDistance = near;
			farDistance = far;
		}

		public FloatRect GetViewport()
		{
			return viewport;
		}

		public void SetViewport(float x, float y, float width, float height)
		{
			viewport = new FloatRect(x, y, width, height);
		}

		public void SetViewport(FloatRect viewport)
		{
			this.viewport = viewport;
		}

		public Matrix4 GetProjectionMatrix()
		{
			if(! isOrthographic)
			{
				return Frustum.PerspectiveFov(Math.Deg2Rad * fov, targetSize.x, targetSize.y, nearDistance, farDistance);
			}
			else
			{
				return Frustum.Orthographic(-targetSize.x / 2, targetSize.x / 2, -targetSize.y / 2, targetSize.y / 2, nearDistance, farDistance, zoom * 2f);
			}
		}

		public abstract Matrix4 GetWorldToCameraMatrix();

		public Matrix4 GetWorldToScreenMatrix()
		{
			return GetProjectionMatrix() * GetWorldToCameraMatrix();
		}

		public Matrix4 GetScreenToWorldMatrix()
		{
			return Math.Inverse(GetWorldToScreenMatrix());
		}

		public Vector2f WorldToScreenPoint(Vector3f worldPosition)
		{
			var screenPosition = Frustum.Project(worldPosition, GetWorldToCameraMatrix(), GetProjectionMatrix(), GetViewport());

			if(! isOrthographic)
			{
				screenPosition.x = -screenPosition.x + 1f;
				screenPosition.y = -screenPosition.y + 1f;
			}

			return screenPosition;
		}

		public Vector2f WorldToScreenPixel(Vector3f worldPosition)
		{
			return WorldToScreenPoint(worldPosition) * targetSize;
		}

		public Vector3f ScreenPointToWorld(Vector2f screenPosition)
		{
			return Frustum.UnProjectFaster(screenPosition, GetWorldToCameraMatrix(), GetProjectionMatrix(), GetViewport());
		}

		public Vector3f ScreenPixelToWorld(Vector2i pixelPosition)
		{
			return ScreenPointToWorld((Vector2f)pixelPosition / targetSize);
		}

		public abstract Ray ScreenPointToWorldRay(Vector2f screenPosition);

		public abstract Ray ScreenPixelToWorldRay(Vector2i pixelPosition);
	}
}
