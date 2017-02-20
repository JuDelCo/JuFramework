
// TODO: Transform.Apply() -> Guardar el estado actual del objeto como base similar a Blender

namespace JuFramework
{
	public class Transform2D
	{
		private Transform2D parentTransform;
		private Vector2f position;
		private Vector2f scale;
		private float rotation;

		private bool rightRefreshNeeded;
		private bool upRefreshNeeded;
		private Vector2f rightCache;
		private Vector2f upCache;

		private bool matrixRefreshNeeded;
		private Matrix3 matrixCache;
		private Matrix3 localToWorldMatrixCache;
		private Matrix3 parentLocalToWorldMatrixCache;
		private Matrix4 matrix3DCache;
		private Matrix4 localToWorldMatrix3DCache;

		private bool inverseMatrixRefreshNeeded;
		private Matrix3 inverseMatrixCache;
		private Matrix3 worldToLocalMatrixCache;
		private Matrix3 parentWorldToLocalMatrixCache;
		private Matrix4 inverseMatrix3DCache;
		private Matrix4 worldToLocalMatrix3DCache;

		public Transform2D()
		{
			parentTransform = null;
			position = Vector2f.zero;
			scale = new Vector2f(1f, 1f);
			rotation = 0f;

			rightRefreshNeeded = false;
			upRefreshNeeded = false;
			rightCache = Vector2f.right;
			upCache = Vector2f.up;

			matrixRefreshNeeded = false;
			matrixCache = Matrix3.identity;
			localToWorldMatrixCache = Matrix3.identity;
			parentLocalToWorldMatrixCache = Matrix3.identity;
			matrix3DCache = Matrix4.identity;
			localToWorldMatrix3DCache = Matrix4.identity;

			inverseMatrixRefreshNeeded = false;
			inverseMatrixCache = Matrix3.identity;
			worldToLocalMatrixCache = Matrix3.identity;
			parentWorldToLocalMatrixCache = Matrix3.identity;
			inverseMatrix3DCache = Matrix4.identity;
			worldToLocalMatrix3DCache = Matrix4.identity;
		}

		public Transform2D(Vector2f position) : this()
		{
			Reset(position);
		}

		public Transform2D(Vector2f position, float rotation) : this()
		{
			Reset(position, rotation);
		}

		public Transform2D(Vector2f position, float rotation, Vector2f scale) : this()
		{
			Reset(position, rotation, scale);
		}

		public void Reset()
		{
			Reset(Vector2f.zero);
		}

		public void Reset(Vector2f position)
		{
			Reset(position, 0f);
		}

		public void Reset(Vector2f position, float rotation)
		{
			Reset(position, rotation, new Vector2f(1f, 1f));
		}

		public void Reset(Vector2f position, float rotation, Vector2f scale)
		{
			SetLocalPosition(position);
			SetLocalScale(scale);
			SetLocalRotation(rotation);
		}

		public Transform2D GetRoot()
		{
			if(parentTransform != null)
			{
				return parentTransform.GetRoot();
			}

			return this;
		}

		public Transform2D GetParent()
		{
			return parentTransform;
		}

		public void SetParent(Transform2D newParentTransform, bool worldPositionStays = true)
		{
			if(worldPositionStays && (newParentTransform != null || parentTransform != null))
			{
				var newTransformMatrix = GetLocalToWorldMatrix();

				if(newParentTransform != null)
				{
					// (N = P * M) => (P-1 * N = P-1 * P * M) => (P-1 * N = M)
					newTransformMatrix = Math.Inverse(newParentTransform.GetLocalToWorldMatrix()) * newTransformMatrix;
				}

				var newPosition = Math.DecomposeMatrixGetPosition(newTransformMatrix);
				var newScale = Math.DecomposeMatrixGetScale(newTransformMatrix);
				var newOrientation = Math.DecomposeMatrixGetOrientation(newTransformMatrix);

				SetLocalPosition(newPosition);
				SetLocalScale(newScale);
				SetLocalRotation(newOrientation);
			}

			parentTransform = newParentTransform;

			parentLocalToWorldMatrixCache = Matrix3.identity;
			parentWorldToLocalMatrixCache = Matrix3.identity;
		}

