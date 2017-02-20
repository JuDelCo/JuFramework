
namespace JuFramework
{
	public class Camera2D : Camera
	{
		private Transform2D transform;

		public Camera2D(Vector2i targetSize) : this(targetSize.x, targetSize.y)
		{
		}

		public Camera2D(int targetWidth, int targetHeight) : base(targetWidth, targetHeight)
		{
			SetOrthographic(true);
			SetTransform(new Transform2D());
			SetDistanceClipping(-1f, 1f);
		}

		public Camera2D(Transform2D transform, Vector2i targetSize) : this(transform, targetSize.x, targetSize.y)
		{
		}

		public Camera2D(Transform2D transform, int targetWidth, int targetHeight) : base(targetWidth, targetHeight)
		{
			SetOrthographic(true);
			SetTransform(transform);
			SetDistanceClipping(-1f, 1f);
		}

		public override void Use()
		{
			graphics.SetCamera(this, GetPosition(), Math.EulerAngles(new Vector3f(0f, 0f, GetRotation())));
			graphics.SetModelToWorldMatrix(Matrix4.identity);
			graphics.SetWorldToViewMatrix(GetWorldToCameraMatrix());
			graphics.SetProjectionMatrix(GetProjectionMatrix());
			graphics.SetViewport(GetViewport());
		}

		public Transform2D GetTransform()
		{
			return transform;
		}

		public void SetTransform(Transform2D transform)
		{
			this.transform = transform;
		}

		public Vector2f GetPosition()
		{
			return transform.GetPosition();
		}

		public void SetPosition(Vector2f position)
		{
			transform.SetLocalPosition(position);
		}

		public float GetRotation()
		{
			return transform.GetRotation();
		}

		public void SetRotation(float radians)
		{
			transform.SetLocalRotation(radians);
		}

		public void Translate(Vector2f translation)
		{
			transform.Translate(translation);
		}

		public void Rotate(float radians)
		{
			transform.Rotate(radians);
		}

		public override Matrix4 GetWorldToCameraMatrix()
		{
			return transform.GetWorldToLocal3DMatrix();
		}

		public override Ray ScreenPointToWorldRay(Vector2f screenPosition)
		{
			return ScreenPixelToWorldRay(Math.Round(screenPosition * GetTargetSize()));
		}

		public override Ray ScreenPixelToWorldRay(Vector2i pixelPosition)
		{
			return new Ray(transform.GetPosition(), new Vector3f(transform.GetPosition()) - ScreenPixelToWorld(GetTargetSize() - pixelPosition));
		}

		public FloatRect GetBounds()
		{
			var bounds = new FloatRect();
			var viewport = GetViewport();

			var bottomLeft = ScreenPointToWorld(new Vector2f(viewport.x, viewport.y));
			var topRight = ScreenPointToWorld(new Vector2f(viewport.x + viewport.width, viewport.y + viewport.height));

			if(Math.Approximately(transform.GetRotation(), 0f))
			{
				bounds.position = bottomLeft;
				bounds.width = topRight.x - bottomLeft.x;
				bounds.height = topRight.y - bottomLeft.y;
			}
			else
			{
				var bottomRight = ScreenPointToWorld(new Vector2f(viewport.x + viewport.width, viewport.y));
				var topLeft = ScreenPointToWorld(new Vector2f(viewport.x, viewport.y + viewport.height));

				var minX = Math.Min(bottomLeft.x, bottomRight.x, topLeft.x, topRight.x);
				var maxX = Math.Max(bottomLeft.x, bottomRight.x, topLeft.x, topRight.x);
				var minY = Math.Min(bottomLeft.y, bottomRight.y, topLeft.y, topRight.y);
				var maxY = Math.Max(bottomLeft.y, bottomRight.y, topLeft.y, topRight.y);

				bounds.position = new Vector2f(minX, minY);
				bounds.width = maxX - minX;
				bounds.height = maxY - minY;
			}

			return bounds;
		}
	}
}
