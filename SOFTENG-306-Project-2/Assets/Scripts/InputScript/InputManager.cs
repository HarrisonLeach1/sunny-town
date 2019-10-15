using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Derived from board to bits games unity channel
public class InputManager : MonoBehaviour
{
    
    public delegate void MoveInputHandler(Vector3 moveVector);

    public delegate void RotateInputHandler(float rotateAmount);

    public delegate void ZoomInputHandler(float zoomAmount);

}
