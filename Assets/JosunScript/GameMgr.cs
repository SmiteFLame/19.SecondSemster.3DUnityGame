using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameMgr : MonoBehaviour
{
    private AudioSource musciPlayer;
    public AudioClip gamestart;
    //몬스터가 출현할 위치를 담을 배열
    public Transform[] points;
    //몬스터 프리팹을 할당할 변수
    public GameObject[] monsterPrefab;

    //몬스터를 발생시킬 주기
    public float createTime = 2;
    //몬스터의 최대 발생 개수
    private int maxMonster = 5;
    //게임 종료 여부 변수
    public bool isGameOver = false;

    public static int stage;
    bool stageStart = false;
    public Text stageText;
    bool restart = true;
    public bool reStartTrigger = false;
    public GameObject timeObject;
    public float Timer = 10.0f;
    public GameObject go;
    float checkTime = 1.0f;
    bool goDestroy = true;
    public static bool stage_clear;
    public loadingtext loadingTime;
    private float reStartTimer = 10.0f;
    private bool SoundCheck = false;
    public bool skiptime;
    // Use this for initialization
    void Start()
    {
        this.musciPlayer = this.gameObject.AddComponent<AudioSource>();
        //Hierarchy View의 Spawn Point를 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        loadingTime.Init(reStartTimer);
        stage = 1;
        stage_clear = false;
        stageText.text = "STAGE : " + stage.ToString();
        skiptime = false;

        monsterPrefab[0].GetComponent<EnemyHealth>().EnemyHp = 30;
        monsterPrefab[1].GetComponent<EnemyHealth>().EnemyHp = 30;
        monsterPrefab[2].GetComponent<EnemyHealth>().EnemyHp = 50;
        monsterPrefab[3].GetComponent<EnemyHealth>().EnemyHp = 200;
        if (points.Length > 0)
        {
            //몬스터 생성 코루틴 함수 호출

            StartCoroutine(WaitForIt());


        }

    }
    void reStart()
    {
        stage++;
        monsterPrefab[0].GetComponent<EnemyHealth>().EnemyHp += 10;
        monsterPrefab[1].GetComponent<EnemyHealth>().EnemyHp += 10;
        monsterPrefab[2].GetComponent<EnemyHealth>().EnemyHp += 20;
        monsterPrefab[3].GetComponent<EnemyHealth>().EnemyHp += 200;
        stageText.text = "STAGE : " + stage.ToString();
        stage_clear = true;
        Timer = reStartTimer;
        timeObject.SetActive(true);
        if (GameObject.Find("Player").GetComponent<ES_CameraManager>().Check == false)
        {
            GameObject.Find("Player").GetComponent<ES_CameraManager>().SetBool(true);
            GameObject.Find("Player").GetComponent<ES_CameraManager>().arrCam[0].enabled = true;
            GameObject.Find("Player").GetComponent<ES_CameraManager>().arrCam[1].enabled = false;
            GameObject.Find("Player").GetComponent<ES_CameraManager>().tmp = "";
            GameObject.Find("fx_line_rotate_dd").GetComponent<mouseMove>().Check = false;
            GameObject.Find("fx_line_rotate_dd").transform.position = new Vector3(0.15f, 2.65f - 100.0f, 15.07f);
        }
        StartCoroutine(WaitForIt());
        loadingTime.SetTime(0.0f);
    }


    IEnumerator CreateMonster()
    {
        //게임 종료 시까지 무한 루프
        while (!isGameOver)
        {

            //현재 생성된 몬스터 개수 산출

            //int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            for (int i = 0; i < maxMonster * stage; i = i + 5)
            // if (monsterCount < maxMonster * stage)
            {
                //몬스터의 생성 주기 시간만큼 대기

                //yield return new WaitForSeconds(2);
                if (i == 0 )
                {

                    yield return new WaitForSeconds(createTime);

                }
                else
                {
                    yield return new WaitForSeconds(1.2f);
                }
                //else if (i == 0 && skiptime)
                //{

                //    yield return new WaitForSeconds(3.2f);
                //    skiptime = false;
                //}

                //else
                //{
                //    yield return new WaitForSeconds(2);
                //}
                //불규칙적인 위치 산출
                if (stage % 3 == 0)
                {


                    Instantiate(monsterPrefab[Random.Range(0, 3)], points[1].position, points[1].rotation);
                    Instantiate(monsterPrefab[Random.Range(0, 3)], points[2].position, points[2].rotation);
                    Instantiate(monsterPrefab[Random.Range(0, 2)], points[3].position, points[3].rotation);
                    Instantiate(monsterPrefab[Random.Range(0, 2)], points[4].position, points[4].rotation);
                    Instantiate(monsterPrefab[3], points[0].position, points[0].rotation);
                    break;
                }
                else
                {
                    //몬스터의 동적 생성
                    Instantiate(monsterPrefab[Random.Range(0, 3)], points[0].position , points[0].rotation);
                    Instantiate(monsterPrefab[Random.Range(0, 3)], points[1].position , points[1].rotation);
                    Instantiate(monsterPrefab[Random.Range(0, 3)], points[2].position , points[2].rotation);
                    Instantiate(monsterPrefab[Random.Range(0, 2)], points[3].position , points[3].rotation);
                    Instantiate(monsterPrefab[Random.Range(0, 2)], points[4].position , points[4].rotation);
                    //Instantiate(monsterPrefab, points[3].position, points[3].rotation);
                }
            }
            // else
            //{
            reStartTrigger = true;
            isGameOver = true;
            yield return null;
            // }
            //StartCoroutine(WaitForIt3());

        }
    }
    void Update()
    {


        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            //timeText.text = Mathf.Ceil(Timer).ToString();
            if (Timer > 3.0f && Input.GetKeyDown(KeyCode.E))
            {
                Timer = 3.0f;
                loadingTime.SetTime(reStartTimer - 3.0f);
                skiptime = true;
            }
            SoundCheck = true;

        }
        else
        {
            if (SoundCheck)
            {
                this.musciPlayer.clip = this.gamestart;
                this.musciPlayer.Play();
                SoundCheck = false;
            }
            if (GameObject.Find("Player").GetComponent<ES_CameraManager>().Check == true)
            {
                GameObject.Find("Player").GetComponent<ES_CameraManager>().SetBool(false);
                GameObject.Find("Player").GetComponent<ES_CameraManager>().arrCam[0].enabled = true;
                GameObject.Find("Player").GetComponent<ES_CameraManager>().arrCam[1].enabled = false;
                GameObject.Find("Player").GetComponent<ES_CameraManager>().tmp = "";
                GameObject.Find("Tower02 (3)").GetComponent<makeZone>().Check = false;
                GameObject.Find("Tower02 (3)").transform.position = new Vector3(0.15f, 2.65f - 100.0f, 15.07f);
                GameObject.Find("Trap02").GetComponent<makeZone>().Check = false;
                GameObject.Find("Trap02").transform.position = new Vector3(0.15f, 2.65f - 100.0f, 15.07f);
            }
            timeObject.SetActive(false);

            if (goDestroy)
            {
                StartCoroutine(WaitForIt2());
            }
            //timeText.text = "START!";



        }



        if (stageStart)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            // 개수 파악하는 법 수정하기
            if (monsterCount == 0 && reStartTrigger)
            {
                reStartTrigger = false;
                isGameOver = true;
                reStart();
            }
        }
    }


    IEnumerator WaitForIt()
    {

        stageStart = false;
        isGameOver = false;
        stage_clear = false;
        StartCoroutine(this.CreateMonster());
        yield return new WaitForSeconds(reStartTimer);
        stageStart = true;
        yield return new WaitForSeconds(4.0f);
        reStartTrigger = true;
    }



    IEnumerator WaitForIt2()
    {

        go.SetActive(true);
        goDestroy = false;
        yield return new WaitForSeconds(1.0f);

        Debug.Log("대기");
        go.SetActive(false);
    }

    IEnumerator WaitForIt3()
    {

        yield return new WaitForSeconds(5.0f);
        isGameOver = true;

    }

}