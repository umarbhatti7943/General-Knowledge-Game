using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingScreenManager : MonoBehaviour
{
    [Header("Logo Animation")]
    public Transform logo;
    public CanvasGroup logoCanvasGroup;

    [Header("UI Elements")]
    public Text percentageText;
    public Text loadingText;

    [Header("Loading Settings")]
    public float minLoadTime = 3f;    

    private void Start()
    {
        Application.targetFrameRate = 60;
        PlayLogoAnimation();
        StartCoroutine(LoadSceneAsync("GameScene"));
        
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float elapsedTime = 0f;
        float progress = 0f;
        while (!operation.isDone)
        {
            float simulatedProgress = Mathf.Clamp01(elapsedTime / minLoadTime);
            float actualProgress = Mathf.Clamp01(operation.progress / 0.9f);
            progress = Mathf.Min(simulatedProgress, actualProgress);
            percentageText.text = Mathf.RoundToInt(progress * 100f) + "%";
            elapsedTime += Time.deltaTime;
            yield return null;
            if (progress >= 1f && operation.progress >= 0.9f)
                break;
        }

        percentageText.text = "100%";
        yield return new WaitForSeconds(0.5f);
        operation.allowSceneActivation = true;
    }

    void PlayLogoAnimation()
    {
        logo.gameObject.SetActive(true);
        logo.localScale = Vector3.one * 0.7f;
        logoCanvasGroup.alpha = 0f;
        Sequence seq = DOTween.Sequence();
        seq.Append(logoCanvasGroup.DOFade(1f, 3f))
            .Join(logo.DOScale(1f, 2f).SetEase(Ease.OutBack));
    }
}