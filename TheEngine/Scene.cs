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
    public class Scene
    {
        protected Camera cam;

        public Camera Cam
        {
            get { return cam; }
            set { cam = value; }
        }

        private TimeSpan TimeToMove;
        private TimeSpan _timeSoFar = TimeSpan.Zero;


        protected bool tooltipVisible = false;
        public bool TooltipVisible
        {
            get { return tooltipVisible; }
            set { tooltipVisible = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private ContentManager content;
        public ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        private SceneManager parent;
        public SceneManager Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private bool loaded = false;
        public bool Loaded
        {
            get { return loaded; }
        }

        protected UIPanel basePanel;
        private UIPanel transformedBasePanel;


        // Entity controls.
        protected List<Entity> allEntities = new List<Entity>();
        protected List<Entity> updatingEntities = new List<Entity>();

        // These stacks prevent any weird behaviour from removing entites mid update.
        private Stack<Entity> toAdd = new Stack<Entity>();
        private Stack<Entity> toRemove = new Stack<Entity>();

        //scene changing
        protected bool leaving = false;
        protected bool entering = true;
        float camLeaveMovementX = 0.1f;
        float camEnterMovementX = 64f;
        private string sceneToGoTo = "";
        //

        public Scene(SceneManager manager, string cameraName)
        {
            parent = manager;

            cam = new Camera(cameraName, new Vector2(parent.BackBufferWidth / 2, parent.BackBufferHeight / 2));

            entering = false;
            leaving = false;
            Cam.TargetPosition = new Vector2(parent.BackBufferWidth / 2, parent.BackBufferHeight / 2);
            Cam.TargetZoom = Cam.Zoom;
            Cam.SetViewport(parent.BackBufferWidth, parent.BackBufferHeight);
            Cam.SetZoomCentreOffset(0, 0);
            Cam.SetZoomCentreTarget(0, 0);
        }

        public Scene(SceneManager manager, string cameraName, float camX, float camY)
        {
            parent = manager;

            cam = new Camera(cameraName, new Vector2(camX, camY));

            entering = false;
            leaving = false;
            Cam.TargetPosition = new Vector2(camX, camY);
            Cam.TargetZoom = Cam.Zoom;
            Cam.SetViewport(parent.BackBufferWidth, parent.BackBufferHeight);
            Cam.SetZoomCentreOffset(0, 0);
            Cam.SetZoomCentreTarget(0, 0);
        }


        public virtual void Load()
        {
            loaded = true;
            content = new ContentManager(parent.Parent.Services);
            content.RootDirectory = "Content";
            device = parent.GraphicsDevice;
            spriteBatch = new SpriteBatch(device);
            TextureManager.Initilize(this.content);
            AudioManager.Initilize(this.content);

            basePanel = new UIPanel(0, 0, Parent.BackBufferWidth, Parent.BackBufferHeight,Color.White, 0.0f, false);
            basePanel.CaptureClicks = false;
            transformedBasePanel = new UIPanel(0, 0, Parent.BackBufferWidth, Parent.BackBufferHeight, Color.White, 0.0f, true);
            transformedBasePanel.CaptureClicks = false;

            
        }

        /// <summary>
        /// Updates base scene class. This should be called before any inherited scenes perform their update logic.
        /// </summary>
        /// <param name="time"></param>
        public virtual void Update(GameTime time)
        {
            InputManager.Update();

            while (toAdd.Count > 0)
            {
                Entity e = toAdd.Pop();
                e.OnAttach(this);
                allEntities.Add(e);
                if (e.Updates)
                {
                    updatingEntities.Add(e);
                }
            }
            while (toRemove.Count > 0)
            {
                Entity e = toRemove.Pop();
                e.OnRemove();
                allEntities.Remove(e);
                if (e.Updates)
                {
                    updatingEntities.Remove(e);
                }
            }
            basePanel.Update(time);
            transformedBasePanel.Update(time);
            for (int i = 0; i < updatingEntities.Count; ++i)
            {
                updatingEntities[i].Update(time);
            }

            //check for clicks
            if (InputManager.IsMouseButtonPressed(MouseButton.Left))
            {
                basePanel.ProcessClick();
                transformedBasePanel.ProcessClick();
            }

            if (CameraManager.Instance.CurrentCamera != cam)
            {
                CameraManager.Instance.CurrentCamera = cam;

            }

            if (entering)
            {
                if (!EnteringScene(time))
                {
                    entering = false;
                }
            }
            else if (leaving)
            {
                if (!LeavingScene(time)) // if finished leaving
                {
                    Parent.SetScene(sceneToGoTo);
                    //SceneManager.CurrentScene.Cam.Position = new Vector2(-Parent.BackBufferWidth, 0);
                }
            }

            Cam.Update(time);
        }

        public void ResumeScene()
        {
            cam.Position = new Vector2(-Parent.BackBufferWidth, 0);
            camLeaveMovementX = 0.1f;
            camEnterMovementX = 64f;
            entering = true;
            leaving = false;
        
        }

        protected void leaveScene(string inputScene)
        {
            sceneToGoTo = inputScene;
            leaving = true;
            //entering = true;
        }

        private bool LeavingScene()
        {
            camLeaveMovementX *= 2;
            cam.ModifyPosition(new Vector2(camLeaveMovementX, camLeaveMovementX));
            if (cam.Position.X > SceneManager.CurrentScene.Parent.BackBufferWidth)
            {
                entering = true;
                return false;
            }
            else return true; //true if still leaving
        }

        private bool LeavingScene(GameTime gametime)
        {
            //return LerpX(gametime, cam.Position, -2);

            if (cam.Position.X > parent.BackBufferWidth)
            {
                _timeSoFar = TimeSpan.Zero;
                return false;
                //has left
            }
            else
            {
                Cam.Position = LerpVec(gametime, cam.Position, new Vector2(parent.BackBufferWidth + 1.0f,cam.Position.Y), 1);
                return true; //true if still leaving
            }
        }
        private float endX = -1;
        public float EndX
        {
            get { return endX; }
            set { endX = value; }
        }
        private bool EnteringScene(GameTime gametime)
        {
            if (cam.Position.X >= 0)
            {
                _timeSoFar = TimeSpan.Zero;
                return false;
            }
            {
                Cam.Position = LerpVec(gametime, cam.Position, new Vector2(EndX + 2, cam.Position.Y), 9);
                return true; //true if still leaving
            }
        }

        private Vector2 LerpVec(GameTime gametime, Vector2 p_source, Vector2 p_destination, int amount)
        {
            TimeToMove = TimeSpan.FromSeconds(amount);

            _timeSoFar += gametime.ElapsedGameTime;
            float lerpamount = (float)_timeSoFar.Ticks / TimeToMove.Ticks;
            return Vector2.Lerp(p_source, p_destination, lerpamount);
        }

        private bool EnteringScene()
        {
            //camEnterMovementX /= 2;
            cam.ModifyPosition(new Vector2(camEnterMovementX, 0));

            if (cam.Position.X >= endX)
            {
                return false;
            }
            else return true; //true if still leaving
        }

        public virtual void Draw()
        {   // Draw Game
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraManager.Instance.CurrentCamera.Transform);
     transformedBasePanel.Draw(this.SpriteBatch);
            spriteBatch.End();
            // Draw Game
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraManager.Instance.CurrentCamera.Transform);

            //spriteBatch.Begin();
            //Transformed UI panels!

            for (int i = 0; i < allEntities.Count; ++i)
            {
                allEntities[i].Draw(this.spriteBatch);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, CameraManager.Instance.CurrentCamera.Transform);
            transformedBasePanel.Draw(this.SpriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            basePanel.Draw(this.spriteBatch);
            spriteBatch.End();
           
         
      
            // Draw HUD UI - does not use camera transforms so this 
          
        }

        static public Vector2 GetMouseWorldPosition()
        {
            return Vector2.Transform(InputManager.GetMouseLocation(), CameraManager.Instance.CurrentCamera.BackwardsTransform);
            //return InputManager.GetMouseLocation();
        }

        static public Point GetMouseWorldPositionPoint()
        {
            return new Point((int)GetMouseWorldPosition().X, (int)GetMouseWorldPosition().Y);
        }

        public void AddEntity(Entity Entity)
        {
            toAdd.Push(Entity);
        }

        public void RemoveEntity(Entity Entity)
        {
            toRemove.Push(Entity);
        }

        public void AddUIPanel(UIPanel panel)
        {
            if (panel.UsesTransform)
            {
                transformedBasePanel.AddChild(panel);
            }
            else
            {
                basePanel.AddChild(panel);
            }
        }

        public void RemoveUIPanel(UIPanel panel)
        {
            if (panel.UsesTransform)
            {
                transformedBasePanel.RemoveChild(panel);
            }
            else
            {
                basePanel.RemoveChild(panel);
            }
        }
    }



}
