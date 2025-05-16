using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints; // Puntos de la ruta a seguir
    [SerializeField] private float moveSpeed = 5f;   // Velocidad de movimiento
    [SerializeField] private float rotationSpeed = 5f; // Velocidad de rotación
    [SerializeField] private bool loopPath = true;    // ¿Debe hacer un bucle al final?

    private int currentPointIndex = 0;                // Índice del punto actual en la ruta

    void Start()
    {
        if (pathPoints.Length == 0)
        {
            Debug.LogError("No se han asignado puntos en el camino.");
            enabled = false;
        }
        else
        {
            transform.position = pathPoints[0].position;
        }
    }

    void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (currentPointIndex < pathPoints.Length)
        {
            Transform targetPoint = pathPoints[currentPointIndex];
            Vector3 targetDirection = (targetPoint.position - transform.position).normalized;

            // Moverse hacia el siguiente punto
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

            // Rotación suave hacia el siguiente punto
            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Verificar si alcanzamos el punto actual
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                currentPointIndex++;

                // Si alcanzamos el último punto
                if (currentPointIndex >= pathPoints.Length)
                {
                    if (loopPath)
                    {
                        currentPointIndex = 0; // Reiniciar para hacer un bucle
                    }
                    else
                    {
                        enabled = false; // Desactivar este script al final de la ruta si no hay bucle
                    }
                }
            }
        }
    }
}
