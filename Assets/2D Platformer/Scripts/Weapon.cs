using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private int Damage;
    [SerializeField] private float LifeTime;

    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Target.tag)
        {
            Character target = collision.gameObject.GetComponent<Character>();

            if (target != null)
            {
                target.Hurt(Damage);
                Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
