using System;
using System.Collections;
using UnityEngine;

namespace InfiniteBall
{
    public class FallingPlatform : MonoBehaviour
    {
        public Rigidbody2D rb2d;

        [SerializeField] private float fallDelay;
        [SerializeField] public int numJumps;
        [SerializeField] public bool infiniteJumps = false;

        // Use this for initialization
        void Awake ()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (infiniteJumps) return;

            numJumps--;
            if(numJumps == 0) StartCoroutine(Fall());
        }

        IEnumerator Fall()
        {
            yield return  new WaitForSeconds(fallDelay);
            rb2d.isKinematic = false;
        }

        public void SetupPlatform(PlatformType platformType)
        {
            var lastPlatformSR = GetComponent<SpriteRenderer>();
            switch (platformType)
            {
                case PlatformType.Steady:
                    infiniteJumps = true;
                    lastPlatformSR.color = Color.grey;
                    break;
                case PlatformType.OneJump:
                    infiniteJumps = false;
                    numJumps = 1;
                    lastPlatformSR.color = Color.yellow;
                    break;
                case PlatformType.TwoJumps:
                    infiniteJumps = false;
                    numJumps = 2;
                    lastPlatformSR.color = Color.blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            rb2d.isKinematic = true;
        }
    }
}
