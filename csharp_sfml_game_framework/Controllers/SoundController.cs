using SFML.Audio;

namespace csharp_sfml_game_framework
{
    public class SoundController
    {
        public Sound Sound { get; private set; }
        private string pathToSound;

        internal SoundController() { }

        public void PlaySound(string pathToSound)
        {
            if (Sound == null || this.pathToSound != pathToSound)
            {
                Sound = new Sound(new SoundBuffer(pathToSound));
                Sound.Play();
                this.pathToSound = pathToSound;
            }
            else
            {
                Sound.Play();
            }
        }

        public void StopSound()
        {
            Sound?.Stop();
        }
    }
}