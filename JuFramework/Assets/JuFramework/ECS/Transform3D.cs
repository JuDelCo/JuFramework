
// TODO: Transform.Apply() -> Guardar el estado actual del objeto como base similar a Blender

namespace JuFramework
{
	public class Transform3D
	{
		private Transform3D parentTransform;
		private Vector3f position;
		private Vector3f scale;
		private Quat orientation;

		private bool eulerAnglesRefreshNeeded;
		private bool rightRefreshNeeded;
		private bool upRefreshNeeded;
		private bool forwardRefreshNeeded;
		private Vector3f eulerAnglesCache;
		private Vector3f rightCache;
		private Vector3f upCache;
		private Vector3f forwardCache;

		private bool matrixRefreshNeeded;
		private Matrix4 matrixCache;
		private Matrix4 localToWorldMatrixCache;
		private Matrix4 parentLocalToWorldMatrixCache;

		private bool inverseMatrixRefreshNeeded;
		private Matrix4 inverseMatrixCache;
		private Matrix4 worldToLocalMatrixCache;
		private Matrix4 parentWorldToLocalMatrixCache;

		public Transform3D()
		{
			parentTransform = null;
			position = Vector3f.zero;
			scale = Vector3f.one;
			orientation = Quat.identity;

			eulerAnglesRefreshNeeded = false;
			rightRefreshNeeded = false;
			upRefreshNeeded = false;
			forwardRefreshNeeded = false;
			eulerAnglesCache = Vector3f.zero;
			rightCache = Vector3f.right;
			upCache = Vector3f.up;
			forwardCache = Vector3f.forward;

			matrixRefreshNeeded = false;
			matrixCache = Matrix4.identity;
			localToWorldMatrixCache = Matrix4.identity;
			parentLocalToWorldMatrixCache = Matrix4.identity;

			inverseMatrixRefreshNeeded = false;
			inverseMatrixCache = Matrix4.identity;
			worldToLocalMatrixCache = Matrix4.identity;
			parentWorldToLocalMatrixCache = Matrix4.identity;
		}

		public Transform3D(Vector3f position) : this()
		{
			Reset(position);
		}

		public Transform3D(Vector3f position, Quat orientation) : this()
		{
			Reset(position, orientation);
		}

		public Transform3D(Vector3f position, Quat orientation, Vector3f scale) : this()
		{
			Reset(position, orientation, scale);
		}

		public void Reset()
		{
			Reset(Vector3f.zero);
		}

		public void Reset(Vector3f position)
		{
			Reset(position, Quat.identity);
		}

		public void Reset(Vector3f position, Quat orientation)
		{
			Reset(position, orientation, Vector3f.one);
		}

		public void Reset(Vector3f position, Quat orientation, Vector3f scale)
		{
			SetLocalPosition(position);
			SetLocalScale(scale);
			SetLocalRotation(orientation);
		}

		public Transform3D GetRoot()
		{
			if(parentTransform != null)
			{
				return parentTransform.GetRoot();
			}

			return this;
		}

		public Transform3D GetParent()
		{
			return parentTransform;
		}

		public void SetParent(Transform3D newParentTransform, bool worldPositionStays = true)
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
				var newOrientation = Math.DecomposeMatrixGetOrientation(newTransformMatrix, newScale);

				SetLocalPosition(newPosition);
				SetLocalScale(newScale);
				SetLocalRotation(newOrientation);
			}

			parentTransform = newParentTransform;

