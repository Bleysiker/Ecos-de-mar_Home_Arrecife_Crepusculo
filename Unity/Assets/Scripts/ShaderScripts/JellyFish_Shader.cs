using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish_Shader : MonoBehaviour
{
    [SerializeField] private float erodeRate = 0.03f;
    [SerializeField] private float erodeRefreshRate = 0.01f;
    [SerializeField] private float erodeDelay = 0.25f;
    [SerializeField] private MeshRenderer erodeObject;
    void Start()
    {
        StartCoroutine(ErodeObject());
    }

    IEnumerator ErodeObject()
    {
        yield return new WaitForSeconds(erodeDelay);

        float t = 0;

        while (t<1)
        {
            t += erodeRate;
            erodeObject.material.SetFloat("_Erode", t);
            yield return  new WaitForSeconds(erodeRefreshRate);
        }
    }
}
