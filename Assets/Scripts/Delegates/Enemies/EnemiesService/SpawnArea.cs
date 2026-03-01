using UnityEngine;

namespace Delegates.Enemies.EnemiesService
{
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] private Color   _color = Color.green;
        [SerializeField] private Vector2 _size  = new (5, 3);

        public Vector3 GetRandomPoint()
        {
            Vector3 center = transform.position;

            float x = Random.Range(-_size.x / 2f, _size.x / 2f);
            float z = Random.Range(-_size.y / 2f, _size.y / 2f);

            return center + new Vector3(x, 0f, z);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_size.x, 0f, _size.y));
        }
    }
}