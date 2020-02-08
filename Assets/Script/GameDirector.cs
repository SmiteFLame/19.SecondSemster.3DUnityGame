using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameDirector : MonoBehaviour
{
    GameObject hpGage1;
    public GameObject GameOver;
    public GameObject retry;
    public GameObject rank;
    // Start is called before the first frame update
    void Start()
    {
        rank.SetActive(false);
        this.hpGage1 = GameObject.Find("Canvas").transform.Find("hpGage").transform.Find("Fill Area").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.hpGage1.GetComponent<Image>().fillAmount <= 0)
        {
            //Debug.Log("확인");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            GameOver.SetActive(true);
            retry.SetActive(true);
            GameObject.Find("Player").GetComponent<ES_CameraManager>().GameOver = true;
            RealTimeText.gameover = true;
            rank.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("Play_Main");
                RealTimeText.gameover = false;
            }



        }
    }

    public void IncreaseHp1()
    {
        Debug.Log("HP증가");

        StartCoroutine(WaitForUp());
    }



    public void DecreaseHp1()
    {
        Debug.Log("HP감소");

        StartCoroutine(WaitForDown());
    }
    public void DecreaseHpWeapon()
    {
        Debug.Log("Weapon HP감소");

        StartCoroutine(WaitForDown());
    }
    IEnumerator WaitForDown()
    {
        this.hpGage1.GetComponent<Image>().fillAmount -= 0.03f;
        yield return new WaitForSeconds(2.0f);

    }

    IEnumerator WaitForUp()
    {
        this.hpGage1.GetComponent<Image>().fillAmount += 0.2f;
        yield return new WaitForSeconds(1.0f);

    }




}