using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Scenes.MainGame;
using Scenes.Common;
using Scenes.Common.Scenes.Common;


public class CameraUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]private RectTransform bigImage;       // 큰 이미지 (배경)
    [SerializeField]private RawImage capturePreview;      // 캡쳐 결과 표시용
    [SerializeField] private Camera mainCam;               // 실제 월드 확인할 카메라

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private List<Collider2D> targetColliders; // 체크할 콜라이더들

    [SerializeField]private ScriptableGameData gameData;

    private bool failed;


    private Vector2 minLimit, maxLimit;
    private bool isCapturing = false;


    [SerializeField] private GameObject clear;
    [SerializeField]private GameObject fail;
    [SerializeField] private GameObject success;



    private void OnEnable()
    {
        // UI 시작 시 랜덤 위치
        Vector2 randomPos = new Vector2(
            Random.Range(-200f, 200f),
            Random.Range(-200f, 200f)
        );
        bigImage.anchoredPosition = randomPos;

        // 리미트 계산 (부모 캔버스 크기 기준)
        RectTransform parent = bigImage.parent as RectTransform;
        Vector2 parentSize = parent.rect.size;
        Vector2 imageSize = bigImage.rect.size;

        minLimit = (parentSize - imageSize) / 2f;
        maxLimit = -minLimit;

        capturePreview.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isCapturing&&!failed) return;

        // 마우스 위치 기준 이동
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseDir = (Vector2)Input.mousePosition - screenCenter;
        Vector2 moveDir = -mouseDir.normalized;

        bigImage.anchoredPosition += moveDir * moveSpeed * Time.deltaTime;

        // 리미트 Clamp
        bigImage.anchoredPosition = new Vector2(
            Mathf.Clamp(bigImage.anchoredPosition.x, minLimit.x, maxLimit.x),
            Mathf.Clamp(bigImage.anchoredPosition.y, minLimit.y, maxLimit.y)
        );

        // 스페이스 입력 → 캡쳐
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CaptureScreen());
        }
        
    }

    bool IsColliderInside(Camera cam, Collider2D col, Rect screenRect)
    {
        Bounds b = col.bounds;

        // Bounds의 8개 꼭짓점 대신 2D니까 4개 코너만 검사
        Vector3[] corners = new Vector3[4];
        corners[0] = cam.WorldToScreenPoint(new Vector3(b.min.x, b.min.y, col.transform.position.z));
        corners[1] = cam.WorldToScreenPoint(new Vector3(b.min.x, b.max.y, col.transform.position.z));
        corners[2] = cam.WorldToScreenPoint(new Vector3(b.max.x, b.min.y, col.transform.position.z));
        corners[3] = cam.WorldToScreenPoint(new Vector3(b.max.x, b.max.y, col.transform.position.z));

        foreach (var c in corners)
        {
            if (c.z < 0) return false; // 카메라 뒤
            if (!screenRect.Contains(c)) return false;
        }
          return true;
    }
  
    private System.Collections.IEnumerator CaptureScreen()
    {
        isCapturing = true;
        yield return new WaitForEndOfFrame();

        // 화면 전체 캡쳐
        Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture();

        // 캡쳐 결과 UI 활성화 + 이미지 넣기
        capturePreview.texture = tex;
        capturePreview.gameObject.SetActive(true);

        // 캡쳐된 화면 내 콜라이더 확인
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        bool allInside = true;

        foreach (var col in targetColliders)
        {
            if (col == null) continue;
            if (!IsColliderInside(mainCam, col, screenRect))
            {
                allInside = false;
                break;
            }
        }
        if (allInside)
        {
            Debug.Log("모든 콜라이더가 캡쳐 화면 안에 있습니다!");
            gameData.currentPoints++;
            success.SetActive(true);
            clear.SetActive(false);
            failed = true;
        }
        else
        {
            Debug.Log("일부 콜라이더가 화면 밖입니다.");
            fail.SetActive(true);
            failed = false;
        } 
    }
}
