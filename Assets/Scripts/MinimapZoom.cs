using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapZoom : MonoBehaviour
{
    public static MinimapZoom instance;
    public float FieldOfView = 70f;
    public float zoomFieldOfView = 90f;
   
    new Camera camera;
    bool isZoomOut;
    bool isZoomIn;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        camera = gameObject.GetComponent<Camera>();
        camera.fieldOfView = FieldOfView;
    }


    public void ZoomOut() 
    {
        if (isZoomOut) return;

        isZoomOut = true;
        isZoomIn = false;
        StartCoroutine(ChangeFieldOfView(FieldOfView, zoomFieldOfView, 1f));
    }

    public void ZoomIn() 
    {
        if (isZoomIn) return;
        
        isZoomIn = true;
        isZoomOut = false;
        StartCoroutine(ChangeFieldOfView(zoomFieldOfView, FieldOfView, 1f));
    }

    IEnumerator ChangeFieldOfView(float start, float end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            camera.fieldOfView = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        camera.fieldOfView = end;
    }
}
