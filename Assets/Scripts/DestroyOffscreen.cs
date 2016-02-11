using UnityEngine;
using InfiniteBall.Pooling;

public class DestroyOffscreen : MonoBehaviour
{
    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    private bool offscreen;

    /// <summary>
    /// distance in Viewport coordinates 
    /// when object is offscreen to this distance he will be destroyed
    /// </summary>
    private float destroyDistX = 1;

    // Update is called once per frame
    void Update()
    {
        offscreen = Camera.main.WorldToViewportPoint(transform.position).x < -destroyDistX;

        if (offscreen) OnOutOfBounds();
    }

    public void OnOutOfBounds()
    {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);

        if (DestroyCallback != null) DestroyCallback();
    }
}
