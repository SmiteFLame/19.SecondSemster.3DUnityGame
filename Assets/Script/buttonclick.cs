using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonclick : MonoBehaviour
{
    public Camera[] arrCam; //카메라 요소들을 추가한다.

    int nCamCount;

    int nNowCam;
    private void OnMouseDown()
    {
        ++nNowCam;
        // Mouse Lock

        Cursor.lockState = CursorLockMode.None;

        // Cursor visible

        Cursor.visible = true;

        if (nNowCam >= nCamCount)
        {
            nNowCam = 0;
        }
        for (int i = 0; i < arrCam.Length; ++i)
        {
            arrCam[i].enabled = (i == nNowCam);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Mouse Lock

        Cursor.lockState = CursorLockMode.None;

        // Cursor visible

        Cursor.visible = true;
        nNowCam = 0;
        nCamCount = 2;
        arrCam[0].enabled = true;
        arrCam[1].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Mouse Lock

        Cursor.lockState = CursorLockMode.None;

        // Cursor visible

        Cursor.visible = true;
    }
}

