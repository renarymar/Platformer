using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyEnemy : MonoBehaviour
{
    
    #region Поля

    private Vector2 StartPos;
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody2D Fireball;
    [SerializeField] private GameObject Bazooka;
    [SerializeField] private float Fireball_Speed;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float PatrolSpeed = 2f;
    private enum Mode { attack, search };
    private Mode CurrentMode;

    private enum Direction { left, right };
    private Direction PlayerDirection;

    [SerializeField] private int HP;
    private float shotInterval = 1.5f;
    private float timeOfLastShot;
    Rigidbody2D rb;


    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentMode = Mode.search;
        StartPos = transform.position;

    }

    void FixedUpdate()
    {
        if (CurrentMode == Mode.search) Search();
        if (CurrentMode == Mode.attack) Attack();

        if (IsWithinRange())
            Patrol();
        else
        {
            PatrolSpeed = -PatrolSpeed;
            Patrol();
        }
    }

    private void Attack()
    {
        if (IsTarget())
        {
            Shoot();
        }
        else
        {
            CurrentMode = Mode.search;
        }
    }

    private void Search()
    {
        if (IsTarget() && Reloaded())
            CurrentMode = Mode.attack;
        else
            CurrentMode = Mode.search;
    }

    private void Shoot()
    {
        if (PlayerDirection == Direction.left)
        {
            Rigidbody2D RB_fireball = Instantiate(Fireball, Bazooka.transform.position, Quaternion.Euler(new Vector3(0, transform.position.x < Player.transform.position.x ? 0 : 180, 0))) as Rigidbody2D;
            RB_fireball.velocity = new Vector2(-Fireball_Speed, 0);
        }
        else if (PlayerDirection == Direction.right)
        {
            Rigidbody2D RB_fireball = Instantiate(Fireball, Bazooka.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            RB_fireball.velocity = new Vector2(Fireball_Speed, 0);
        }
        timeOfLastShot = Time.time;

        CurrentMode = Mode.search;
    }

    public bool IsTarget()
    {
        RaycastHit2D hit_right = Physics2D.Raycast(transform.position, Vector2.right, 2, mask);
        RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, 2, mask);
        Debug.DrawRay(transform.position, Vector2.right * 2, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * 2, Color.red);

        if (hit_left)
        {
            PlayerDirection = Direction.left;
            return true;
        }
        else if (hit_right)
        {
            PlayerDirection = Direction.right;
            return true;
        }
        return false;
    }

    private bool Reloaded()
    {
        if (Time.time > timeOfLastShot + shotInterval)
            return true;
        return false;
    }

    private void Patrol()
    {
        //Debug.Log(PatrolSpeed);
        rb.velocity = new Vector2(PatrolSpeed, 0);

    }

    private bool IsWithinRange()
    {
        if (Vector2.Distance(StartPos, transform.position) < 2)
            return true;
        return false;
    }

    public void Hurt(int Damage)
    {
        HP = -Damage;
        if (HP <= 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}