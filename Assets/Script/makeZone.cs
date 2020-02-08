using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class makeZone : MonoBehaviour
{
    public string input;
    ES_CameraManager CM;
    string tmp;
    float A;
    public bool Check;
    int mon;
    public GameObject obj;
    public AudioClip DeathSound;
    public GameObject moneylittle;
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
        tmp = CM.tmp;
        if (Input.GetButtonUp(input) && (tmp == "" || tmp == input))
        {
            if (Check == false && GameObject.Find("Player").GetComponent<ES_CameraManager>().Check == true)
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
        if (input == "V" && Getmoney.money >= 200)
        {
            Getmoney.money -= 200;
            GameObject tmp = Instantiate(obj, transform.localPosition, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(DeathSound);
        }
        else if(input == "V" && Getmoney.money < 200)
        {
            StartCoroutine(Showmoneylittle());
        }
        if (input == "B" && Getmoney.money >= 100)
        {
            Getmoney.money -= 100;
            GameObject tmp = Instantiate(obj, transform.localPosition, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(DeathSound);
        }
        else if(input == "B" && Getmoney.money < 100)
        {
            StartCoroutine(Showmoneylittle());
        }
        Getmoney.setMoney();
        yield return null;
    }

    IEnumerator Showmoneylittle()
    {
        moneylittle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        moneylittle.SetActive(false);
    }
}
