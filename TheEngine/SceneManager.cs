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
    public class SceneManager
    {
        public static Random random = new Random();

        private List<Scene> scenes = new List<Scene>();
        private static Scene currentScene;
        public static Scene CurrentScene
        {
            get { return SceneManager.currentScene; }
            set { SceneManager.currentScene = value; }
        }

        private Game parent;
        public Game Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        GraphicsDeviceManager graphicsDeviceManager;
        GraphicsDevice graphicsDevice;
        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
            set { graphicsDevice = value; }
        }

        //unfinished
        int backBufferWidth;
        public int BackBufferWidth
        {
            get { return backBufferWidth; }
            set
            {
                backBufferWidth = value;
                graphicsDeviceManager.PreferredBackBufferWidth = value;
                graphicsDeviceManager.ApplyChanges();
                //CameraManager.Instance.Viewport = new Vector3(backBufferWidth, backBufferHeight, 0);
            }
        }

        //unfinished
        int backBufferHeight;
        public int BackBufferHeight
        {
            get { return backBufferHeight; }
            set
            {
                backBufferHeight = value;
                graphicsDeviceManager.PreferredBackBufferHeight = value;
                graphicsDeviceManager.ApplyChanges();
                //CameraManager.Instance.Viewport = new Vector3(backBufferWidth, backBufferHeight, 0);
            }
        }

        public SceneManager(Game game)
        {
            parent = game;
            graphicsDeviceManager = new GraphicsDeviceManager(parent);
            graphicsDeviceManager.IsFullScreen = false;
            //graphicsDeviceManager.PreferMultiSampling = true;
            //graphicsDeviceManager.ApplyChanges();
        }

        public void Initialize()
        {


//#if WINDOWS
            graphicsDeviceManager.PreferredBackBufferWidth = 1280;
            graphicsDeviceManager.PreferredBackBufferHeight = 720;
//#endif

            graphicsDevice = parent.GraphicsDevice;
            BackBufferWidth = graphicsDeviceManager.PreferredBackBufferWidth;
            BackBufferHeight = graphicsDeviceManager.PreferredBackBufferHeight;
            
        }

        public void ToggleFullScreen()
        {
            graphicsDeviceManager.IsFullScreen = !graphicsDeviceManager.IsFullScreen;
            graphicsDeviceManager.ApplyChanges();
        }

        protected void LoadCurrrentScene()
        {
            if (!currentScene.Loaded)
            {
                currentScene.Load();
            }
        }

        public void Update(GameTime gameTime)
        {
            

            if (currentScene != null)
                currentScene.Update(gameTime);
        }

        public void Draw()
        {
            if (currentScene != null)
            {
                GraphicsDevice.Clear(new Color(255, 255, 255));
                currentScene.Draw();
            }
        }

        public void AddScene(Scene scene)
        {
            if (!scenes.Contains(scene))
            {
                scenes.Add(scene);
            }
        }
        public void DeleteScene(Scene scene)
        {
            if (scenes.Contains(scene))
            {
                scenes.Remove(scene);
            }
        }

        public void DeleteScene(string sceneName)
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i].Name == sceneName)
                {
                    scenes.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Change scene by object
        /// </summary>
        /// <param name="scene"></param>
        public void SetScene(Scene scene)
        {
            AddScene(scene);
            currentScene = scene;
            LoadCurrrentScene();
            
        }
        public Scene getScene(string scenename)
        {
            for (int i = 0; i < scenes.Count; ++i)
            {
                if (scenes[i].Name == scenename)
                {
                    return scenes[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Change scene by name
        /// </summary>
        /// <param name="sceneName"></param>
        public void SetScene(string sceneName)
        {
            for (int i = 0; i < scenes.Count; ++i)
            {
                if (scenes[i].Name == sceneName)
                {
                    currentScene = scenes[i];
                    currentScene.ResumeScene();
                    break;
                }
            }

        }
        public bool Exists(string sceneName)
        {
            for (int i = 0; i < scenes.Count; ++i)
            {
                if (scenes[i].Name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
