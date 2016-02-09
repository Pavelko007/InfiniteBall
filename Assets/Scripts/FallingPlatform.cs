using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private float fallDelay;
    [SerializeField] private int numJumps;
    [SerializeField] private bool infiniteJumps = false;

	// Use this for initialization
	void Start ()
	{
	    rb2d = GetComponent<Rigidbody2D>();
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (infiniteJumps) return;

        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        yield return  new WaitForSeconds(fallDelay);
        rb2d.isKinematic = false;
    }
}
