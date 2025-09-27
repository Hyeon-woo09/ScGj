using UnityEngine;
using UnityEngine.UI;

public class CameraCaptureUI : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform cameraPanel;    // 카메라 UI 패널
    public RawImage movingImage;         // 화면 안에서 움직이는 이미지
    public RectTransform captureDisplay; // 캡처 후 보여줄 이미지

    [Header("Settings")]
    public float moveSpeed = 100f;       // 이미지 이동 속도
    public Vector2 panelBounds = new Vector2(500, 500); // 이미지 이동 가능한 영역

    private Vector2 imagePos;

    void OnEnable()
    {
        // 패널이 켜지면 이미지 랜덤 위치로
        imagePos = new Vector2(
            Random.Range(-panelBounds.x / 2, panelBounds.x / 2),
            Random.Range(-panelBounds.y / 2, panelBounds.y / 2)
        );
        movingImage.rectTransform.anchoredPosition = imagePos;
    }

    void Update()
    {
        HandleMovement();
        HandleCapture();
    }

    void HandleMovement()
    {
        // 마우스 위치 기준 반대 방향으로 이동
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cameraPanel, Input.mousePosition, null, out mousePos);

        Vector2 dir = (imagePos - mousePos).normalized; // 반대 방향
        imagePos += dir * moveSpeed * Time.deltaTime;

        // 리미트 적용
        imagePos.x = Mathf.Clamp(imagePos.x, -panelBounds.x / 2, panelBounds.x / 2);
        imagePos.y = Mathf.Clamp(imagePos.y, -panelBounds.y / 2, panelBounds.y / 2);

        movingImage.rectTransform.anchoredPosition = imagePos;
    }

    void HandleCapture()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 캡처
            Texture2D tex = new Texture2D((int)cameraPanel.rect.width, (int)cameraPanel.rect.height, TextureFormat.RGB24, false);
            // RenderTexture 없이 UI 단순화: RawImage의 스크린샷
            Rect rect = new Rect(cameraPanel.position.x - cameraPanel.rect.width / 2,
                                 cameraPanel.position.y - cameraPanel.rect.height / 2,
                                 cameraPanel.rect.width,
                                 cameraPanel.rect.height);

            tex.ReadPixels(rect, 0, 0);
            tex.Apply();

            // 패널 내리기
            cameraPanel.gameObject.SetActive(false);

            // 캡처 이미지 보여주기
            captureDisplay.gameObject.SetActive(true);
            captureDisplay.GetComponent<RawImage>().texture = tex;

            // 콜라이더 검사
            CheckCollidersInView(rect);
        }
    }

    void CheckCollidersInView(Rect captureRect)
    {
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();
        foreach (var col in colliders)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(col.bounds.center);

            if (captureRect.Contains(screenPos))
            {
                Debug.Log(col.name + " is inside capture!");
            }
            else
            {
                Debug.Log(col.name + " is outside capture!");
            }
        }
    }
}