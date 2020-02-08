using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class summon_weapon : MonoBehaviour
{
    public GameObject moneylittle;
    public Transform Target;
    bool isSearch;
    bool Activating_weapon_instance;
    bool ActivatingPlace1;
    bool ActivatingPlace2;
    bool ActivatingPlace3;
    bool[] ActivatingPlace = new bool[3];
    int weaponCount;
    //몬스터가 출현할 위치를 담을 배열
    public Transform[] points;
    //몬스터 프리팹을 할당할 변수
    public GameObject[] WeaponPrefab;
   
    // Start is called before the first frame update
    void Start()
    {
        isSearch = false;
        Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //ActivatingPlace.Add(ActivatingPlace1);
        //ActivatingPlace.Add(ActivatingPlace2);
        //ActivatingPlace.Add(ActivatingPlace3);
        ActivatingPlace[0] = true;
        ActivatingPlace[1] = true;
        ActivatingPlace[2] = true;
        weaponCount = 0;
        moneylittle.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       
      
        Search();
        if(Activating_weapon_instance)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                KeyDown_O();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                KeyDown_I();
            }

           
        }
    }

    void Search()
    {
        float distance = Vector3.Distance(Target.transform.position, transform.position);
        if (distance <= 3)
        {
            isSearch = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Activating_weapon_instance = true;
        }
       
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Activating_weapon_instance = false;
        }
    }



    //points[0].position
    private void KeyDown_I()
    {
        Debug.Log("I");
        StartCoroutine(InstantceWeapon(0));
        //Instantiate(WeaponPrefab[0], points[Random.Range(0, points.Length)].position, points[Random.Range(0, points.Length)].rotation);
    }
    private void KeyDown_O()
    {
        Debug.Log("O");
        StartCoroutine(InstantceWeapon(1));
       // Instantiate(WeaponPrefab[1], points[Random.Range(0, points.Length)].position, points[Random.Range(0, points.Length)].rotation);
    }

    public IEnumerator InstantceWeapon(int WeaponNum)
    {

        if (WeaponNum == 0)
        {
            if (ActivatingPlace[weaponCount])
            {
                if (Getmoney.money >= 200)
                {
                    Getmoney.money -= 200;
                    GameObject Temp = Instantiate(WeaponPrefab[WeaponNum], points[weaponCount].position, points[weaponCount].rotation);
                    //Temp.GetComponent<>().SetNumber();
                    ActivatingPlace[weaponCount] = false;
                    weaponCount++;
                }
                else
                {
                    StartCoroutine(Showmoneylittle());

                }
            }
            if (weaponCount >= 3)
            {
                weaponCount = 0;
            }
        }
       
        if (WeaponNum == 1)
        {
            if (ActivatingPlace[weaponCount])
            {
                if (Getmoney.money >= 100)
                {
                    Getmoney.money -= 100;
                    GameObject Temp = Instantiate(WeaponPrefab[WeaponNum], points[weaponCount].position, points[weaponCount].rotation);
                    //Temp.GetComponent<>().SetNumber();
                    ActivatingPlace[weaponCount] = false;
                    weaponCount++;
                }
                else
                {
                    StartCoroutine(Showmoneylittle());

                }

            }
            if (weaponCount >= 3)
            {
                weaponCount = 0;
            }
        }
        





        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Showmoneylittle()
    {
        moneylittle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        moneylittle.SetActive(false);
    }
}
