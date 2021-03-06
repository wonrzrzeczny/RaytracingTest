﻿using Raytracing.Algebra;
using Raytracing.Lighting;
using Raytracing.Meshes;
using Raytracing.Surfaces;
using System;

namespace Raytracing.TestScenes
{
    public static partial class TestScene
    {
        //Scene presenting different materials
        public static Scene Materials()
        {
            Random random = new Random();
            Scene scene = new Scene(new Color(255, 255, 255));

            SurfaceMaterial solid = new SurfaceUnlit(new Color(128, 128, 128));
            SurfaceMaterial solidRed = new SurfaceUnlit(new Color(255, 128, 128));
            SurfaceMaterial solidBlue = new SurfaceUnlit(new Color(128, 128, 255));
            SurfaceMaterial reflective = new SurfaceReflective(scene);
            SurfaceMaterial hybrid = new SurfaceMaterialBlend(solid, reflective, 0.2); //20% reflective, 80% solid

            //80% transparent, 10% reflective, 10% absorptive
            SurfaceMaterial glass = new SurfaceMaterialBlend(new SurfaceTransparent(scene),
                new SurfaceMaterialBlend(reflective, new SurfaceUnlit(new Color(0, 0, 0)), 0.5), 0.2);

            //Red glass
            SurfaceMaterial redGlass = new SurfaceMaterialBlend(new SurfaceTransparent(scene), new SurfaceUnlit(new Color(0, 0, 0)),
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
                        new SurfaceMaterialBlend
                        (new SurfaceUnlit(new Color((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                            reflective, 0.25)));
            }

            return scene;
        }
        
        //Scene presenting a non-euclidean space
        public static Scene MagicWindow()
        {
            Scene mainScene = new Scene(new Color(255, 255, 255));
            Scene magicScene = new Scene(new Color(255, 255, 255));

            SurfaceMaterial solidRed = new SurfaceUnlit(new Color(200, 100, 100));
            SurfaceMaterial solidGreen = new SurfaceUnlit(new Color(100, 200, 100));
            SurfaceMaterial solidBlue = new SurfaceUnlit(new Color(100, 100, 200));
            SurfaceMaterial solidGray = new SurfaceUnlit(new Color(100, 100, 100));
            SurfaceMaterial magicWindow = new SurfaceMaterialBlend(solidGray, new SurfaceTransparent(magicScene), 0.9);

            //Objects are added into a different scene
            magicScene.addSurface(new Surface(new SurfaceSphere(new Vector3(-20, 15, 80), 10), solidRed));
            magicScene.addSurface(new Surface(new SurfaceSphere(new Vector3(20, 15, 80), 10), solidGreen));
            magicScene.addSurface(new Surface(new SurfaceSphere(new Vector3(0, -15, 80), 10), solidBlue));

            //Object in main scene
            //We use transparent material, but propagate rays into a different scene
            //Therefore, the objects can be seen only while looking through the sphere
            mainScene.addSurface(new Surface(new SurfaceSphere(new Vector3(0, 2, 40), 10), magicWindow));

            return mainScene;
        }
        
        public static Scene Lighting1()
        {
            Scene scene = new Scene(new Color(255, 255, 255));

            Light sun = new LightDirectional(new Color(255, 255, 255), new Vector3(1, -1, 1), scene);
            LightGroup lightGroup = new LightGroup();
            lightGroup.addLight(sun);

            SurfaceMaterial diffuseRed = new SurfaceDiffuse(new Color(255, 50, 50), lightGroup);
            SurfaceMaterial diffuseGreen = new SurfaceDiffuse(new Color(50, 255, 50), lightGroup);
            SurfaceMaterial diffuseBlue = new SurfaceDiffuse(new Color(50, 50, 255), lightGroup);
            SurfaceMaterial diffuseWhite = new SurfaceDiffuse(new Color(255, 255, 255), lightGroup);

            SurfaceMaterial specular = new SurfaceSpecular(new Color(100, 100, 100), 50, lightGroup);

            SurfaceMaterial red = new SurfaceMaterialSum(specular, diffuseRed);
            SurfaceMaterial blue = new SurfaceMaterialSum(specular, diffuseBlue);
            SurfaceMaterial green = new SurfaceMaterialSum(specular, diffuseGreen);
            SurfaceMaterial white = new SurfaceMaterialSum(specular, diffuseWhite);

            scene.addSurface(new Surface(new SurfacePlane(-25 * Vector3.Up, Vector3.Forward, Vector3.Right), white));
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(-40, 0, 80), 10), red));
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(0, 0, 80), 10), green));
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(40, 0, 80), 10), blue));

            return scene;
        }

        public static Scene Lighting2()
        {
            Scene scene = new Scene(new Color(255, 255, 255));
            Light lightRed = new LightDirectional(new Color(255, 0, 0), new Vector3(1, -3, 0), scene, true);
            Light lightGreen = new LightDirectional(new Color(0, 255, 0), new Vector3(-1, -3, 0), scene, true);
            Light lightBlue = new LightDirectional(new Color(0, 0, 255), new Vector3(0, -3, 1), scene, true);
            LightGroup lightGroup = new LightGroup();
            lightGroup.addLight(lightRed);
            lightGroup.addLight(lightGreen);
            lightGroup.addLight(lightBlue);
            
            SurfaceMaterial diffuseWhite = new SurfaceDiffuse(new Color(255, 255, 255), lightGroup);
            SurfaceMaterial specular = new SurfaceSpecular(new Color(100, 100, 100), 50, lightGroup);
            SurfaceMaterial material = new SurfaceMaterialSum(specular, diffuseWhite);

            scene.addSurface(new Surface(new SurfacePlane(-25 * Vector3.Up, Vector3.Forward, Vector3.Right), diffuseWhite));
            scene.addSurface(new Surface(new SurfaceSphere(new Vector3(0, 0, 80), 10), material));

            return scene;
        }

        public static Scene Mesh()
        {
            Scene scene = new Scene(new Color(200, 200, 255));
            
            Mesh mesh = OBJ.ParseFile(@"..\..\monkey.obj");

            LightGroup lightGroup = new LightGroup();
            lightGroup.addLight(new LightDirectional(new Color(255, 255, 255), new Vector3(0.2, -0.2, 1), scene));

            SurfaceGeometry geometry = new SurfaceMesh(mesh, Vector3.Forward * 10, new Vector3(0, Math.PI, 0), SurfaceMesh.NormalMode.PER_VERTEX);

            SurfaceMaterial diffuse = new SurfaceDiffuse(new Color(200, 212, 180), lightGroup);
            SurfaceMaterial specular = new SurfaceSpecular(new Color(255, 255, 255), 150, lightGroup);
            SurfaceMaterial material = new SurfaceMaterialSum(diffuse, specular);

            Surface monkey = new Surface(geometry, material);
            //We enclose the monkey in a bounding sphere, which speeds up the rendering over 7 times
            Surface boundedMonkey = new Surface(new SurfaceBoundingVolume(new SurfaceSphere(Vector3.Forward * 10, 2.5), monkey), null);
            scene.addSurface(boundedMonkey);

            return scene;
        }
    }
}
