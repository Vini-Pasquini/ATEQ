using UnityEngine;

public class AI_Agent
{
    private AI_Data _agentData;

    private IState[] _states;
    private EState _currentState;

    private GameObject _gameObject;

    public AI_Agent(AI_Data agentData, GameObject gameObject)
    {
        this._gameObject = gameObject;

        this._agentData = agentData;
        this._currentState = EState.Idle;

        this._states = new IState[]
        {
            new IdleState(this._gameObject),
            new ChaseState(this._gameObject),
            new AttackState(this._gameObject),
        };
    }

    public void CustomUpdate()
    {
        EState nextState = this._states[(int)this._currentState].OnUpdate();

        if (nextState != this._currentState)
        {
            this._states[(int)this._currentState].OnExit();
            this._currentState = nextState;
            this._states[(int)this._currentState].OnEnter();
        }
    }
}
