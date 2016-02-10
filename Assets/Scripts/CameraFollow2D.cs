using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        transform.position = player.position + offset;
    }
}