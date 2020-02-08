using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_CameraManager : MonoBehaviour
{
    public Camera[] arrCam; //카메라 요소들을 추가한다.


    public string tmp;
    public bool Check = true;
    public bool Moding = false;
    public bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        arrCam[0].enabled = true;
        arrCam[1].enabled = false;
        tmp = "";
        Moding = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameOver)
        {
            if (arrCam[0].enabled == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        if (Check)
        {
            //if (Input.GetButtonUp("G") && (tmp == "" || tmp == "G"))
            //{
            //    if (tmp == "")
            //        tmp = "G";
            //    else
            //        tmp = "";

            //    Moding = !Moding;
            //    arrCam[0].enabled = !Moding;
            //    arrCam[1].enabled = Moding;

            //}
            if (Input.GetButtonUp("V") && (tmp == "" || tmp == "V"))
            {
                if (tmp == "")
                {
                    tmp = "V";
                    arrCam[0].enabled = false;
                    arrCam[1].enabled = true;
                }
                else
                {
                    tmp = "";
                    arrCam[0].enabled = true;
                    arrCam[1].enabled = false;
                }

                //Moding = !Moding;
                //arrCam[0].enabled = !Moding;
                //arrCam[1].enabled = Moding;
            }

            if (Input.GetButtonUp("B") && (tmp == "" || tmp == "B"))
            {
                if (tmp == "")
                {
                    tmp = "B";
                    arrCam[0].enabled = false;
                    arrCam[1].enabled = true;
                }
                else
                {
                    tmp = "";
                    arrCam[0].enabled = true;
                    arrCam[1].enabled = false;
                }

                //Moding = !Moding;
                //arrCam[0].enabled = !Moding;
                //arrCam[1].enabled = Moding;
            }
            //Debug.Log("A" + Moding);
        }
        else
        {
            if (Input.GetButtonUp("F") && (tmp == "" || tmp == "F"))
            {
                if (tmp == "")
                {
                    tmp = "F";
                    arrCam[0].enabled = false;
                    arrCam[1].enabled = true;
                }
                else
                {
                    tmp = "";
                    arrCam[0].enabled = true;
                    arrCam[1].enabled = false;
                }


            }
        }
    }

    public void SetBool(bool _Check)
    {
        Check = _Check;
    }
}
