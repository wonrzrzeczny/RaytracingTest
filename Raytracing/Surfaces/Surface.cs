namespace Raytracing.Surfaces
{
    public class Surface
    {
        public SurfaceGeometry Geometry { get; }
        public SurfaceMaterial Material { get; }

        public Surface(SurfaceGeometry geometry, SurfaceMaterial material)
        {
            Geometry = geometry;
            Material = material;
        }
    }
}