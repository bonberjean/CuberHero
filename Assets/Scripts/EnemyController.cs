using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;
    Vector3 velocity;
    Vector3 smoothVelocity;
    public float smoothTime = 3f;
    public float distanceThreshold = 1f;
    public float avoidDistance = 5f;
    public float detectRange = 10f;
    public string targetTag = "Ally";
    public Transform target;
    public Transform player;
    Rigidbody rb;
    bool surface = false;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Find objects with tag
        /*GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        //Choose a target
        target = targets[Random.Range(0, targets.Length)];*/
    }

    void FixedUpdate()
    {/*
        if(surface == true)
        {       
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            Vector3 directionToPlayer = (PlayerController.playerPosition - transform.position).normalized;
            if(Vector3.Distance(transform.position, target.transform.position) > distanceThreshold)
            {
                //Pursue target
                rb.velocity = directionToTarget * speed;
            }
            else if(Vector3.Distance(transform.position, PlayerController.playerPosition) < avoidDistance)
            {
                //Avoid the player if close
                rb.velocity = directionToPlayer * -speed;
            }
            else 
            {
                //stop moving if not objective is present
                rb.velocity = Vector3.zero;
            }
        }
        
        if (surface == true)
        {
            bool hasHit = Physics.Raycast(transform.position, target.transform.position - transform.position, out hit);
            if(hasHit)
            {
                //Do something with the hit info
            }
            if(hasHit && hit.collider.tag == "Player")
            {
                //Avoid the player
            }
            else
            {
                //Go after target
            }
            //Find the target
            Vector3 direction = (target.transform.position - transform.position).normalized;
            //Move to target
            rb.velocity = direction * speed;
        }
        */
    }
    void OnCollisionEnter(Collision collision)
    {
        //if collision happens do...
        if (collision.gameObject.CompareTag("Ally"))
        {
            Destroy(collision.gameObject);
        }
    }
    void OnCollisionStay(Collision collision)
    {
        //Checks if the object is touching a surface
        if (collision.gameObject.CompareTag("Surface"))
        {
            surface = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        //Checks if the object is no longer touching a surface
        if (collision.gameObject.CompareTag("Surface"))
        {
            surface = false;
        }
    }
    void Update()
    {
        //Check if targets still exists
        if (target == null)
        {
            //Find new target
            target = GameObject.FindWithTag(targetTag).transform;
            //If no targets are found
            if (target == null)
            {
                return;
            }
        }
        //Check if the player is close
        if (surface == true)
        {
            if (Vector3.Distance(transform.position, player.position) < detectRange)
            {
                //Ray to check if the player is visible
                RaycastHit hit;
                if (Physics.Raycast(transform.position, player.position - transform.position, out hit, detectRange))
                {
                    if (hit.transform == player)
                    {
                        //if player is sighted, avoid it
                        Vector3 direction = (transform.position - player.position).normalized;
                        velocity.x = Mathf.SmoothDamp(velocity.x, direction.x * speed, ref smoothVelocity.x, smoothTime);
                        velocity.z = Mathf.SmoothDamp(velocity.z, direction.z * speed, ref smoothVelocity.z, smoothTime);
                        transform.position += velocity * speed * Time.deltaTime;
                    }
                }
            }
            else
            {
                //If the player is not detected
                Vector3 direction = (target.position - transform.position).normalized;
                velocity.x = Mathf.SmoothDamp(velocity.x, direction.x * speed, ref smoothVelocity.x, smoothTime);
                velocity.z = Mathf.SmoothDamp(velocity.z, direction.z * speed, ref smoothVelocity.z, smoothTime);
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}