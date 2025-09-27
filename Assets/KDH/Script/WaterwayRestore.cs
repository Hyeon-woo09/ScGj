using UnityEngine;
using UnityEngine.UI;

public class WaterwayRestore : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject dragEffectPrefab;
    public Image completePlate;
    public RectTransform canvasRectTransform;

    private bool isDrawing = false;
    private Vector2 startPos;
    private GameObject currentLine;
    private GameObject currentDragEffect;

    public void StartDrawingLine(RectTransform startNodeRect)
    {
        if (isDrawing) return;

        isDrawing = true;
        startPos = startNodeRect.anchoredPosition;

        currentLine = Instantiate(linePrefab, canvasRectTransform);
        currentLine.SetActive(true);

        if (dragEffectPrefab != null)
        {
            currentDragEffect = Instantiate(dragEffectPrefab, canvasRectTransform);
        }
    }

    public void StopDrawingLine(RectTransform endNodeRect)
    {
        if (!isDrawing) return;

        if (endNodeRect != null)
        {
            Vector2 endPos = endNodeRect.anchoredPosition;
            DrawUILine(startPos, endPos, currentLine);

            Debug.Log("연결 성공!");

            currentLine = null;
        }

        isDrawing = false;

        if (currentLine != null)
        {
            Destroy(currentLine);
        }
        if (currentDragEffect != null)
        {
            Destroy(currentDragEffect);
        }
    }

    void Update()
    {
        if (isDrawing)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform, Input.mousePosition, Camera.main, out Vector2 mousePos);

            DrawUILine(startPos, mousePos, currentLine);

            if (currentDragEffect != null)
            {
                currentDragEffect.GetComponent<RectTransform>().anchoredPosition = mousePos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopDrawingLine(null);
            }
        }
    }

    void DrawUILine(Vector2 start, Vector2 end, GameObject lineObj)
    {
        if (lineObj == null) return;
        RectTransform rectTransform = lineObj.GetComponent<RectTransform>();

        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);

        rectTransform.sizeDelta = new Vector2(distance, 5f);
        rectTransform.anchoredPosition = start + direction * distance * 0.5f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
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