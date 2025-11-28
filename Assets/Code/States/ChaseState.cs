using UnityEngine;

public class ChaseState : IState
{
    private GameObject _self;

    public ChaseState(GameObject self)
    {
        this._self = self;
    }

    public void OnEnter()
    {

    }

    public EState OnUpdate()
    {
        return EState.Chase;
    }

    public void OnExit()
    {

    }
}
