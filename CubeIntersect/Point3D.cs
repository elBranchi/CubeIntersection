namespace CubeIntersect
{
	public class Point3D
	{

		public Point3D()
		{
		}
		public Point3D(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		public float X { get; init; }
		public float Y { get; init; }
		public float Z { get; init; }

		public override string ToString()
		{
			return $"<{X},{Y},{Z}>";
		}
	}
}
