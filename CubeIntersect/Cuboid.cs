namespace CubeIntersect
{
	public class Cuboid : ICuboid
	{

		internal Cuboid()
		{
		}
		public Point3D Center => new Point3D
		{
			X = (XMax + XMin) / 2,
			Y = (YMax + YMin) / 2,
			Z = (ZMax + ZMin) / 2,
		};

		public float XMin { get; init; }

		public float XMax { get; init; }

		public float YMin { get; init; }

		public float YMax { get; init; }

		public float ZMin { get; init; }

		public float ZMax { get; init; }
	}
}