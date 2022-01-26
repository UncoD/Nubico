using Nubico.Objects;

namespace Nubico.Interfaces
{
    /// <summary>
    /// Интерфейс, используемый для обозначения объектов, которые могут сталкиваться между собой
    /// </summary>
    internal interface IOnCollidable
    {
        /// <summary>
        /// Вызывается при столкновении с другим объектом
        /// </summary>
        /// <param name="collideObject">Объект, с которым произошло столкновение</param>
        void OnCollide(GameObject collideObject);
    }
}