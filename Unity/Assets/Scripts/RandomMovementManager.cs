using UnityEngine;

public class RandomMovementManager : MonoBehaviour
{
    [SerializeField] private GameObject[] fishObjects; // Objetos que representan los peces
    [SerializeField] private GameObject[] trashObjects; // Objetos que representan la basura
    [SerializeField] private Transform corner1; // Primer vértice del área
    [SerializeField] private Transform corner2; // Segundo vértice del área
    [SerializeField] private Transform corner3; // Tercer vértice del área
    [SerializeField] private Transform corner4; // Cuarto vértice del área

    [SerializeField] private float moveSpeed = 1.5f; // Velocidad de movimiento de los objetos
    [SerializeField] private float rotationSpeed = 2f; // Velocidad de rotación de la basura
    [SerializeField] private float changeDirectionInterval = 3f; // Tiempo para cambiar de dirección
    [SerializeField] private float waterSurfaceHeight = 1.0f; // Altura máxima para los peces (superficie del agua)
    [SerializeField] private float waterBottomHeight = -3.0f; // Altura mínima para los peces (fondo del agua)

    private Vector3[] fishTargets; // Posiciones objetivo de los peces
    private Vector3[] trashTargets; // Posiciones objetivo de la basura
    private Quaternion[] trashTargetRotations; // Rotaciones objetivo solo para la basura
    private float timer;

    void Start()
    {
        fishTargets = new Vector3[fishObjects.Length];
        trashTargets = new Vector3[trashObjects.Length];
        trashTargetRotations = new Quaternion[trashObjects.Length];
        SetRandomTargets();
    }

    void Update()
    {
        MoveObjects(fishObjects, fishTargets);
        MoveAndRotateTrash(trashObjects, trashTargets, trashTargetRotations);

        timer += Time.deltaTime;

        // Cambiar la dirección y rotación después de un intervalo
        if (timer >= changeDirectionInterval)
        {
            SetRandomTargets();
            timer = 0f;
        }
    }

    void MoveObjects(GameObject[] objects, Vector3[] targets)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            // Mover el objeto hacia la posición objetivo
            objects[i].transform.position = Vector3.MoveTowards(objects[i].transform.position, targets[i], moveSpeed * Time.deltaTime);
        }
    }

    void MoveAndRotateTrash(GameObject[] trashObjects, Vector3[] targets, Quaternion[] rotations)
    {
        for (int i = 0; i < trashObjects.Length; i++)
        {
            // Mover la basura hacia la posición objetivo
            trashObjects[i].transform.position = Vector3.MoveTowards(trashObjects[i].transform.position, targets[i], moveSpeed * Time.deltaTime);

            // Rotar la basura hacia la rotación objetivo
            trashObjects[i].transform.rotation = Quaternion.Slerp(trashObjects[i].transform.rotation, rotations[i], rotationSpeed * Time.deltaTime);
        }
    }

    void SetRandomTargets()
    {
        // Generar nuevas posiciones para los peces dentro del área delimitada
        for (int i = 0; i < fishObjects.Length; i++)
        {
            fishTargets[i] = GetRandomPointInArea(true); // Limitar la altura de los peces
        }

        // Generar nuevas posiciones y rotaciones objetivo para la basura
        for (int i = 0; i < trashObjects.Length; i++)
        {
            trashTargets[i] = GetRandomPointInArea(false); // No limitamos la altura de la basura
            trashTargetRotations[i] = GetRandomRotation();
        }
    }

    Vector3 GetRandomPointInArea(bool isFish)
    {
        // Encuentra el punto mínimo y máximo en el área delimitada por los cuatro puntos
        Vector3 minBounds = new Vector3(
            Mathf.Min(corner1.position.x, corner2.position.x, corner3.position.x, corner4.position.x),
            Mathf.Min(corner1.position.y, corner2.position.y, corner3.position.y, corner4.position.y),
            Mathf.Min(corner1.position.z, corner2.position.z, corner3.position.z, corner4.position.z)
        );

        Vector3 maxBounds = new Vector3(
            Mathf.Max(corner1.position.x, corner2.position.x, corner3.position.x, corner4.position.x),
            Mathf.Max(corner1.position.y, corner2.position.y, corner3.position.y, corner4.position.y),
            Mathf.Max(corner1.position.z, corner2.position.z, corner3.position.z, corner4.position.z)
        );

        // Si el objeto es un pez, limitamos su altura entre el fondo y la superficie del agua
        if (isFish)
        {
            return new Vector3(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(waterBottomHeight, waterSurfaceHeight), // Controlamos la altura del pez
                Random.Range(minBounds.z, maxBounds.z)
            );
        }
        else
        {
            // Para la basura, no limitamos la altura
            return new Vector3(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );
        }
    }

    Quaternion GetRandomRotation()
    {
        // Genera una rotación aleatoria en el espacio 3D
        return Quaternion.Euler(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f)
        );
    }
}
