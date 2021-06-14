namespace Ungine
{
    /// <summary>
    /// ���������, ������������ ��� ����������� ��������, ������� ����� ������������ ����� �����
    /// </summary>
    internal interface IOnCollidable
    {
        /// <summary>
        /// ���������� ��� ������������ � ������ ��������
        /// </summary>
        /// <param name="collideObject">������, � ������� ��������� ������������</param>
        void OnCollide(GameObject collideObject);
    }
}