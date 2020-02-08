using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_TargetMove : MonoBehaviour
{
    Transform enemy;
    Transform turret; // 포탑
    const float RADAR_DIST = 100f;
    public Transform bullet; // 총알
    public Transform spPoint; //발사 시점
    public AudioClip ShotSound;
    bool canFire = true;
    private GameObject collEnemys;// = new List<GameObject>();
    private CapsuleCollider SpColl;
    private GameObject ArrowPoint;
    private AudioSource Sound;

    //초기화 

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("weaponTarget시작");
        //enemy = GameObject.FindWithTag("Enemy").transform;
        //Debug.Log("Enemy발견");
        turret = transform.Find("Tower02");
        Debug.Log("turret발견");
        SpColl = GetComponent<CapsuleCollider>();
        ArrowPoint = transform.GetChild(0).gameObject;
        Sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collEnemys != null)
        {
            ArrowPoint.transform.LookAt(collEnemys.transform);
            float dist = Vector3.Distance(collEnemys.transform.position, transform.position);

            if (dist <= RADAR_DIST && canFire)
            {
                Debug.Log("포탑 적인식&발사");
                canFire = false;
                Invoke("Disable", 1.6f);
            }
        }
        else
        {
            SpColl.enabled = true;
            canFire = true;
        }

    }

    void Disable()
    {
        canFire = true;
        Instantiate(bullet, spPoint.position, spPoint.rotation);
        Sound.PlayOneShot(ShotSound);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collEnemys = collision.gameObject;
            SpColl.enabled = false;
        }
    }
}

