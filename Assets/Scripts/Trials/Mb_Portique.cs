using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Portique : MonoBehaviour
{
    [Header("Realisation Part")]
    [SerializeField] Transform southPoint;
    [SerializeField] Transform northPoint;
    [SerializeField] Mb_Door[] doorToOpenFromSouth;
    [SerializeField] Mb_Door[] doorToOpenFromNorth;
    List<Mb_PlayerControler> userList = new List<Mb_PlayerControler>();

    private void OnTriggerEnter(Collider other)
    {
        Mb_PlayerControler player = other.GetComponent<Mb_PlayerControler>();
        if(player.itemHold != null)
        {
            userList.Add(player);

            float distanceToSouth = Vector3.Distance(other.transform.position , southPoint.position);
            float distanceToNorth = Vector3.Distance(other.transform.position, northPoint.position);

            if (distanceToNorth >= distanceToSouth)
                {
                    for (int i=0; i < doorToOpenFromSouth.Length; i++)
                    {
                        doorToOpenFromSouth[i].CloseDoor();
                        doorToOpenFromSouth[i].ResetParameters();
                        print(doorToOpenFromSouth[i]);
                    }
                }

            else
                {
                    for (int i = 0; i < doorToOpenFromNorth.Length; i++)
                    {
                        doorToOpenFromNorth[i].CloseDoor();
                        doorToOpenFromNorth[i].ResetParameters();
                        print(doorToOpenFromNorth[i]);
                    }
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Mb_PlayerControler player = other.GetComponent<Mb_PlayerControler>();
        if (player.itemHold != null)
        {
            userList.Remove(player);
            if (userList.Count == 0)
            {
                float distanceToSouth = Vector3.Distance(other.transform.position, southPoint.position);
                float distanceToNorth = Vector3.Distance(other.transform.position, northPoint.position);

                if (distanceToNorth >= distanceToSouth)
                {
                    for (int i = 0; i < doorToOpenFromSouth.Length; i++)
                    {
                        doorToOpenFromSouth[i].OpenDoor();
                        doorToOpenFromSouth[i].ResetParameters();
                        print(doorToOpenFromSouth[i]);
                    }
                }

                else
                {
                    for (int i = 0; i < doorToOpenFromNorth.Length; i++)
                    {
                        doorToOpenFromNorth[i].OpenDoor();
                        doorToOpenFromNorth[i].ResetParameters();
                        print(doorToOpenFromNorth[i]);
                    }
                }
            }
        }
    }
}