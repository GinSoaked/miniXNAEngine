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
    public class Tooltip : UIPanel
    {
        Texture2D texture;
        Color colour;
        public int tree, tier, attribute;
        public Tooltip(int leftPoint, int topPoint, int panelWidth, int panelHeight, string textureAddress, Color color)
            : base(leftPoint, topPoint, panelWidth, panelHeight, color, 0.1f, true)
        {
            texture = TextureManager.GetTexture(textureAddress);
            this.colour = color;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            if (Visible)
            {
                spriteBatch.Draw(texture, this.Bounds, colour);
            }
            base.Draw(spriteBatch);

        }
        public void UpdateText()
        {

        }
        public void HideText()
        {

        }
        public void ShowText()
        {

        }





    }
}
