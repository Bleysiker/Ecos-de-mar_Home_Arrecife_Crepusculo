using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyItweens : MonoBehaviour
{

    [SerializeField] private GameObject[] tweens;
    public void DestroyTweens(int i)
    {
        Destroy(tweens[i]);
    }
}
