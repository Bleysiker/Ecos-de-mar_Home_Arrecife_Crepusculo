using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class MangroveInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] manglares; // Leñoso, Arbustivo, Denso, Colorado
    [SerializeField] private GameObject[] uiMangles; // UI correspondientes a cada manglar
    [SerializeField] private GameObject growthSeed;
    [SerializeField] private GameObject seed;
    [SerializeField] private ParticleSystem spiralLeaves;
    [SerializeField] private AudioManager audioInstance;
    [SerializeField] private VRInteractionHandler interactionHandler;

    public bool startInteraction = false;
    private bool spiralend = false, interactionEnd = false, interactionCompleted = false;
    private float timer = 0f, time;
    private int currentManglarIndex = 0;
    private float[] lightUpTimes = { 23.676f, 21.914f, 23.835f, 26.347f };
    private float[] audioCutPoints = { 1.872f, 3.670f }; // Puntos de corte del audio
    private int lastCutIndex = 0; // Para saber el último corte alcanzado

    void Start()
    {
        foreach (GameObject manglar in manglares)
        {
            XRSimpleInteractable interactable = manglar.GetComponent<XRSimpleInteractable>();

            if (interactable != null)
            {
                interactionHandler.AddInteractable(interactable);
            }
            else
            {
                Debug.LogWarning($"El objeto {manglar.name} no tiene un componente XRSimpleInteractable.");
            }
        }
        interactionHandler.OnInteractionStarted += OnMangroveInteractionStarted;

        // Desactivar todos los UI inicialmente
        foreach (GameObject ui in uiMangles)
        {
            ui.SetActive(false);
        }
    }

    public int GetManglarByIndex(int i)
    {
        return i;
    }
    public void SetManglarState(int i,bool b)
    {

    }
    void Update()
    {
        if (!interactionCompleted && startInteraction)
        {
            if (!spiralend) Invoke("SpiralParticules", 11f);
            timer += Time.deltaTime;

            if (currentManglarIndex == 0 && timer >= lightUpTimes[0])
            {
                LightUpManglar(manglares[0], true);
                EnableManglarInteraction(manglares[0]);
                interactionCompleted = true;
            }
        }

        if (currentManglarIndex == 3)
        {
            time += Time.deltaTime;
            if (time > 5f) interactionEnd = true;
        }

        if (interactionEnd)
        {
            foreach (GameObject ui in uiMangles) { ui.SetActive(false); }
            Destroy(this);
        }
    }

    public void MangroveIndex(int index)
    {
        if (index == currentManglarIndex)
        {
            StartInteractionSequence();
        }
    }

    public void OnMangroveInteractionStarted(XRSimpleInteractable interactable)
    {
        MangroveIndex(currentManglarIndex);
    }

    private void StartInteractionSequence()
    {
        timer = lightUpTimes[currentManglarIndex];

        switch (currentManglarIndex)
        {
            //case 0: audioInstance.InitializeVoice(FmodEvents.instance.Manglar21, this.transform.position); break;
            //case 1: audioInstance.InitializeVoice(FmodEvents.instance.Manglar22, this.transform.position); break;
            //case 2: audioInstance.InitializeVoice(FmodEvents.instance.Manglar23, this.transform.position); break;
        }

        DisableManglarInteraction(manglares[currentManglarIndex]);

        // Activar el UI después de que el manglar sea interactuado
        ActivateUI(currentManglarIndex);

        currentManglarIndex++;

        if (currentManglarIndex < manglares.Length)
        {
            LightUpManglar(manglares[currentManglarIndex], true);
            EnableManglarInteraction(manglares[currentManglarIndex]);
        }
        else
        {
            growthSeed.SetActive(true);
            seed.SetActive(true);
        }
    }




    private void SpiralParticules()
    {
        spiralLeaves.Stop();
        spiralend = true;
    }

    private void EnableManglarInteraction(GameObject manglar)
    {
        manglar.GetComponent<XRSimpleInteractable>().enabled = true;
        manglar.GetComponent<Collider>().enabled = true;
    }

    private void DisableManglarInteraction(GameObject manglar)
    {
        manglar.GetComponent<XRSimpleInteractable>().enabled = false;
        manglar.GetComponent<Collider>().enabled = false;
        LightUpManglar(manglar, false);
    }

    private void LightUpManglar(GameObject manglar, bool active)
    {
        Outline manglarOutline = manglar.GetComponent<Outline>();
        if (manglarOutline != null)
        {
            manglarOutline.enabled = active;
        }
    }

    private void ActivateUI(int index)
    {
        if (index >= 0 && index < uiMangles.Length)
        {
            uiMangles[index].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Índice {index} fuera de rango para uiMangles.");
        }
    }

    void OnDestroy()
    {
        audioInstance.CleanUp();

        if (interactionHandler != null)
        {
            interactionHandler.OnInteractionStarted -= OnMangroveInteractionStarted;
        }
    }
}
