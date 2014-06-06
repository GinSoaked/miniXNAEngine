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
    public class UITexture : UIPanel
    {
        protected Texture2D texture;

        public UITexture(int leftPoint, int topPoint, int panelWidth, int panelHeight, string textureAddress, Color colour,float layer, bool transform)
            : base(leftPoint, topPoint, panelWidth, panelHeight, colour, layer, transform)
        {
            texture = TextureManager.GetTexture(textureAddress);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(texture, this.Bounds, null, Colour, 0, Vector2.Zero, SpriteEffects.None, Layer);
            }
            base.Draw(spriteBatch);
        }
    }

}
