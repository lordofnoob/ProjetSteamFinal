using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection_RightPanelManager : MonoBehaviour
{
    public Mb_CityCamera miniMap;
    public List<GameObject> levelDescr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelSelection(int levelIndex)
    {
        miniMap.one = false;
        miniMap.two = false;
        miniMap.three = false;
        miniMap.four = false;

        foreach(GameObject descr in levelDescr)
        {
            descr.SetActive(false);
        }

        switch (levelIndex)
        {
            case 0:
                miniMap.one = true;
                levelDescr[levelIndex].SetActive(true);
                break;

            case 1:
                miniMap.two = true;
                levelDescr[levelIndex].SetActive(true);
                break;

            case 2:
                miniMap.three = true;
                levelDescr[levelIndex].SetActive(true);
                break;

            case 3:
                miniMap.four = true;
                levelDescr[levelIndex].SetActive(true);
                break;

        }


    }
}
