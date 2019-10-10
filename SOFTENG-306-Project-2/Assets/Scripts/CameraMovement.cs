using UnityEngine;

public class CameraMovement : MonoBehaviour
{
      [Header("Camera Positioning")]
      public Vector2 cameraOffset = new Vector2(10f, 14f);

      public float lookAtOffset = 2f;

      [Header("Move Controls")] 
      public float inOutSpeed = 5f;
      public float lateralSpeed = 5f;
      public float rotateSpeed = 45f;

      [Header("Move Bounds")] 
      public Vector2 minBounds, maxBounds;

      [Header("Zoom Controls")] 
      public float zoomSpeed = 4f;
      public float nearZoomLimit = 2f;
      public float farZoomLimit = 16f;
      public float startingZoom = 5f;

      private IZoomStrategy zoomStrategy;
      private Vector3 frameMove;
      private float frameRotate;
      private float frameZoom;
      private Camera cam;

}
