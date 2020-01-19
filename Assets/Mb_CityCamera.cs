using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_CityCamera : MonoBehaviour
{
    public Transform cam;
    public Transform[] presets;


    public bool one;
    public bool two;
    public bool three;
    public bool four;

    private void Start()
    {
        BanquettePos();
    }

    private void Update()
    {

        if (one)
        {
            BanquettePos();
        }

        if (two)
        {
            BanquePos();
        }

        if (three)
        {
            SuperBanquePos();
        }
        if (four)
        {
            MegaBanquePos();
        }

    }

    public void BanquettePos()
    {
        cam.position = presets[0].position;
        cam.rotation = presets[0].rotation;
    }

    public void BanquePos()
    {
        cam.position = presets[1].position;
        cam.rotation = presets[1].rotation;
    }


    public void SuperBanquePos()
    {
        cam.position = presets[2].position;
        cam.rotation = presets[2].rotation;
    }

    public void MegaBanquePos()
    {
        cam.position = presets[3].position;
        cam.rotation = presets[3].rotation;
    }

}
