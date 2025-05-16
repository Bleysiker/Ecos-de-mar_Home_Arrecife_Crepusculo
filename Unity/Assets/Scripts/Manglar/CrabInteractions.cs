using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CrabInteractions : MonoBehaviour
{
    [SerializeField] private GameObject crab; // El cangrejo
    [SerializeField] private Outline crabOutline; // Outline del cangrejo
    [SerializeField] private VRInteractionHandler interactionHandler; // Referencia al VRInteractionHandler
    [SerializeField] private AudioManager audioInstance; // Audio Manager para reproducir los sonidos
    [SerializeField] private GameObject fisherman; // Pescador que aparece
    [SerializeField] private GameObject fish; // Peces que aparecerán
   // [SerializeField] private GameObject trash; // Basura que aparecerá
    [SerializeField] private GameObject boat; // Bote con outline
    [SerializeField] private GameObject StartedFish; // Bote con outline

    private bool hasGrabbed = false;
    private bool audioPlayed = false;
    private bool FisherInteract = false;
   // private bool isPlaying = false;
    private float activationtime = 24.181f; // Tiempo para activar el pescador

    private XRSimpleInteractable crabInteractable;

    void Start()
    {
        ActivateFisherman();
        // Obtener el interactuable del cangrejo
        crabInteractable = crab.GetComponent<XRSimpleInteractable>();

        if (crabInteractable != null && interactionHandler != null)
        {
            // Añadir la interacción con el cangrejo al VRInteractionHandler
            interactionHandler.AddInteractable(crabInteractable);

            // Suscribirse al evento de interacción
            interactionHandler.OnInteractionStarted += OnCrabInteractionStarted;
        }
        else
        {
            Debug.LogError("Error: Falta alguna referencia en CrabInteractions.");
        }
    }

    void Update()
    {
        HandleTimedActions();
    }

    // Método invocado cuando se interactúa con el cangrejo
    private void OnCrabInteractionStarted(XRSimpleInteractable interactable)
    {
        if (interactable == crabInteractable && !hasGrabbed)
        {
            StartCrabInteraction();
        }
    }

    private void StartCrabInteraction()
    {
        // Iniciar el audio cuando se interactúa con el cangrejo
        //audioInstance.InitializeVoice(FmodEvents.instance.Manglar4, crab.transform.position);
        //audioInstance.PlayOneShot(FmodEvents.instance.Manglar3, crab.transform.position);
        crabOutline.enabled = false; // Desactivar el outline del cangrejo
        crab.GetComponent<XRSimpleInteractable>().enabled = false;
        crab.GetComponent<Collider>().enabled = false;
        // Remover la capacidad de interactuar con el cangrejo
        interactionHandler.RemoveInteractable(crabInteractable);
        hasGrabbed = true; // Marcar que la interacción fue completada
        audioPlayed = true; // Marcar que el audio está en reproducción
    }

    private void HandleTimedActions()
    {
        if (hasGrabbed && audioPlayed)
        {
            float currentTime = audioInstance.GetTimelinePosition() / 1000f;
           // print(currentTime);

            // Usamos if-else para manejar los eventos en función del tiempo
            if (currentTime >= 0.11f && currentTime < activationtime)
            {
                // Activar el pescador si no se ha activado antes
                
            }

            if (FisherInteract)
            {
                Invoke("ActiveFish", 12f);
            }
             
          //  else if (currentTime >= activationtime && currentTime < 61.21f)
         //   {
                // Activar los peces en el momento de la interacción
            //    ActivateFish();
          //  }
           // else if (currentTime >= 61.21f && currentTime < 73.20f)
           // {
                // Activar la basura
           //     ActivateTrash();
          //  }
           // else if (currentTime >= 73.20f)
           // {
               // audioInstance.CreateInstance(FmodEvents.instance.Manglar4);
             //   audioInstance.InitializeVoice(FmodEvents.instance.Manglar5, this.transform.position);
            //    EndTrashInteraction();
            //}
        }
    }

    private void ActiveFish()
    {
        StartedFish.SetActive(true);
    }

    // Funciones para gestionar cada acción
    private void ActivateFisherman()
    {
        if (!fisherman.activeInHierarchy)
        {
            fisherman.SetActive(true);
        }
    }

    public void ActivateFish()
    {
        //audioInstance.CleanUp();
        //audioInstance.InitializeVoice(FmodEvents.instance.Manglar6, this.transform.position);

        if (!fish.activeInHierarchy)
        {
            fish.SetActive(true);
            print("peces activados");
        }
    }

   

    public void FisherManVoice()
    {
        //audioInstance.InitializeVoice(FmodEvents.instance.Manglar5, this.transform.position);
        FisherInteract = true;
    }

    void OnDestroy()
    {
        // Desuscribirse para evitar errores cuando se destruya el objeto
        if (interactionHandler != null)
        {
            interactionHandler.OnInteractionStarted -= OnCrabInteractionStarted;
        }
    }
}
