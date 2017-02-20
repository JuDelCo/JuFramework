
namespace JuFramework
{
	public class Tuple<T1, T2>
	{
		public T1 first;
		public T2 second;

		public Tuple(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}

		//-----------------------------------------------------------------------

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + first.GetHashCode();
				hash = hash * 23 + second.GetHashCode();

				return hash;
			}
		}

		public override bool Equals(object obj)
		{
			return (this == (Tuple<T1, T2>)obj);
		}

		public static bool operator == (Tuple<T1, T2> a, Tuple<T1, T2> b)
		{
			if (ReferenceEquals(a, b))
			{
				return false;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return (a.first.Equals(b.first) && a.second.Equals(b.second));
		}

		public static bool operator != (Tuple<T1, T2> a, Tuple<T1, T2> b)
		{
			return !(a == b);
		}

		//-----------------------------------------------------------------------

		public override string ToString()
		{
			return string.Format("<{0}, {1}>", first, second);
		}
	}
}
