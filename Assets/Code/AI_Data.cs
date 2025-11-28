using UnityEngine;

[CreateAssetMenu(fileName = "AI_Data", menuName = "Scriptable Objects/AI_Data")]
public class AI_Data : ScriptableObject
{
    public AI_Type aiType;
    public bool isHostile;
}
