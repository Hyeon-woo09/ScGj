using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestoreButton : MonoBehaviour
{
    public enum MiniGames
    {
        ReedPlant,
        EggProtect,
        WaterwayRestore
    }

    public MiniGames playMiniGame;
    public GameObject[] MiniGamePrefabs;

    public void OnClick()
    {
        GameObject game = Instantiate(MiniGamePrefabs[(int)playMiniGame], transform.parent);
        if (playMiniGame == MiniGames.ReedPlant)
        {
            game.GetComponent<ReedPlant>().disableButton = gameObject;
        }
        else if (playMiniGame == MiniGames.EggProtect)
        {
            game.GetComponent<EggProtect>().disableButton = gameObject;
        }
    }
}
