using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RunaInteraction : MonoBehaviour
{
    [SerializeField] private VRInteractionHandler interactionHandler; // Referencia al VRInteractionHandler
    [SerializeField] private string sceneName; // Nombre de la escena para identificar el audio a reproducir
    [SerializeField] private Outline outline;
    [SerializeField] private GameObject spiralLeaves;
    [SerializeField] private GameObject OutlineGlow;
    [SerializeField] private MangroveInteraction mangrove;

    private bool hasInteracted = false;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        // Obtener el componente interactuable de la runa y suscribirlo al VRInteractionHandler
        XRSimpleInteractable interactable = GetComponentInChildren<XRSimpleInteractable>();
        //XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        interactionHandler.AddInteractable(interactable);
        interactionHandler.OnInteractionStarted += HandleRunaInteraction;
    }

    private void HandleRunaInteraction(XRSimpleInteractable interactable)
    {
        if (hasInteracted) return; // Evita que la interacción ocurra más de una vez

        GetComponentInChildren<Outline>().enabled = false;
        GetComponentInChildren<XRSimpleInteractable>().enabled = false;
        spiralLeaves.SetActive(true);
        OutlineGlow.SetActive(false);
        mangrove.startInteraction = true;
        AudioManager.instance.PlayZoneAudio(sceneName);
        hasInteracted = true; // Marca que ya se ha realizado la interacción
    }

    private void OnDestroy()
    {
        // Limpiar la suscripción al destruir el objeto
        interactionHandler.OnInteractionStarted -= HandleRunaInteraction;
    }
}

