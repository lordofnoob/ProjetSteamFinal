using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_LoadSkinAndMask : MonoBehaviour
{
    [Header("Scriptable Player Skin & Mask")]
    public Sc_PlayerSkinAndMask[] allScriptable = new Sc_PlayerSkinAndMask[4];
    [Header("All Skins")]
    public List<GameObject> allSkins = new List<GameObject>();

    private Mb_PlayerControler playerController;

    public void Start()
    {
        playerController = GetComponent<Mb_PlayerControler>();
    }

    public void LoadSkinAndMask()
    {
        for(int i = 0; i < allSkins.Count; i++)
        {
            if(allScriptable[(int)playerController.playerIndex].skinIndex != -1)
            {
                if (allScriptable[(int)playerController.playerIndex].skinIndex == i)
                {
                    allSkins[i].SetActive(true);
                    Mb_HoldingMasks maskHolder = allSkins[i].GetComponent<Mb_HoldingMasks>();
                    playerController.itemHandle = maskHolder.handler;
                    playerController.rAnimator = maskHolder.animator;

                    for (int y = 0; y < maskHolder.listOfAllMasks.Count; y++)
                    {
                        if(allScriptable[(int)playerController.playerIndex].maskIndex != -1)
                        {
                            if (allScriptable[(int)playerController.playerIndex].maskIndex == y)
                            {
                                maskHolder.listOfAllMasks[y].SetActive(true);
                            }
                            else
                            {
                                maskHolder.listOfAllMasks[y].SetActive(false);
                            }
                        }
                        else if(allScriptable[(int)playerController.playerIndex].maskIndex == -1)
                        {
                            gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    allSkins[i].SetActive(false);
                }
            }
            else if(allScriptable[(int)playerController.playerIndex].skinIndex == -1)
            {
                gameObject.SetActive(false);
            }
        }

        /*for (int i = 0; i < allSkins.Count; i++)
        {
            if (CrossSceneInformation.selectedPlayersSkins[(int)playerController.playerIndex] != -1)
            {
                if (CrossSceneInformation.selectedPlayersSkins[(int)playerController.playerIndex] == i)
                {
                    allSkins[i].SetActive(true);
                    Mb_HoldingMasks maskHolder = allSkins[i].GetComponent<Mb_HoldingMasks>();
                    playerController.itemHandle = maskHolder.handler;
                    playerController.rAnimator = maskHolder.animator;

                    for (int y = 0; y < maskHolder.listOfAllMasks.Count; y++)
                    {
                        if (CrossSceneInformation.selectedPlayersMasks[(int)playerController.playerIndex] != -1)
                        {
                            if (CrossSceneInformation.selectedPlayersMasks[(int)playerController.playerIndex] == y)
                            {
                                maskHolder.listOfAllMasks[y].SetActive(true);
                            }
                            else
                            {
                                maskHolder.listOfAllMasks[y].SetActive(false);
                            }
                        }
                        else if (CrossSceneInformation.selectedPlayersMasks[(int)playerController.playerIndex] == -1)
                        {
                            gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    allSkins[i].SetActive(false);
                }
            }
            else if (CrossSceneInformation.selectedPlayersSkins[(int)playerController.playerIndex] == -1)
            {
                gameObject.SetActive(false);
            }
        }*/
    }
}
