using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class OrthographicZoomStrategy : IZoomStrategy
{
    /// <summary>
    /// Constructor to set the initail size of the starting zoom
    /// </summary>

    public OrthographicZoomStrategy(Camera cam, float startingZoom)
    {
        cam.orthographicSize = startingZoom;
    }
    /// <summary>
    /// Method to resize the orthographicSize of the camera when zooming in
    /// </summary>
    public void ZoomIn(Camera cam, float delta, float nearZoomLimit)
    {
        if (cam.orthographicSize == nearZoomLimit) return;
        cam.orthographicSize = Mathf.Max(cam.orthographicSize - delta, nearZoomLimit);
        
        
    }

    /// <summary>
    /// Method to resize the orthographicSize of the camera when zooming out
    /// </summary>
    public void ZoomOut(Camera cam, float delta, float farZoomLimit)
    {
        if (cam.orthographicSize == farZoomLimit) return;
        cam.orthographicSize = Mathf.Min(cam.orthographicSize + delta, farZoomLimit);
        
    }
}
