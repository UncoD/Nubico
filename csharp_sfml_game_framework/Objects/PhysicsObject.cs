using SFML.Graphics;
using System;

namespace Ungine
{
    /// <summary>
    /// Объект, который проверяется на столкновение с объектами этого же класса
    /// </summary>
    public class PhysicsObject : GameObject, IOnCollidable
    {
        /// <summary>
        /// <br>Тип столкновения: горизонтальное, вертикальное, отстутствие столкновения</br>
        /// <br>Изменяется при столкновении объектов</br>
        /// </summary>
        protected CollideType collideType = CollideType.None;
        
        /// <summary>
        /// Конуструктор физического объекта, содержащего графическое представление
        /// </summary>
        /// <param name="x">Горизонтальная позиция</param>
        /// <param name="y">Вертикальная позиция</param>
        /// <param name="pathToSprite">Путь к файлу изображения (.png, .jpg)</param>
        public PhysicsObject(float x, float y, string pathToSprite) : base(x, y, pathToSprite) {}

        /// <summary>
        /// Конструктор физичекого объекта без начального графического представления
        /// </summary>
        /// <param name="x">Горизонтальная позиция</param>
        /// <param name="y">Вертикальная позиция</param>
        public PhysicsObject(float x, float y) : base(x, y) {}

        /// <summary>
        /// Тип столкновения: горизонтальное, вертикальное, отстутствие столкновения
        /// </summary>
        protected enum CollideType
        {
            /// <summary>
            /// Вертикальное столкновение - объекты столкнулись боковыми сторонами
            /// </summary>
            Vertical,
            /// <summary>
            /// Горизонтальное столкновение - объекты столкнулись верхней и нижней сторонами
            /// </summary>
            Horizontal,
            /// <summary>
            /// Столкновения не произошло
            /// </summary>
            None
        }

        /// <summary>
        /// <br>Проверка на столкновение с другим физическим объектом</br>
        /// <br>Сохраняет тип столкновения (collideType): горизонтальное, вертикальное, не было столкновения</br>
        /// </summary>
        /// <param name="other">Физический объект, с которым осуществляется проверка</param>
        /// <returns>Столкнулись ли объекты</returns>
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

        public virtual void OnCollide(GameObject collideObject) { }

        internal void ClearCollide()
        {
            collideType = CollideType.None;
        }
    }
}