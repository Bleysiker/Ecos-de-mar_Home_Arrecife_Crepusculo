using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerAutoRotation : MonoBehaviour
{

    
    [SerializeField] private Transform targetPoint; // El punto al que queremos que el jugador mire
    [SerializeField] private float rotationSpeed = 1.0f; // Velocidad de rotación
    [SerializeField] private AudioManager audioInstance; // Instancia del AudioManager
    //[SerializeField] private EventReference audioEvent; // Evento de FMOD que desencadenará la rotación
    [SerializeField] private GameObject xrOrigin; // El XR Origin que contiene la cámara
    [SerializeField] private float activationtime = 4.56f;
    
    
    private Vector3 directionToTarget;
    
    private bool shouldRotate = false; // Controla si el jugador debe rotar
    private Quaternion targetRotation; // La rotación objetivo (hacia el punto)

    private Transform playerCamera; // Referencia a la cámara principal del jugador

    void Start()
    {
        // Obtener la referencia a la cámara del XR Origin
        XROrigin origin = xrOrigin.GetComponent<XROrigin>();
        playerCamera = origin.Camera.transform;

        // Inicializar el evento de audio
        //audioInstance.CreateInstance(audioEvent);
    }

    void Update()
    {
        float currentTime = audioInstance.GetTimelinePosition() / 1000f;
        //print(currentTime);

        if (currentTime >= activationtime)
        {
            StartRotationWithAudio();
            print("Activo check vision");
        }

        if (shouldRotate)
        {
            RotateTowardsTarget();
        }
    }

    // Llamar a este método cuando se dispare el audio
    public void StartRotationWithAudio()
    {
        // Iniciar el audio
        //audioInstance.InitializeVoice(audioEvent, playerCamera.position);

        // Calcular la rotación que el jugador debe tener para mirar al target
        Vector3 directionToTarget = (targetPoint.position - playerCamera.position).normalized;
        targetRotation = Quaternion.LookRotation(directionToTarget);

        // Iniciar la rotación hacia el punto
        shouldRotate = true;
    }

    private void RotateTowardsTarget()
    {
        // Rotar suavemente hacia la rotación objetivo
        playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Verificar si la rotación ha alcanzado el objetivo
        if (Quaternion.Angle(playerCamera.rotation, targetRotation) < 0.1f)
        {
            shouldRotate = false;  // Detener la rotación una vez alcanzado el punto
        }
    }


    
}
