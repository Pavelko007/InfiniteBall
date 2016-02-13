using System;
using InfiniteBall.Extentions;
using InfiniteBall.Pooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InfiniteBall
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform platformPrefab;
        [SerializeField] private Transform ballPrefab;
        [SerializeField] private Transform coinPrefab;

        [SerializeField] private int maxNumCoins = 3;
        [SerializeField] private float platformHeightVariation = 2;

        private SpriteRenderer lastPlatformSR;
        private Sprite platformSprite;
        private float jumpHeight;

        // Use this for initialization
        void Start()
        {
            SpawnBall();
            SpawnPlatform(Vector3.zero, PlatformType.Steady);
            SpawnPlatforms();
        }

        // Update is called once per frame
        void Update()
        {
            SpawnPlatforms();
        }

        private void SpawnBall()
        {
            platformSprite = platformPrefab.GetComponent<SpriteRenderer>().sprite;
            float platformWidth = platformSprite.bounds.size.x;
            Transform ballTransform = Instantiate(ballPrefab, new Vector3(platformWidth / 2, 5, 0), Quaternion.identity) as Transform;
            jumpHeight = ballTransform.GetComponent<BallMovement>().JumpHeight;
            Camera.main.GetComponent<Camera2DFollow>().target = ballTransform;
        }

        private void SpawnPlatforms()
        {
            while (lastPlatformSR.IsVisibleFrom(Camera.main))
            {
                SpawnPlatform(GetRandomPlatformPos(), GetRandomPlatform());
            }
        }

        private Vector3 GetRandomPlatformPos()
        {
            return new Vector2(
                lastPlatformSR.transform.position.x + platformSprite.bounds.size.x * 1.5f,
                lastPlatformSR.transform.position.y + Random.Range(-platformHeightVariation, platformHeightVariation)
                );
        }

        private static PlatformType GetRandomPlatform()
        {
            return (PlatformType)Random.Range(0, Enum.GetValues(typeof(PlatformType)).Length);
        }

        private void SpawnPlatform(Vector3 nextPos, PlatformType platformType)
        {
            lastPlatformSR = GameObjectUtil.Instantiate(platformPrefab.gameObject, nextPos)
                .GetComponent<SpriteRenderer>();

            lastPlatformSR.GetComponent<FallingPlatform>().SetupPlatform(platformType);
        
            int numCoins = Random.Range(0, maxNumCoins + 1);
            while (numCoins-- > 0) SpawnCoin(lastPlatformSR);
        }

        private void SpawnCoin(SpriteRenderer platformSR)
        {
            GameObjectUtil.Instantiate(coinPrefab.gameObject, GetRandomCoinPos(platformSR));
        }

        private Vector2 GetRandomCoinPos(SpriteRenderer platformSR)
        {
            //TODO: verify that no coins overlap
            Bounds bounds = platformSR.bounds;
            return new Vector2(
                bounds.min.x + Random.Range(0, bounds.size.x),
                bounds.max.y + Random.Range(jumpHeight / 5, jumpHeight));
        }
    }
}