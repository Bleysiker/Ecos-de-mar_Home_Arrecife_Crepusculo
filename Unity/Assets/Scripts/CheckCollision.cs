using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField]private PathMover pathMover;
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionó tiene el tag "Boat"
        if (collision.gameObject.CompareTag("Boat"))
        {
            // Llama al método PausePath de PathMover
            if (pathMover != null)
            {
                //pathMover.PausePath();
                Debug.Log("Path pausado al colisionar con el objeto 'Boat'.");
            }
        }

        Debug.Log($" colisionar con el objeto'{collision}'." );
    }
}