			parentLocalToWorldMatrixCache = Matrix4.identity;
			parentWorldToLocalMatrixCache = Matrix4.identity;
		}

		public bool IsChildOf(Transform3D parent)
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

		public Vector3f GetPosition()
		{
			if(parentTransform != null)
			{
				return position + parentTransform.GetPosition();
			}

			return position;
		}

		public Vector3f GetLossyScale()
		{
			if(parentTransform != null)
			{
				return scale * parentTransform.GetLossyScale();
			}

			return scale;
		}

		public Vector3f GetEulerAngles()
		{
			if(parentTransform != null)
			{
				return GetRotation().eulerAngles;
			}

			return GetLocalEulerAngles();
		}

		public Quat GetRotation()
		{
			if(parentTransform != null)
			{
				return GetLocalRotation() * parentTransform.GetRotation();
			}

			return GetLocalRotation();
		}

		public void SetPosition(Vector3f position)
		{
			if(parentTransform != null)
			{
				position = parentTransform.InverseTransformPoint(position);
			}

			SetLocalPosition(position);
		}

		public void SetLossyScale(Vector3f scale)
		{
			if(parentTransform != null)
			{
				scale = parentTransform.InverseTransformVector(scale);
			}

			SetLocalScale(scale);
		}

		public void SetEulerAngles(Vector3f eulerAngles)
		{
			SetRotation(new Quat(eulerAngles));
		}

		public void SetRotation(Quat orientation)
		{
			if(parentTransform != null)
			{
				orientation = (parentTransform.GetWorldToLocalMatrix() * orientation.ToMatrix4()).ToQuaternion();
			}

			SetLocalRotation(orientation);
		}

		public Vector3f Right()
		{
			if(parentTransform != null)
			{
				return parentTransform.GetRotation() * LocalRight();
			}

			return LocalRight();
		}

		public Vector3f Up()
		{
			if(parentTransform != null)
			{
				return parentTransform.GetRotation() * LocalUp();
			}

			return LocalUp();
		}

		public Vector3f Forward()
		{
			if(parentTransform != null)
			{
				return parentTransform.GetRotation() * LocalForward();
			}

			return LocalForward();
		}

		public Vector3f GetLocalPosition()
		{
			return position;
		}

		public Vector3f GetLocalScale()
		{
			return scale;
		}

		public Vector3f GetLocalEulerAngles()
		{
			if(eulerAnglesRefreshNeeded)
			{
				eulerAnglesCache = orientation.eulerAngles;
				eulerAnglesRefreshNeeded = false;
			}

			return eulerAnglesCache;
		}

		public Quat GetLocalRotation()
		{
			return orientation;
		}

		public void SetLocalPosition(Vector3f position)
		{
			this.position = position;

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void SetLocalScale(Vector3f scale)
		{
			this.scale = scale;

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void SetLocalEulerAngles(Vector3f eulerAngles)
		{
			SetLocalRotation(new Quat(eulerAngles));
		}

		public void SetLocalRotation(Quat orientation)
		{
			this.orientation = Math.Normalize(orientation);

			eulerAnglesRefreshNeeded = true;
			rightRefreshNeeded = true;
			upRefreshNeeded = true;
			forwardRefreshNeeded = true;
			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void Translate(Vector3f translation, bool relativeToWorld = false)
		{
			if(relativeToWorld)
			{
				position += translation;
			}
			else
			{
				position += (orientation * translation);
			}

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void Translate(Vector3f translation, Transform3D relativeTo)
		{
			Translate(relativeTo.TransformDirection(Math.Normalize(translation)) * translation.length, true);
		}

		public void Scale(Vector3f scale, bool relativeToWorld = false)
		{
			if(relativeToWorld)
			{
				this.scale *= scale;
			}
			else
			{
				this.scale *= (orientation * scale);
			}

			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void Rotate(Vector3f eulerAngles, bool relativeToWorld = false)
		{
			Rotate(new Quat(eulerAngles), relativeToWorld);
		}

		public void Rotate(Vector3f eulerAngles, Transform3D relativeTo)
		{
			Rotate((relativeTo.GetLocalToWorldMatrix() * new Quat(eulerAngles).ToMatrix4()).ToQuaternion(), true);
		}

		public void Rotate(Quat orientation, bool relativeToWorld = false)
		{
			if(relativeToWorld)
			{
				this.orientation = orientation * this.orientation;
			}
			else
			{
				this.orientation = this.orientation * orientation;
			}

			this.orientation = Math.Normalize(this.orientation);

			eulerAnglesRefreshNeeded = true;
			rightRefreshNeeded = true;
			upRefreshNeeded = true;
			forwardRefreshNeeded = true;
			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void Rotate(Quat orientation, Transform3D relativeTo)
		{
			Rotate(orientation.eulerAngles, relativeTo);
		}

		public void RotateAround(Vector3f point, Vector3f axis, float angle)
		{
			position += (orientation * point);
			orientation *= new Quat(axis * angle);
			orientation = Math.Normalize(orientation);
			position += (orientation * -point);

			eulerAnglesRefreshNeeded = true;
			rightRefreshNeeded = true;
			upRefreshNeeded = true;
			forwardRefreshNeeded = true;
			matrixRefreshNeeded = true;
			inverseMatrixRefreshNeeded = true;
		}

		public void LookAt(Transform3D target)
		{
			LookAt(target.GetPosition());
		}

		public void LookAt(Vector3f worldPosition)
		{
			LookAt(worldPosition, Vector3f.up);
		}

		public void LookAt(Transform3D target, Vector3f worldUp)
		{
			LookAt(target.GetPosition(), worldUp);
		}

		public void LookAt(Vector3f worldPosition, Vector3f worldUp)
		{
			SetRotation(Math.LookAt(GetPosition(), worldPosition, worldUp));
		}

		public Matrix4 GetLocalToWorldMatrix()
		{
			bool matrixRecalculated = false;

			if(matrixRefreshNeeded)
			{
				matrixCache = CalculateMatrix();
				matrixRefreshNeeded = false;
				matrixRecalculated = true;
			}

			if(parentTransform != null)
			{
				if(parentLocalToWorldMatrixCache != parentTransform.GetLocalToWorldMatrix())
				{
					parentLocalToWorldMatrixCache = parentTransform.GetLocalToWorldMatrix();
					localToWorldMatrixCache = parentLocalToWorldMatrixCache * matrixCache;
				}

				if(matrixRecalculated)
				{
					localToWorldMatrixCache = parentLocalToWorldMatrixCache * matrixCache;
				}

				return localToWorldMatrixCache;
			}

			return matrixCache;
		}

		public Vector3f TransformPoint(Vector3f position)
		{
			return (Vector3f)(GetLocalToWorldMatrix() * new Vector4f(position, 1f));
		}

		public Vector3f TransformVector(Vector3f vector)
		{
			return (Matrix3)(GetLocalToWorldMatrix()) * vector;
		}

		public Vector3f TransformDirection(Vector3f direction)
		{
			return GetRotation() * direction;
		}

		public Matrix4 GetWorldToLocalMatrix()
		{
			bool matrixRecalculated = false;

			if(inverseMatrixRefreshNeeded)
			{
				inverseMatrixCache = CalculateInverseMatrix();
				inverseMatrixRefreshNeeded = false;
				matrixRecalculated = true;
			}

			if(parentTransform != null)
			{
				if(parentWorldToLocalMatrixCache != parentTransform.GetWorldToLocalMatrix())
				{
					parentWorldToLocalMatrixCache = parentTransform.GetWorldToLocalMatrix();
					worldToLocalMatrixCache = inverseMatrixCache * parentWorldToLocalMatrixCache;
				}

				if(matrixRecalculated)
				{
					worldToLocalMatrixCache = inverseMatrixCache * parentWorldToLocalMatrixCache;
				}

				return worldToLocalMatrixCache;
			}

			return inverseMatrixCache;
		}

		public Vector3f InverseTransformPoint(Vector3f position)
		{
			return (Vector3f)(GetWorldToLocalMatrix() * new Vector4f(position, 1f));
		}

		public Vector3f InverseTransformVector(Vector3f vector)
		{
			return (Matrix3)(GetWorldToLocalMatrix()) * vector;
		}

		public Vector3f InverseTransformDirection(Vector3f direction)
		{
			return Math.Inverse(GetRotation()) * direction;
		}

		private Vector3f LocalRight()
		{
			if(rightRefreshNeeded)
			{
				rightCache = orientation * Vector3f.right;

				rightRefreshNeeded = false;
			}

			return rightCache;
		}

		private Vector3f LocalUp()
		{
			if(upRefreshNeeded)
			{
				upCache = orientation * Vector3f.up;

				upRefreshNeeded = false;
			}

			return upCache;
		}

		private Vector3f LocalForward()
		{
			if(forwardRefreshNeeded)
			{
				forwardCache = orientation * Vector3f.forward;

				forwardRefreshNeeded = false;
			}

			return forwardCache;
		}

		private Matrix4 CalculateMatrix()
		{
			var translationMatrix = Matrix4.identity;
			translationMatrix.m30 = position.x;
			translationMatrix.m31 = position.y;
			translationMatrix.m32 = position.z;

			var scalingMatrix = Matrix4.identity;
			scalingMatrix.m00 = scale.x;
			scalingMatrix.m11 = scale.y;
			scalingMatrix.m22 = scale.z;

			var rotationMatrix = orientation.ToMatrix4();

			return (translationMatrix * rotationMatrix * scalingMatrix);
		}

		private Matrix4 CalculateInverseMatrix()
		{
			var translationMatrix = Matrix4.identity;
			translationMatrix.m30 = -position.x;
			translationMatrix.m31 = -position.y;
			translationMatrix.m32 = -position.z;

			var scalingMatrix = Matrix4.identity;
			scalingMatrix.m00 = 1f / scale.x;
			scalingMatrix.m11 = 1f / scale.y;
			scalingMatrix.m22 = 1f / scale.z;

			var rotationMatrix = Math.Conjugate(orientation).ToMatrix4();

			return (scalingMatrix * rotationMatrix * translationMatrix);
		}

		//-----------------------------------------------------------------------

		public override string ToString()
		{
			return "[ " +
				"LT { " + GetLocalPosition() + " } , " +
				"LR { " + GetLocalEulerAngles() + " } , " +
				"LS { " + GetLocalScale() + " } , " +
				"WT { " + GetPosition() + " } , " +
				"WR { " + GetEulerAngles() + " } , " +
				"WS { " + GetLossyScale() + " } ]";
		}
	}
}
