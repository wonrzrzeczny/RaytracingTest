# RaytracingTest

Implementation of a raytracing rendering model in C#.
Operations are performed entirely on CPU and the code is not well optimized, therefore any real-time rendering is out of the question.
The aim of this project was to create a very flexible, highly customizable interface (taking advantage of raytracing "highly objective nature"). 

The project is (and probably will be for a long time) in a work-in-progress state.

Quick guide
-----------
To render an image you have to declare a ```Scene``` object, a ```Camera``` object and a ```Render``` object.
```Scene``` contains a list of ```Surface``` objects to be rendered, which can be added using ```addSurface(Surface)``` function.

Each ```Surface``` is composed of two components: ```SurfaceGeometry``` which determines how rays should intersect with the surface and ```SurfaceMaterial``` which determines what color does the ray intersecting with surface should carry.

Any type of ```SurfaceGeometry``` should be implemented as a class deriving from ```SurfaceGeometry```, implementing ```calculateCollision()``` method.
Any type of ```SurfaceMaterial``` should be implemented as a class deriving from ```SurfaceMaterial```, implementing ```propagateRay()``` method. This method could either calculate a color based solely on hit information, or cast another ray into the scene to resolve reflection, refraction or other similar events (refer to ```SurfaceReflective``` for an example). 

Additionaly, several basic surface properties such as ```SurfaceSphere```, ```SurfaceMesh```, ```SurfaceDiffuse```, ```SurfaceMaterialBlend``` etc. are already provided (refer to the content of Surfaces folder).

The initialization code is located in App.xaml.cs file.
Example scenes are located in TestScene class.

Screenshots
-----------

![Materials sample scene](/materials.png)
![Mesh sample scene](/monkey.png)
![Lighting2 sample scene](/lighting2.png)

Benchmarks
----------

Render time of sample scenes on my personal laptop (2.50 Ghz Intel Core i7-4710 CPU)

Materials:

1920 x 1080 - 13.44 s

1280 x 720  - 5.67 s

640 x 480   - 2.02 s

160 x 120   - 150 ms

Magic Window:

1920 x 1080 - 1.56 s

1280 x 720  - 706 ms

640 x 480   - 245 ms

160 x 120   - 20 ms

TODO list
---------
- meshes ✓
- some global illumination techniques
- mesh texturing
- volumetric lighting
- more sophisticated example scenes
- video rendering (camera movement, scene programming, etc.)
