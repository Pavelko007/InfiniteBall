using UnityEngine;
using InfiniteBall;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject ballPrefab;

	// Use this for initialization
	void Awake ()
	{
	    Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);

	    var platformSprite = platformPrefab.GetComponent<SpriteRenderer>().sprite;
	    float platformWidth = platformSprite.bounds.size.x;

	    var ballTransform = Instantiate(ballPrefab, new Vector3(platformWidth / 2, 5, 0), Quaternion.identity) as GameObject;

        Camera.main.GetComponent<Camera2DFollow>().target = ballTransform.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
