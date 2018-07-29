using UnityEngine;
using System.Collections;

public class LayBomb : MonoBehaviour {

    [SerializeField] private GameObject Explosion;              // Explosion Prefab
    [SerializeField] private AudioClip ExplosionAudio;			// Audioclip of explosion.
    [SerializeField] private AudioClip fuse;                  // Audioclip of fuse.
    [SerializeField] private float fuseTime;

    private void Start()
    {
        StartCoroutine(BombDetonation());
    }

    IEnumerator BombDetonation()
    {
        // Play the fuse audioclip.
        AudioSource.PlayClipAtPoint(fuse, transform.position);

        // Wait for 2 seconds.
        yield return new WaitForSeconds(fuseTime);

        // Explode the bomb.
        BombExplosion();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //if bomb colides with an object that tagged as Enemy
        {
            MyEnemy Enemy = collision.gameObject.GetComponent<MyEnemy>();
            if (Enemy != null) // if the link is valid
            {
                BombExplosion();
                Enemy.Death(); //initiate the enemy's death
            }
        }
    }

    void BombExplosion()
    {
        // Instantiate the explosion prefab.
        Instantiate(Explosion, transform.position, Quaternion.identity);

        // Play the explosion sound effect.
        AudioSource.PlayClipAtPoint(ExplosionAudio, transform.position);

        Destroy(gameObject); //destroy the bomb
    }
}
