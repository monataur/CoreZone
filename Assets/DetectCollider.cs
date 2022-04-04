using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollider : MonoBehaviour
{
    public GameObject enemyParent;
    public string targetName;
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collide)
    {
        switch (collide.tag)
        {
            case "Player":
                Debug.Log("fjnakgwn");
                break;

            case "DetectionBlocker":
                break;
        }
    }
}
