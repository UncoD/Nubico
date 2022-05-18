using SFML.Audio;

namespace Nubico.Controllers
{
    /// <summary>
    /// <br>Контроллер звуков</br>
    /// <br>Индивидуален для каждых объектов и сцен</br>
    /// </summary>
    public class SoundController : IDisposable
    {
        /// <summary>
        /// <br>Объект класса Sound библиотеки SFML</br>
        /// <br>Можно управлять звуком напрямую, используя функционал SFML</br>
        /// </summary>
        public Sound Sound { get; private set; }
        private SoundBuffer soundBuffer;
        private string pathToSound;
        private bool disposedValue;

        internal SoundController() { }

        /// <summary>
        /// Запустить полигрывание звукового файла
        /// </summary>
        /// <param name="pathToSound">Путь к звуковому файлу .wav</param>
        public void Play(string pathToSound)
        {
            if (Sound == null || this.pathToSound != pathToSound)
            {
                soundBuffer?.Dispose();
                Sound?.Dispose();
                soundBuffer = new SoundBuffer(pathToSound);
                Sound = new Sound(soundBuffer);
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

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        /// <param name="disposing">Освобождать ли управляемые ресурсы.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                soundBuffer?.Dispose();
                Sound?.Dispose();
                disposedValue = true;
            }
        }

        /// <summary>
        /// Освободить неуправляемые ресурсы.
        /// </summary>
        ~SoundController()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Освободить управляемые и неуправляемые ресурсы.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}