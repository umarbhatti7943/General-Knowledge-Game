using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    private const string QuestionProgressKey = "QuestionProgress";
    private const string StreakProgressKey = "StreakCount";

    private QuizData quizData;
    private int currentQuestionIndex = 0;
    private int streakCount = 0;
    private bool isAnswered = false;

    void Start()
    {
        //SoundManager._SoundManager.playGameplaySound();
        // Get QuizData from GameData singleton
        if (GameData.Instance != null)
        {
            quizData = GameData.Instance.quizData;
        }
        else
        {
            Debug.LogError("GameData Instance not found!");
            return;
        }

        if (quizData == null || quizData.Count == 0)
        {
            Debug.LogError("No questions in QuizData!");
            return;
        }

        currentQuestionIndex = LoadQuestionProgress();
        if (currentQuestionIndex >= quizData.Count)
        {
            ResetQuestionProgress();
            currentQuestionIndex = 0;
        }

        streakCount = PlayerPrefs.GetInt(StreakProgressKey, 0);
        uiManager.SetStreak(streakCount);

        // Setup button listeners
        uiManager.AnsBtn01.onClick.AddListener(() => OnAnswerSelected(0));
        uiManager.AnsBtn02.onClick.AddListener(() => OnAnswerSelected(1));
        uiManager.AnsBtn03.onClick.AddListener(() => OnAnswerSelected(2));
        uiManager.AnsBtn04.onClick.AddListener(() => OnAnswerSelected(3));

        // Display saved question or first question
        DisplayQuestion(currentQuestionIndex);
    }

    void DisplayQuestion(int questionIndex)
    {
        if (questionIndex >= quizData.Count)
        {
            Debug.Log("Quiz Complete!");
            return;
        }

        currentQuestionIndex = questionIndex;
        isAnswered = false;
        SaveQuestionProgress(questionIndex);

        var question = quizData.GetQuestion(questionIndex);
        if (question == null || !question.IsValid())
        {
            Debug.LogError($"Invalid question at index {questionIndex}");
            return;
        }

        // Set UI text
        uiManager.SetQuestionText(question.questionText);
        uiManager.SetAnswerText(0, question.options[0]);
        uiManager.SetAnswerText(1, question.options[1]);
        uiManager.SetAnswerText(2, question.options[2]);
        uiManager.SetAnswerText(3, question.options[3]);

        // Play fade in animation
        uiManager.PlayFadeIn();
    }

    void OnAnswerSelected(int selectedIndex)
    {
        if (isAnswered) return;
        isAnswered = true;

        var question = quizData.GetQuestion(currentQuestionIndex);
        if (question == null) return;

        bool isCorrect = (selectedIndex == question.correctOptionIndex);

        if (isCorrect)
        {
            streakCount++;
        }
        else
        {
            streakCount = 0;
        }

        uiManager.SetStreak(streakCount);
        uiManager.PlayStreakPulse(isCorrect);
        SaveStreakProgress();

        int nextQuestionIndex = currentQuestionIndex + 1;
        if (nextQuestionIndex >= quizData.Count)
        {
            nextQuestionIndex = 0;
        }

        SaveQuestionProgress(nextQuestionIndex);

        // Show feedback
        uiManager.ShowFeedback(selectedIndex, question.correctOptionIndex, isCorrect);

        // Wait 1 second then load next question
        StartCoroutine(LoadNextQuestionAfterDelay(nextQuestionIndex));
    }

    IEnumerator LoadNextQuestionAfterDelay(int nextQuestionIndex)
    {
        yield return new WaitForSeconds(1f);
        // Fade out
        uiManager.PlayFadeOut();
        yield return new WaitForSeconds(0.5f);

        // Load next question
        DisplayQuestion(nextQuestionIndex);
    }

    private int LoadQuestionProgress()
    {
        return PlayerPrefs.GetInt(QuestionProgressKey, 0);
    }

    private void SaveQuestionProgress(int questionIndex)
    {
        PlayerPrefs.SetInt(QuestionProgressKey, questionIndex);
        PlayerPrefs.Save();
    }

    private void SaveStreakProgress()
    {
        PlayerPrefs.SetInt(StreakProgressKey, streakCount);
        PlayerPrefs.Save();
    }

    private void ResetQuestionProgress()
    {
        PlayerPrefs.DeleteKey(QuestionProgressKey);
        PlayerPrefs.DeleteKey(StreakProgressKey);
        PlayerPrefs.Save();

        streakCount = 0;
        if (uiManager != null)
        {
            uiManager.SetStreak(streakCount);
        }
    }
}
