using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public float delayBeforeLoad = 5f;
    public float fadeDuration = 2f;
    public string nextSceneName;

    private void Start()
    {
        StartCoroutine(LoadNextSceneWithDelay());
    }

    IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);

        // Fade out current scene
        yield return StartCoroutine(FadeOutScene());

        // Load next scene
        SceneManager.LoadScene(nextSceneName);

        // Fade in next scene
        yield return StartCoroutine(FadeInScene());
    }

    IEnumerator FadeOutScene()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1f - (elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    IEnumerator FadeInScene()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = elapsedTime / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
