using Raytracing.Algebra;

namespace Raytracing.Surfaces
{
    public class SurfaceBoundingVolume : SurfaceGeometry
    {
        private SurfaceGeometry bounds;
        private Surface realSurface;

        public SurfaceBoundingVolume(SurfaceGeometry bounds, Surface realSurface)
        {
            this.bounds = bounds;
            this.realSurface = realSurface;
        }

        public override CollisionInfo calculateCollision(Ray ray)
        {
            CollisionInfo boundsInfo = bounds.calculateCollision(ray);
            if (boundsInfo.Occured)
                return realSurface.Geometry.calculateCollision(ray);
            else return new CollisionInfo(false);
        }
    }
}
