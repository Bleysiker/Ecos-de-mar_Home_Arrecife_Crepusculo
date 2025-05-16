using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using EPOOutline;

public class JellyFishInteractionController : MonoBehaviour
{
    [SerializeField] SwitchFishPaths swithcJelly;
    [SerializeField] VRInteractionHandler interactionHandler;
    XRSimpleInteractable jellyInteractable;
    bool onInteraction;

    Outlinable outline;
    Collider coll;

    // Start is called before the first frame update

    private void Awake()
    {
        jellyInteractable = GetComponent<XRSimpleInteractable>();
        outline = GetComponent<Outlinable>();
        coll = GetComponent<Collider>();
    }
    

    public void StartInteraction(XRSimpleInteractable interactable)
    {
        if(interactable==jellyInteractable && !onInteraction) {
            StartCoroutine(JellyChangeMove());
        }
    }

    IEnumerator JellyChangeMove()
    {
        ActiveInteraction(false);
        swithcJelly.SwitchPath();
        JellyFishManager.Instance.AppearCard();
        yield return new WaitForSeconds(1f);
        swithcJelly.RestoreOriginalPath();
    }


    void ActiveInteraction(bool value)
    {
        outline.enabled = value;
        coll.enabled = value;
        jellyInteractable.enabled = value;
        onInteraction = !value;
    }

    
    //testing 

    private void OnDisable()
    {
        if (!onInteraction) {
            StartCoroutine(JellyChangeMove());
        }
    }

}
