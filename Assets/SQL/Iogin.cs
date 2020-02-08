using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;     //C#의 데이터 테이블 때문에 사용
using MySql.Data;     //MYSQL함수들을 불러오기 위해서 사용

using MySql.Data.MySqlClient;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Iogin : MonoBehaviour
{
    //public GameObject notcorrectpass;
    public GameObject okbtu;
    public GameObject loginPanel;
    public GameObject createaccountPanel;
    public GameObject warning;
    [Header("loginPanel")]
    public InputField IdInputField;
    //public InputField PasswordInputField;
    [Header("createaccountPanel")]
    public InputField NEW_InputField;
    public InputField NEW_PasswordInputField;


    MySqlConnection sqlconn = null;
    MySqlDataReader read1;
    MySqlDataReader read2;
    MySqlDataReader read3;
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
    public static Text myscore;
    public GameObject my_score;
    public GameObject myscore_object;
    public GameObject goldmedal;
    public GameObject sivermedal;
    public GameObject dongmedal;
    public GameObject ranking;

    public static Text goldscore;
    public static Text twoscore;
    public static Text threescore;
    public static Text goldid;
    public static Text twoid;
    public static Text threeid;

    public GameObject goldscore_object;
    public GameObject twoscore_object;
    public GameObject threescore_object;
    public GameObject goldid_object;
    public GameObject twoid_object;
    public GameObject threeid_object;
    public string goldnametemp = "";
    public string silvernametemp = "";
    public string metalnametemp = "";
    public static int scoremy;
    public static int temp = 0;

    [HideInInspector]
    public DataSet CurrentViewingDataset;
    //string Id = null;

    //float[] ComparisonScore = CurrentViewingDataset.CompareAll();

    public void Start()
    {
        // notcorrectpass.gameObject.SetActive(false);
    }

    public void LoginBtn()
    {

        StartCoroutine(LoginCo());

    }



    IEnumerator LoginCo()
    {
        Debug.Log(IdInputField.text);
        //Debug.Log(PasswordInputField.text);

        yield return null;
    }

    public void CeateAccountBtn()
    {
        loginPanel.gameObject.SetActive(false);
        createaccountPanel.gameObject.SetActive(true);
    }

    public void Finishcreate()
    {
        loginPanel.gameObject.SetActive(true);
        createaccountPanel.gameObject.SetActive(false);
    }

    public void SearchID()
    {


        //a=SELECT*from'rowingTB'GROUP BY 
        /* if(SELECTID == IdInputField.text)
         {
             if (SELECTPASSWORD == PasswordInputField.text)
             {
                 warning.gameObject.SetActive(false);
             }


         }*/

    }


    //db 연결
    public void sqlConnect()
    {
        Debug.Log("sqlConnect");
        if (IdInputField.text != "" /*&& PasswordInputField.text != ""*/)
        {
            Debug.Log("sql연결");

            //DB정보 입력
            string sqlDatabase = "Server=" + sqlDBip + ";port=" + sqlDBport + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";

            //string Comman3 = "select * from rowingTB";
            //string CommandText = "insert into rowingTB VALUES('IdInputField.text', 'PasswordInputField.qtext', 3, '1000-01-01' ,3)";
            string CommandText = "insert into game VALUES('" + IdInputField.text + "'," + GameMgr.stage + "," + RealTimeText.Sum_time + "); ";
            //  string CommandText = "insert into tenn VALUES('" + IdInputField.text + "'," + "'" + PasswordInputField.text + "',0)";

            Debug.Log(CommandText);
            //접속 확인하기
            try
            {
                sqlconn = new MySqlConnection(sqlDatabase);
                cmd = new MySqlCommand(CommandText, sqlconn);


                sqlconn.Open();
                Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 되면 OPEN이라고 나타남


                cmd.ExecuteNonQuery();
                sqlconn.Close();
                //sqlcmdall("DELETE FROM mongi");
                //SceneManager.LoadScene("Title");
            }
            catch (Exception msg)
            {
                Debug.Log(msg); //기타다른오류가 나타나면 오류에 대한 내용이 나타남
            }


        }

    }

    public void Sqlread()
    {
        string sqlDatabase = "Server=" + sqlDBip + ";port=" + sqlDBport + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";

        sqlconn = new MySqlConnection(sqlDatabase);
        cmd = new MySqlCommand(Commandselect, sqlconn);
        sqlconn.Open();
        read1 = cmd.ExecuteReader();
        Debug.Log("sqlread");

        if (id == "" && pass == "")
        {
            Debug.Log("id넣음");
            id = IdInputField.text;
            //Debug.Log(IdInputField.text);
            Debug.Log(id);
            // pass = PasswordInputField.text;
            sqlConnect();
        }

        sqlconn.Close();

    }





    public void sqlchoiceid()
    {
        string sqlDatabase = "Server=" + sqlDBip + ";port=" + sqlDBport + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";
        string commandchoicname = "select * from game where id = " + "'" + "sdsd" + "'";

        sqlconn = new MySqlConnection(sqlDatabase);
        cmd = new MySqlCommand(commandchoicname, sqlconn);
        sqlconn.Open();
        read1 = cmd.ExecuteReader();

        while (read1.Read())
        {

            Debug.Log("아이디:{0}" + read1["id"]);
            Debug.Log("날짜:{0}" + read1["password"]);
            Debug.Log("점수:{0}" + read1["score"]);

        }
        sqlconn.Close();
    }

    private void sqldisConnect()
    {
        sqlconn.Close();
        Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 끊기면 Close가 나타남 
    }


    public void sqlcmdall(string allcmd) //함수를 불러올때 명령어에 대한 String을 인자로 받아옴
    {
        //qlConnect(); //접속


        MySqlCommand dbcmd = new MySqlCommand(allcmd, sqlconn); //명령어를 커맨드에 입력
        dbcmd.ExecuteNonQuery(); //명령어를 SQL에 보냄

        sqldisConnect(); //접속해제
    }



    public void okbtu1()
    {
        okbtu.gameObject.SetActive(false);
        //    notcorrectpass.gameObject.SetActive(false);

        loginPanel.gameObject.SetActive(true);
        //IdInputField.text = "";
        //PasswordInputField.text = "";
    }


    public void updatedata()
    {
        //float[] ComparisonScore = CurrentViewingDataset.CompareAll();
        float Score = GameMgr.stage;
        string sqlDatabase = "Server=" + sqlDBip + ";port=" + sqlDBport + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";
        string CommandText = "UPDATE game SET score = " + Score + " WHERE id = = " + "'" + "id" + "'";
        sqlconn = new MySqlConnection(sqlDatabase);
        cmd = new MySqlCommand(CommandText, sqlconn);
        sqlconn.Open();
        cmd.ExecuteNonQuery();
        sqlconn.Close();
    }

    public void Sqlread_rank()
    {
        string sqlDatabase = "Server=" + sqlDBip + ";port=" + sqlDBport + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";

        sqlconn = new MySqlConnection(sqlDatabase);
        cmd = new MySqlCommand(Commandselect, sqlconn);
        sqlconn.Open();
        read1 = cmd.ExecuteReader();
        while (read1.Read())
        {


            if (score_1 < Convert.ToInt32(read1["score"]))
            {
                score_1 = Convert.ToInt32(read1["score"]);
                Debug.Log("1등: " + score_1);
                goldnametemp = read1["id"].ToString();
                goldid.text = read1["id"].ToString();
                goldscore.text = read1["score"].ToString();

            }


            //Debug.Log("도서제목:{0}" + read1["USER_ID"]);
            //Debug.Log("도서제목:{0}" + read1["DATE"]);
            //Debug.Log("도서제목:{0}" + read1["SCORE"]);
            //Debug.Log("도서제목:{0}" + read1["password"]);
            if (read1["id"].ToString() == Iogin.id)
            {
                myscore.text = read1["score"].ToString();
                scoremy = Convert.ToInt32(read1["score"]);
            }
            temp++;
        }
        sqlconn.Close();

        sqlconn.Open();
        read2 = cmd.ExecuteReader();
        while (read2.Read())
        {


            if (score_2 < Convert.ToInt32(read2["score"]) && Convert.ToInt32(read2["score"]) <= score_1 && goldnametemp != read2["id"].ToString())
            {
                score_2 = Convert.ToInt32(read2["score"]);
                Debug.Log("2등: " + score_2);
                silvernametemp = read2["id"].ToString();
                twoid.text = read2["id"].ToString();
                twoscore.text = read2["score"].ToString();
            }


        }
        sqlconn.Close();

        sqlconn.Open();
        read3 = cmd.ExecuteReader();
        while (read3.Read())
        {


            if (score_3 < Convert.ToInt32(read3["score"]) && Convert.ToInt32(read3["score"]) <= score_2 && silvernametemp != read3["id"].ToString() && goldnametemp != read3["id"].ToString())
            {
                score_3 = Convert.ToInt32(read3["score"]);
                Debug.Log("3등: " + score_3);
                metalnametemp = read3["id"].ToString();
                threeid.text = read3["id"].ToString();
                threescore.text = read3["score"].ToString();
            }


        }

        sqlconn.Close();
        clickscore();



    }
    public void clickscore()
    {
        canvas.gameObject.SetActive(true);
    }




}
