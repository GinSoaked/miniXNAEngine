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
    public class Component
    {
        private Entity parent;
        public Entity Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Component()
        {
        }

        public virtual void OnAttach(Entity e)
        {
            parent = e;
        }

        public virtual void OnRemove()
        {

        }

        public virtual void Update(GameTime time)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
