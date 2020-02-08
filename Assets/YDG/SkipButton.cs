using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkipButton : MonoBehaviour
{
    public GameObject Text;
    public GameObject ShopText;
    public GameObject ShopImage;
    public loadingtext loading;
    private float timer;
    private bool Check = false;
    public GameObject warningText;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (GameMgr.stage % 3 == 0)
        {

            if (timer > 0.25f)
            {
                timer = 0.0f;
                Check = !Check;
            }
            if (loading.GetTime() < 0.7f)
            {
                Text.SetActive(Check);
                ShopText.SetActive(Check);
                ShopImage.SetActive(true);
                warningText.SetActive(Check);

            }
            else
            {
                Text.SetActive(false);
                ShopText.SetActive(false);
                ShopImage.SetActive(false);
                warningText.SetActive(false);
            }
        }

        else
        {

            if (timer > 0.25f)
            {
                timer = 0.0f;
                Check = !Check;
            }
            if (loading.GetTime() < 0.7f)
            {
                Text.SetActive(Check);
                ShopText.SetActive(Check);
                ShopImage.SetActive(true);

            }
            else
            {
                Text.SetActive(false);
                ShopText.SetActive(false);
                ShopImage.SetActive(false);
            }
        }
    }
}