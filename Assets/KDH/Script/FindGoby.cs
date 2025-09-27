using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindGoby : MonoBehaviour
{
    public Button[] gobys;
    public GameObject check;
    public Image completePlate;
    public Texture2D cursorImage;
    public Texture2D cursorImageClick;
    public TMP_Text guideText;

    public GameObject disableButton;
    private SkeletonGraphic checkSkeleton;
    private int checkCount = 0;

    private void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        guideText.gameObject.SetActive(true);
        completePlate.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorImageClick, Vector2.zero, CursorMode.Auto);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        }
    }

    public void GobyClick(Button slot)
    {
        checkSkeleton = Instantiate(check, slot.transform).GetComponent<SkeletonGraphic>();
        checkSkeleton.AnimationState.AddAnimation(0, "idle", true, 0);

        Destroy(slot);

        checkCount++;
        if (checkCount == gobys.Length)
        {
            Complete();
        }

        guideText.gameObject.SetActive(false);
    }

    void Complete()
    {
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