		public bool IsChildOf(Transform2D parent)
		{
			if(parentTransform != null)
			{
				if(parentTransform == parent)
				{
					return true;
				}

				return parentTransform.IsChildOf(parent);
			}

			return (parent == this);
		}

		public Vector2f GetPosition()
		{
			if(parentTransform != null)
			{
				return position + parentTransform.GetPosition();
			}

			return position;
		}

		public Vector2f GetLossyScale()
		{
			if(parentTransform != null)
			{
				return scale * parentTransform.GetLossyScale();
			}

			return scale;
		}

		public float GetRotation()
		{
			if(parentTransform != null)
			{
				return Math.Repeat(GetLocalRotation() + parentTransform.GetRotation(), 2f * Math.Pi);
			}

			return GetLocalRotation();
		}

		public void SetPosition(Vector2f position)
		{
			if(parentTransform != null)
			{
				position = parentTransform.InverseTransformPoint(position);
			}

			SetLocalPosition(position);
		}

		public void SetPosition(float x, float y)
		{
			SetPosition(new Vector2f(x, y));
		}

		public void SetLossyScale(Vector2f scale)
		{
			if(parentTransform != null)
			{
				scale = parentTransform.InverseTransformVector(scale);
			}

			SetLocalScale(scale);
		}

		public void SetLossyScale(float scale)
		{
			SetLossyScale(new Vector2f(scale, scale));
		}

		public void SetRotation(float radians)
		{
			if(parentTransform != null)
			{
				radians -= parentTransform.GetRotation();
			}

			SetLocalRotation(radians);
		}

		public Vector2f Right()
		{
			if(parentTransform != null)
			{
				return Math.Rotate(LocalRight(), parentTransform.GetRotation());
			}

			return LocalRight();
		}

		public Vector2f Up()
		{
			if(parentTransform != null)
			{
				return Math.Rotate(LocalUp(), parentTransform.GetRotation());
			}

			return LocalUp();
		}

		public Vector2f GetLocalPosition()
		{
			return position;
		}

		public Vector2f GetLocalScale()
		{
			return scale;
		}

		public float GetLocalRotation()
		{
			return rotation;
		}

