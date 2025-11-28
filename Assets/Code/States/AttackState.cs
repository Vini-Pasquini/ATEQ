using UnityEngine;

public class AttackState : IState
{
    private GameObject _self;

    public AttackState(GameObject self)
    {
        this._self = self;
    }

    public void OnEnter()
    {

    }

    public EState OnUpdate()
    {
        return EState.Attack;
    }

    public void OnExit()
    {

    }
}
