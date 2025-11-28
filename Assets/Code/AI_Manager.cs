using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    private AI_Agent[] _agents;

    [SerializeField] private GameObject _agentPrefab;
    [SerializeField] private AI_Data _testData;

    private void Start()
    {
        this._agents = new AI_Agent[]
        {
            new AI_Agent(this._testData, GameObject.Instantiate(this._agentPrefab)),
        };
    }

    private void Update()
    {
        for (int i = 0; i < this._agents.Length; i++)
        {
            this._agents[i].CustomUpdate();
        }
    }
}
