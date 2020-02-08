using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingtext : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;

    public float speed = 200f;
    public Text text;


    // Use this for initialization
    void Start () {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        int a = 0;
        if (imageComp.fillAmount != 1f)
        {
            imageComp.fillAmount = imageComp.fillAmount + Time.deltaTime * speed;
            a = (int)(imageComp.fillAmount * 100);
            text.text = (100 - a) / (int)(100.0f * speed) + 1 + "";
        }
    }
    public void Init(float _speed)
    {
        speed = 1.0f / _speed;
    }
    public void SetTime(float timer)
    {
        imageComp.fillAmount = timer / 10;
    }

    public float GetTime()
    {
        return imageComp.fillAmount;
    }
}
