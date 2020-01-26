using UnityEngine;
using UnityEngine.SceneManagement;


public class Mb_Fade : MonoBehaviour
{
    public Animator animator;
    public float fadeDuration = 1f;

    private int levelToLoad;


    public void FadeToLevel(int levelToLoadIndex)
    {
        levelToLoad = levelToLoadIndex;
        animator.SetTrigger("FadeOut");
        animator.speed = fadeDuration;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
