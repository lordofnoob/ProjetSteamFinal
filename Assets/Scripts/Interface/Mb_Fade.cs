using UnityEngine;
using UnityEngine.SceneManagement;


public class Mb_Fade : MonoBehaviour
{
    public Animator animator;
    public float fadeDuration = 1f;

    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
        animator.speed = fadeDuration;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
