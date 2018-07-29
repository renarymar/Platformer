using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRocket : MonoBehaviour {

    [SerializeField] private GameObject BOOM;
    [SerializeField] private int Damage;
    [SerializeField] private float LifeTime;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            MyEnemy Enemy = collision.gameObject.GetComponent<MyEnemy>();
            if (Enemy != null) // Если ссылка не пуста
            {
                Enemy.Hurt(Damage); // Вызываем метод урона и указываем его размер
                Instantiate(BOOM, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
