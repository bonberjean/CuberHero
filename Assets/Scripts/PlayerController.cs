using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float fallDuration = 2f;
    public Vector3 respawnPoint;
    public static Vector3 playerPosition;
    public new ParticleSystem particleSystem;
    bool surfaceTouch = false;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionStay(Collision collision)
    {
        //checks if the player is touching a surface
        if (collision.gameObject.CompareTag("Surface"))
        {
            surfaceTouch = true;
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ally"))
        {
            //Destroys the object
            Destroy(collision.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ally"))
        {
            Vector3 explosionPosition = other.gameObject.transform.position;
            Destroy(other.gameObject);
            PlayParticleEffect(explosionPosition);
        }
    }
    void PlayParticleEffect(Vector3 explosionPosition)
    {
        //instantiate particle
        ParticleSystem ps = Instantiate(particleSystem, explosionPosition, Quaternion.identity);
        //plays the particle system
        particleSystem.Play();
        Destroy(ps, particleSystem.GetComponent<ParticleSystem>().main.duration);
    }
    void OnCollisionExit(Collision collision)
    {
        //checks if the player is no longer touching a surface
        if (collision.gameObject.CompareTag("Surface"))
        {
            surfaceTouch = false;
            //Fall coroutine
            StartCoroutine(FallCheck());
        }
    }
    IEnumerator FallCheck()
    {
        //wait for fall duration
        yield return new WaitForSeconds(fallDuration);
        //checks if its still not touching a surface
        if (!surfaceTouch)
        {
            //teleports to spawn
            transform.position = respawnPoint;
        }
    }
    void FixedUpdate()
    {
        if (surfaceTouch == true)
        {
            //Action the player can do when on a surface
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new(horizontal, 0f, vertical);
            rb.velocity = direction * speed;
        }
        else
        {
            //Action the player can do when midair
        }
    }
    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;
    }
}