using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour {

    [SerializeField] private float Acceleration;				//character's speed
    [SerializeField] private GameObject Bomb;
    [SerializeField] private GameObject StartBomb;  
    [SerializeField] private float jumpForce;
    [SerializeField] public int HP;		



    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private bool grounded = false;			// Whether or not the player is grounded.

    bool jump = false;
    bool moving = false;

    [HideInInspector] public bool facingRight = true;			// For determining which way the player is currently facing.
    private	Vector3 Dir = new Vector3(0,0,0);					//character's moving direction

    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        Dir.x = Input.GetAxis("Horizontal");
        if (Dir.x != 0)
            moving = true;

        if (Dir.x < 0)
        {
            if (facingRight)
                Flip();
        }

        else if (Dir.x > 0)
        {
            if (!facingRight)
                Flip();
        }

        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;

        if (Input.GetButtonDown("Fire2"))
            Instantiate(Bomb, StartBomb.transform.position, transform.rotation); //then create a bomb
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 30), "HP: " + HP);

    }

    // Update is called once per frame
    void FixedUpdate () 
	{
        if (moving) Move();
        if (jump) Jump();
    }

    void Move()
    {
        moving = false;
        GetComponent<Rigidbody2D>().velocity = new Vector3(Dir.x * Acceleration, GetComponent<Rigidbody2D>().velocity.y, 0f);
    }

    void Jump()
    {
        jump = false;
        GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Hurt(int Damage)
    {
        HP--;
        Debug.Log("HEALTH: " + HP);
        if (HP <= 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

   
}
