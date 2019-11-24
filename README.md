# RaytracingTest

Implementation of a raytracing rendering model in C#.
Operations are performed entirely on CPU and the code is "highly objective", not well optimized, therefore any real-time rendering is out of the question (however in the future I plan to add GPU support).
The code should be treated more like a proof of a concept and was written only for educational purpose.

The project is (and probably will be for a long time) in a work-in-progress state.

Quick guide
-----------
To render an image you have to declare a ```Scene``` object, ```Camera``` object and ```Render``` object.
You can add ```Surface``` objects to the scene. ```Surface``` objects are composed of two components: ```SurfaceGeometry``` and ```SurfaceMaterial```.

Currently supported ```SurfaceGeometry``` types are: ```SurfaceSphere```, ```SurfacePlane``` and ```SurfaceFloor```.

Currently supported ```SurfaceMaterial``` types are: ```SurfaceUnlit```, ```SurfaceDiffuse```, ```SurfaceReflective```, ```SurfaceTransparent``` and ```SurfaceMaterialBlend```.

The scene initialization code is located in App.xaml.cs file.

Screenshots
-----------

![Materials sample scene](/materials.png)
![Lighting2 sample scene](/lighting2.png)
![MagicWindow sample scene](/window.png)

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
- meshes
- more realistic lighting and more materials
- calculating stuff on GPU (and other optimisations)
