namespace CubeIntersect
{
	public static class ICuboidExtensions
	{

		public static float CalculateVolume(this ICuboid cuboid)
		{
			return (cuboid.XMax - cuboid.XMin) * (cuboid.YMax - cuboid.YMin) * (cuboid.ZMax - cuboid.ZMin);
		}

		public static bool TryIntersect(this ICuboid cuboidA, ICuboid cuboidB, out ICuboid? intersection)
		{
			intersection = null;

			var intersectX = IntersectSegments((cuboidA.XMin, cuboidA.XMax), (cuboidB.XMin, cuboidB.XMax));
			var intersectY = IntersectSegments((cuboidA.YMin, cuboidA.YMax), (cuboidB.YMin, cuboidB.YMax));
			var intersectZ = IntersectSegments((cuboidA.ZMin, cuboidA.ZMax), (cuboidB.ZMin, cuboidB.ZMax));

			if (intersectX is null || intersectY is null || intersectZ is null)
			{
				return false;
			}

			intersection = new Cuboid
			{
				XMin = intersectX.Value.min,
				XMax = intersectX.Value.max,
				YMin = intersectY.Value.min,
				YMax = intersectY.Value.max,
				ZMin = intersectZ.Value.min,
				ZMax = intersectZ.Value.max,
			};

			return true;
		}

		/// <summary>
		/// Single dimensional segment intersection calculation
		/// </summary>
		/// <param name="segmentA"></param>
		/// <param name="segmentB"></param>
		/// <returns>Intersecting segment or null if intersection does not exist</returns>

		internal static (float min, float max)? IntersectSegments((float min, float max) segmentA, (float min, float max) segmentB)
		{
			if (segmentA.max <= segmentB.min || segmentA.min >= segmentB.max)
				return null;
			var bigMin = Math.Max(segmentA.min, segmentB.min);
			var smallMax = Math.Min(segmentA.max, segmentB.max);
			return (bigMin, smallMax);
		}
	}
}
