using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject minecartPrefab;
    [SerializeField]
    int poolSize;
    [SerializeField]
    float timeBetweenCarts;
    [SerializeField]
    float cooldown;
    List<GameObject> minecarts =  new List<GameObject>();
    bool spawningCarts = false;
    float timer = 0;
    void Start()
    {
        for(int i =0; i< poolSize; i++)
        {
            GameObject newCart = (GameObject)Instantiate(minecartPrefab, transform.position, Quaternion.identity);
            minecarts.Add(newCart);
            minecarts[i].SetActive(false);
            //minecarts[i].transform.position = transform.position;
        }
    }

    void Update()
    {
        if (timer > cooldown)
        {
            timer = 0;
            StartCoroutine("SpawnCarts");
        }
        if (!spawningCarts)
        {
            timer += Time.deltaTime;
        }

    }
    IEnumerator SpawnCarts()
    {
        spawningCarts = true;
        for (int i = 0; i< minecarts.Count; i++)
        {
            minecarts[i].SetActive(true);
            yield return new WaitForSeconds(timeBetweenCarts);
        }
        spawningCarts = false;
    }
}
