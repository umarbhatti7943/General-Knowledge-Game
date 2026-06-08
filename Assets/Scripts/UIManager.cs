using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Question Text")]
    public Text QuestionText;

    [Header("Answer Texts")]
    public Text AnsText01;
    public Text AnsText02;
    public Text AnsText03;
    public Text AnsText04;
    public Text StreakText;

    [Header("AnswerBtns")]
    public Button AnsBtn01;
    public Button AnsBtn02;
    public Button AnsBtn03;
    public Button AnsBtn04;

    [Header("Fade Settings")]
    public float fadeSpeed = 0.5f;
    private CanvasGroup canvasGroup;
    private Color correctColor = Color.green;
    private Color wrongColor = Color.red;
    private Color defaultButtonColor = Color.white;
    private Color defaultTextColor = Color.black;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        ResetAllAnswerTextColor();
        ResetAllButtonColors();
    }

    public void SetQuestionText(string text)
    {
        QuestionText.text = text;
    }

    public void SetAnswerText(int index, string text)
    {
        switch (index)
        {
            case 0: AnsText01.text = text; break;
            case 1: AnsText02.text = text; break;
            case 2: AnsText03.text = text; break;
            case 3: AnsText04.text = text; break;
        }
    }

    public void ShowFeedback(int selectedIndex, int correctIndex, bool isCorrect)
    {
        ResetAllAnswerTextColor();
        ResetAllButtonColors();
        Button selectedButton = GetAnswerButton(selectedIndex);
        Text selectedText = GetAnswerText(selectedIndex);
        
        if (selectedButton != null)
        {
            selectedButton.image.color = isCorrect ? correctColor : wrongColor;
        }
        if (selectedText != null)
        {
            selectedText.color = Color.white;
        }

        if (!isCorrect)
        {
            Button correctButton = GetAnswerButton(correctIndex);
            Text correctText = GetAnswerText(correctIndex);
            if (correctButton != null)
            {
                correctButton.image.color = correctColor;
            }
            if (correctText != null)
            {
                correctText.color = Color.white;
            }
        }

        StartCoroutine(ResetFeedback());
    }

    IEnumerator ResetFeedback()
    {
        yield return new WaitForSeconds(1.5f);
        ResetAllAnswerTextColor();
        ResetAllButtonColors();
    }

    public void SetStreak(int streak)
    {
        if (StreakText == null) 
            return;

        StreakText.text = streak.ToString();
    }

    public void PlayStreakPulse(bool isPositive)
    {
        if (StreakText == null) 
            return;

        DOTween.Kill(StreakText.transform);
        StreakText.transform.localScale = Vector3.one;
        Sequence pulseSequence = DOTween.Sequence();
        pulseSequence.Append(StreakText.transform.DOScale(Vector3.one * 1.2f, 0.15f).SetEase(Ease.OutBack));
        pulseSequence.Append(StreakText.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.InBack));
    }

    void ResetAllAnswerTextColor()
    {
        AnsText01.color = defaultTextColor;
        AnsText02.color = defaultTextColor;
        AnsText03.color = defaultTextColor;
        AnsText04.color = defaultTextColor;
    }

    void ResetAllButtonColors()
    {
        SetAnswerButtonColor(0, defaultButtonColor);
        SetAnswerButtonColor(1, defaultButtonColor);
        SetAnswerButtonColor(2, defaultButtonColor);
        SetAnswerButtonColor(3, defaultButtonColor);
    }

    void SetAnswerButtonColor(int index, Color color)
    {
        Button button = GetAnswerButton(index);
        if (button != null && button.image != null)
        {
            button.image.color = color;
        }
    }

    Text GetAnswerText(int index)
    {
        switch (index)
        {
            case 0: 
                return AnsText01;
            case 1: 
                return AnsText02;
            case 2: 
                return AnsText03;
            case 3: 
                return AnsText04;
            default: 
                return null;
        }
    }

    Button GetAnswerButton(int index)
    {
        switch (index)
        {
            case 0: 
                return AnsBtn01;
            case 1: 
                return AnsBtn02;
            case 2: 
                return AnsBtn03;
            case 3: 
                return AnsBtn04;
            default: 
                return null;
        }
    }

    public void PlayFadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeSpeed);
    }

    public void PlayFadeOut()
    {
        canvasGroup.DOFade(0f, fadeSpeed);
    }
}