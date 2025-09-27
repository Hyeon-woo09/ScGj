using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrabRace : MonoBehaviour
{
    public GameObject[] crabs;
    private float[] crabSpeeds;
    public TMP_Text countText;
    private int arriveCount;
    private int firstArrive;

    public Image completePlate;
    public Texture2D cursorImage;
    public Texture2D cursorImageClick;
    public TMP_Text guideText;

    public GameObject disableButton;
    private SkeletonGraphic checkSkeleton;
    private int checkCount = 0;

    private void Start()
    {
        crabSpeeds = new float[crabs.Length];
        for (int i = 0; i < crabs.Length; i++)
        {
            crabSpeeds[i] = i;
        }
        CrabInit();
        StartCoroutine(CountDown());
        guideText.gameObject.SetActive(true);
        completePlate.gameObject.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(arriveCount);
        if (arriveCount == crabs.Length)
        {
            guideText.text = "1등을 골라주세요.";
        }    
    }

    IEnumerator CountDown()
    {
        countText.text = "3";
        yield return new WaitForSeconds(1);
        countText.text = "2";
        yield return new WaitForSeconds(1);
        countText.text = "1";
        yield return new WaitForSeconds(1);
        countText.text = "Start!";
        CrabMove();
        yield return new WaitForSeconds(1);
        countText.text = "";
    }

    void CrabInit()
    {
        for (int i = 0; i < crabs.Length; i++)
        {
            int k = Random.Range(i, crabSpeeds.Length);
            float temp = crabSpeeds[i];
            crabSpeeds[i] = crabSpeeds[k];
            crabSpeeds[k] = temp;
            crabSpeeds[i] += 3;

            crabs[i].GetComponent<RectTransform>().localScale = Vector3.one * Random.Range(0.8f, 1f);
        }
    }

    void CrabMove()
    {
        for(int i = 0; i < crabs.Length; i++)
        {
            if(crabSpeeds[i] == 3)
            {
                firstArrive = i;
            }
            StartCoroutine(MoveCoroutine(crabs[i].GetComponent<RectTransform>(), crabSpeeds[i], crabs[i].GetComponent<RectTransform>().anchoredPosition));
        }
    }

    IEnumerator MoveCoroutine(RectTransform rt, float t,Vector2 startPos)
    {
        float mt = 0;
        while (mt < t)
        {
            mt += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(startPos, new Vector2(startPos.x + 900, startPos.y), mt / t);
            yield return null;
        }
        arriveCount += 1;
    }

    public void CrabClick(GameObject game)
    {
        if (arriveCount < crabs.Length)
        {
            return;
        }

        if (crabs[firstArrive] == game)
        {
            Complete();
        }
    }

    void Complete()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Debug.Log("보상 지급");
        guideText.gameObject.SetActive(false);
        disableButton.gameObject.SetActive(false);
        completePlate.gameObject.SetActive(true);
    }

    public void Return()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Destroy(gameObject);
    }
}
