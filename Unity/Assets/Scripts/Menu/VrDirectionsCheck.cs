using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrDirectionsCheck : MonoBehaviour
{
    [SerializeField] Transform vrHeadset;

    private float previousYRotation;
    private float previousXRotation;
    public bool doneRight { get; private set; }
    public bool doneLeft { get; private set; }
    public bool doneLookUp { get; private set; }
    public bool doneLookDown { get; private set; }


    void Start()
    {
        previousYRotation = vrHeadset.eulerAngles.y;
        previousXRotation = vrHeadset.eulerAngles.x;
        
        doneRight = false;
        doneLeft = false;
        doneLookUp = false;
        doneLookDown = false;
        
    }

    public void DetectTurnDirection()
    {
        float currentYRotation = vrHeadset.eulerAngles.y;
        float rotationDifference = currentYRotation - previousYRotation;

        // Detecta giro a la derecha
        if (rotationDifference > 0) doneRight = true;
        
        // Detecta giro a la izquierda
        else if (rotationDifference < 0) doneLeft = true;    

        // Actualizamos la rotación previa
        previousYRotation = currentYRotation;
    }

    public void DetectLookDirection()
    {
        float currentXRotation = vrHeadset.eulerAngles.x;
        float rotationDifference = currentXRotation - previousXRotation;

        //  si ha mirado hacia arriba
        if (rotationDifference > 0) doneLookUp = true;
       
        //  si ha mirado hacia abajo
        else if (rotationDifference < 0) doneLookDown = true;
       

        previousXRotation = currentXRotation;
    }

    public void ResetDirections()
    {
        doneRight = false;
        doneLeft = false;
    }
    public void ResetLook()
    {
        doneLookUp = false;
        doneLookDown = false;
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