		public void SetLocalPosition(Vector2f position)
		{
			this.position = position;

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void SetLocalPosition(float x, float y)
		{
			SetLocalPosition(new Vector2f(x, y));
		}

		public void SetLocalScale(Vector2f scale)
		{
			this.scale = scale;

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void SetLocalScale(float scale)
		{
			SetLocalScale(new Vector2f(scale, scale));
		}

		public void SetLocalRotation(float radians)
		{
			rotation = Math.Repeat(radians, 2f * Math.Pi);

			rightRefreshNeeded = true;
			upRefreshNeeded = true;
			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void Translate(Vector2f translation, bool relativeToWorld = false)
		{
			if(relativeToWorld)
			{
				position += translation;
			}
			else
			{
				position += Math.Rotate(translation, rotation);
			}

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void Translate(Vector2f translation, Transform2D relativeTo)
		{
			Translate(relativeTo.TransformDirection(Math.Normalize(translation)) * translation.length, true);
		}

		public void Scale(Vector2f scale, bool relativeToWorld = false)
		{
			if(relativeToWorld)
			{
				this.scale *= scale;
			}
			else
			{
				this.scale *= Math.Rotate(scale, rotation);
			}

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void Scale(float scale)
		{
			Scale(new Vector2f(scale, scale));
		}

		public void Rotate(float rotation)
		{
			this.rotation = Math.Repeat(this.rotation + rotation, 2f * Math.Pi);

			rightRefreshNeeded = true;
			upRefreshNeeded = true;
			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void RotateAround(Vector2f point, float angle)
		{
			position += Math.Rotate(point, rotation);
			rotation = Math.Repeat(rotation + angle, 2f * Math.Pi);
			position += Math.Rotate(point, -rotation);

			rightRefreshNeeded = true;
			upRefreshNeeded = true;
			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void LookAt(Transform2D target)
		{
			LookAt(target.GetPosition());
		}

		public void LookAt(Vector2f worldPosition)
		{
			LookAt(worldPosition, Vector2f.up);
		}

		public void LookAt(Transform2D target, Vector2f worldUp)
		{
			LookAt(target.GetPosition(), worldUp);
		}

// TODO: Eliminar ? (Usar LookAt con Math y vectores)
		public void LookAt(Vector2f worldPosition, Vector2f worldUp)
		{
			var angles = Math.LookAt(GetPosition(), worldPosition, worldUp).angle;

			if (!float.IsNaN(angles))
			{
				SetRotation(angles);
			}
		}

		public Matrix4 GetLocalToWorld3DMatrix()
		{
			bool matrixRecalculated = false;

			if(matrixRefreshNeeded)
			{
				matrixCache = CalculateMatrix();
				matrix3DCache = Calculate3DMatrix(matrixCache);
				matrixRefreshNeeded = false;
				matrixRecalculated = true;
			}

			if(parentTransform != null)
			{
				if(parentLocalToWorldMatrixCache != parentTransform.GetLocalToWorldMatrix())
				{
					parentLocalToWorldMatrixCache = parentTransform.GetLocalToWorldMatrix();
					localToWorldMatrixCache = parentLocalToWorldMatrixCache * matrixCache;
					localToWorldMatrix3DCache = Calculate3DMatrix(localToWorldMatrixCache);
				}

				if(matrixRecalculated)
				{
					localToWorldMatrixCache = parentLocalToWorldMatrixCache * matrixCache;
					localToWorldMatrix3DCache = Calculate3DMatrix(localToWorldMatrixCache);
				}

				return localToWorldMatrix3DCache;
			}

			return matrix3DCache;
		}

		public Matrix3 GetLocalToWorldMatrix()
		{
			bool matrixRecalculated = false;

			if(matrixRefreshNeeded)
			{
				matrixCache = CalculateMatrix();
				matrix3DCache = Calculate3DMatrix(matrixCache);
				matrixRefreshNeeded = false;
				matrixRecalculated = true;
			}

			if(parentTransform != null)
			{
				if(parentLocalToWorldMatrixCache != parentTransform.GetLocalToWorldMatrix())
				{
					parentLocalToWorldMatrixCache = parentTransform.GetLocalToWorldMatrix();
					localToWorldMatrixCache = parentLocalToWorldMatrixCache * matrixCache;
					localToWorldMatrix3DCache = Calculate3DMatrix(localToWorldMatrixCache);
				}

				if(matrixRecalculated)
				{
					localToWorldMatrixCache = parentLocalToWorldMatrixCache * matrixCache;
					localToWorldMatrix3DCache = Calculate3DMatrix(localToWorldMatrixCache);
				}

				return localToWorldMatrixCache;
			}

			return matrixCache;
		}

		public Vector2f TransformPoint(Vector2f position)
		{
			return (Vector2f)(GetLocalToWorldMatrix() * new Vector3f(position));
		}

		public Vector2f TransformVector(Vector2f vector)
		{
			var matrix = GetLocalToWorldMatrix();
			matrix.m20 = 0f;
			matrix.m21 = 0f;

			return (Vector2f)(matrix * new Vector3f(vector));
		}

		public Vector2f TransformDirection(Vector2f direction)
		{
			return Math.Rotate(direction, GetRotation());
		}

		public Matrix4 GetWorldToLocal3DMatrix()
		{
			bool matrixRecalculated = false;

			if(inverseMatrixRefreshNeeded)
			{
				inverseMatrixCache = CalculateInverseMatrix();
				inverseMatrix3DCache = Calculate3DMatrix(inverseMatrixCache);
				inverseMatrixRefreshNeeded = false;
				matrixRecalculated = true;
			}

			if(parentTransform != null)
			{
				if(parentWorldToLocalMatrixCache != parentTransform.GetWorldToLocalMatrix())
				{
					parentWorldToLocalMatrixCache = parentTransform.GetWorldToLocalMatrix();
					worldToLocalMatrixCache = inverseMatrixCache * parentWorldToLocalMatrixCache;
					worldToLocalMatrix3DCache = Calculate3DMatrix(worldToLocalMatrixCache);
				}

				if(matrixRecalculated)
				{
					worldToLocalMatrixCache = inverseMatrixCache * parentWorldToLocalMatrixCache;
					worldToLocalMatrix3DCache = Calculate3DMatrix(worldToLocalMatrixCache);
				}

				return worldToLocalMatrix3DCache;
			}

			return inverseMatrix3DCache;
		}

		public Matrix3 GetWorldToLocalMatrix()
		{
			bool matrixRecalculated = false;

			if(inverseMatrixRefreshNeeded)
			{
				inverseMatrixCache = CalculateInverseMatrix();
				inverseMatrix3DCache = Calculate3DMatrix(inverseMatrixCache);
				inverseMatrixRefreshNeeded = false;
				matrixRecalculated = true;
			}

			if(parentTransform != null)
			{
				if(parentWorldToLocalMatrixCache != parentTransform.GetWorldToLocalMatrix())
				{
					parentWorldToLocalMatrixCache = parentTransform.GetWorldToLocalMatrix();
					worldToLocalMatrixCache = inverseMatrixCache * parentWorldToLocalMatrixCache;
					worldToLocalMatrix3DCache = Calculate3DMatrix(worldToLocalMatrixCache);
				}

				if(matrixRecalculated)
				{
					worldToLocalMatrixCache = inverseMatrixCache * parentWorldToLocalMatrixCache;
					worldToLocalMatrix3DCache = Calculate3DMatrix(worldToLocalMatrixCache);
				}

				return worldToLocalMatrixCache;
			}

			return inverseMatrixCache;
		}

		public Vector2f InverseTransformPoint(Vector2f position)
		{
			return (Vector2f)(GetWorldToLocalMatrix() * new Vector3f(position));
		}

		public Vector2f InverseTransformVector(Vector2f vector)
		{
			var matrix = GetWorldToLocalMatrix();
			matrix.m20 = 0f;
			matrix.m21 = 0f;

			return (Vector2f)(matrix * new Vector3f(vector));
		}

		public Vector2f InverseTransformDirection(Vector2f direction)
		{
			return Math.Rotate(direction, -GetRotation());
		}

		private Vector2f LocalRight()
		{
			if(rightRefreshNeeded)
			{
				rightCache = Math.Rotate(Vector2f.right, rotation);

				rightRefreshNeeded = false;
			}

			return rightCache;
		}

		private Vector2f LocalUp()
		{
			if(upRefreshNeeded)
			{
				Math.Rotate(Vector2f.up, rotation);

				upRefreshNeeded = false;
			}

			return upCache;
		}

		private Matrix3 CalculateMatrix()
		{
			var translationMatrix = Matrix3.identity;
			translationMatrix.m20 = position.x;
			translationMatrix.m21 = position.y;

			var scalingMatrix = Matrix3.identity;
			scalingMatrix.m00 = scale.x;
			scalingMatrix.m11 = scale.y;

			var rotationMatrix = (Matrix3)Math.Rotate(rotation, Vector3f.forward);

			return (translationMatrix * rotationMatrix * scalingMatrix);
		}

		private Matrix4 Calculate3DMatrix(Matrix3 m)
		{
			return new Matrix4(
				m.m00, m.m01, 0, 0,
				m.m10, m.m11, 0, 0,
				    0,     0, 1, 0,
				m.m20, m.m21, 0, 1
			);
		}

		private Matrix3 CalculateInverseMatrix()
		{
			var translationMatrix = Matrix3.identity;
			translationMatrix.m20 = -position.x;
			translationMatrix.m21 = -position.y;

			var scalingMatrix = Matrix3.identity;
			scalingMatrix.m00 = 1f / scale.x;
			scalingMatrix.m11 = 1f / scale.y;

			var rotationMatrix = (Matrix3)Math.Rotate(-rotation, Vector3f.forward);

			return (scalingMatrix * rotationMatrix * translationMatrix);
		}

		//-----------------------------------------------------------------------

		public override string ToString()
		{
			return "[ " +
				"LT { " + GetLocalPosition() + " } , " +
				"LR { " + GetLocalRotation() + " } , " +
				"LS { " + GetLocalScale() + " } , " +
				"WT { " + GetPosition() + " } , " +
				"WR { " + GetRotation() + " } , " +
				"WS { " + GetLossyScale() + " } ]";
		}
	}
}
