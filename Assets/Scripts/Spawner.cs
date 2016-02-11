﻿using System.Collections.Generic;
using System.Linq;
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

    private List<Transform> platforms = new List<Transform>();

    // Use this for initialization
	void Awake ()
	{
	    SpawnBall();

	    rightmostPlatform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity) as Transform;
        platforms.Add(rightmostPlatform);
	    //Instantiate(coinTransform, 
     //       rightmostPlatform.GetComponent<SpriteRenderer>().bounds.max + Vector3.up * jumpHeight, 
     //       Quaternion.identity);

	    SpawnPlatforms();
	}

    // Update is called once per frame
    void Update ()
    {
        DeletePlatforms();
	    SpawnPlatforms();
	}

    private void DeletePlatforms()
    {
        var leftmostPlatform = platforms.Aggregate((c, d) => c.position.x < d.position.x ? c : d);
        SpriteRenderer leftmostPlatformSR = leftmostPlatform.GetComponent<SpriteRenderer>();
        if (!leftmostPlatformSR.IsVisibleFrom(Camera.main))
        {
            Destroy(leftmostPlatform.gameObject);
            platforms.Remove(leftmostPlatform);
        }
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
            platforms.Add(rightmostPlatform);
        }
    }
}
