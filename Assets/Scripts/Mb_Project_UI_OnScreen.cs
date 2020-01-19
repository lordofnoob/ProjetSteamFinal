using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Project_UI_OnScreen : MonoBehaviour
{
    public float UiScale = 50f;
    protected Transform UIWorldTransform;

    private void Start()
    {
        UIWorldTransform = Instantiate(new GameObject(), transform.position, Quaternion.identity, transform.parent).transform;
      //  transform.SetParent(Ma_UiManager.instance.inGameCanvas.transform);
        transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        transform.localScale = new Vector3(UiScale, UiScale);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(UIWorldTransform.position);
    }
}
