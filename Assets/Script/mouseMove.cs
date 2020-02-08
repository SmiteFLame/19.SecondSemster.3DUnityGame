using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMove : MonoBehaviour
{
    public string input;
    ES_CameraManager CM;
    string tmp;
    float A;
    public bool Check;
    public GameObject obj;
    public AudioClip DeathSound;
    // Start is called before the first frame update
    void Start()
    {
        A = 1.0f;
        Check = false;
        transform.position = new Vector3(0.15f, 2.65f - 100.0f, 15.07f);
        CM = GameObject.Find("Player").GetComponent<ES_CameraManager>();
        tmp = "";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CM.tmp);
        if (Input.GetButtonUp(input) && (CM.tmp == input || CM.tmp == ""))
        {
            if (Check == false && GameObject.Find("Player").GetComponent<ES_CameraManager>().Check == false)
            {
                Check = true;
                transform.position = new Vector3(0.15f, 2.65f, 15.07f);
            }
            else
            {
                Check = false;
                transform.position = new Vector3(0.15f, 2.65f - 100.0f, 15.07f);
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
        GetComponent<AudioSource>().PlayOneShot(DeathSound);
        Getmoney.money -= 20;
        Getmoney.setMoney();
        GameObject tmp = Instantiate(obj, transform.localPosition, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(tmp);
    }
}
