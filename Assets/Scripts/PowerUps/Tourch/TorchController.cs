using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour, ITargetCombat_1
{

    [SerializeField] GameObject destructionEffect;
    [SerializeField] AudioClip destructionSfx;


    //[SerializeField] GameObject coin;
    //[SerializeField] GameObject heart;

    //[SerializeField] List<GameObject> powerUps;

    //public bool powerUpsOnly;
    //public bool indestructible = false;
    public void TakeDamage(int damagePoints)
    {

        Instantiate(destructionEffect, this.transform.position, Quaternion.identity);
        GameObject prefabToInstance = null;
/*
        if (!powerUpsOnly)
        {
            if (Random.Range(-10, 10) > 0)
            {
                if (Random.Range(-10, 10) > 0)
                {
                    prefabToInstance = coin;
                }
                else
                {
                    if (Random.Range(-10, 10) > 0)
                    {
                        prefabToInstance = heart;
                    }
                  

                }

            }
        }
        else {

            if (Random.Range(-10, 10) > 0)
            {
                prefabToInstance = powerUps[Random.Range(0, powerUps.Count)];
            }
        }
*/
        if (prefabToInstance != null) {
            Instantiate(prefabToInstance, this.transform.position, Quaternion.identity);

        }

        AudioManager_1.instance.PlaySfx(destructionSfx);

        //if(!indestructible)
         Destroy(this.gameObject);

    }
}
