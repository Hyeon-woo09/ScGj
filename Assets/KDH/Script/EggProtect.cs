using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggProtect : MonoBehaviour
{
    public float playTime;
    public float enemySpawnTime;
    public float enemyMoveTime;

    public Scrollbar timeScroll;
    public GameObject enemyparent;
    public Image enemyPrefab;
    public Image completePlate;
    public Image failedPlate;

    private float spawnTimer;
    private float timer = 0;
    private bool flag = true;
    private bool failed = false;

    private void Start()
    {
        completePlate.gameObject.SetActive(false);
        failedPlate.gameObject.SetActive(false);
        spawnTimer = enemySpawnTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < playTime && !failed)
        {
            timeScroll.size = timer / playTime;

            if (timer >= spawnTimer)
            {
                SpawnEnemy();
                spawnTimer += enemySpawnTime;
            }
        }
        else
        {
            if (flag && !failed)
            {
                StopAllCoroutines();
                Complete();
                flag = false;
            }
        }
    }

    void SpawnEnemy()
    {
        int spawnDir = Random.Range(0, 3);
        float spawnPosx = 0;
        float spawnPosy = 0;
        if (spawnDir == 0)
        {
            spawnPosx = Random.Range(-700f, 700f);
            spawnPosy = -350;
        }
        else if (spawnDir == 1)
        {
            spawnPosx = 700;
            spawnPosy = Random.Range(-350f, 100f);
        }
        else if(spawnDir == 2)
        {
            spawnPosx = -700;
            spawnPosy = Random.Range(-350f, 100f);
        }

        Image enemy = Instantiate(enemyPrefab, enemyparent.transform);
        enemy.GetComponent<RectTransform>().anchoredPosition = new Vector2(spawnPosx, spawnPosy);
        StartCoroutine(enemy.GetComponent<EggProtect_Enemy>().EggTracking(enemyMoveTime, this));
    }

    public void Failed()
    {
        failed = true;
        StopAllCoroutines();
        failedPlate.gameObject.SetActive(true);
    }

    void Complete()
    {
        Debug.Log("보상 지급");
        completePlate.gameObject.SetActive(true);
    }

    public void Return()
    {
        Destroy(gameObject);
    }
}
