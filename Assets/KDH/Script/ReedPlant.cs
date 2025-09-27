using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class ReedPlant : MonoBehaviour
{
    public Button[] slots;
    public GameObject reed;
    public Image completePlate;

    private SkeletonGraphic reedSkeleton;
    private int reedCount = 0;

    private void Start()
    {
        completePlate.gameObject.SetActive(false);
    }

    public void ReedClick(Button slot)
    {
        reedSkeleton = Instantiate(reed, slot.transform).GetComponent<SkeletonGraphic>();
        reedSkeleton.AnimationState.AddAnimation(0, "idle", true, 0);

        Destroy(slot);

        reedCount++;
        if (reedCount == slots.Length)
        {
            Complete();
        }
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
