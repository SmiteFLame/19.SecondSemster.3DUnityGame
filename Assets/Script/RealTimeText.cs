using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTimeText : MonoBehaviour
{
    Text TextHintUI;
    float timer;
    public static int min_timer;
    public static string OutText;
    string split_text;
    string zero_text;
    public static int Sum_time;
    public static bool gameover;
    // Start is called before the first frame update
    void Start()
    {
        TextHintUI = GetComponent<Text>();
        TextHintUI.text = "0";
        timer = 0.0f;
        min_timer = 0;
        split_text = ":";
        zero_text = "0";
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            timer += Time.deltaTime;
            int int_timer = (int)timer;
            Sum_time = (int)timer;
            if (int_timer >= 60)
            {
                timer = 0.0f;
                int_timer = 0;
                min_timer++;
            }
            if (int_timer < 10)
                OutText = min_timer.ToString() + split_text + zero_text + int_timer.ToString();
            else
                OutText = min_timer.ToString() + split_text + int_timer.ToString();
            TextHintUI.text = OutText;

        }
    }
}