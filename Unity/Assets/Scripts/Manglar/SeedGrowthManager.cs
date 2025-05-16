using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SeedGrowthManager : MonoBehaviour
{
    [SerializeField] private GameObject[] seeds;  // Las semillas con las que se puede interactuar
    [SerializeField] private GameObject[] mangroveStages;  // Fases del manglar
    [SerializeField] private GameObject crab;
    [SerializeField] private GameObject seedManager;

    private bool hasSelectedSeed = false;
    private int currentStage = 0;

    void Start()
    {
        DeactivateAllStages();
    }

    private void DeactivateAllStages()
    {
        foreach (var stage in mangroveStages)
        {
            stage.SetActive(false);
        }
        crab.SetActive(false);
    }

    public void StartSeeds()
    {
        foreach (GameObject seed in seeds)
        {
            seed.SetActive(true); // Activa las semillas cuando sea el momento adecuado
        }
    }

    public void SelectSeed()
    {
        
            // Desactiva todas las semillas al seleccionar una
            foreach (GameObject seed in seeds)
            {
                seed.SetActive(false);
            }

           // StartMangroveGrowth(); // Inicia el crecimiento del manglar
            hasSelectedSeed = true;
        
    }

    public void StartMangroveGrowth()
    {
        StartCoroutine(GrowMangrove());
    }

    private IEnumerator GrowMangrove()
    {
        while (currentStage < mangroveStages.Length)
        {
            if (currentStage > 0)
            {
                mangroveStages[currentStage - 1].SetActive(false); // Desactivar la fase anterior
            }

            mangroveStages[currentStage].SetActive(true); // Activar la siguiente fase
            currentStage++;

            yield return new WaitForSeconds(2f); // Esperar 2 segundos entre fases
        }

        // Cuando el manglar ha alcanzado su fase completa
        if (currentStage == mangroveStages.Length)
        {
            mangroveStages[3].GetComponent<Outline>().enabled = false;
            StartCoroutine(ActivateCrab()); // Iniciar la interacción del cangrejo
        }
    }

    private IEnumerator ActivateCrab()
    {
        yield return new WaitForSeconds(5f); // Tiempo de espera antes de mostrar el cangrejo
        crab.SetActive(true);
        Destroy(seedManager);
        Destroy(this);
    }
}
