using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
      [Header("Camera Positioning")] public Vector2 cameraOffset = new Vector2(0f, -111f);

      public float lookAtOffset = 2f;

      [Header("Move Controls")] public float inOutSpeed = 5f;
      public float lateralSpeed = 5f;
      public float rotateSpeed = 45f;

      [Header("Move Bounds")] 
      public Vector2 minBounds, maxBounds;

      [Header("Zoom Controls")] 
      public float zoomSpeed = 1000000000000f;
      public float nearZoomLimit = 2f;
      public float farZoomLimit = 16f;
      public float startingZoom = 5f;

       IZoomStrategy zoomStrategy;
       Vector3 frameMove;
       float frameRotate;
       float frameZoom;
       public Camera cam;

      private void Awake()
      {
            cam = GetComponentInChildren<Camera>();
            cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));
            zoomStrategy = new OrthographicZoomStrategy(cam, startingZoom);
            //cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);
      }

      private void OnEnable()
      {
            KeyboardInputManager.OnMoveInput += UpdateFrameMove;
            KeyboardInputManager.OnRotateInput += UpdateFrameRotate;
            KeyboardInputManager.OnZoomInput += UpdateFrameZoom;
      }

      private void OnDisable()
      {
            KeyboardInputManager.OnMoveInput -= UpdateFrameMove;
            KeyboardInputManager.OnRotateInput -= UpdateFrameRotate;
            KeyboardInputManager.OnZoomInput -= UpdateFrameZoom;
      }

      private void UpdateFrameMove(Vector3 movevector)
      {
            frameMove += movevector;
      }

      private void UpdateFrameRotate(float rotateamount)
      {
            frameRotate += rotateamount;
      }


      private void UpdateFrameZoom(float zoomamount)
      {
            frameZoom += zoomamount;
      }

      private void LateUpdate()
      {
            // Moving
            if (frameMove != Vector3.zero)
            {
                  Vector3 speedModFrameMove = new Vector3(frameMove.x * lateralSpeed, frameMove.y, frameMove.z *inOutSpeed);
                  transform.position += transform.TransformDirection(speedModFrameMove) * Time.deltaTime;
                  LockPositionInBounds();
                  frameMove = Vector3.zero;
            }
            // Rotating
            if (frameRotate != 0f)
            {
                  transform.Rotate(Vector3.up, frameRotate * Time.deltaTime * rotateSpeed);
                  frameRotate = 0f;
            }

            if (frameZoom < 0f)
            {
                  zoomStrategy.ZoomIn(cam, Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
                  frameZoom = 0f;
                  
            } 
            else if (frameZoom > 0f)
            {
                  zoomStrategy.ZoomOut(cam, Time.deltaTime * frameZoom  * zoomSpeed, farZoomLimit);
                  frameZoom = 0f;
            }
      }

      private void LockPositionInBounds()
      {
            transform.position = new Vector3(
                  Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
                  transform.position.y,
                  Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y));
      }
}