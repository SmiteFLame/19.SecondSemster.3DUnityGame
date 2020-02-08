using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Tower : MonoBehaviour
{

    public GameObject bullet = null;
    private GameObject closeEnemy = null;
    Transform turret;
    private List<GameObject> collEnemys = new List<GameObject>();
    private float fTime = 0;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fTime += Time.deltaTime;
        if (collEnemys.Count > 0)
        {
            GameObject target = collEnemys[0];
            if (gameObject.tag == "Tower1")
            {
                if (target != null && fTime > 1.0f)
                {
                    fTime = 0.0f;
                    var aBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
                   
                    //aBullet.GetComponent<Lamp>().targetPosition = (target.transform.position - transform.position).normalized;
                    aBullet.transform.localScale = new Vector3(0.5f, 0.5f);
                }
            }
            else if (gameObject.tag == "Tower2")
            {
                if (target != null && fTime > 0.5f)
                {
                    fTime = 0.0f;
                    var aBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
                    Vector3 dir = (target.transform.position - transform.position).normalized;
                    float angle = Vector2.SignedAngle(Vector2.down, dir);
                    Quaternion qut = new Quaternion();
                    qut.eulerAngles = new Vector3(0, 0, angle);
                    aBullet.transform.rotation = qut;
                    aBullet.transform.position += dir * 1.0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            collEnemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject go in collEnemys)
        {
            if (go == collision.gameObject)
            {
                collEnemys.Remove(go);
                break;
            }
        }
    }
}

