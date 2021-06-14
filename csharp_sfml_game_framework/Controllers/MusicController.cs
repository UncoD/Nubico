using SFML.Audio;

namespace Ungine
{
    /// <summary>
    /// <br>Контроллер музыки - общий для всей игры</br>
    /// <br>Одновременно можно запустить только один музыкальный трек</br>
    /// </summary>
    public class MusicController
    {
        private string pathToMusic;
        /// <summary>
        /// <br>Объект класса Music библиотеки SFML</br>
        /// <br>Можно управлять музыкой напрямую, используя функционал SFML</br>
        /// </summary>
        public Music Music { get; private set; }
        private bool isLoop;

        internal MusicController() {}

        /// <summary>
        /// Запустить музыкальный файл
        /// </summary>
        /// <param name="pathToMusic">Путь к музыкальному файлу .wav</param>
        public void Play(string pathToMusic)
        {
            if (Music == null || this.pathToMusic != pathToMusic)
            {
                Stop();
                Music = new Music(pathToMusic) { Loop = isLoop };
                Music.Play();
                this.pathToMusic = pathToMusic;
            }
            else
            {
                Music.Play();
            }
        }

        /// <summary>
        /// <br>Остановить проигрывание музыки</br>
        /// <br>Сбрасывает трек на начало</br>
        /// </summary>
        public void Stop() => Music?.Stop();
        /// <summary>
        /// Приостановить проигрывание музыки
        /// </summary>
        public void Pause() => Music?.Pause();
        /// <summary>
        /// Продолжить воспроизведение музыки
        /// </summary>
        public void Continue() => Music?.Play();
        /// <summary>
        /// Задать повтор проигрывания музыки
        /// </summary>
        /// <param name="loop">Повторять?</param>
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
