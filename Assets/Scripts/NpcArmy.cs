using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcArmy : MonoBehaviour
{
    public enum State
     {
        Attack,
        IDLE
    };
   

    public State currentState;
    Vector3 dir;
    public int Range , outRange;
    public GameObject BloodPrefab;
    public GameObject zombie ;
    public GameObject NpcZombie;
    public GameObject Gun;
    private Transform ZombiePos;
    private Animator m_anim;
    public bool fire;
    public float fireRate = 0.3f;
    float frate;

    void Start()
    {
        m_anim = this.GetComponent<Animator> ();
        m_anim.SetBool("IsShooting",false);
        zombie = GameObject.Find("Player");
        
        
        fire = false;
        //ZombiePos.FindClosetGameObect(s);
    }

    // Update is called once per frame
    void Update()
    {
        //Info
        Raycast();
        float distance = Vector3.SqrMagnitude(zombie.transform.position - transform.position);
        //
        switch (currentState)
        {
            case State.Attack:
            
                m_anim.SetBool("IsShooting",true);
                this.transform.LookAt(zombie.transform);
                 fire = true;
                 
            
            break;

            case State.IDLE:
            
               m_anim.SetBool("IsShooting",false); 
                fire = false;
            
            break;

        }
        //LookAtZombie
        if(distance<=outRange+70)
        {
            this.transform.LookAt(zombie.transform);
            
        }else
        currentState = State.IDLE;
        
        if(distance<=Range+50)
            {
                currentState = State.Attack;

            }
        if(distance>outRange+70)
            {
               currentState = State.IDLE; 
            }
        //fire
         if (fire)
        {
            frate += Time.deltaTime;

            if (frate > fireRate)
            {
                ParticleSystem[] parts = GetComponentsInChildren<ParticleSystem>();

                    foreach (ParticleSystem ps in parts)
                    {
                        ps.Emit(1);
                        SoundManager.PlaySound("FIRE");
                    }
                frate = 0;
                fire = false;
            }
        }
        
    }
    public bool Raycast()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(transform.position, forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, forward * 10, Color.red);
        if(Physics.Raycast(ray, out hit, 1))
        {
            print(hit.collider.name);
            return true;
        }
        return false;
    }

    


     private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position ,Range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position ,outRange);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Zombie")
        {
            SoundManager.PlaySound("BIT");
            //Debug.Log("Hit zombie");
            GameObject obj = Instantiate(BloodPrefab,this.transform.position, Quaternion.identity);
            GameObject Npc = Instantiate(NpcZombie,this.transform.position, Quaternion.identity);
            GameObject Gundrop = Instantiate(Gun,this.transform.position, Quaternion.identity);
            Destroy(gameObject);

            
        }
       
    }
    
}
