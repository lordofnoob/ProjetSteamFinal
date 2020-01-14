using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;

public class Mb_InGameInterface : MonoBehaviour
{
    public static Mb_InGameInterface instance;
    [SerializeField] Image[] allPlayerPicture;
    [SerializeField] Image[] allPlayerItemSpot;

    private void Awake()

    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void UpdatePlayerUiItem(PlayerIndex playerToUpdate, Sprite itemHoldSprite)
    {
        switch (playerToUpdate)
        {
            case PlayerIndex.One:
                allPlayerItemSpot[0].sprite = itemHoldSprite;
                break;
            case PlayerIndex.Two:
                allPlayerItemSpot[1].sprite = itemHoldSprite;
                break;
            case PlayerIndex.Three:
                allPlayerItemSpot[2].sprite = itemHoldSprite;
                break;
            case PlayerIndex.Four:
                allPlayerItemSpot[3].sprite = itemHoldSprite;
                break;
        }
    }

    public void UpdatePlayerPicture(PlayerIndex playerToUpdate, Sprite playerPicture)
    {
        switch (playerToUpdate)
        {
            case PlayerIndex.One:
                allPlayerPicture[0].sprite = playerPicture;
                break;
            case PlayerIndex.Two:
                allPlayerPicture[1].sprite = playerPicture;
                break;
            case PlayerIndex.Three:
                allPlayerPicture[2].sprite = playerPicture;
                break;
            case PlayerIndex.Four:
                allPlayerPicture[3].sprite = playerPicture;
                break;
        }

    }
}
