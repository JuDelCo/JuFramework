
namespace JuFramework
{
	public class Camera3D : Camera
	{
		private Transform3D transform;

		public Camera3D(Vector2i targetSize) : base(targetSize.x, targetSize.y)
		{
			SetTransform(new Transform3D());
		}

		public Camera3D(int targetWidth, int targetHeight) : base(targetWidth, targetHeight)
		{
			SetTransform(new Transform3D());
		}

		public Camera3D(Transform3D transform, Vector2i targetSize) : base(targetSize.x, targetSize.y)
		{
			SetTransform(transform);
		}

		public Camera3D(Transform3D transform, int targetWidth, int targetHeight) : base(targetWidth, targetHeight)
		{
			SetTransform(transform);
		}

		public override void Use(Graphics graphics)
		{
			graphics.SetCamera(this, GetPosition(), GetTransform().GetRotation());
			graphics.SetModelToWorldMatrix(Matrix4.identity);
			graphics.SetWorldToViewMatrix(GetWorldToCameraMatrix());
			graphics.SetProjectionMatrix(GetProjectionMatrix());
			graphics.SetViewport(GetViewport());
		}

		public Transform3D GetTransform()
		{
			return transform;
		}

		public void SetTransform(Transform3D transform)
		{
			this.transform = transform;
		}

		public Vector3f GetPosition()
		{
			return transform.GetPosition();
		}

		public void SetPosition(Vector3f position)
		{
			transform.SetLocalPosition(position);
		}

		public Vector3f GetRotation()
		{
			return transform.GetLocalEulerAngles();
		}

		public void SetRotation(float pitch, float yaw, float roll = 0f)
		{
			var cameraPitch = Math.Map(pitch, -Math.Pi, Math.Pi, Math.Pi / 2f, -Math.Pi / 2f);

			transform.SetLocalEulerAngles(new Vector3f(cameraPitch, yaw, roll));
		}

		public void LookAt(Vector3f position)
		{
			transform.LookAt(position);
		}

		public void LookAt(Vector3f position, Vector3f up)
		{
			transform.LookAt(position, up);
		}

		public void Translate(Vector3f translation)
		{
			transform.Translate(translation);
		}

		public void Rotate(float pitch, float yaw, float roll = 0f)
		{
			transform.Rotate(new Vector3f(pitch, 0f, 0f));
			transform.Rotate(new Vector3f(0f, yaw, 0f), true);
			transform.Rotate(new Vector3f(0f, 0f, roll));
		}

		public override Matrix4 GetWorldToCameraMatrix()
		{
			return transform.GetWorldToLocalMatrix();
		}

		public override Ray ScreenPointToWorldRay(Vector2f screenPosition)
		{
			return ScreenPixelToWorldRay(Math.Round(screenPosition * GetTargetSize()));
		}

		public override Ray ScreenPixelToWorldRay(Vector2i pixelPosition)
		{
			return new Ray(transform.GetPosition(), transform.GetPosition() - ScreenPixelToWorld(GetTargetSize() - pixelPosition));
		}

// TODO: Get Frustum ?
		/*public FloatRect GetBounds()
		{
			var bounds = new FloatRect();

			var bottomLeft = ScreenToWorldPoint(new Vector2f(viewport.x, viewport.y));
			var topRight = ScreenToWorldPoint(new Vector2f(viewport.x + viewport.width, viewport.y + viewport.height));

			if(Math.Approximately(transform.GetRotation(), 0f))
			{
				bounds.position = bottomLeft;
				bounds.width = topRight.x - bottomLeft.x;
				bounds.height = topRight.y - bottomLeft.y;
			}
			else
			{
				var bottomRight = ScreenToWorldPoint(new Vector2f(viewport.x + viewport.width, viewport.y));
				var topLeft = ScreenToWorldPoint(new Vector2f(viewport.x, viewport.y + viewport.height));

				var minX = Math.Min(bottomLeft.x, bottomRight.x, topLeft.x, topRight.x);
				var maxX = Math.Max(bottomLeft.x, bottomRight.x, topLeft.x, topRight.x);
				var minY = Math.Min(bottomLeft.y, bottomRight.y, topLeft.y, topRight.y);
				var maxY = Math.Max(bottomLeft.y, bottomRight.y, topLeft.y, topRight.y);

				bounds.position = new Vector2f(minX, minY);
				bounds.width = maxX - minX;
				bounds.height = maxY - minY;
			}

			return bounds;
		}*/
	}
}
