using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TrashAndFishTransition : MonoBehaviour
{
    [SerializeField] private Transform fishGroup; // Transform que agrupa los peces
    [SerializeField] private Transform trashGroup; // Transform que agrupa la basura
    [SerializeField] private Transform upTargetTransform; // Transform objetivo hacia arriba para los peces
    [SerializeField] private Transform downTargetTransform; // Transform objetivo hacia abajo para la basura
    [SerializeField] private float transitionDuration = 2f;
    [SerializeField] private float transitionDuration1 = 8f;
    [SerializeField] private VRInteractionHandler interactionHandler; // Referencia al VRInteractionHandler
    private int interactionCount = 0;
    private int maxInteractions = 1;

    private bool isTransitioning = false;
    private bool hasDoneInverseTransition = false; // Controla si la transici�n inversa se ha realizado

    void Start()
    {
        // Iniciar la transici�n inversa solo una vez
        StartCoroutine(InverseTrashAndFishTransitionCoroutine());
        Invoke("SetupTrashInteractions", 5f);
    }

    private void SetupTrashInteractions()
    {
        foreach (Transform trash in trashGroup)
        {
            // Obtener el componente de interacci�n de cada objeto de basura
            XRSimpleInteractable interactable = trash.GetComponent<XRSimpleInteractable>();

            // Si el objeto tiene un componente interactable, a�adirlo al manejador de interacci�n
            if (interactable != null)
            {
                interactionHandler.AddInteractable(interactable);
            }
        }

        // Suscribirse al evento de interacci�n
        interactionHandler.OnInteractionStarted += HandleTrashInteraction;
    }

    private void HandleTrashInteraction(XRSimpleInteractable interactable)
    {
        // Si ya se alcanz� el n�mero m�ximo de interacciones o est� en transici�n, no hacer nada
        if (interactionCount >= maxInteractions || isTransitioning) return;

        interactionCount++;
        StartCoroutine(TrashAndFishTransitionCoroutine());

        // Si ya se alcanz� el l�mite de interacciones, deshabilitar la interacci�n
        if (interactionCount >= maxInteractions)
        {
            DisableTrashInteraction();
        }
    }

    // Corutina de transici�n regular: Basura baja, peces suben
    private IEnumerator TrashAndFishTransitionCoroutine()
    {
        if (isTransitioning) yield break; // Asegura que solo una transici�n est� activa
        isTransitioning = true;

        float elapsedTime = 0f;
        bool trahsIsDesactive = false;
        // Guardar las posiciones iniciales de cada objeto
        Vector3[] initialTrashPositions = new Vector3[trashGroup.childCount];
        Vector3[] initialFishPositions = new Vector3[fishGroup.childCount];

        for (int i = 0; i < trashGroup.childCount; i++)
        {
            initialTrashPositions[i] = trashGroup.GetChild(i).position;
        }

        for (int i = 0; i < fishGroup.childCount; i++)
        {
            initialFishPositions[i] = fishGroup.GetChild(i).position;
        }

        Vector3 targetTrashPos = downTargetTransform.position;
        Vector3 targetFishPos = upTargetTransform.position;

        // Realizar la transici�n simult�nea de la basura descendiendo y los peces subiendo
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Mover cada pez hacia la posici�n objetivo
            for (int i = 0; i < fishGroup.childCount; i++)
            {
                fishGroup.GetChild(i).position = Vector3.Lerp(initialFishPositions[i], targetFishPos, t);
            }


            // Mover cada objeto de basura hacia la posici�n objetivo
            
            
            // Esperar 3 segundos antes de desactivar o destruir la basura
            if(!trahsIsDesactive) yield return new WaitForSeconds(3f);

            // Desactivar o destruir la basura despu�s de 3 segundos
            for (int i = 0; i < trashGroup.childCount; i++)
            {
                trashGroup.GetChild(i).gameObject.SetActive(false); // Si prefieres, puedes usar Destroy() para eliminar los objetos
                trahsIsDesactive = true;
            }

            
            yield return null;
        }

       

        isTransitioning = false;
    }

    // Corutina de transici�n inversa: Se ejecuta solo una vez al inicio
    private IEnumerator InverseTrashAndFishTransitionCoroutine()
    {
        if (hasDoneInverseTransition) yield break; // Se asegura que solo ocurra una vez
        hasDoneInverseTransition = true;

        float elapsedTime = 0f;

        // Posiciones iniciales y objetivo para la transici�n inversa
        Vector3 initialTrashPos = trashGroup.position;
        Vector3 targetTrashPos = upTargetTransform.position; // Ahora la basura sube
        Vector3 initialFishPos = fishGroup.position;
        Vector3 targetFishPos = downTargetTransform.position; // Ahora los peces bajan

        // Realizar la transici�n inversa: basura subiendo y peces bajando
        while (elapsedTime < transitionDuration1)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration1;

            // Mover la basura hacia arriba
            trashGroup.position = Vector3.Lerp(initialTrashPos, targetTrashPos, t);

            // Mover los peces hacia abajo
            fishGroup.position = Vector3.Lerp(initialFishPos, targetFishPos, t);

            yield return null;
        }
    }

    private void DisableTrashInteraction()
    {
        foreach (Transform trash in trashGroup)
        {
            XRSimpleInteractable interactable = trash.GetComponent<XRSimpleInteractable>();
            if (interactable != null)
            {
                trash.GetComponent<Outline>().enabled = false;
                trash.GetComponent<Collider>().enabled = false;
                interactionHandler.RemoveInteractable(interactable);
            }
        }
    }
}
