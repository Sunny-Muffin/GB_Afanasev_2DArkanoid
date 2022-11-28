using UnityEngine;

namespace Arkanoid
{
    public static partial class BuilderExtensions
    {
        public static GameObject SetName(this GameObject gameObject, string name)
        {
            gameObject.name = name;
            return gameObject;
        }
        public static GameObject SetLayer(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            return gameObject;
        }
        public static GameObject SetTransform(this GameObject gameObject, Transform transform)
        {
            gameObject.transform.localScale /= 4;
            gameObject.transform.position = transform.position;
            gameObject.transform.rotation = transform.rotation;
            return gameObject;
        }
        public static GameObject AddRigidbody2D(this GameObject gameObject, float mass)
        {
            var component = gameObject.GetOrAddComponent<Rigidbody2D>();
            component.gravityScale = 0;
            component.mass = mass;
            return gameObject;
        }
        public static GameObject AddBoxCollider2D(this GameObject gameObject)
        {
            gameObject.GetOrAddComponent<BoxCollider2D>();
            return gameObject;
        }
        public static GameObject AddSprite(this GameObject gameObject, Sprite sprite)
        {
            var component = gameObject.GetOrAddComponent<SpriteRenderer>();
            component.sprite = sprite;
            return gameObject;
        }
        public static GameObject AddForce(this GameObject gameObject, float force, Transform barrel)
        {
            var component = gameObject.GetOrAddComponent<Rigidbody2D>();
            component.AddForce(barrel.up * force);
            return gameObject;
        }
        private static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var result = gameObject.GetComponent<T>();
            if (!result)
            {
                result = gameObject.AddComponent<T>();
            }
            return result;
        }
    }
}
