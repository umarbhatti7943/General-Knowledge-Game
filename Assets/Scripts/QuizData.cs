using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Scriptable Objects/QuizData")]
public class QuizData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        [TextArea(2, 6)]
        public string questionText;

        [Header("Options (exactly 4)")]
        public string[] options = new string[4];

        [Tooltip("Index of the correct option: 0..3")]
        [Range(0, 3)]
        public int correctOptionIndex = 0;

        public bool IsValid()
        {
            if (options == null || options.Length != 4) return false;
            for (int i = 0; i < 4; i++)
                if (string.IsNullOrWhiteSpace(options[i])) return false;
            return correctOptionIndex >= 0 && correctOptionIndex < 4 && !string.IsNullOrWhiteSpace(questionText);
        }
    }

    [Header("Questions")]
    public List<Question> questions = new List<Question>();

    public Question GetQuestion(int index)
    {
        if (questions == null || questions.Count == 0) return null;
        if (index < 0 || index >= questions.Count) return null;
        return questions[index];
    }

    public int Count => questions == null ? 0 : questions.Count;
}