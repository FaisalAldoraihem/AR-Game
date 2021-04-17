using System;
using UnityEngine;

[Serializable()]
public struct Answer
{
    [SerializeField] private string _info;
    public string Info { get { return _info; } }

    [SerializeField] private bool _isCorrect;
    public bool IsCorrect { get { return _isCorrect; } }

}

[Serializable()]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestionSO", order = 2)]
public class Question : ScriptableObject
{

    [SerializeField] private int questionID;
    public int ID { get { return questionID; } }

    [SerializeField] [TextArea(10, 100)] private string _info = string.Empty;
    public string Info { get { return _info; } }

    [SerializeField] private String _hint = String.Empty;
    public string Hint { get { return _hint; } }

    [SerializeField] [TextArea(10, 100)] private String _explanation = String.Empty;
    public string Explanation { get { return _explanation; } }


    [SerializeField] private Answer[] _answers = null;
    public Answer[] Answers { get { return _answers; } }

    // Function to get the right answer beb
    public int GetCorrectAnswer()
    {

        for (int i = 0; i < _answers.Length; i++)
        {
            if (Answers[i].IsCorrect)
            {
                return i;
            }
        }
        return 0;
    }
}
