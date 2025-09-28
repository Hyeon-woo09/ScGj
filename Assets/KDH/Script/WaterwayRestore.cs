using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using Scenes.Common.Scenes.Common;

public class WaterwayRestore : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject dragEffectPrefab;
    public GameObject guideLine;
    public Image completePlate;
    public GameObject pos;
    public GameObject reverEfect;

    public List<RectTransform> allNodes;

    public GameObject disableButton;
    private RectTransform canvasRectTransform;
    private bool isDrawing = false;
    private Vector2 startPos;
    private RectTransform startNode;
    private GameObject currentLine;
    private GameObject currentDragEffect;
    public Texture2D cursorImage;
    private bool end = false;

    public GameObject guideText;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        graphicRaycaster = canvasRectTransform.GetComponentInParent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(EventSystem.current);
        guideText.gameObject.SetActive(true);
        reverEfect.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !end)
        {
            startNode = GetNodeUnderMouse();
            if (startNode != null)
            {
                StartDrawingLine(startNode);
            }
        }

        if (isDrawing)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform, Input.mousePosition, null, out Vector2 mousePos);

            DrawUILine(startPos, mousePos, currentLine);

            if (currentDragEffect != null)
            {
                currentDragEffect.GetComponent<RectTransform>().anchoredPosition = mousePos;
            }
        }

        if (Input.GetMouseButtonUp(0) && !end)
        {
            if (isDrawing)
            {
                RectTransform endNode = GetNodeUnderMouse();

                if (endNode != null && endNode != startNode)
                {
                    StopDrawingLine(endNode, true);
                }
                else
                {
                    StopDrawingLine(null, false);
                }
            }
        }
    }

    private RectTransform GetNodeUnderMouse()
    {
        pointerEventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        foreach (var result in results)
        {
            if (allNodes.Contains(result.gameObject.GetComponent<RectTransform>()))
            {
                return result.gameObject.GetComponent<RectTransform>();
            }
        }
        return null;
    }

    void StartDrawingLine(RectTransform startNodeRect)
    {
        guideText.gameObject.SetActive(false);
        guideLine.SetActive(false);
        isDrawing = true;
        startNode = startNodeRect;
        startPos = startNodeRect.anchoredPosition;

        currentLine = Instantiate(linePrefab, pos.transform);
        currentLine.GetComponent<Image>().raycastTarget = false;
        currentLine.SetActive(true);

        if (dragEffectPrefab != null)
        {
            currentDragEffect = Instantiate(dragEffectPrefab, canvasRectTransform);
        }
    }

    void StopDrawingLine(RectTransform endNodeRect, bool isSuccess)
    {
        if (isSuccess)
        {
            Vector2 endPos = endNodeRect.anchoredPosition;
            DrawUILine(startPos, endPos, currentLine);
            Complete();
        }

        isDrawing = false;
        startNode = null;

        if (currentDragEffect != null)
        {
            Destroy(currentDragEffect);
        }
    }

    void DrawUILine(Vector2 start, Vector2 end, GameObject lineObj)
    {
        if (lineObj == null) return;
        RectTransform rectTransform = lineObj.GetComponent<RectTransform>();
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);
        rectTransform.sizeDelta = new Vector2(distance, 300f);
        rectTransform.anchoredPosition = start + direction * distance * 0.5f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
    [SerializeField] private ScriptableGameData gameData;

    void Complete()
    {
        end = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Debug.Log("보상 지급");
        allNodes = null;
        reverEfect.SetActive(true);
        disableButton.gameObject.SetActive(false);
        StartCoroutine(EfectWating());

        gameData.currentPoints++;
    }

    IEnumerator EfectWating()
    {
        yield return new WaitForSeconds(2);
        completePlate.gameObject.SetActive(true);
    }
    public void Return()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Destroy(gameObject);
    }
}