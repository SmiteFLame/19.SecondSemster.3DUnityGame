using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Getmoney : MonoBehaviour
{
    public static int money;
    public static Text moneyText;
    // Start is clled before the first frame update
    void Start()
    {
        moneyText = GetComponent<Text>();
        moneyText.text = "0";
        money = 0;
    }

    // Update is called once per frame
    public void getMoney()
    {
        money += Random.Range(10,50);
        moneyText.text = money.ToString();
    }

    public static void setMoney()
    {
        moneyText.text = money.ToString();
    }
}
