using InfiniteBall.Pooling;
using UnityEngine;

namespace InfiniteBall
{
    public class DestroyOffscreen : MonoBehaviour
    {
        public delegate void OnDestroy();
        public event OnDestroy DestroyCallback;
    
        /// <summary>
        /// distance in Viewport coordinates 
        /// when object is offscreen to this distance he will be destroyed
        /// </summary>
        [SerializeField] private float destroyDist = .5f;

        // Update is called once per frame
        void Update()
        {
            if (IsOffscreen()) OnOutOfBounds();
        }

        private bool IsOffscreen()
        {
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);

            return viewportPoint.x < -destroyDist || viewportPoint.y < -destroyDist;
        }

        public void OnOutOfBounds()
        {
            GameObjectUtil.Destroy(gameObject);

            if (DestroyCallback != null) DestroyCallback();
        }
    }
}
