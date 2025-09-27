using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class WaterwayRestore : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject dragEffectPrefab;
    public GameObject guideLine;
    public Image completePlate;

    public List<RectTransform> allNodes;

    public GameObject disableButton;
    private RectTransform canvasRectTransform;
    private bool isDrawing = false;
    private Vector2 startPos;
    private RectTransform startNode;
    private GameObject currentLine;
    private GameObject currentDragEffect;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    void Start()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        graphicRaycaster = canvasRectTransform.GetComponentInParent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

        if (Input.GetMouseButtonUp(0))
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
        guideLine.SetActive(false);
        isDrawing = true;
        startNode = startNodeRect;
        startPos = startNodeRect.anchoredPosition;

        currentLine = Instantiate(linePrefab, canvasRectTransform);
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

        if (currentLine != null)
        {
            Destroy(currentLine);
        }
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
        rectTransform.sizeDelta = new Vector2(distance, 50f);
        rectTransform.anchoredPosition = start + direction * distance * 0.5f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Complete()
    {
        Destroy(currentLine);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Debug.Log("보상 지급");
        disableButton.gameObject.SetActive(false);
        completePlate.gameObject.SetActive(true);
    }

    public void Return()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Destroy(gameObject);
    }
}