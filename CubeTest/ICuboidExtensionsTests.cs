using CubeIntersect;
using NUnit.Framework;
using System.Linq;

namespace CubeTest
{
	public class ICuboidExtensionsTests
	{

		[TestCaseSource(nameof(TestCaseData_IntersectSegments))]
		public void IntersectSegments_ShouldReturnExpectedValue(
			(float min, float max) segmentA,
			(float min, float max) segmentB,
			(float min, float max)? expectedResult)
		{
			var result = ICuboidExtensions.IntersectSegments(segmentA, segmentB);

			Assert.AreEqual(expectedResult, result);
		}

		[TestCaseSource(nameof(TestCaseData_CalculateVolume_ShouldReturnExpected))]
		public void CalculateVolume_ShouldReturnExpected(ICuboid cuboid, float expectedVolume)
		{
			var volume = cuboid.CalculateVolume();


			Assert.AreEqual(expectedVolume, volume);
		}

		[TestCaseSource(nameof(TestCaseData_TryIntersect_ShouldReturnExpectedValue))]
		public void TryIntersect_ShouldReturnExpectedValue(Cube cubeA, Cube cubeB, bool expectedResult)
		{
			var result = cubeA.TryIntersect(cubeB, out var _);

			Assert.AreEqual(expectedResult, result);

		}

		[TestCaseSource(nameof(TestCaseData_TryIntersect_ShouldGenerateCuboid))]
		public void TryIntersect_ShouldGenerateCuboid(
			Cube cubeA,
			Cube cubeB,
			(float minX, float maxX, float minY, float maxY, float minZ, float maxZ) expected)
		{
			var result = cubeA.TryIntersect(cubeB, out var intersection);

			Assert.IsTrue(result);
			Assert.IsNotNull(intersection);
			Assert.AreEqual(expected.minX, intersection.XMin);
			Assert.AreEqual(expected.maxX, intersection.XMax);
			Assert.AreEqual(expected.minY, intersection.YMin);
			Assert.AreEqual(expected.maxY, intersection.YMax);
			Assert.AreEqual(expected.minZ, intersection.ZMin);
			Assert.AreEqual(expected.maxZ, intersection.ZMax);
		}
		[TestCaseSource(nameof(TestCaseData_TryIntersect_ShouldGenerateCuboidUsingPreviousIntersection))]
		public void TryIntersect_ShouldGenerateCuboidUsingPreviousIntersection(
			Cube cubeA,
			Cube cubeB,
			Cube cubeC,
			(float minX, float maxX, float minY, float maxY, float minZ, float maxZ) expected
			)
		{
			var prevResult = cubeA.TryIntersect(cubeB, out var prevIntersection);

			Assert.IsTrue(prevResult);
			Assert.IsNotNull(prevIntersection);
			var result = prevIntersection.TryIntersect(cubeC, out var intersection);

			Assert.IsTrue(result);
			Assert.IsNotNull(intersection);
			Assert.AreEqual(expected.minX, intersection.XMin);
			Assert.AreEqual(expected.maxX, intersection.XMax);
			Assert.AreEqual(expected.minY, intersection.YMin);
			Assert.AreEqual(expected.maxY, intersection.YMax);
			Assert.AreEqual(expected.minZ, intersection.ZMin);
			Assert.AreEqual(expected.maxZ, intersection.ZMax);
		}

		public static object[] TestCaseData_IntersectSegments()
		{
			(float, float)? noIntersection = null;
			var testCases = new[]
			{
				new TestCaseData((0f,1f),(-1f, 0.5f), (0f, 0.5f) ),
				new TestCaseData((0f,1f),(0.5f, 0.75f), (0.5f, 0.75f) ),
				new TestCaseData((0f,0.75f),(-0.1f, 5f), (0f, 0.75f) ),
				new TestCaseData((0f,0.75f),(0.1f, 5f), (0.1f, 0.75f) ),
				new TestCaseData((1f, 2f),(2.5f, 3f),noIntersection),
				new TestCaseData((4f, 12f),(2.5f, 3f),noIntersection),
			};

