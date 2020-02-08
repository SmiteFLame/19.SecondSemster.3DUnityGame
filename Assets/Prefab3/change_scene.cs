using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    //public static int characternumber;
    public void Change_Character_Select()
    {
        SceneManager.LoadScene("Character_Select");
    }
    public void Change_Play_Main_Warrior()
    {
        //characternumber = 0;
        SceneManager.LoadScene("Play_Main");
    }
    public void Change_Play_Main_Archer()
    {
        //characternumber = 1;
        SceneManager.LoadScene("Play_Main");
    }

    public void Title()
    {
        //characternumber = 1;
        SceneManager.LoadScene("Title2");
    }

}
