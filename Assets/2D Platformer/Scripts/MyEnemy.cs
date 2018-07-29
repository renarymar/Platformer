using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyEnemy : MonoBehaviour
{


    #region Поля

    private Vector3 StartPos;
    private Vector3 MovementDirection;
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody2D Fireball;
    [SerializeField] private GameObject Bazooka;
    [SerializeField] private float Fireball_Speed;
    [SerializeField] private LayerMask mask;
    private Vector2 PlayerDirection;
    private bool shooting = false;
    private enum Mode { attack, search };
    private Mode CurrentMode;
    int HP;
    #endregion

    void Start()
    {
        CurrentMode = Mode.search;
        StartPos = transform.position;
    }

    void FixedUpdate()
    {
        if (CurrentMode == Mode.search) Search();
        if (CurrentMode == Mode.attack) Attack();
    }
    
    private void Attack()
    {
        Debug.Log("Attack");
        if (IsTarget()) Shoot();
        else
        {
            CurrentMode = Mode.search;
        }
    }

    private void Search()
    {
        Debug.Log("Search");
        if (IsTarget()) CurrentMode = Mode.attack;
        else
        {
            CurrentMode = Mode.search;
        }
    }

    private void Shoot()
    {
        Rigidbody2D RB_fireball = Instantiate(Fireball, Bazooka.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        RB_fireball.velocity = new Vector2(transform.position.x < Player.transform.position.x ? Fireball_Speed : -Fireball_Speed, 0);
        Reload();
        CurrentMode = Mode.search;
    }

    public bool IsTarget()
    {
        RaycastHit2D hit_right = Physics2D.Raycast(transform.position, Vector2.right, 2, mask);
        RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, 2, mask);
        Debug.DrawRay(transform.position, Vector2.right * 2, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * 2, Color.red);

        if (hit_left || hit_right)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Reload()
    {

    }

    private void Patrol()
    {

    }

    public void Hurt(int Damage)
    {
        HP--;
        if (HP <= 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}