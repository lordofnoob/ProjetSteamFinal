using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Mb_EditorCameraSplitScreen1 : MonoBehaviour
{
    public bool splitScreen;
    public bool twoPlayers;
    public bool threePlayers;
    public bool fourPlayers;

    public List<GameObject> twoPlayersGo;
    public List<GameObject> threePlayersGo;
    public List<GameObject> fourPlayersGo;
    public GameObject notSplit;

    void Start()
    {

        twoPlayersGo[0].SetActive(false);
        twoPlayersGo[1].SetActive(false);

        fourPlayersGo[0].SetActive(false);
        fourPlayersGo[1].SetActive(false);
        fourPlayersGo[2].SetActive(false);
        fourPlayersGo[3].SetActive(false);

        threePlayersGo[0].SetActive(false);
        threePlayersGo[1].SetActive(false);
        threePlayersGo[2].SetActive(false);


        notSplit.SetActive(true);
    }

    void Update()
    {
        if (splitScreen)
        {

            if (twoPlayers)
            {
                FalseFour();

                FalseFour();

                for (int i = 0; i < twoPlayersGo.Count; i++)
                {
                    twoPlayersGo[i].SetActive(true);
                }

                notSplit.SetActive(false);

            }

            if (threePlayers)
            {
                FalseTwo();

                FalseFour();

                for (int i = 0; i < threePlayersGo.Count; i++)
                {
                    threePlayersGo[i].SetActive(true);
                }

                notSplit.SetActive(false);

            }

            if (fourPlayers)
            {

                FalseTwo();

                FalseThree();

                for (int i = 0; i < fourPlayersGo.Count; i++)
                {
                    fourPlayersGo[i].SetActive(true);
                }

                notSplit.SetActive(false);

            }

        }
        else
        {
            FalseTwo();

            FalseThree();

            FalseFour();

            notSplit.SetActive(true);
        }
    }

    private void FalseTwo()
    {
        for (int i = 0; i < twoPlayersGo.Count; i++)
        {
            twoPlayersGo[i].SetActive(false);
        }

    }

    private void FalseThree()
    {
        for (int i = 0; i < threePlayersGo.Count; i++)
        {
            threePlayersGo[i].SetActive(false);
        }

    }

    private void FalseFour()
    {
        for (int i = 0; i < fourPlayersGo.Count; i++)
        {
            fourPlayersGo[i].SetActive(false);
        }

    }
}
