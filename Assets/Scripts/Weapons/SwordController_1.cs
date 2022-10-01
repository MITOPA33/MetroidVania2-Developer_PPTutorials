using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController_1 : MonoBehaviour
{
    [SerializeField] int damagePoints;
    [SerializeField] TagId_1 targetTag;
    private Collider2D collider2D;
    private void Awake()
    {

        collider2D = GetComponent<Collider2D>();
        //Debug.Log("1 " + collider2D.isTrigger);
        collider2D.enabled = false;
        //Debug.Log("11 " + collider2D.isTrigger);
    }

    public void Attack(float attackDuration)
    {
        //Debug.Log("2");
        collider2D.enabled = true;
        //Debug.Log("2 " + collider2D.isTrigger);
        StartCoroutine(_Attack(attackDuration));
    }

    private IEnumerator _Attack(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);
        collider2D.enabled = false;
        //Debug.Log("3 " + collider2D.isTrigger);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag.Equals(targetTag.ToString()))
        {
            var component = collision.gameObject.GetComponent<ITargetCombat_1>();
            if (component != null)
                component.TakeDamage(damagePoints);
            Debug.Log(component);
        }

    }
}
