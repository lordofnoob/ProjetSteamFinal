using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_IconsChara : MonoBehaviour
{

    public Animator[] animator;
    

    
    private void OnEnable()
    {
        SetIdleUI();
    }

    public void SetIdleUI()
    {
        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetFloat("Offset", Random.Range(0.0f, 1.0f));
        }
    }
}
