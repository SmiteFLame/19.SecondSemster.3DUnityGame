using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDrop : MonoBehaviour
{
    public GameObject[] Item;

    void Start()
    {
        InvokeRepeating("Spawnitem", 30, 10); //1초에 1번씩 Spawnitem()를 호출한다.
    }

    void Spawnitem()
    {
        float randomX = Random.Range(-15f, 15f);
        float randomZ = Random.Range(4f, 20f);
        if (true)
        {
            Debug.Log("생성");
            int idx = Random.Range(0, 3);
            GameObject item = (GameObject)Instantiate(Item[idx], new Vector3(randomX, 3f, randomZ), Quaternion.identity);
        }
    }
}

