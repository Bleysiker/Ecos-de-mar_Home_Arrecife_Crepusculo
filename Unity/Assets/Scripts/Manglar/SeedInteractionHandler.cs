using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SeedInteractionHandler : MonoBehaviour
{
    //[SerializeField] private VRInteractionHandler interactionHandler;
   
  //  private XRSimpleInteractable interactable;

    void Start()
    {
       // interactable = GetComponent<XRSimpleInteractable>();

      //  if (interactable != null && interactionHandler != null)
       // {
            // Configura la interacción para esta semilla
          //  interactionHandler.AddInteractable(interactable);

            // Suscribirse al evento específico para esta semilla
          //  interactionHandler.OnInteractionStarted += OnSeedInteractionStarted;
     //   }
      //  else
      //  {
       //     Debug.LogError("Error: Falta alguna referencia en SeedInteractionHandler.");
      //  }
    }

    // Método manejador de la interacción con la semilla
    private void OnSeedInteractionStarted(XRSimpleInteractable selectedInteractable)
    {
        // Verificar si el interactuable seleccionado es la semilla
       // if (selectedInteractable == interactable)
       // {
          //  HandleInteraction(); // Iniciar la interacción
        //}
    }

   

    void OnDestroy()
    {
        // Desuscribirse para evitar errores cuando se destruya el objeto
        //interactionHandler.OnInteractionStarted -= OnSeedInteractionStarted;
    }
}

