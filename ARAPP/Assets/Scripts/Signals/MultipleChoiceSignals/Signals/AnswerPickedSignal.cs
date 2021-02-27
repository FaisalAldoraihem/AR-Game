public class AnswerPickedSignal
{
    public AnswerPickedSignal(AnswerData answer)
    {
        Answer = answer;
    }

    public AnswerData Answer
    {
        get; private set;
    }
}
