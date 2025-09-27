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
        WaterwayRestore,
        FindGoby,
        CrabRace
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
        else if (playMiniGame == MiniGames.WaterwayRestore)
        {
            game.GetComponent<WaterwayRestore>().disableButton = gameObject;
        }
        else if (playMiniGame == MiniGames.FindGoby)
        {
            game.GetComponent<FindGoby>().disableButton = gameObject;
        }
        else if (playMiniGame == MiniGames.CrabRace)
        {
            game.GetComponent<CrabRace>().disableButton = gameObject;
        }
    }
}
