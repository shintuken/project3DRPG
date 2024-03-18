using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRefresh : MonoBehaviour
{
    public Vector3 startingPosition; // Vị trí ban đầu của camera

    private void Awake()
    {
       // ResetCameraPosition();
    }

    public void ResetCameraPosition()
    {
        transform.position = startingPosition; // Đặt lại vị trí của camera về vị trí ban đầu
    }
}
