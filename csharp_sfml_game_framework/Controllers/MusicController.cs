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

        public void Play(string pathToMusic)
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

        public void Stop() => Music?.Stop();
        public void Pause() => Music?.Pause();
        public void Continue() => Music?.Play();

        public void SetLoop(bool loop)
        {
            isLoop = loop;
            if (Music != null)
            {
                Music.Loop = isLoop;
            }
        }
    }
}
