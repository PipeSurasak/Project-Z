using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider orther)
    {
        

        if(orther.tag =="Bullet")
        {
            Destroy(gameObject);
            
            //agent.enabled =false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
