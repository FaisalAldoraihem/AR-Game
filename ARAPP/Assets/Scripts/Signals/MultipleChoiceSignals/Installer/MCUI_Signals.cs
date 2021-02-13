using UnityEngine;
using Zenject;

public class MCUI_Signals : MonoInstaller
{
    [SerializeField]
    GameObject answerPrefab;
    public override void InstallBindings()
    {
        Container.DeclareSignal<AnswerPickedSignal>();
        Container.DeclareSignal<QuestionUpdatedSignal>();
        Container.DeclareSignal<QuestionAnswerdSignal>();
    }
}