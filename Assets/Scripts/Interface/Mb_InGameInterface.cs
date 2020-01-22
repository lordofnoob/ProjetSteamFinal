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
        UpdatePlayerUiItem(PlayerIndex.One, null);
        UpdatePlayerUiItem(PlayerIndex.Two, null);
        UpdatePlayerUiItem(PlayerIndex.Three, null);
        UpdatePlayerUiItem(PlayerIndex.Four, null);

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
                if (itemHoldSprite == null)
                    allPlayerItemSpot[0].gameObject.SetActive(false);
                else
                    allPlayerItemSpot[0].gameObject.SetActive(true);
                allPlayerItemSpot[0].sprite = itemHoldSprite;
                break;
            case PlayerIndex.Two:
                if (itemHoldSprite == null)
                    allPlayerItemSpot[1].gameObject.SetActive(false);
                else
                    allPlayerItemSpot[1].gameObject.SetActive(true);
                allPlayerItemSpot[1].sprite = itemHoldSprite;
                break;
            case PlayerIndex.Three:
                if (itemHoldSprite == null)
                    allPlayerItemSpot[2].gameObject.SetActive(false);
                else
                    allPlayerItemSpot[2].gameObject.SetActive(true);
                allPlayerItemSpot[2].sprite = itemHoldSprite;
                break;
            case PlayerIndex.Four:
                if (itemHoldSprite == null)
                    allPlayerItemSpot[3].gameObject.SetActive(false);
                else
                    allPlayerItemSpot[3].gameObject.SetActive(true);
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
                if (playerPicture == null)
                    allPlayerPicture[1].gameObject.SetActive(false);
                else
                    allPlayerPicture[1].gameObject.SetActive(true);
                allPlayerPicture[1].sprite = playerPicture;
            
                break;
            case PlayerIndex.Three:
                if (playerPicture == null)
                    allPlayerPicture[2].gameObject.SetActive(false);
                else
                    allPlayerPicture[2].gameObject.SetActive(true);
                allPlayerPicture[2].sprite = playerPicture;

                break;
            case PlayerIndex.Four:
                if (playerPicture == null)
                    allPlayerPicture[3].gameObject.SetActive(false);
                else
                    allPlayerPicture[3].gameObject.SetActive(true);

                allPlayerPicture[3].sprite = playerPicture;
                break;
        }

    }
}
