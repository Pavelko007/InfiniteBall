using InfiniteBall.Pooling;
using UnityEngine;

namespace InfiniteBall
{
    public class Coin : MonoBehaviour {

        void OnCollisionEnter2D(Collision2D coll)
        {
            GameObjectUtil.Destroy(gameObject);
        }
    }
}
