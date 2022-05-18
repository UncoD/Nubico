using SFML.Window;

namespace Nubico.Interfaces
{
    /// <summary>
    /// ������������ ��� ����������� �������� (� ����), ������� ����� ����������� �� ���� � ����������
    /// </summary>
    internal interface IOnKeyPressable
    {
        /// <summary>
        /// ���������� ��� ������� ������� ����������
        /// </summary>
        /// <param name="pressedKey">������� �������</param>
        /// <param name="isAlreadyPressed">���� �� ������ ������� �� ���������� ����� (����������� �������)</param>
        void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed);

        /// <summary>
        /// ���������� ��� ������� ������ ����������
        /// </summary>
        /// <param name="pressedKeys">������ ������� ������ �� ������� �����</param>
        void OnKeyPress(Dictionary<Keyboard.Key, bool> pressedKeys);
    }
}