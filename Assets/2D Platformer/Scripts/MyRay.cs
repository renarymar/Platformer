using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRay : MonoBehaviour {

    [SerializeField] private LayerMask mask = 1 << 9;
    [SerializeField] private float Distance;
    
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, Distance, mask);
        Debug.DrawRay(transform.position, Vector2.right * Distance, Color.red);

        if(hit) Debug.Log("The ray hit the player");
        
    }
}
