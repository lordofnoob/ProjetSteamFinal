using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_EscapeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Gamemanager.instance.addSecuredPlayer();
    }

    private void OnTriggerExit(Collider other)
    {
        Gamemanager.instance.removeSecuredPlayer();
    }
}
