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
    public class Entity
    {
        protected bool updates = true;



        public bool Updates
        {
            get { return updates; }
        }

        private Scene parent;
        public Scene Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Vector2 Position;
        public float Rotation;
        public float Scale;

        // Component management
        protected List<Component> components = new List<Component>();
        private Stack<Component> toAdd = new Stack<Component>();
        private Stack<Component> toRemove = new Stack<Component>();

        public Entity()
        {
            Position = new Vector2();
            Rotation = 0.0f;
            Scale = 1.0f;
        }

        public virtual void OnAttach(Scene scene)
        {
            parent = scene;
        }

        public virtual void OnRemove()
        {
            for (int i = components.Count; i > 0; i--)
            {
                components[0].OnRemove();
                components.RemoveAt(0);
            }

        }

        public Component FindComponentByType<T>()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (typeof(T) == components[i].GetType())
                {
                    return components[i];
                }
            }

            return null;
        }



        public virtual void Update(GameTime time)
        {
            while (toAdd.Count > 0)
            {
                Component c = toAdd.Pop();
                c.OnAttach(this);
                components.Add(c);
            }
            while (toRemove.Count > 0)
            {
                Component c = toRemove.Pop();
                c.OnRemove();
                components.Remove(c);
            }

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(time);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw(spriteBatch);
            }
        }

        public void AddComponent(Component c)
        {
            toAdd.Push(c);
        }

        public void RemoveComponent(Component c)
        {
            toRemove.Push(c);
        }

        public bool CicleCollision(Entity rhs, float distance)
        {
            Vector2 difference = rhs.Position - this.Position;
            if (difference.Length() < distance) return true;
            return false;      
        }
    }
}
