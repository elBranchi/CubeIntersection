namespace CubeIntersect
{
	public class Cube : ICuboid
	{
		public Cube(Point3D center, float sideLength)
		{
			Center = center;
			//Math.Abs would be innecessary in case that we 
			// prevent the usage of negative lengths previous to the instatiation of a Cube
			SideLength = Math.Abs(sideLength);
		}

		public float SideLength { get; }
		public Point3D Center { get; }


		public float XMin => Center.X - SideLength / 2;

		public float XMax => Center.X + SideLength / 2;

		public float YMin => Center.Y - SideLength / 2;

		public float YMax => Center.Y + SideLength / 2;

		public float ZMin => Center.Z - SideLength / 2;

		public float ZMax => Center.Z + SideLength / 2;

		public float CalculateVolume()
		{
			return SideLength * SideLength * SideLength;
		}
		public override string ToString()
		{
			return $"Center:{Center} Length:{SideLength}";
		}
	}
}
