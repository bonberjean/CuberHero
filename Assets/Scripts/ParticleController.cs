using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public new ParticleSystem particleSystem;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ally"))
        {
            //Instantiate the particle system
            ParticleSystem ps = Instantiate(particleSystem, transform.position, transform.rotation);
            //Play the particle system
            ps.Play();
            //Destroy particle after played
            Destroy(collision.gameObject, ps.main.duration);
        }
    }
}