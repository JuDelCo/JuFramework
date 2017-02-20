
// TODO:
//		Vector3 ClosestPoint(Vector3 point) // The point on the bounding box or inside the bounding box
//		bool Contains(Vector3 point)
//		void Encapsulate(Vector3 point) // Grows the Bounds to include the point
//		void Expand(float amount) // Expand the bounds by increasing its size by amount along each side.
//		bool IntersectRay(Ray ray)
//		bool Intersects(Box box)
//		void SetMinMax(Vector3 min, Vector3 max)
//		float SqrDistance(Vector3 point) // The smallest squared distance between the point and this bounding box.

namespace JuFramework
{
	public partial struct Box
	{
		private Vector3f centerPosition;
		private Vector3f boxSize;

		public Box(Vector3f center, Vector3f size)
		{
			this.centerPosition = center;
			this.boxSize = size;
		}

		//-----------------------------------------------------------------------

		public Vector3f center
		{
			get { return centerPosition; }
			set { centerPosition = value; }
		}

		public Vector3f extents
		{
			get { return (size / 2f); }
			set { boxSize = value * 2f; }
		}

		public Vector3f size
		{
			get { return boxSize; }
			set { boxSize = value; }
		}

		//-----------------------------------------------------------------------

		public Vector3f min
		{
			get { return (centerPosition - extents); }
		}

		public Vector3f max
		{
			get { return (centerPosition + extents); }
		}

		//-----------------------------------------------------------------------

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + min.GetHashCode();
				hash = hash * 23 + max.GetHashCode();
				return hash;
			}
		}

		public override bool Equals(object obj)
		{
			return (obj is Box && (this == (Box)obj));
		}

		public static bool operator == (Box a, Box b)
		{
			return (a.centerPosition == b.centerPosition && a.boxSize == b.boxSize);
		}

		public static bool operator != (Box a, Box b)
		{
			return !(a == b);
		}

		//-----------------------------------------------------------------------

		public override string ToString()
		{
			return "[ " + centerPosition + " , " + boxSize + " ]";
		}
	}
}
