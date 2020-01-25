using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_EscapeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Mb_PlayerControler>())
            Gamemanager.instance.addSecuredPlayer();
    }

    private void OnTriggerExit(Collider other)
    {
        if (GetComponent<Mb_PlayerControler>())
            Gamemanager.instance.removeSecuredPlayer();
    }
}
