using SFML.Graphics;
using System;

namespace Ungine
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
        /// <returns>������������ �� �������</returns>
        public bool IsIntersects(PhysicsObject other)
        {
            var thisBoundsOnNextFrame = new FloatRect(X - Width / 2 + Velocity.X, Y - Height / 2, Width, Height);
            var otherBoundsOnNextFrame = new FloatRect
            (
                other.X - other.Width / 2 + other.Velocity.X,
                other.Y - other.Height / 2,
                other.Width, other.Height
            );

            if (thisBoundsOnNextFrame.Intersects(otherBoundsOnNextFrame))
            {
                collideType = CollideType.Horizontal;
                other.collideType = collideType;
                return true;
            }

            thisBoundsOnNextFrame = new FloatRect(X - Width / 2 + Velocity.X, Y - Height / 2 + Velocity.Y, Width, Height);
            otherBoundsOnNextFrame = new FloatRect
            (
                other.X - other.Width / 2 + other.Velocity.X,
                other.Y - other.Height / 2 + other.Velocity.Y,
                other.Width, other.Height
            );
            
            if (thisBoundsOnNextFrame.Intersects(otherBoundsOnNextFrame))
            {
                collideType = CollideType.Vertical;
                other.collideType = collideType;
                return true;
            }
            
            return false;
        }

        public virtual void OnCollide(GameObject collideObject) { }

        internal void ClearCollide()
        {
            collideType = CollideType.None;
        }
    }
}