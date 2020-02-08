using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;     //C#의 데이터 테이블 때문에 사용
using MySql.Data;     //MYSQL함수들을 불러오기 위해서 사용

using MySql.Data.MySqlClient;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Rank2 : MonoBehaviour
{
    public Text minText1;
    public Text secText1;

    public Text minText2;
    public Text secText2;
    public Text minText3;
    public Text secText3;
    public Text minText4;
    public Text secText4;
    public Text minText5;
    public Text secText5;
    public Text minTextmy;
    public Text secTextmy;


    MySqlConnection sqlconn = null;
    MySqlDataReader read1;
    MySqlDataReader read2;
    MySqlDataReader read3;
    MySqlDataReader read4;
    MySqlDataReader read5;
    MySqlCommand cmd;
    private string sqlDBip = "211.254.214.211";
    private string sqlDBname = "sonic747";
    private string sqlDBid = "sonic747";
    private string sqlDBpw = "@@nani3005";
    private string sqlDBport = "3314";
    public static string pass = "";
    public static string id = "";
    string SELECTID = "select USER_ID from game";
    string SELECTSCORE = "select SCORE from game";
    string SELECTDATE = "select DATE from game";
    string Commandselect = "select * from game";
    string SELECTPASSWORD = "select password from game";

    public GameObject canvas;
    public int score_1 = 0;
    public int score_2 = 0;
    public int score_3 = 0;
    public int score_4 = 0;
    public int score_5 = 0;
    public int time_1 = 10000;
    public int time_2 = 10000;
    public int time_3 = 10000;
    public int time_4 = 10000;
    public int time_5 = 10000;

    public static Text myscore;
    public GameObject my_score;
    public GameObject myscore_object;
    public GameObject goldmedal;
    public GameObject sivermedal;
    public GameObject dongmedal;
    public GameObject fourthmedal;
    public GameObject fifthmedal;
    public GameObject ui;
    public static Text goldscore;
    public static Text twoscore;
    public static Text threescore;
    public static Text goldid;
    public static Text twoid;
    public static Text threeid;
    public static Text fourthscore;
    public static Text fifthscore;
    public static Text fourthid;
    public static Text fifthid;


    public GameObject fourth_score_object;
    public GameObject fifth_score_object;
    public GameObject goldscore_object;
    public GameObject twoscore_object;
    public GameObject threescore_object;
    public GameObject fourthid_object;
    public GameObject fifthid_object;
    public GameObject goldid_object;
    public GameObject twoid_object;
    public GameObject threeid_object;
    public string goldnametemp = "";
    public string silvernametemp = "";
    public string metalnametemp = "";
    public string fourthnametemp = "";
    public string fifthnametemp = "";

    public static int scoremy;
    public static int temp = 0;
    int tmp;
    [HideInInspector]
    public DataSet CurrentViewingDataset;
    //string Id = null;

    //float[] ComparisonScore = CurrentViewingDataset.CompareAll();

    public void Start()
    {
        myscore = myscore_object.GetComponent<Text>();
        goldscore = goldscore_object.GetComponent<Text>();
        twoscore = twoscore_object.GetComponent<Text>();
        threescore = threescore_object.GetComponent<Text>();
        fourthscore = fourth_score_object.GetComponent<Text>();
        fifthscore = fifth_score_object.GetComponent<Text>();

        goldid = goldid_object.GetComponent<Text>();
        twoid = twoid_object.GetComponent<Text>();
        threeid = threeid_object.GetComponent<Text>();
        fourthid = fourthid_object.GetComponent<Text>();
        fifthid = fifthid_object.GetComponent<Text>();

    }





    public void Sqlread_rank()
    {
        string sqlDatabase = "Server=" + sqlDBip + ";port=" + sqlDBport + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";

        sqlconn = new MySqlConnection(sqlDatabase);
        Debug.Log(sqlconn);
        cmd = new MySqlCommand(Commandselect, sqlconn);
        sqlconn.Open();
        read1 = cmd.ExecuteReader();
        while (read1.Read())
        {


            if (score_1 < Convert.ToInt32(read1["stage"]))
            {
                score_1 = Convert.ToInt32(read1["stage"]);
                Debug.Log("1등: " + score_1);
                goldnametemp = read1["id"].ToString();
                goldid.text = read1["id"].ToString();
                goldscore.text = read1["stage"].ToString();
                tmp = Convert.ToInt32(read1["time"]) / 60;
                minText1.text = tmp.ToString() + ":";
                tmp = Convert.ToInt32(read1["time"]) - tmp * 60;
                secText1.text = tmp.ToString();

            }

            if (read1["id"].ToString() == Iogin.id)
            {
                myscore.text = read1["stage"].ToString();
                scoremy = Convert.ToInt32(read1["stage"]);
                tmp = Convert.ToInt32(read1["time"]) / 60;
                minTextmy.text = tmp.ToString() + ":";
                tmp = Convert.ToInt32(read1["time"]) - tmp * 60;
                secTextmy.text = tmp.ToString();
            }
            temp++;
        }
        sqlconn.Close();



        sqlconn.Open();
        read2 = cmd.ExecuteReader();
        while (read2.Read())
        {


            if (score_2 < Convert.ToInt32(read2["stage"]) && Convert.ToInt32(read2["stage"]) <= score_1 && goldnametemp != read2["id"].ToString())
            {
                score_2 = Convert.ToInt32(read2["stage"]);
                Debug.Log("2등: " + score_2);
                silvernametemp = read2["id"].ToString();
                twoid.text = read2["id"].ToString();
                twoscore.text = read2["stage"].ToString();
                tmp = Convert.ToInt32(read2["time"]) / 60;
                minText2.text = tmp.ToString() + ":";
                tmp = Convert.ToInt32(read2["time"]) - tmp * 60;
                secText2.text = tmp.ToString();
            }


        }
        sqlconn.Close();

        sqlconn.Open();
        read3 = cmd.ExecuteReader();
        while (read3.Read())
        {


            if (score_3 < Convert.ToInt32(read3["stage"]) && Convert.ToInt32(read3["stage"]) <= score_2 && silvernametemp != read3["id"].ToString() && goldnametemp != read3["id"].ToString())
            {
                score_3 = Convert.ToInt32(read3["stage"]);
                Debug.Log("3등: " + score_3);
                metalnametemp = read3["id"].ToString();
                threeid.text = read3["id"].ToString();
                threescore.text = read3["stage"].ToString();
                tmp = Convert.ToInt32(read3["time"]) / 60;
                minText3.text = tmp.ToString() + ":";
                tmp = Convert.ToInt32(read3["time"]) - tmp * 60;
                secText3.text = tmp.ToString();
            }


        }

        sqlconn.Close();

        sqlconn.Open();
        read4 = cmd.ExecuteReader();
        while (read4.Read())
        {


            if (score_4 < Convert.ToInt32(read4["stage"]) && Convert.ToInt32(read4["stage"]) <= score_3 && metalnametemp != read4["id"].ToString() && silvernametemp != read4["id"].ToString() && goldnametemp != read4["id"].ToString())
            {
                score_4 = Convert.ToInt32(read4["stage"]);
                Debug.Log("4등: " + score_4);
                fourthnametemp = read4["id"].ToString();
                fourthid.text = read4["id"].ToString();
                fourthscore.text = read4["stage"].ToString();
                tmp = Convert.ToInt32(read4["time"]) / 60;
                minText4.text = tmp.ToString() + ":";
                tmp = Convert.ToInt32(read4["time"]) - tmp * 60;
                secText4.text = tmp.ToString();
            }


        }

        sqlconn.Close();

        sqlconn.Open();
        read5 = cmd.ExecuteReader();
        while (read5.Read())
        {


            if (score_5 < Convert.ToInt32(read5["stage"]) && Convert.ToInt32(read5["stage"]) <= score_4 && fourthnametemp != read5["id"].ToString() && metalnametemp != read5["id"].ToString() && silvernametemp != read5["id"].ToString() && goldnametemp != read5["id"].ToString())
            {
                score_5 = Convert.ToInt32(read5["stage"]);
                Debug.Log("4등: " + score_5);
                fifthnametemp = read5["id"].ToString();
                fifthid.text = read5["id"].ToString();
                fifthscore.text = read5["stage"].ToString();
                tmp = Convert.ToInt32(read5["time"]) / 60;
                minText5.text = tmp.ToString() + ":";
                tmp = Convert.ToInt32(read5["time"]) - tmp * 60;
                secText5.text = tmp.ToString();
            }


        }

        sqlconn.Close();


        clickscore();



    }
    public void clickscore()
    {
        canvas.gameObject.SetActive(true);
        ui.gameObject.SetActive(false);
    }




}