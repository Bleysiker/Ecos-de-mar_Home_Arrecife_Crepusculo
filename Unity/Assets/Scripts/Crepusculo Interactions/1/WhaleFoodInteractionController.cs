using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WhaleFoodInteractionController : MonoBehaviour
{
    [SerializeField] float wholeDelay=27f;
    [SerializeField] ParticleSystem inkEffect;
    [SerializeField] float nextInteractionDelay=2f;

    void Start()
    {
        StartCoroutine(WhaleArrive());
        //empieza el guia
        GuidePathTwilight.Instance.StartExperience();
    }

    IEnumerator WhaleArrive()
    {
        yield return new WaitForSeconds(wholeDelay);
        inkEffect.Play();
        yield return new WaitForSeconds(nextInteractionDelay);
        //se activa la interaccion de plancton
        FullTwilightFlow.Instance.ActivateInteraction(2, 0f, true);
    }
}
