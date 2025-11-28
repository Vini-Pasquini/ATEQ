using UnityEngine;

public class IdleState : IState
{
    private GameObject _self;

    public IdleState(GameObject self)
    {
        this._self = self;
    }

    public void OnEnter()
    {
        
    }

    public EState OnUpdate()
    {
        this._self.transform.position += Vector3.up * Time.deltaTime;

        return EState.Idle;
    }

    public void OnExit()
    {

    }
}
