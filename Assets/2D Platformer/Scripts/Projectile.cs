using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float Speed;
    [SerializeField] private Rigidbody2D Rocket;			//bullet object
    [SerializeField] private GameObject Bazooka;

    private BasicController Player;

    void Awake()
    {
        // Setting up the references.
        Player = transform.root.GetComponent<BasicController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            if (Player.facingRight)
                Shoot_Right(true);
            else
                Shoot_Right(false);
    }

    void Shoot_Right(bool right)
    {
        if (!right)
        {
            Rigidbody2D RB_rocket = Instantiate(Rocket, Bazooka.transform.position, Quaternion.Euler(new Vector3(0, 0, 180))) as Rigidbody2D;
            RB_rocket.velocity = new Vector2(-Speed, 0);
        }

        if (right)
        {
            Rigidbody2D RB_rocket = Instantiate(Rocket, Bazooka.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            RB_rocket.velocity = new Vector2(Speed, 0);
        }
    }
}
