using SFML.Audio;

namespace Nubico.Controllers
{
    /// <summary>
    /// <br>Контроллер звуков</br>
    /// <br>Индивидуален для каждых объектов и сцен</br>
    /// </summary>
    public class SoundController
    {
        /// <summary>
        /// <br>Объект класса Sound библиотеки SFML</br>
        /// <br>Можно управлять звуком напрямую, используя функционал SFML</br>
        /// </summary>
        public Sound Sound { get; private set; }
        private string pathToSound;

        internal SoundController() { }

        /// <summary>
        /// Запустить полигрывание звукового файла
        /// </summary>
        /// <param name="pathToSound">Путь к звуковому файлу .wav</param>
        public void Play(string pathToSound)
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

        /// <summary>
        /// Остановить проигрывание звука
        /// </summary>
        public void Stop() => Sound?.Stop();
    }
}