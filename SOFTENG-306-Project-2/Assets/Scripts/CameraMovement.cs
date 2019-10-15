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
      public float zoomSpeed = 10f;
      public float nearZoomLimit = 2f;
      public float farZoomLimit = 16f;
      public float startingZoom = 5f;

       IZoomStrategy zoomStrategy;
       Vector3 frameMove;
       float frameRotate;
       float frameZoom;
       public Camera cam;

       /// <summary>
       /// Initial setup of the camera, this defines the scene the game starts on
       /// </summary>
      private void Awake()
      {
            cam = GetComponentInChildren<Camera>();
            cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));
            zoomStrategy = new OrthographicZoomStrategy(cam, startingZoom);
      }

       /// <summary>
       /// This defines the method that changes the movement of the camera depending on the key input
       /// </summary>
      private void OnEnable()
      {
            KeyboardInputManager.OnMoveInput += UpdateFrameMove;
            KeyboardInputManager.OnRotateInput += UpdateFrameRotate;
            KeyboardInputManager.OnZoomInput += UpdateFrameZoom;
      }

       /// <summary>
       /// This method disables the methods assigned to each input movement 
       /// </summary>
      private void OnDisable()
      {
            KeyboardInputManager.OnMoveInput -= UpdateFrameMove;
            KeyboardInputManager.OnRotateInput -= UpdateFrameRotate;
            KeyboardInputManager.OnZoomInput -= UpdateFrameZoom;
      }

       /// <summary>
       /// Method called when moving the camera
       /// </summary>
       private void UpdateFrameMove(Vector3 movevector)
      {
            frameMove += movevector;
      }

       /// <summary>
       /// Method called to rotate the camera
       /// </summary>
       private void UpdateFrameRotate(float rotateamount)
      {
            frameRotate += rotateamount;
      }

       /// <summary>
       /// Method called to rotate the camera
       /// </summary>
       /// <param name="zoomamount"></param>
      private void UpdateFrameZoom(float zoomamount)
      {
            frameZoom += zoomamount;
      }

       /// <summary>
       /// Updater method that runs every frame to check for keyboard input and call the right methods for camera movement
       /// </summary>
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

       /// <summary>
       /// Ensures that the camera does not move out of the bounds as defined by parameters within unity
       /// </summary>
      private void LockPositionInBounds()
      {
            transform.position = new Vector3(
                  Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
                  transform.position.y,
                  Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y));
      }
}