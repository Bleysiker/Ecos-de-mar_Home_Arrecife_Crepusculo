using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] popUps;
    [SerializeField] VrDirectionsCheck vrDirectionDetection;
    [SerializeField] InputActionProperty rightGripAction;
    [SerializeField] InputActionProperty primaryXAction;
    [SerializeField] InputActionProperty triggerPressAction;

    [SerializeField] Transform vrHeadset;

    private float previousYRotation;
    private float previousXRotation;
    private int popUpIndex = 0;
    private bool hasGrabbed = false;
    private bool xPress = false;
    private bool TriggerPress = false;

    public bool doneRight { get; private set; }
    public bool doneLeft { get; private set; }
    public bool doneLookUp { get; private set; }
    public bool doneLookDown { get; private set; }

    private List<Func<bool>> conditions;

    void Start()
    {

        previousXRotation = vrHeadset.eulerAngles.x;
        previousYRotation = vrHeadset.eulerAngles.y;

        doneRight = false;
        doneLeft = false;
        doneLookUp = false;
        doneLookDown = false;
        // conditions = new List<Func<bool>>()
        // {
        //  () => vrDirectionDetection.HasTurnedBothWays(),
        //  () => vrDirectionDetection.HasLookedUpAndDown(),
        // () => hasGrabbed,
        // () => xPress,
        // () => TriggerPress
        //};
    }


    void Update()
    {
        CheckInputActions();

        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(i == popUpIndex);
        }

        if (popUpIndex == 0)
        {
           
            DetectTurnDirection();
            if (HasTurnedBothWays())
            {
                
                popUpIndex++;
                print("primer check");
            }
        }
        else if (popUpIndex == 1)
        {
            
            DetectLookDirection();
            if (HasLookedUpAndDown())
            {
                popUpIndex++;
                print("segundo check");
            }

        }
        else if (popUpIndex == 2)
        {
            if (hasGrabbed)
            {
                popUpIndex++;
                print(" tercero check"[popUpIndex]);
            }

        }
        else if (popUpIndex == 3)
        {
            if (xPress)
            {
                popUpIndex++;
                print("cuarto check"[popUpIndex]);
            }

        }
        else if (popUpIndex == 4)
        {
            if (TriggerPress) { popUpIndex++;
                print("quinto check"[popUpIndex]);
            }
        }
    }


    private void CheckInputActions()
    {
        if (rightGripAction.action.ReadValue<float>() > 0.1f) hasGrabbed = true;

        if (primaryXAction.action.ReadValue<float>() > 0.1f) xPress = true;

        if (triggerPressAction.action.ReadValue<float>() > 0.1f) TriggerPress = true;
    }

    public void DetectTurnDirection()
    {

        float currentYRotation = vrHeadset.eulerAngles.y;
        float rotationDifference = Mathf.DeltaAngle(previousYRotation, currentYRotation); // Calcula la diferencia angular teniendo en cuenta el ciclo 360°
        print(rotationDifference);
        // Detecta giro a la derecha de al menos 90 grados
        if (rotationDifference > 9f) doneRight = true;

        // Detecta giro a la izquierda de al menos 90 grados
        else if (rotationDifference < -9f) doneLeft = true;

        // Actualizamos la rotación previa
        previousYRotation = currentYRotation;
    }

    public void DetectLookDirection()
    {

        float currentXRotation = vrHeadset.eulerAngles.x;
        float rotationDifference = Mathf.DeltaAngle(previousXRotation, currentXRotation);

        print(rotationDifference);
        // Detecta si ha mirado hacia arriba (al menos 90 grados)
        if (rotationDifference > 2f) doneLookUp = true;

        // Detecta si ha mirado hacia abajo (al menos 90 grados)
        else if (rotationDifference < -2f) doneLookDown = true;

        previousXRotation = currentXRotation;
    }

    public bool HasLookedUpAndDown()
    {
        return doneLookUp && doneLookDown;
    }
    public bool HasTurnedBothWays()
    {
        return doneRight && doneLeft;
    }
}
