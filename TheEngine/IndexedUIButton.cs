using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TheEngine
{
    public  class IndexedUIButton: UIPanel
    {
        Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }


        public delegate void ClickMethod(int index);
        ClickMethod method = null;

        public IndexedUIButton(int leftPoint, int topPoint, int panelWidth, int panelHeight, string textureAddress, Color colour, ClickMethod onClickMethod, float layer, bool transforms, int index)
            : base(leftPoint, topPoint, panelWidth, panelHeight, colour, layer, transforms)
        {
            texture = TextureManager.GetTexture(textureAddress);
            method = onClickMethod;
            Index = index;
        }

        protected override void onClick()
        {
            if (Visible)
            {
                method.Invoke(index);
                base.onClick();
                //AudioManager.PlaySound("Sounds/Click");
            }
        }


        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(texture, this.Bounds, null, Colour, 0.0f, new Vector2(0, 0), SpriteEffects.None, Layer);
            }
            base.Draw(spriteBatch);
        }
    }
}
