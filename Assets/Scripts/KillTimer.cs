using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTimer : MonoBehaviour
{
    public float timer = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        //Initialize when created
        StartCoroutine(TimeCounter());
    }
    IEnumerator TimeCounter()
    {
        //Wait for a set time before destroying
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}