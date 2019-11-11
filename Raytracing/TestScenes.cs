using Raytracing.Algebra;
using Raytracing.Surfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public static class TestScenes
    {
		//Scene presenting different materials
        public static Scene Materials()
        {
            Random random = new Random();
            Scene scene = new Scene(new Color(255, 255, 255));

            SurfaceMaterial solid = new SurfaceDiffuse(new Color(128, 128, 128));
            SurfaceMaterial solidRed = new SurfaceDiffuse(new Color(255, 128, 128));
            SurfaceMaterial solidBlue = new SurfaceDiffuse(new Color(128, 128, 255));
            SurfaceMaterial reflective = new SurfaceReflective(scene);
            SurfaceMaterial hybrid = new SurfaceMaterialProduct(solid, reflective, 0.2); //20% reflective, 80% solid

            //80% transparent, 10% reflective, 10% absorptive
            SurfaceMaterial glass = new SurfaceMaterialProduct(new SurfaceTransparent(scene),
                new SurfaceMaterialProduct(reflective, new SurfaceDiffuse(new Color(0, 0, 0)), 0.5), 0.2);

            //Red glass
            SurfaceMaterial redGlass = new SurfaceMaterialProduct(new SurfaceTransparent(scene), new SurfaceDiffuse(new Color(0, 0, 0)),
                new Vector3(0, 1, 1)); //We specify that only red channel is affected by transparent material

            scene.addSurface(new Surface(new SurfaceFloor(-10, -500, 500, -500, 500), hybrid));
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(-20, 0, 50), 10), glass));
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(20, 0, 50), 10), redGlass));

            //We add 4 random spheres into the scene
            for (int i = 0; i < 4; i++)
            {
                scene.addSurface(new Surface
                    (new SurfaceSphere
                        (new Vector3(random.Next(-50, 50), random.Next(5, 25), random.Next(60, 80)), random.Next(4, 14)),
                        new SurfaceMaterialProduct
                        (new SurfaceDiffuse(new Color((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                            reflective, 0.25)));
            }

            return scene;
        }
        
        //Scene presenting a non-euclidean space
        public static Scene MagicWindow()
        {
            Scene mainScene = new Scene(new Color(255, 255, 255));
            Scene magicScene = new Scene(new Color(255, 255, 255));

            SurfaceMaterial diffuseRed = new SurfaceDiffuse(new Color(200, 100, 100));
            SurfaceMaterial diffuseGreen = new SurfaceDiffuse(new Color(100, 200, 100));
            SurfaceMaterial diffuseBlue = new SurfaceDiffuse(new Color(100, 100, 200));
            SurfaceMaterial diffuseGray = new SurfaceDiffuse(new Color(100, 100, 100));
            SurfaceMaterial magicWindow = new SurfaceMaterialProduct(diffuseGray, new SurfaceTransparent(magicScene), 0.9);

            //Objects are added into a different scene
            magicScene.addSurface(new Surface(new SurfaceSphere(new Vector3(-20, 15, 80), 10), diffuseRed));
            magicScene.addSurface(new Surface(new SurfaceSphere(new Vector3(20, 15, 80), 10), diffuseGreen));
            magicScene.addSurface(new Surface(new SurfaceSphere(new Vector3(0, -15, 80), 10), diffuseBlue));

            //Object in main scene
            //We use transparent material, but propagate rays into a different scene
            //Therefore, the objects can be seen only while looking through the sphere
            mainScene.addSurface(new Surface(new SurfaceSphere(new Vector3(0, 2, 40), 10), magicWindow));

            return mainScene;
        }
    }
}
