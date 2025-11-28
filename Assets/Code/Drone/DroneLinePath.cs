using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DroneLinePath : MonoBehaviour
{
    [Header("Path Settings")]
    public LineRenderer lineRenderer;   // a linha que define o caminho aqui><
    public float speed = 5f;            // velocidade do dorone
    public float rotationSpeed = 5f;    // suavidade da rotação
    public float reachThreshold = 0.2f; // distância para considerar que chegou no ponto certo

    private int currentPointIndex = 0;  // o ponto que tá atualmente
    private Vector3[] pathPoints;       // quantos pontos vai te

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();


        pathPoints = new Vector3[lineRenderer.positionCount];   // copia os pontos do LineRenderer
        lineRenderer.GetPositions(pathPoints);
    }

    void Update()
    {
        if (pathPoints == null || pathPoints.Length == 0) return;

        Vector3 target = pathPoints[currentPointIndex];
        Vector3 direction = (target - transform.position).normalized;


        transform.position += direction * speed * Time.deltaTime;   // movimento para frente


        if (direction.sqrMagnitude > 0.001f)    // rotação suave sempre para frente
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target) < reachThreshold)  // chegou perto do ponto atual -> passa para o próximo
        {
            currentPointIndex++;

            if (currentPointIndex >= pathPoints.Length) // se chegou no fim, volta pro início (loop)
                currentPointIndex = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (lineRenderer == null) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = lineRenderer.GetPosition(i);
            Gizmos.DrawSphere(pos, 0.15f);
        }
    }
}
