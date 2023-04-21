using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private ParticleSystem appleParticle;

    public BoxCollider2D myCollider2d;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knife"))
        {
            myCollider2d.enabled = false;
            sr.enabled = false;
            transform.parent = null;

            appleParticle.Play();
            Destroy(gameObject, 2f);
        }
    }
}
