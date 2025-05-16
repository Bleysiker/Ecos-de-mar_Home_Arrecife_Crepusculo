using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioSynchronization : MonoBehaviour
{
    [SerializeField] private GameObject joyStickUi;
    [SerializeField] private Transform  head;

    [SerializeField] private Animator animator;     
    [SerializeField] private Animator animator1;     
    [SerializeField] private Animator animator2;     
    [SerializeField] private Animator animator3;     
    [SerializeField] private string animacionTrigger = "SeaRise"; 
    [SerializeField] private float delayWaveRise,DelayTutorial;
    [SerializeField] private List<GameObject> ZoneBalls = new List<GameObject>();
    [SerializeField] private GameObject tutorialChecks;

    //private float spawDistance = 4f;
    private bool uiIsActive;
    void Awake()
    {
        
         StartCoroutine(StarWaveRise());
         StartCoroutine(StarTutorial());
        
    }

    private void LateUpdate()
    {
        if(uiIsActive)
        {
           UiFollowPlayer.FollowPlayerLooK(joyStickUi, head);
        }
    }

    private IEnumerator StarWaveRise()
    {
       yield return new WaitForSeconds(delayWaveRise);
       animator.SetTrigger(animacionTrigger);
       animator1.SetTrigger(animacionTrigger);
       animator2.SetTrigger(animacionTrigger);
       animator3.SetTrigger(animacionTrigger);
       
    }

    private IEnumerator StarTutorial()
    {
        yield return new WaitForSeconds(DelayTutorial);
        
        joyStickUi.SetActive(true);
        uiIsActive = true;

        tutorialChecks.SetActive(true);
        ZoneBalls[0].SetActive(true);
        Material objMaterial = ZoneBalls[0].GetComponentInChildren<Renderer>().material;
        if (objMaterial.HasProperty("_Disolve"))
        {
            float dissolveValue = 1f;
            float dissolveSpeed = 0.5f;  // Velocidad de cambio

            // Ciclo para disminuir el valor de _Dissolve
            while (dissolveValue > 0)
            {
                dissolveValue -= Time.deltaTime * dissolveSpeed;
                objMaterial.SetFloat("_Disolve", dissolveValue);
                yield return null; // Espera un frame antes de continuar
            }

            // Asegurarse que _Dissolve quede en 0
            objMaterial.SetFloat("_Disolve", 0f);
            //outline.SetActive(true);
        }
        else
        {
            Debug.LogWarning("El material no tiene el parámetro _Dissolve.");
        }
    }
    

    public void IsNoLongerActive()
    {
        uiIsActive = false;
        joyStickUi.SetActive(false);
        //Invoke("DisableZoneBall", 3f);//cambiar a destruir
        Destroy(ZoneBalls[0],3f);
    }

    

    void DisableZoneBall()
    {
        ZoneBalls[0].SetActive(false);
    }
}
