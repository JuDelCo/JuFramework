
// TODO: https://github.com/raysan5/raylib/blob/17f09cb03484a408cdd50a3d2e4d6604bb1f4c70/src/shapes.c
// https://github.com/libgdx/libgdx/blob/master/gdx/src/com/badlogic/gdx/math/Intersector.java

// TODO: Renombrar
// Intersects
// Overlaps
// Collides

namespace JuFramework
{
	public static partial class Physics3D
	{
		// TODO: Point & Ray
		// TODO: Point & Box

		public static bool CheckCollisionPointSphere(Vector3f point, Sphere s)
		{
			return CheckCollisionSpheres(new Sphere(point, 0), s);
		}

		// TODO: Ray & Ray
		// TODO: Ray & Box (raylib)
		// TODO: Ray & Sphere (raylib) (+ including intersection point)

		// TODO: Box & Box (raylib)
		// TODO: Box & Sphere (raylib)

		public static bool CheckCollisionSpheres(Sphere a, Sphere b)
		{
			bool collision = false;

			if (Math.DistanceSqr(b.position, a.position) <= Math.Sqr(a.radius + b.radius))
			{
				collision = true;
			}

			return collision;
		}
	}
}
