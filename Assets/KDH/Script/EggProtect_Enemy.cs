using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggProtect_Enemy : MonoBehaviour
{
    bool move = true;
    public IEnumerator EggTracking(float duration, EggProtect egp)
    {
        float elapsedTime = 0f;
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 startPos = rectTransform.anchoredPosition;
        while (elapsedTime < duration && move)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, Vector2.zero, elapsedTime / duration);
            yield return null;
        }

        if (move)
        {
            egp.Failed();
        }
    }

    public void Click()
    {
        Image enemyImage = gameObject.GetComponent<Image>();
        if (enemyImage != null)
        {
            enemyImage.color = Color.red;
        }

        move = false;
        Destroy(this);
    }
}
