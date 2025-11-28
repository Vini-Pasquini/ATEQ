using UnityEngine;

public interface IState
{
    public void OnEnter();
    public EState OnUpdate();
    public void OnExit();
}
