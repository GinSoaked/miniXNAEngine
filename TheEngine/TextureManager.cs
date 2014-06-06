using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace TheEngine
{
    public class TextureManager
    {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        public static ContentManager content;
        static public Vector2 getCenter(string name)
        {
            return new Vector2(GetTexture(name).Bounds.Center.X,GetTexture(name).Bounds.Center.Y);
        }
        static public Vector2 getCenter(Texture2D texture)
        {
            return new Vector2(texture.Bounds.Center.X, texture.Bounds.Center.Y);
        }
        static public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder sb = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

        public static List<Texture2D> GetAllTexturesFromFolder(string folder)
        {
            DirectoryInfo dir = new DirectoryInfo(content.RootDirectory + "/" + folder);
            FileInfo[] files = dir.GetFiles("*.*");
            List<Texture2D> myTextures = new List<Texture2D>();
            foreach (FileInfo file in files)
            {
                //string key = Path.GetFileNameWithoutExtension(file.Name);
                myTextures.Add(GetTexture(folder+"/"+Path.GetFileNameWithoutExtension(file.Name)));
            }
            return myTextures;
        }

        public static void Initilize(ContentManager c)
        {
            content = c;
            if (!textures.Keys.Contains("error"))
            {
                Texture2D t = content.Load<Texture2D>("error");
                textures.Add("error", t);
            }
        }

        public static Texture2D GetTexture(string address)
        {

            if (textures.Keys.Contains(address))
            {
              
                return textures[address];
            }
            try
            {
                Texture2D t = content.Load<Texture2D>(address);
                textures.Add(address, t);
            }
            catch
            {
               // address = "error";
                Texture2D t = content.Load<Texture2D>("error");
                textures.Add(address, t);
            }
            return textures[address];
        }

        public static bool exists(string address)
        {
            
            if (textures.Keys.Contains(address))
            {
                if (textures[address] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            try
            {
                Texture2D t = content.Load<Texture2D>(address);
                textures.Add(address, t);
                return true;
            }
            catch
            {
                textures.Add(address, null);
                return false;
            }
          
        }

        public static SpriteFont GetFont(string address)
        {
            if (fonts.Keys.Contains(address))
            {
                return fonts[address];
            }
            SpriteFont t = content.Load<SpriteFont>(address);
            fonts.Add(address, t);
            return fonts[address];
        }

        public static void PreloadTexture(string address)
        {
            if (textures.Keys.Contains(address))
            {
                return;
            }
            Texture2D t = content.Load<Texture2D>(address);
            textures.Add(address, t);
        }
    }
}
