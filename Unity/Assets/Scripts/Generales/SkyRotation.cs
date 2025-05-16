using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Material cloudy;
    //[SerializeField] private AudioManager audioManager;
    private bool skyDone = false;
    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", speed*Time.time);

        if(skyDone)
        {
             ChangeSkybox();
        }
        //Invoke("ChangeSkybox", 5f);
    }

    public void SetCloudySkybox(bool a)
    {

    }
    public void ChangeSkybox()
    {
        
        RenderSettings.skybox = cloudy;
    }

    public void SkyChange()
    {
        skyDone = true;
        
    }
}
