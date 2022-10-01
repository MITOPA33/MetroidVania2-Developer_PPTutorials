using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDemo_1 : MonoBehaviour,ITargetCombat_1
{
    [SerializeField] int health;
    [SerializeField] DamageFeedbackEffect damageFeedbackEffect; //9

    public void TakeDamage(int damagePoints)
    {
        //  health = health-damagePoints;
        damageFeedbackEffect.PlayDamageEffect();                //9
        health -= damagePoints;


        if (health < 10)
            Destroy(gameObject);

    }
}
