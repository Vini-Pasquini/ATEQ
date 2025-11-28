using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DroneSmoothPath : MonoBehaviour
{
    [Header("Path Settings")]
    public LineRenderer lineRenderer;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    [Range(2, 20)] public int smoothness = 10;  // mais pontos = curva mais suave

    private Vector3[] rawPoints;    // pontos originais do LineRenderer
    private Vector3[] smoothPoints; // pontos interpolados(curva) --> deve pesar bem mais(ver se compensa)
    private int currentIndex = 0;

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        rawPoints = new Vector3[lineRenderer.positionCount];    // pega os pontos originais do LineRenderer
        lineRenderer.GetPositions(rawPoints);


        smoothPoints = GenerateSmoothPath(rawPoints, smoothness);           // gera pontos suavizados
    }

    void Update()
    {
        if (smoothPoints == null || smoothPoints.Length == 0) return;

        Vector3 target = smoothPoints[currentIndex];
        Vector3 direction = (target - transform.position).normalized;


        transform.position += direction * speed * Time.deltaTime;           // move o drone


        if (direction.sqrMagnitude > 0.001f)            // rotaciona suave
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }


        if (Vector3.Distance(transform.position, target) < 0.2f)            // checa se chegou no ponto atual
        {
            currentIndex++;
            if (currentIndex >= smoothPoints.Length)
                currentIndex = 0;   // loop
        }
    }


    

    Vector3[] GenerateSmoothPath(Vector3[] points, int subdivisions)    // gera pontos suavizados usando Catmull-Rom Spline (loucura tá)
    {
        if (points.Length < 2) return points;

        var smoothList = new System.Collections.Generic.List<Vector3>();

        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p0 = points[(i - 1 + points.Length) % points.Length];
            Vector3 p1 = points[i];
            Vector3 p2 = points[(i + 1) % points.Length];
            Vector3 p3 = points[(i + 2) % points.Length];

            for (int j = 0; j < subdivisions; j++)
            {
                float t = j / (float)subdivisions;
                Vector3 newPoint = CatmullRom(p0, p1, p2, p3, t);
                smoothList.Add(newPoint);
            }
        }
        return smoothList.ToArray();
    }


    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) // fórmula da Catmull-Rom (emoji do nerd)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }

    void OnDrawGizmosSelected()
    {
        if (smoothPoints == null || smoothPoints.Length < 2) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < smoothPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(smoothPoints[i], smoothPoints[i + 1]);
        }
    }
}