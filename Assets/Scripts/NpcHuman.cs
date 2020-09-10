using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class NpcHuman : MonoBehaviour
{
    public enum State
     {
        Wondering,
        Flee,
        IDLE
    };
    public State currentState;
    Vector3 dir;
    public int offset = 4, outRange;
    public float detectionRadius = 10.0f;
    public float Speed;
    public NavMeshAgent agent;
    public GameObject Zombie;
    public GameObject BloodPrefab;
    public GameObject NpcZombie;
    public GameObject Body;
    private Transform ZombiePos;
    private int multiplier = 1;
    private int CurrentPoint = 0;
    private Animator m_anim;
    public int SpeedRun;
    
    

    List<Transform> path = new List<Transform>();


    
    void Start()
    {
        m_anim = this.GetComponent<Animator> ();
        m_anim.SetBool("isRunning",false);
       m_anim.SetFloat("MoveSpeed",Speed);
       agent.speed  = Speed;
       Speed= 1.5f;
         //Zombie = GameObject.FindGameObjectsWithTag("Zombie");
         /*foreach (GameObject go in Zombie)
         {
             path.Add(go.transform);
         }*/
    }

    int FindClosest()
    {
        if(path.Count == 0)return-1;
        int closest = 0;
        float lastDist = Vector3.Distance(this.transform.position, path[0].position);
        for(int i = 1; i< path.Count;i++)
        {
            float thisDist = Vector3.Distance(this.transform.position, path[i].position);
            if(lastDist > thisDist && i != CurrentPoint)
            {
                closest = i;

            }
        }
        return closest;
    }

    // Update is called once per frame
    void Update()
    {
        ZombiePos =Zombie.transform;
        Vector3 runTo = transform.position + ((transform.position - ZombiePos.position) * multiplier);

        float distance = Vector3.Distance(transform.position,ZombiePos.position);
        m_anim.SetFloat("MoveSpeed",Speed);
        agent.speed  = Speed;
        //Vector3 direction = path[CurrentPoint].position
        CurrentPoint = FindClosest();
        if(currentState== State.Wondering)
        {
            if(distance < offset)
                    {
                        currentState = State.Flee;
                    }
        }

        switch (currentState)
        {
            case State.Wondering: 
                if(agent.velocity == Vector3.zero && Speed> 0f)
                {
                    if(distance < offset)
                    {
                        currentState = State.Flee;
                    }
                    m_anim.SetBool("isRunning",true);
                    //Temp = player.transform.position;

                    dir = transform.position + Random.insideUnitSphere * Random.Range(10, 40);
                    agent.SetDestination(dir);
                    
                }
            break;
            case State.Flee:
                agent.SetDestination(runTo);
                Speed= SpeedRun;
                m_anim.SetBool("isRunning",true);
 
            break;
            case State.IDLE:
                    m_anim.SetBool("isRunning",false);
                    agent.isStopped = false;
                    Speed= 0f;
                    if(Vector3.SqrMagnitude(ZombiePos.transform.position - transform.position) < outRange )
                    {
                        currentState = State.Flee;
                    }
            break;

            
        }
    }
    void OnTriggerEnter(Collider Collider)
    {
        if(Collider.gameObject.tag == "Zombie")
        {
            Debug.Log("Hit zombie");
            SoundManager.PlaySound("Splash");
            GameObject obj = Instantiate(BloodPrefab,this.transform.position, Quaternion.identity);
            GameObject Npc = Instantiate(NpcZombie,this.transform.position, Quaternion.identity);
            Destroy(gameObject);

            
        }
    }
}
