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
    static public class AudioManager
    {
        //Dictionary of all the loaded sound effects, key is their address as this is unique for each sound.
        private static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        private static ContentManager content;

        public static void Initilize(ContentManager c)
        {
            content = c;
        }

        public static void PlaySound(string address)
        {
            if (soundEffects.Keys.Contains(address))
            {
                soundEffects[address].Play();
            }
            else
            {
                SoundEffect s = content.Load<SoundEffect>(address);
                soundEffects.Add(address, s);
                soundEffects[address].Play();
            }
        }

        static void PlaySound(string address, float volume, float pitch, float pan)
        {
            if (soundEffects.Keys.Contains(address))
            {
                soundEffects[address].Play(volume, pitch, pan);
            }
            SoundEffect s = content.Load<SoundEffect>(address);
            soundEffects.Add(address, s);
            soundEffects[address].Play(volume, pitch, pan);
        }

        #region Some Testing of Playing Unique Sound Instances
        //public static void PlayUniqueSoundInstance(string address, string callerString, bool loop)
        //{
        //    if (soundEffects.Keys.Contains(address))
        //    {
        //        if (soundEffectInstances.Keys.Contains(address + callerString))
        //        {
        //            soundEffectInstances[address + callerString].Play();
        //        }
        //        else
        //        {
        //            SoundEffectInstance sI = soundEffects[address].CreateInstance();
        //            sI.IsLooped = loop;
        //            soundEffectInstances.Add(address + callerString, sI);
        //            soundEffectInstances[address + callerString].Play();
        //        }
        //    }
        //    else
        //    {
        //        SoundEffect s = content.Load<SoundEffect>(address + callerString);
        //        soundEffects.Add(address + callerString, s);
        //        SoundEffectInstance sI = s.CreateInstance();
        //        sI.IsLooped = loop;
        //        soundEffectInstances.Add(address + callerString, sI);
        //        soundEffectInstances[address + callerString].Play();
        //        //sI.Play();
        //    }
        //}

        //public static void StopSound(string address)
        //{
        //    if (soundEffects.Keys.Contains(address))
        //    {
        //        soundEffects[address].Stop();
        //    }
        //}

        //public static void PauseUniqueSoundInstance(string address, string callerString)
        //{
        //    if (soundEffectInstances.Keys.Contains(address + callerString))
        //    {
        //        soundEffectInstances[address + callerString].Pause();
        //        //soundEffectInstances[address + callerString].
        //    }
        //}
        #endregion

        /// <summary>
        /// Preloads the sound ready to play
        /// </summary>
        /// <param name="address">Address of the sound</param>
        /// <param name="loop"></param>
        public static void PreloadSound(string address)
        {
            if (soundEffects.Keys.Contains(address))
            {
                return;
            }

            SoundEffect s = content.Load<SoundEffect>(address);
            soundEffects.Add(address, s);
        }

        /// <summary>
        /// Get new soundEffectInstance from the sound at "address"
        /// </summary>
        /// <param name="address">Address of the sound file</param>
        /// <param name="loop">To loop or not to loop</param>
        /// <param name="volume">The volume of the soundInstance</param>
        /// <returns></returns>
        public static SoundEffectInstance GetSoundEffectInstance(string address, bool loop, float volume)
        {
            if (soundEffects.Keys.Contains(address))
            { //if the soundEffect is already loaded... create a new instance from it
                SoundEffectInstance instance = soundEffects[address].CreateInstance();
                instance.IsLooped = loop;
                instance.Volume = volume;
                return instance;
            }
            else
            { //Otherwise... load in the sound effect, then create an instance
                SoundEffect s = content.Load<SoundEffect>(address);
                soundEffects.Add(address, s);
                SoundEffectInstance instance = s.CreateInstance();
                instance.IsLooped = loop;
                instance.Volume = volume;
                return instance;
            }
        }
    }
}
