using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TrashSystemVR : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text m_progresoText; // Texto para mostrar progreso num�rico
    [SerializeField] private Slider progressSlider; // Slider para mostrar progreso visual
    [SerializeField] private Image[] m_trashImages; // Im�genes de basura recolectada

    [Header("Trash Settings")]
    [SerializeField] private GameObject[] m_trash; // Objetos de basura en escena
    [SerializeField] private int trashToCollect = 3; // Cantidad de basura a recolectar (personalizable)
    [SerializeField] private VRInteractionHandler interactionHandler; // Sistema de interacci�n VR
    [SerializeField] private PathMover pathMover; // Sistema de movimiento en path
    [SerializeField] private GameObject trashbottle;
    private int m_trashIndex = 0; // Progreso actual

    void Start()
    {
        trashbottle.transform.SetParent(this.transform);
        InitializeSystem();

        // Agregar todos los objetos de basura al sistema de interacci�n
        foreach (GameObject trash in m_trash)
        {
            XRSimpleInteractable interactable = trash.GetComponent<XRSimpleInteractable>();
            if (interactable != null)
            {
                interactionHandler.AddInteractable(interactable);
            }
            else
            {
                Debug.LogWarning($"El objeto {trash.name} no tiene un componente XRSimpleInteractable.");
            }
        }

        // Suscribirse al evento de interacci�n
        interactionHandler.OnInteractionStarted += HandleTrashInteraction;
    }

    private void InitializeSystem()
    {
        // Desactiva todos los objetos de basura
        foreach (var trash in m_trash)
        {
            trash.SetActive(false);
        }
        m_trashIndex = 0;
        m_progresoText.text = $"0/{trashToCollect}";
        progressSlider.value = 0f; // Inicializa el slider en 0

        // Asegurar que las im�genes reflejen el nuevo l�mite
        for (int i = 0; i < m_trashImages.Length; i++)
        {
            var color = m_trashImages[i].color;
            color.a = (i < trashToCollect) ? 0.5f : 0f; // Oculta las im�genes que est�n fuera del l�mite
            m_trashImages[i].color = color;
        }

        RandomInteractables(); // Generar el primer objeto
    }

    private void HandleTrashInteraction(XRSimpleInteractable interactable)
    {
        // Verifica si el interactable pertenece a los objetos de basura
        if (!IsTrashInteractable(interactable)) return;

        AddTrash();
    }

    private bool IsTrashInteractable(XRSimpleInteractable interactable)
    {
        foreach (GameObject trash in m_trash)
        {
            if (trash.GetComponent<XRSimpleInteractable>() == interactable)
            {
                return true; // Es un objeto de basura v�lido
            }
        }
        return false;
    }

    private void RandomInteractables()
    {
        if (m_trashIndex >= trashToCollect)
        {
            Debug.Log("Se ha alcanzado el m�ximo de basura recolectable. No se activar�n m�s objetos.");
            return;
        }

        // Desactiva todos los objetos de basura
        foreach (var trash in m_trash)
        {
            trash.SetActive(false);
        }

        // Activa un objeto aleatorio dentro del l�mite configurado
        int randomIndex = Random.Range(0, m_trash.Length);

        // Asegurarse de que el objeto seleccionado est� dentro del rango v�lido
        if (randomIndex < m_trash.Length)
        {
            m_trash[randomIndex].SetActive(true);
        }
    }

    private void AddTrash()
    {
        if (m_trashIndex < trashToCollect)
        {
            // Actualiza el progreso visual
            var color = m_trashImages[m_trashIndex].color;
            color.a = 1; // Hace la imagen completamente visible
            m_trashImages[m_trashIndex].color = color;

            m_trashIndex++; // Incrementa el �ndice de progreso

            // Actualiza el texto y el slider de progreso
            m_progresoText.text = $"{m_trashIndex}/{trashToCollect}";
            progressSlider.value = (float)m_trashIndex / trashToCollect;

            if (m_trashIndex < trashToCollect)
            {
                RandomInteractables(); // Generar un nuevo objeto
            }
            else
            {
                Debug.Log("�Mini juego completado!");
                EndGame();
            }
        }
    }

    private void EndGame()
    {
       // pathMover.enabled = true;   
        // pathMover.StartPath("FishUp");
        //foreach (var trash in m_trash)
        //{
        //    trash.SetActive(false);
        //}

        Debug.Log("�Todos los objetos recolectados!");
        // L�gica adicional para finalizar el mini juego
    }

    private void OnDestroy()
    {
        // Desuscribir del evento para evitar errores
        if (interactionHandler != null)
        {
            interactionHandler.OnInteractionStarted -= HandleTrashInteraction;
        }
    }
}
