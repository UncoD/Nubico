using Nubico.Interfaces;

namespace Nubico.Objects
{
    /// <summary>
    /// ������, ������� ����������� �� ������������ � ��������� ����� �� ������
    /// </summary>
    public class PhysicsObject : GameObject, IOnCollidable
    {
        /// <summary>
        /// <br>��� ������������: ��������������, ������������, ����������� ������������</br>
        /// <br>���������� ��� ������������ ��������</br>
        /// </summary>
        protected CollideType collideType = CollideType.None;
        
        /// <summary>
        /// ������������ ����������� �������, ����������� ����������� �������������
        /// </summary>
        /// <param name="x">�������������� �������</param>
        /// <param name="y">������������ �������</param>
        /// <param name="pathToSprite">���� � ����� ����������� (.png, .jpg)</param>
        public PhysicsObject(float x, float y, string pathToSprite) : base(x, y, pathToSprite) {}

        /// <summary>
        /// ����������� ���������� ������� ��� ���������� ������������ �������������
        /// </summary>
        /// <param name="x">�������������� �������</param>
        /// <param name="y">������������ �������</param>
        public PhysicsObject(float x, float y) : base(x, y) {}

        /// <summary>
        /// ��� ������������: ��������������, ������������, ����������� ������������
        /// </summary>
        protected enum CollideType
        {
            /// <summary>
            /// ������������ ������������ - ������� ����������� �������� ���������
            /// </summary>
            Vertical,
            /// <summary>
            /// �������������� ������������ - ������� ����������� ������� � ������ ���������
            /// </summary>
            Horizontal,
            /// <summary>
            /// ������������ �� ���������
            /// </summary>
            None
        }

        /// <summary>
        /// <br>�������� �� ������������ � ������ ���������� ��������</br>
        /// <br>��������� ��� ������������ (collideType): ��������������, ������������, �� ���� ������������</br>
        /// </summary>
        /// <param name="other">���������� ������, � ������� �������������� ��������</param>
        /// <returns>����������� �� �������</returns>
        public bool IsIntersects(PhysicsObject other)
        {
            var thisBoundsOnNextFrame = SpriteController.GetBounds();
            thisBoundsOnNextFrame.Left += Velocity.X;
            var otherBoundsOnNextFrame = other.SpriteController.GetBounds();
            otherBoundsOnNextFrame.Left += other.Velocity.X;

            if (thisBoundsOnNextFrame.Intersects(otherBoundsOnNextFrame))
            {
                collideType = CollideType.Horizontal;
                other.collideType = collideType;
                return true;
            }

            thisBoundsOnNextFrame.Top += Velocity.Y;
            otherBoundsOnNextFrame.Top += other.Velocity.Y;
            
            if (thisBoundsOnNextFrame.Intersects(otherBoundsOnNextFrame))
            {
                collideType = CollideType.Vertical;
                other.collideType = collideType;
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// ���������� ��� ������������ � ������ ��������
        /// </summary>
        /// <param name="collideObject">������, � ������� ��������� ������������</param>
        public virtual void OnCollide(GameObject collideObject) { }

        internal void ClearCollide()
        {
            collideType = CollideType.None;
        }
    }
}