using UnityEngine;

public class TrashManager : MonoBehaviour
{
    public static TrashManager Instance;

    public Transform instancingCenter; // Centro de instanciación
    public Vector3 movementLimits = new Vector3(10f, 0f, 10f); // Límites de movimiento
    public int numTrash = 20; // Cantidad total de basura a generar
    public GameObject[] interactiveTrash; // Basura interactuable en escena
    public GameObject[] nonInteractiveTrashPrefabs; // Prefabs de basura no interactuable

    private GameObject[] allTrash; // Todos los objetos de basura

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        allTrash = new GameObject[numTrash];
        SetupExistingInteractiveTrash();
        InstantiateNonInteractiveTrash();
    }

    private void SetupExistingInteractiveTrash()
    {
        int count = Mathf.Min(interactiveTrash.Length, numTrash);

        for (int i = 0; i < count; i++)
        {
            if (interactiveTrash[i] != null)
            {
                // Colocar los interactuables dentro de los límites de instanciación
                interactiveTrash[i].transform.position = instancingCenter.position + new Vector3(
                    Random.Range(-movementLimits.x, movementLimits.x),
                    0.0f,
                    Random.Range(-movementLimits.z, movementLimits.z));

                allTrash[i] = interactiveTrash[i];
            }
            else
            {
                Debug.LogWarning($"El objeto interactivo en la posición {i} es nulo. Revisa el inspector.");
            }
        }
    }

    private void InstantiateNonInteractiveTrash()
    {
        int startIndex = interactiveTrash.Length;
        int nonInteractiveCount = Mathf.Min(nonInteractiveTrashPrefabs.Length, numTrash - startIndex);

        for (int i = 0; i < nonInteractiveCount; i++)
        {
            Vector3 position = instancingCenter.position + new Vector3(
                Random.Range(-movementLimits.x, movementLimits.x),
                0.0f,
                Random.Range(-movementLimits.z, movementLimits.z));

            GameObject newTrash = Instantiate(nonInteractiveTrashPrefabs[i], position, Quaternion.identity, this.transform);
            allTrash[startIndex + i] = newTrash;
        }
    }
}
