using UnityEngine;
using InfiniteBall;
using InfiniteBall.Extentions;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform platformPrefab;
    [SerializeField] private Transform ballPrefab;
    private Transform lastPlatform;
    private Sprite platformSprite;

    // Use this for initialization
	void Awake ()
	{
	    SpawnBall();

	    lastPlatform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity) as Transform;

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

        Camera.main.GetComponent<Camera2DFollow>().target = ballTransform;
    }

    private void SpawnPlatforms()
    {
        while ( lastPlatform.GetComponent<SpriteRenderer>().IsVisibleFrom(Camera.main))
        {
            Vector3 nextPos = lastPlatform.position + Vector3.right * platformSprite.bounds.size.x * 1.5f;
            lastPlatform = Instantiate(platformPrefab, nextPos, Quaternion.identity) as Transform;
        }
    }
}
