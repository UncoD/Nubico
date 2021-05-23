using System.Collections.Generic;
using SFML.Audio;

namespace Ungine
{
    public class MusicController
    {
        private string pathToMusic;
        public Music Music { get; private set; }
        private bool isLoop;

        internal MusicController() {}

        public void PlayMusic(string pathToMusic)
        {
            if (Music == null || this.pathToMusic != pathToMusic)
            {
                Music = new Music(pathToMusic) { Loop = isLoop };
                Music.Play();
                this.pathToMusic = pathToMusic;
            }
            else
            {
                Music.Play();
            }
        }

        public void StopMusic()
        {
            Music?.Stop();
        }

        public void LoopMusic(bool loop)
        {
            isLoop = loop;
            if (Music != null)
            {
                Music.Loop = isLoop;
            }
        }
    }
}
