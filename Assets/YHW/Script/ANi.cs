using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANi : MonoBehaviour
{
    GameObject capturedImage;

    private void Awake()
    {
        capturedImage = GameObject.Find("CapturedImgae");

    }

    public void ABCDEFFF()
    {
        capturedImage.SetActive(false);
    }
}
