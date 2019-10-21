using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Derived from board to bits games unity channel
/// <summary>
/// The InputManager script is a superclass with method stubs for controlling the movement of the camera
/// </summary>
public class InputManager : MonoBehaviour
{
    
    public delegate void MoveInputHandler(Vector3 moveVector);

    public delegate void RotateInputHandler(float rotateAmount);

    public delegate void ZoomInputHandler(float zoomAmount);

}
