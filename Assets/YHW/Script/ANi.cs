using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANi : MonoBehaviour
{
    GameObject capturedImage;
    GameObject groupresultfail;

    private void Awake()
    {
        capturedImage = GameObject.Find("CapturedImgae");
        groupresultfail = GameObject.Find("Group_Result_Fail");

    }

    public void ABCDEFFF()
    {
        capturedImage.SetActive(false);
        groupresultfail.SetActive(false);
    }
}
