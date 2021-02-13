public class QuestionUpdatedSignal
{
    public QuestionUpdatedSignal(Question question)
    {
        Question = question;
    }

    public Question Question
    {
        get; private set;
    }
}
