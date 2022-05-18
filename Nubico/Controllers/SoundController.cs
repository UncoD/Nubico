using SFML.Audio;

namespace Nubico.Controllers
{
    /// <summary>
    /// <br>���������� ������</br>
    /// <br>������������ ��� ������ �������� � ����</br>
    /// </summary>
    public class SoundController : IDisposable
    {
        /// <summary>
        /// <br>������ ������ Sound ���������� SFML</br>
        /// <br>����� ��������� ������ ��������, ��������� ���������� SFML</br>
        /// </summary>
        public Sound Sound { get; private set; }
        private SoundBuffer soundBuffer;
        private string pathToSound;
        private bool disposedValue;

        internal SoundController() { }

        /// <summary>
        /// ��������� ������������ ��������� �����
        /// </summary>
        /// <param name="pathToSound">���� � ��������� ����� .wav</param>
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
        /// ���������� ������������ �����
        /// </summary>
        public void Stop() => Sound?.Stop();

        /// <summary>
        /// ���������� �������.
        /// </summary>
        /// <param name="disposing">����������� �� ����������� �������.</param>
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
        /// ���������� ������������� �������.
        /// </summary>
        ~SoundController()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// ���������� ����������� � ������������� �������.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}