using NUnit.Framework;
using System.Linq;
using CubeIntersect;

namespace CubeTest
{
	public class CubeTests
	{
		[TestCaseSource(nameof(TestCaseData_CalculateVolume))]
		public void CalculateVolume_ShouldReturnExpectedResult(Point3D center, float length, float expectedVolume)
		{
			var cube = new Cube(center, length);

			var volume = cube.CalculateVolume();

			Assert.AreEqual(expectedVolume, volume);
		}

		[TestCaseSource(nameof(TestCaseData_CubeConsistant))]
		public void CubeProperties_ShouldBeConsistant(Point3D center, float length)
		{
			var cube = new Cube(center, length);

			Assert.IsTrue(cube.XMin <= cube.XMax);
			Assert.IsTrue(cube.YMin <= cube.YMax);
			Assert.IsTrue(cube.ZMin <= cube.ZMax);

			// This assertions might probably fail once non "nice" lengths 
			// are used
			Assert.AreEqual(length, cube.XMax - cube.XMin);
			Assert.AreEqual(length, cube.YMax - cube.YMin);
			Assert.AreEqual(length, cube.ZMax - cube.ZMin);
		}

		public static object[] TestCaseData_CalculateVolume()
		{
			var cases = new[]
			{
				new TestCaseData(new Point3D(0f,0f,0f), 1f, 1f),
				new TestCaseData(new Point3D(0f,0.25f,0f), 1f, 1f),
				new TestCaseData(new Point3D(1f,0.25f,0f), 2f, 8f),
			};

			return cases.Cast<object>().ToArray();
		}

		public static object[] TestCaseData_CubeConsistant()
		{
			var cases = new object[]
			{
				new TestCaseData(new Point3D(1f, 0.75f, 2f), 1f),
				new TestCaseData(new Point3D(0f, -0.75f, 2f), 2f),
				new TestCaseData(new Point3D(-10f, 1.75f, -1f), 3f),
			};

			return cases;
		}
	}
}
