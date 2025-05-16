using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectsMovement : MonoBehaviour
{
    [SerializeField] private Transform corner1; // Primer v�rtice del �rea
    [SerializeField] private Transform corner2; // Segundo v�rtice del �rea
    [SerializeField] private Transform corner3; // Tercer v�rtice del �rea
    [SerializeField] private Transform corner4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetRandomPointInArea()
    {
        // Encuentra el punto m�nimo y m�ximo en el �rea delimitada por los cuatro puntos
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

        // Genera un punto aleatorio dentro de estos l�mites
        return new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            Random.Range(minBounds.z, maxBounds.z)
        );
    }

    Quaternion GetRandomRotation()
    {
        // Genera una rotaci�n aleatoria en el espacio 3D
        return Quaternion.Euler(
            Random.Range(0f, 360f), // Rotaci�n en el eje Y (horizontal)
            Random.Range(0f, 360f), // Rotaci�n en el eje X
            Random.Range(0f, 360f)  // Rotaci�n en el eje Z
        );
    }
}
