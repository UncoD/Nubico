using SFML.Audio;

namespace csharp_sfml_game_framework
{
    public class SoundController
    {
        private Sound sound;
        private string pathToSound;

        internal SoundController() { }

        public void PlaySound(string pathToSound)
        {
            if (sound == null || this.pathToSound != pathToSound)
            {
                sound = new Sound(new SoundBuffer(pathToSound));
                sound.Play();
                this.pathToSound = pathToSound;
            }
            else
            {
                sound.Play();
            }
        }

        public void StopSound()
        {
            sound.Stop();
        }
    }
}