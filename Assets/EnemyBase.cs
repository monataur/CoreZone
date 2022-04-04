using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject enemyObject;
    public GameObject playerObject;
    public Vector3 targetPosition;
    public string targetName;
    public Transform enemyObjectTransform;
    public float speed = 10f;
    public GameObject detectionColliders;
    // import
    public string enemyName; // "Generic" name, I.E. "Reincarnated Bird"
    public string enemyNameTrue; // "True" name, I.E. "Pheonix"
    public string typeOne; // First type, I.E. "Fire"
    public string typeTwo; // Second type, I.E. "Time"
    //
    public int baseHp; // Base HP Stat, not calculated, use final number.
    public int basePhysStr; // Base Physical Strength
    public int baseMagStr; // Base Magical Strength
    public int basePhysDef; // Base Physical Defense
    public int baseMagDef; // Base Magical Defense
    public int baseAgl; // Base Agility
    public int baseLu; // Base Luck
    // import
    public bool playerHasBeenSeen;
    void Start()
    {
        
    }


    private void Update()
    {
        if (playerHasBeenSeen == true)
        {
            playerObject = GameObject.Find(targetName);
        }

        if (playerHasBeenSeen == true)
        {
            float step = speed * Time.deltaTime;
            targetPosition = playerObject.transform.position;
            enemyObjectTransform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            float dist = Vector3.Distance(targetPosition, transform.position);
            detectionColliders.SetActive(false);
            if (dist >= 20)
            {
                StopChase();
            }
        }
    }

    public void StopChase()
    {
        playerHasBeenSeen = false;
        playerObject = null;
        targetName = null;
        detectionColliders.SetActive(true);
    }
}
