using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using EPOOutline;
public class LastInteractionManager : MonoBehaviour
{
    public static LastInteractionManager Instance { get; private set; }

    public List<GameObject> fishes;
    public List<Outlinable> outlines;
    public List<XRSimpleInteractable> interactables;
    public List<SwitchFishPaths> endPaths;
    public List<Collider> colliders;
    [SerializeField] VRInteractionHandler interactionHandler;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject fish in fishes) {
            XRSimpleInteractable interactable = fish.GetComponent<XRSimpleInteractable>();
            if (interactable != null) {
                interactionHandler.AddInteractable(interactable);
            } else {
                Debug.LogWarning($"El objeto {fish.name} no tiene un componente XRSimpleInteractable.");
            }
            interactionHandler.OnInteractionStarted += fish.GetComponent<LastInteractionController>().StartInteraction;
        }
        //desactiva interaccion 4
        FullTwilightFlow.Instance.ActivateInteraction(4, 5f, false);
    }

    public void ActivateInteractiveObject(int i, bool value)
    {
        outlines[i].enabled = value;
        interactables[i].enabled = value;
        colliders[i].enabled = value;
    }

    public void EndInteraction()
    {
        foreach (SwitchFishPaths endP in endPaths) {
            endP.SwitchPath();
        }
        //Initiate.Fade("Arrecife", Color.white, 6f); revisar
    }

    private void OnDestroy()
    {
        foreach (GameObject fish in fishes) {

            interactionHandler.OnInteractionStarted -= fish.GetComponent<LastInteractionController>().StartInteraction;
        }
    }
}
