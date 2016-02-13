using InfiniteBall.Pooling;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiniteBall
{
    public class RestartWhenFall : MonoBehaviour
    {
        private float timeAfterGrounded;
        [SerializeField] private float fallingTimeToRestart = 3;

        void Update()
        {
            if (GetComponent<BallMovement>().isGrounded)
            {
                timeAfterGrounded = 0;
            }
            else
            {
                timeAfterGrounded += Time.deltaTime;
                if (timeAfterGrounded < fallingTimeToRestart) return;

                SceneManager.LoadScene(0);
                GameObjectUtil.ShutdownAll();
            }
        }

       
    }
}
