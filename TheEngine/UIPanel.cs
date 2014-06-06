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
    public class UIPanel
    {
        protected Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }
        protected UIPanel parent;

        public UIPanel Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        protected bool captureClicks;
        public bool CaptureClicks
        {
            get { return captureClicks; }
            set { captureClicks = value; }
        }
        private List<UIPanel> children = new List<UIPanel>();
        public List<UIPanel> Children
        {
            get { return children; }
        }

        bool visible;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }


        protected int left, top, width, height;

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        protected float scale;

        private bool usesTransform = false;
        public bool UsesTransform
        {
            get { return usesTransform; }
            set { usesTransform = value; }
        }
        private float layer;
        public float Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        private Color colour;

        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }


        public UIPanel(int X, int Y, int panelWidth, int panelHeight, Color colour, float layer, bool transforms)
        {
            left = X;
            top = Y;
            width = panelWidth;
            height = panelHeight;
            bounds = new Rectangle(left, top, width, height);
            scale = 1.0f;
            captureClicks = true;
            visible = true;
            Colour = colour;
            UsesTransform = transforms;
            Layer = layer;
        }

        public virtual void Update(GameTime time)
        {

            for (int i = 0; i < children.Count; ++i)
            {
                children[i].Update(time);
            }
        }

        public bool clicked = false;
        Texture2D selected = TextureManager.GetTexture("Selection");


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //draw click grey-out
            if (clicked)
            {
                spriteBatch.Draw(selected, this.Bounds, new Color(1f, 1f, 1f, 1f));
            }
            for (int i = 0; i < children.Count; ++i)
            {
                children[i].Draw(spriteBatch);
            }
        }

        public virtual void ProcessClick()
        {
            bool contains = false;
            if (usesTransform)
            {
                Vector2 worldpos = Scene.GetMouseWorldPosition();
                contains = this.bounds.Contains(new Point((int)worldpos.X, (int)worldpos.Y));
            }
            else
            {
                contains = this.bounds.Contains(InputManager.GetMousePoint());
            }

            if (captureClicks && contains)
            {
                onClick();
                //AudioManager.PlaySound("Sounds/Click");
            }
            else
            {
                for (int i = 0; i < children.Count; ++i)
                {
                    children[i].ProcessClick();
                }
            }
        }

        protected virtual void onClick()
        {
        }

        public void AddChild(UIPanel child)
        {
            captureClicks = false;
            child.parent = this;
            this.children.Add(child);
            UpdateRectangle();
        }

        public void RemoveChild(UIPanel child)
        {
            children.Remove(child);
            if (children.Count == 0)
                captureClicks = true;
        }

        public void UpdateRectangle()
        {
            if (parent != null)
            {
                bounds = new Rectangle((int)(left + parent.left), (int)(top + parent.top), (int)(width * scale), (int)(height * scale));
            }
            for (int i = 0; i < children.Count; i++)
            {
                children[i].UpdateRectangle();
            }
        }

        public void PropigateScale(float scale)
        {
            this.scale = scale;
            for (int i = 0; i < children.Count; i++)
            {
                children[i].scale = this.scale;
            }
        }

        public void ToggleChildren()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Visible = !Children[i].Visible;
            }
        }
        public void HideChildren()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Visible = false;
            }
        }
        public void ShowChildren()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Visible = true;
            }
        }
        public void DisposeChildren()
        {
            Children.Clear();
        }
    }
}
