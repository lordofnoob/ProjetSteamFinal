using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_EscapeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.GetComponent<Mb_PlayerControler>())
        {
            print("Puck");
            Gamemanager.instance.addSecuredPlayer();
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Mb_PlayerControler>())
            Gamemanager.instance.removeSecuredPlayer();
    }
}
