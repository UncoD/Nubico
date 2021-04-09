using System.Collections.Generic;
using SFML.Audio;

namespace csharp_sfml_game_framework
{
    public class MusicController
    {
        private string pathToMusic;
        private Music music;
        private bool isLoop;

        internal MusicController() {}

        public void PlayMusic(string pathToMusic)
        {
            if (music == null || this.pathToMusic != pathToMusic)
            {
                music = new Music(pathToMusic) { Loop = isLoop };
                music.Play();
                this.pathToMusic = pathToMusic;
            }
            else
            {
                music.Play();
            }
        }

        public void StopMusic()
        {
            music?.Stop();
        }

        public void LoopMusic(bool loop)
        {
            isLoop = loop;
            if (music != null)
            {
                music.Loop = isLoop;
            }
        }
    }
}
