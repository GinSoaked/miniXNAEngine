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
    public class UIButton : UIPanel
    {

        Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public delegate void ClickMethod();
        ClickMethod method = null;
        

        public UIButton(int leftPoint, int topPoint, int panelWidth, int panelHeight, string textureAddress, Color colour, ClickMethod onClickMethod, float layer, bool transform)
            : base(leftPoint, topPoint, panelWidth, panelHeight, colour, layer, transform)
        {
            texture = TextureManager.GetTexture(textureAddress);
            method = onClickMethod;
        }

        public void setMethod(ClickMethod c)
        {
            method = c;
        }
        protected override void onClick()
        {
            if (Visible)
            {
                method.Invoke();
                base.onClick();
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

    public class UIIndexedButton : UIPanel
    {

        Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public delegate void ClickMethod(int x, int y);
        ClickMethod method = null;
        int x, y;

        public UIIndexedButton(int leftPoint, int topPoint, int panelWidth, int panelHeight, string textureAddress, Color colour, ClickMethod onClickMethod, float layer, bool transforms, int x, int y)
            : base(leftPoint, topPoint, panelWidth, panelHeight, colour, layer, transforms)
        {
            texture = TextureManager.GetTexture(textureAddress);
            method = onClickMethod;
            this.x = x;
            this.y = y;
        }

        
        protected override void onClick()
        {
            if (Visible)
            {
                method.Invoke(x, y);
                base.onClick();
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
