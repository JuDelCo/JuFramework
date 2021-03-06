
namespace JuFramework
{
	public partial struct Ray
	{
		public Vector3f position;
		public Vector3f direction;

		public Ray(Vector3f position, Vector3f direction)
		{
			this.position = position;
			this.direction = Math.Normalize(direction);
		}

		//-----------------------------------------------------------------------

		public Vector3f origin
		{
			get { return position; }
			set { position = value; }
		}

		public Vector3f GetPoint(float distance)
		{
			return position + (direction * distance);
		}

		//-----------------------------------------------------------------------

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + position.GetHashCode();
				hash = hash * 23 + direction.GetHashCode();
				return hash;
			}
		}

		public override bool Equals(object obj)
		{
			return (obj is Ray && (this == (Ray)obj));
		}

		public static bool operator == (Ray a, Ray b)
		{
			return (a.position == b.position && a.direction == b.direction);
		}

		public static bool operator != (Ray a, Ray b)
		{
			return !(a == b);
		}

		//-----------------------------------------------------------------------

		public override string ToString()
		{
			return "[ " + position + " , " + direction + " ]";
		}
	}
}
