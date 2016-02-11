using UnityEngine;
using InfiniteBall;
using InfiniteBall.Extentions;
using InfiniteBall.Pooling;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform platformPrefab;
    [SerializeField] private Transform ballPrefab;
    [SerializeField] private Transform coinTransform;

    private SpriteRenderer rightmostPlatformSR;
    private Sprite platformSprite;
    private float jumpHeight;


    // Use this for initialization
	void Awake ()
	{
	    SpawnBall();

	    SpawnPlatform(Vector3.zero);

	    SpawnPlatforms();
	}

    // Update is called once per frame
    void Update ()
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
        while (rightmostPlatformSR.IsVisibleFrom(Camera.main))
        {
            Vector3 nextPos = rightmostPlatformSR.transform.position + Vector3.right * platformSprite.bounds.size.x * 1.5f;

            SpawnPlatform(nextPos);
        }
    }

    private void SpawnPlatform(Vector3 nextPos)
    {
        rightmostPlatformSR = GameObjectUtil.Instantiate(platformPrefab.gameObject, nextPos)
            .GetComponent<SpriteRenderer>();

        GameObjectUtil.Instantiate(coinTransform.gameObject,
               rightmostPlatformSR.bounds.center + Vector3.up * jumpHeight);
    }
}
