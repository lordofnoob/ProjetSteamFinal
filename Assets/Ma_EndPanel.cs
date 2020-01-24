using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ma_EndPanel : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
