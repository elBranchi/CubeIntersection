namespace CubeIntersect
{
	public interface ICuboid
	{
		Point3D Center { get; }
		float XMin { get; }
		float XMax { get; }

		float YMin { get; }
		float YMax { get; }

		float ZMin { get; }
		float ZMax { get; }
	}
}
