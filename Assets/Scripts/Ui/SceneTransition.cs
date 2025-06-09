using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; // Associe o painel aqui pelo Inspector.
    public float fadeDuration = 1f;
    public GameObject TransitionPainel;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            SetAlpha(alpha);
            yield return null;
            TransitionPainel.SetActive(false);
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }
}
