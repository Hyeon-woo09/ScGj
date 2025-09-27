using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class EggProtect : MonoBehaviour
{
    public float playTime;
    public float enemySpawnTime;
    public float enemyMoveTime;

    public Scrollbar timeScroll;
    public GameObject enemyparent;
    public GameObject enemyPrefab;
    public Image completePlate;
    public Image failedPlate;
    public TMP_Text guideText;
    public Texture2D cursorImage;
    public Texture2D cursorImageClick;

    private float spawnTimer;
    private float timer = 0;
    private bool flag = true;
    private bool failed = false;

    private void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);

        guideText.gameObject.SetActive(true);
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

            if (Input.GetMouseButtonDown(0))
            {
                guideText.gameObject.SetActive(false);
                Cursor.SetCursor(cursorImageClick, Vector2.zero, CursorMode.Auto);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
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

        GameObject enemy = Instantiate(enemyPrefab, enemyparent.transform);
        enemy.GetComponent<RectTransform>().anchoredPosition = new Vector2(spawnPosx, spawnPosy);
        Vector2 newPos = Vector2.zero - enemy.GetComponent<RectTransform>().anchoredPosition;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
        StartCoroutine(enemy.GetComponent<EggProtect_Enemy>().EggTracking(enemyMoveTime, this));
    }

    public void Failed()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        failed = true;
        StopAllCoroutines();
        failedPlate.gameObject.SetActive(true);
    }

    void Complete()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Debug.Log("보상 지급");
        completePlate.gameObject.SetActive(true);
    }

    public void Return()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Destroy(gameObject);
    }
}