			//Had to rename test cases, default naming with tuples as parameters causes some 
			// issue with test adapter
			return testCases
				.Select((c, i) => c.SetName($"{nameof(IntersectSegments_ShouldReturnExpectedValue)}(Case {i})"))
				.Cast<object>()
				.ToArray();
		}


		public static object[] TestCaseData_TryIntersect_ShouldReturnExpectedValue()
		{
			return new object[]
			{
				new TestCaseData(
					new Cube(new Point3D(0f,0f,0f), 1),
					new Cube(new Point3D(0.5f,0f,0f), 1),
					true),
				new TestCaseData(
					new Cube(new Point3D(0f,0f,0f), 1),
					new Cube(new Point3D(0f,-0.5f,0f), 1),
					true),
				new TestCaseData(
					new Cube(new Point3D(0f,0f,0f), 1),
					new Cube(new Point3D(0f,-1f,0f), 0.9f),
					false),
				new TestCaseData(
					new Cube(new Point3D(0f,0f,0f), 1),
					new Cube(new Point3D(0f,0f,1f), 1f),
					false)
			};
		}
		public static object[] TestCaseData_TryIntersect_ShouldGenerateCuboid()
		{
			return new object[] {

				new TestCaseData(
					new Cube(new Point3D(), 1f),
					new Cube(new Point3D(), 1f),
					(-0.5f,0.5f,-0.5f,0.5f,-0.5f,0.5f)
					),
				new TestCaseData(
					new Cube(new Point3D(), 1f),
					new Cube(new Point3D(), 2f),
					(-0.5f,0.5f,-0.5f,0.5f,-0.5f,0.5f)
					),
				new TestCaseData(
					new Cube(new Point3D(), 1f),
					new Cube(new Point3D{ Y = 1f }, 2f),
					(-0.5f,0.5f,0f,0.5f,-0.5f,0.5f)
					),
				new TestCaseData(
					new Cube(new Point3D{ Z = 0.5f }, 1f),
					new Cube(new Point3D{ Y = 1f }, 2f),
					(-0.5f,0.5f,0f,0.5f,0f,1f)
					),
				new TestCaseData(
					new Cube(new Point3D{ Y = 1f }, 2f),
					new Cube(new Point3D{ X = -0.2f, Z = 0.5f }, 1f),
					(-0.7f,0.3f,0f,0.5f,0f,1f)
					),
			};
		}

		public static object[] TestCaseData_TryIntersect_ShouldGenerateCuboidUsingPreviousIntersection()
		{
			return new object[]
			{
				new TestCaseData(
					new Cube(new Point3D(), 1f),
					new Cube(new Point3D(), 1f),
					new Cube(new Point3D{ Y = 0.5f }, 0.5f),
					(-0.25f,0.25f,0.25f,0.5f,-0.25f,0.25f)
					),
				new TestCaseData(
					new Cube(new Point3D{ X = -0.5f }, 1f),
					new Cube(new Point3D(), 0.75f),
					new Cube(new Point3D{ Z = 0.5f }, 0.5f),
					(-0.25f, 0.0f, -0.25f, 0.25f, 0.25f, 0.375f)
					),
			};
		}

		public static object[] TestCaseData_CalculateVolume_ShouldReturnExpected()
		{
			return new object[] { 
			
				new TestCaseData(new Cube(new Point3D(), 1f), 1f),
				new TestCaseData(new Cube(new Point3D(), 3f), 27f),
				new TestCaseData(new Cuboid{ XMax = 2, YMax = 1, ZMax = 0.5f}, 1f),
				new TestCaseData(new Cuboid{ XMax = 2, YMax = 2, ZMax = 1.5f}, 6f),
				new TestCaseData(new Cuboid{ XMax = 2, YMin =-1f ,YMax = 2, ZMax = 1.5f}, 9f),
				new TestCaseData(new Cuboid{ XMin = 1.5f, XMax = 2, YMax = 2, ZMax = 1.5f}, 1.5f),
			};
		}
	}
}