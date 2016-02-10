using UnityEngine;
using InfiniteBall;
using InfiniteBall.Extentions;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform platformPrefab;
    [SerializeField] private Transform ballPrefab;
    [SerializeField] private Transform coinTransform;

    private Transform rightmostPlatform;
    private Sprite platformSprite;
    private float jumpHeight;

    // Use this for initialization
	void Awake ()
	{
	    SpawnBall();

	    rightmostPlatform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity) as Transform;
        
	    //Instantiate(coinTransform, 
     //       rightmostPlatform.GetComponent<SpriteRenderer>().bounds.max + Vector3.up * jumpHeight, 
     //       Quaternion.identity);

	    SpawnPlatforms();
	}

    // Update is called once per frame
    void Update () {
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
        while ( rightmostPlatform.GetComponent<SpriteRenderer>().IsVisibleFrom(Camera.main))
        {
            Vector3 nextPos = rightmostPlatform.position + Vector3.right * platformSprite.bounds.size.x * 1.5f;
            rightmostPlatform = Instantiate(platformPrefab, nextPos, Quaternion.identity) as Transform;
        }
    }
}
