using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_EscapeZone : MonoBehaviour
{

    Material materialToModify;

    private void Awake()
    {
        materialToModify = GetComponent<Material>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.GetComponent<Mb_PlayerControler>())
        {
            print("Puck");
            Gamemanager.instance.addSecuredPlayer();

            if (Gamemanager.instance.securisedPlayer >= 1)
            {
                materialToModify.SetVector("Color_BC940844", new Vector4(27,154,0,0));
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Mb_PlayerControler>())
        {
            Gamemanager.instance.removeSecuredPlayer();

            if (Gamemanager.instance.securisedPlayer < Gamemanager.numberOfPlayer)
            {
                materialToModify.SetVector("Color_BC940844", new Vector4(53, 156, 250, 0));
            }
        }

    }
}
