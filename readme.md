## Solution with hopefully simple approach finding the intersecting volume of cubic objects, in case that they collide

### Solution is divided in two projects:
#### **CubeIntersect**
Library project that contains the classes and interfaces that allow performing the intersection:

* Point3D: Class that allows defining the X,Y,Z coordinates of a point in 3D space, used to define the center of a Cube

* ICuboid: Interface defining the required properties for any cube-like polyhedron (see [Rectangular Cuboid](https://en.wikipedia.org/wiki/Cuboid)), basically its center and maxim and minimum value of each axis. 

* Cube: Plain class implementing *ICuboid*, with single constructor taking a center Point3D and a side length/width.

* Cuboid: Class implementin also *ICuboid*, with no public constructor currently, meant to be used to contain the intersection result.

* ICuboidExtensions: Extension class on ICuboid interface, providind the following methods:
    * CalculateVolume: Obtain the volume of a *ICuboid* object
    * TryIntersect: Obtain (if exists) the intersection of a pair of ICuboids,
as a Cube implements *ICuboid*, the method is usable on Cubes, also allows the possibly to further intersect the resulting intersection with other cubes or intersections

#### **CubeTest** 
Test project with tests on some of the classes on *CubeIntersect* project:

* CubeTests: Some tests on the consistency of required constraints on Cube objects.
* ICuboidExtensions: tests on intersection, volume calculation and internal helper methods.

Created with VS 2020, targeting .Net Core 6 & C# 10, no spefic feature of .Net Core 6 or C# 10 has been used, should be possible to retarget to .Net Core 5 C# 9