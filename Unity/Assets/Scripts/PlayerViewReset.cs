using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewReset : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform correctPlayerView;
    [SerializeField] private Camera playerHead;

    private void start()
    {
        ResetViewPosition();
    }
    void  ResetViewPosition()
    {
        var rotationAngleY = correctPlayerView.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;

        playerHead.transform.Rotate(0, rotationAngleY,0);
    }
}
