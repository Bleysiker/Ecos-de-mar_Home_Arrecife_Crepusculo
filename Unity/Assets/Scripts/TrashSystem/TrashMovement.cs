using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    private Vector3 direction;

    private void Start()
    {
        // Dirección inicial aleatoria
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 center = TrashManager.Instance.instancingCenter.position;
        Vector3 limits = TrashManager.Instance.movementLimits;

        // Movimiento calculado
        Vector3 newPos = transform.position + direction * speed * Time.deltaTime;

        // Limitar posición dentro del área de movimiento
        newPos.x = Mathf.Clamp(newPos.x, center.x - limits.x, center.x + limits.x);
        newPos.z = Mathf.Clamp(newPos.z, center.z - limits.z, center.z + limits.z);

        transform.position = newPos;

        // Si alcanza el borde, cambia la dirección
        if (newPos.x == center.x - limits.x || newPos.x == center.x + limits.x)
        {
            direction.x *= -1;
        }
        if (newPos.z == center.z - limits.z || newPos.z == center.z + limits.z)
        {
            direction.z *= -1;
        }
    }
}
