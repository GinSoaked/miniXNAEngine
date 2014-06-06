using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheEngine
{
    public class CameraManager
    {
        private List<Camera> cameras = new List<Camera>();
        private Camera currentCamera;
        public Camera CurrentCamera
        {
            get
            {
                if (currentCamera == null)
                {
                    return defaultCamera;
                }
                return currentCamera;
            }
            set
            {
                currentCamera = value;
            }
        }

        private Camera defaultCamera;

        private static CameraManager instance;
        public static CameraManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CameraManager();
                }
                return CameraManager.instance;

            }
        }

        //private int viewportWidth, viewportHeight;
        //private Vector3 viewport;
        //public Vector3 Viewport
        //{
        //    get { return viewport; }
        //    set { viewport = value; }
        //}

        private CameraManager()
        {
            defaultCamera = new Camera("default", Vector2.Zero);
            defaultCamera.Position = Vector2.Zero;
        }

        //public void SetViewport(int width, int height)
        //{
        //    viewportWidth = width;
        //    viewportHeight = height;
        //    viewport.X = viewportWidth;
        //    viewport.Y = viewportHeight;
        //}

        public Camera GetCamera(string name)
        {
            for (int i = 0; i < cameras.Count; ++i)
            {
                if (name == cameras[i].Name)
                {
                    return cameras[i];
                }
            }
            return null;
        }
    }
}
