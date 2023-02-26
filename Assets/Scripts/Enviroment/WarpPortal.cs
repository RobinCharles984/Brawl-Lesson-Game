using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPortal : MonoBehaviour
{
    //Variables
    [Header("Components")]
    public Transform exitTransform;
    public GameObject spawnFx;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = exitTransform.position;

            Instantiate(spawnFx.gameObject, exitTransform.position, exitTransform.rotation);
        }
    }
}
