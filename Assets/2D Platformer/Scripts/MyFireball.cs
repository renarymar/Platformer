using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFireball : MonoBehaviour {

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
        if (collision.gameObject.tag == "Player")
        {
            BasicController Player = collision.gameObject.GetComponent<BasicController>();
            if (Player != null) // Если ссылка не пуста
            {
                Player.Hurt(Damage); // Вызываем метод урона и указываем его размер
                Instantiate(BOOM, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
