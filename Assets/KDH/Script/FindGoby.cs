using Scenes.Common.Scenes.Common;
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
    public TMP_Text guideText;

    public GameObject disableButton;
    private SkeletonGraphic checkSkeleton;
    private int checkCount = 0;
    [SerializeField] private ScriptableGameData gameData;

    private void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        guideText.gameObject.SetActive(true);
        completePlate.gameObject.SetActive(false);
    }

    public void GobyClick(Button slot)
    {
        Instantiate(check, slot.transform);

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

        gameData.currentPoints++;
    }

    public void Return()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Destroy(gameObject);
    }
}