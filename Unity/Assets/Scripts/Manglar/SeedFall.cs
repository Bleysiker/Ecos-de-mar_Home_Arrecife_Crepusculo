using DG.Tweening;
using UnityEngine;

public class SeedFall : MonoBehaviour
{
    [SerializeField] private SeedGrowthManager seedGrowthManager; // Referencia al SeedGrowthManager
    [SerializeField] private AudioManager audioInstance;
    [SerializeField] private float fallSpeed = 5f; // Velocidad de ca�da
    void Start()
    {
        transform.DOMoveY(-1f, fallSpeed)
         .SetEase(Ease.InOutSine)
         .SetLoops(-1, LoopType.Restart); // Loop infinito
         //.From(15f); // Empieza desde la posici�n 15
    }


    public void HandleInteraction()
    {
        // Llamamos al m�todo de SeedGrowthManager para iniciar el crecimiento cuando la semilla es seleccionada
        seedGrowthManager.SelectSeed();
        //audioInstance.InitializeVoice(FmodEvents.instance.Manglar31, this.transform.position);
        // Remover la interacci�n despu�s de que ocurra
        // interactionHandler.RemoveInteractable(interactable);
    }
}
