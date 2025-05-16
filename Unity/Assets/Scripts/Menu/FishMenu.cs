using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FishMenu : MonoBehaviour
{
    [SerializeField] private GameObject fishDescrp;
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance = 4f;
    [SerializeField] bool activeFishUi = true;
    UiFollowPlayer followPlayer;
    void Start()
    {
        DisplayMenu();
        followPlayer = new UiFollowPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        fishDescrp.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;


        fishDescrp.transform.LookAt(new Vector3(head.position.x, fishDescrp.transform.position.y, head.position.z));
    }

    public void DisplayMenu()
    {
        if (activeFishUi)
        {
            fishDescrp.SetActive(false);
            activeFishUi = false;
            Time.timeScale = 1f;
        }
        else if (!activeFishUi)
        {
            fishDescrp.SetActive(true);
            activeFishUi = true;
            Time.timeScale = 0f;
        }
    }

    public void ActiveFishMenu()
    {
        activeFishUi = false;
        DisplayMenu();
    }

    public void DesactiveFishMenu()
    {
        activeFishUi = true;
        DisplayMenu();
    }
}
