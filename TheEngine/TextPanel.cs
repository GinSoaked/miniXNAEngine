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
    public enum Origin
    {
        TOP_LEFT,
        TOP_RIGHT,
        TOP_CENTRE,
        BOTTOM_LEFT,
        BOTTOM_RIGHT,
        BOTTOM_CENTRE,
        CENTRE,
        CENTRE_LEFT,
        CENTRE_RIGHT
    }

    public class TextPanel : UIPanel
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
        float m_spacing = 0;

        public TextPanel(int leftPoint, int topPoint, int panelWidth, int panelHeight, string fontAddress, string displayText, Color colour, float scale, Origin origin, float spacing, float layer, bool transform)
            : base(leftPoint, topPoint, panelWidth, panelHeight, colour, layer, transform)
        {
            font = TextureManager.GetFont(fontAddress);
            m_spacing = spacing;
            text = displayText;
            stringOffset = new Vector2(this.Bounds.Width * 0.5f, this.Bounds.Height * 0.5f);

            switch (origin)
            {
                case Origin.TOP_LEFT:
                    stringOrigin = Vector2.Zero;
                    break;
                case Origin.TOP_RIGHT:
                    stringOrigin.X = font.MeasureString(displayText).X;
                    stringOrigin.Y = 0;
                    break;
                case Origin.TOP_CENTRE:
                    stringOrigin.X = font.MeasureString(displayText).X * 0.5f;
                    stringOrigin.Y = 0;
                    break;
                case Origin.BOTTOM_LEFT:
                    stringOrigin.X = 0;
                    stringOrigin.Y = font.MeasureString(displayText).Y;
                    break;
                case Origin.BOTTOM_RIGHT:
                    stringOrigin.X = font.MeasureString(displayText).X;
                    stringOrigin.Y = font.MeasureString(displayText).Y;
                    break;
                case Origin.BOTTOM_CENTRE:
                    stringOrigin.X = font.MeasureString(displayText).X * 0.5f;
                    stringOrigin.Y = font.MeasureString(displayText).Y;
                    break;
                case Origin.CENTRE_LEFT:
                    stringOffset.X = 0;
                    stringOrigin.Y = font.MeasureString(displayText).Y * 0.5f;
                    break;
                case Origin.CENTRE_RIGHT:
                    stringOrigin.X = font.MeasureString(displayText).X;
                    stringOrigin.Y = font.MeasureString(displayText).Y * 0.5f;
                    break;
                case Origin.CENTRE:
                    stringOrigin.X = font.MeasureString(displayText).X * 0.5f;
                    stringOrigin.Y = font.MeasureString(displayText).Y * 0.5f;
                    break;
                default:
                    //default to centre
                    stringOrigin.X = font.MeasureString(displayText).X * 0.5f;
                    stringOrigin.Y = font.MeasureString(displayText).Y * 0.5f;
                    break;
            }
            
            this.scale = scale;
        }

        float m_lifeTimer;
        float m_fadeTimer = 1;
        bool m_timed = false;
        Color m_startColor;
        public TextPanel(int leftPoint, int topPoint, int panelWidth, int panelHeight, string fontAddress, string displayText, Color colour, float scale, Origin origin, float spacing, float layer, bool transform, float lifeTimer) :
            this(leftPoint, topPoint, panelWidth, panelHeight, fontAddress, displayText, colour, scale, origin, spacing, layer, transform)
        {
            m_timed = true;
            m_lifeTimer = lifeTimer;
            m_startColor = colour;

        }

        public override void Update(GameTime time)
        {
            if (m_timed)
            {
                m_lifeTimer -= (float)time.ElapsedGameTime.Milliseconds;
                if (m_lifeTimer <= 0) // if time is up, begin fade
                {
                    m_fadeTimer -= (float)time.ElapsedGameTime.Milliseconds / 1000;
                    if (m_fadeTimer > 0)
                        Colour = m_startColor * m_fadeTimer;
                    else SceneManager.CurrentScene.RemoveUIPanel(this);
                }


                //if (m_entering)
                //{
                    
                //    if (m_fadeTimer < 1)
                //    {
                //        m_fadeTimer += (float)time.ElapsedGameTime.Milliseconds / 1000;
                //        // moving in
                //        Vector2 pos = Vector2.Lerp(m_startPos, m_endPos, m_fadeTimer);
                //        Bounds = new Rectangle((int)pos.X, (int)pos.Y, Bounds.Width, Bounds.Height);
                //        UpdateRectangle();
                //    }
                //    else
                //    {
                //        //stationary
                //        m_lifeTimer -= (float)time.ElapsedGameTime.Milliseconds / 1000;
                //        if (m_lifeTimer < 0)
                //        {
                //            // go back
                //            m_entering = false;
                //            m_fadeTimer = 0;
                //        }
                //    }

                //}
                //else
                //{
                //    if (m_fadeTimer < 1)
                //    {
                //        m_fadeTimer += (float)time.ElapsedGameTime.Milliseconds / 1000;

                //        // moving out
                //        Vector2 pos = Vector2.Lerp(m_endPos, m_startPos, m_fadeTimer);
                //        Bounds = new Rectangle((int)pos.X, (int)pos.Y, Bounds.Width, Bounds.Height);
                //        UpdateRectangle();
                //    }
                //    else
                //    {
                //        SceneManager.CurrentScene.RemoveUIPanel(this);
                //    }

                //}
                

            }
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Visible)
            {
                font.Spacing = m_spacing;
                spriteBatch.DrawString(font, text, new Vector2(this.Bounds.X, this.Bounds.Y) + stringOffset, Colour, 0.0f, stringOrigin, scale, SpriteEffects.None, Layer);
                font.Spacing = 0;
            }
        }
    }
}
