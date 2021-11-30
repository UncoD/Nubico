using SFML.Audio;

namespace Nubico.Controllers
{
    /// <summary>
    /// <br>���������� ������</br>
    /// <br>������������ ��� ������ �������� � ����</br>
    /// </summary>
    public class SoundController
    {
        /// <summary>
        /// <br>������ ������ Sound ���������� SFML</br>
        /// <br>����� ��������� ������ ��������, ��������� ���������� SFML</br>
        /// </summary>
        public Sound Sound { get; private set; }
        private string pathToSound;

        internal SoundController() { }

        /// <summary>
        /// ��������� ������������ ��������� �����
        /// </summary>
        /// <param name="pathToSound">���� � ��������� ����� .wav</param>
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
        /// ���������� ������������ �����
        /// </summary>
        public void Stop() => Sound?.Stop();
    }
}