using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePeople : MonoBehaviour
{
    public string input;
    ES_CameraManager CM;
    string tmp;
    float A;
    bool Check;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        A = 1.0f;
        Check = false;
        transform.Translate(0, -100, 0);

        CM = GameObject.Find("Player").GetComponent<ES_CameraManager>();
        tmp = "";
    }

    // Update is called once per frame
    void Update()
    {
        tmp = CM.tmp;
        if (Input.GetButtonUp(input) && (tmp == "" || tmp == input))
        {
            if (Check == false)
            {
                Check = true;
                transform.Translate(0, 100, 0);
            }
            else
            {
                Check = false;
                transform.Translate(0, -100, 0);
            }
        }

        if (Check == true)
        {
            Cursor.visible = false;
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Make_Explosion());
            }
            transform.Translate(Input.GetAxis("Mouse X") * A, 0, Input.GetAxis("Mouse Y") * A);
        }
    }

    IEnumerator Make_Explosion()
    {
        GameObject tmp = Instantiate(obj, transform.localPosition, Quaternion.identity);

        //if (CM.nNowCam >= CM.nCamCount)
        //{
        //    CM.nNowCam = 0;
        //}
        //for (int i = 0; i < CM.arrCam.Length; ++i)
        //{
        //    CM.arrCam[i].enabled = (i == CM.nNowCam);
        //}
        yield return new WaitForSeconds(20.0f);
        Destroy(tmp);

    }
}
