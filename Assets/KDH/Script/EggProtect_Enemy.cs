using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class EggProtect_Enemy : MonoBehaviour
{
    bool move = true;
    SkeletonGraphic skeleton;

    private void Start()
    {
        skeleton = GetComponent<SkeletonGraphic>();
    }

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
        skeleton.AnimationState.SetAnimation(0, "die", false);

        move = false;
        Destroy(this);
    }
}
