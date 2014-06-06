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
    public class TextButton : UIButton
    {
        SpriteFont font;
        string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        Vector2 stringOffset;
        Vector2 stringOrigin;

        public TextButton(int leftPoint, int topPoint, int panelWidth, int panelHeight, string textureAddress, ClickMethod onClickMethod, string fontAddress, string displayText, Color colour, bool transform, float layer)
            : base(leftPoint, topPoint, panelWidth, panelHeight, textureAddress, colour,  onClickMethod, layer,transform)
        {
            font = TextureManager.GetFont(fontAddress);
            text = displayText;
            stringOffset = new Vector2(this.Bounds.Width * 0.5f, this.Bounds.Height * 0.5f);
            stringOrigin = font.MeasureString(displayText) * 0.5f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Visible)
            {
                spriteBatch.DrawString(font, text, new Vector2(this.Bounds.X, this.Bounds.Y) + stringOffset, Colour, 0.0f, stringOrigin, 1, SpriteEffects.None, Layer);
            }
        }
    }

    public class TextIndexedButton : UIIndexedButton
    {
        SpriteFont font;
        string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        Vector2 stringOffset;
        Vector2 stringOrigin;

        public TextIndexedButton(int leftPoint, int topPoint, int panelWidth, int panelHeight, string textureAddress, ClickMethod onClickMethod, string fontAddress, string displayText, Color colour, float layer, bool transform, int x, int y)
            : base(leftPoint, topPoint, panelWidth, panelHeight, textureAddress, colour, onClickMethod, layer, transform, x, y)
        {
            font = TextureManager.GetFont(fontAddress);
            text = displayText;
            stringOffset = new Vector2(this.Bounds.Width * 0.5f, this.Bounds.Height * 0.5f);
            stringOrigin = font.MeasureString(displayText) * 0.5f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Visible)
            {
                spriteBatch.DrawString(font, text, new Vector2(this.Bounds.X, this.Bounds.Y) + stringOffset, Colour, 0.0f, stringOrigin, 1, SpriteEffects.None, Layer);
            }
        }
    }
}
