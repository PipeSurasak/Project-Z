using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcAgent : MonoBehaviour
{

    public enum State
     {
        Wondering,
        Following,
        IDLE,
        Death
    };

    public State currentState;

    Vector3 dir;
    public int offset = 1, outRange;
    public bool m_Following;
    public float detectionRadius = 10.0f;
    public float Speed;

    public NavMeshAgent agent;

    public GameObject player;
    public GameObject BloodPrefab;
    public GameObject Body;
    Vector3 Temp;
    private Animator m_anim;

    float timer;
    //public Text name;
   // public string m_name;

    private void Start()
    {
        dir = transform.position;
        m_anim = this.GetComponent<Animator> ();
        player = GameObject.Find("Player");
       m_anim.SetBool("isRunning",false);
       m_anim.SetFloat("MoveSpeed",Speed);
       m_Following = false;
       Temp = player.transform.position;
            Speed= 1.5f;
      agent.speed  = Speed;
       //gent.updatePosition = false;
    }

    private void Update()
    {
        m_anim.SetFloat("MoveSpeed",Speed);
        agent.speed  = Speed;
        //Vector3 namePos= Camera.main.WorldToScreenPoint(this.transform.position);
		//name.transform.position = namePos;
        //name.text = m_name;
        switch (currentState)
        {
            case State.Wondering: 
                if(agent.velocity == Vector3.zero && !m_Following)
                {
                    m_anim.SetBool("isRunning",true);
                    Temp = player.transform.position;

                    dir = transform.position + Random.insideUnitSphere * Random.Range(10, 40);
                }
                if(Vector3.SqrMagnitude(player.transform.position - transform.position) < outRange && m_Following)
                {
                    currentState = State.Following;
                }
                break;

            case State.Following:
                dir = player.transform.position + (Vector3)Random.insideUnitCircle;
                agent.enabled = true;
                Speed= 1.5f;

                //agent.stoppingDistance = offset;
                if(Vector3.SqrMagnitude(player.transform.position - transform.position) < offset)
                {
                  currentState = State.IDLE;
                }

                 if(Vector3.SqrMagnitude(player.transform.position - transform.position) > outRange)
                    {
                        Speed= 3f;
                    }
                    else
                 if(Vector3.SqrMagnitude(player.transform.position - transform.position) <= outRange)
                    {
                        Speed= 1.5f;
                    }
                

                //agent.isStopped = false;
                //CheckCollider();
               m_anim.SetBool("isRunning",true);
                break;

            case State.IDLE:
               
                    m_anim.SetBool("isRunning",false);
                    agent.isStopped = false;
                    Speed= 0f;
                    //agent.enabled = false;
                    if(Vector3.SqrMagnitude(player.transform.position - transform.position) > offset)
                    {
                        currentState = State.Following;
                        //agent.enabled = false;
                        
                    }
                    
                
                break;

            case State.Death:
                Speed= 0f;
                agent.velocity = Vector3.zero;
                break;
        }
        agent.SetDestination(dir);

        if (Input.GetKeyDown(KeyCode.Space)&& m_Following)
        {
            m_Following =false;
            Debug.Log("NotFollowing");
            
            
        }
        if (Input.GetKeyDown(KeyCode.Space)&& !m_Following )
        {
            m_Following = true;
            Debug.Log("Following");
        }

           
        
        
        /*Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        if(worldDeltaPosition.magnitude > agent.radius)
        {
            agent.nextPosition = transform.position + 0.9f* worldDeltaPosition;
        }*/
    }


    public void AddPlayer(GameObject _player)
    {
        if (currentState == State.Wondering)
        {
            //GameManager.GM.RemoveNpcFromArray(gameObject);
        }

        currentState = State.Following;
        player = _player;
        agent.stoppingDistance = offset;
        agent.speed = 12;
    }

    
    void OnTriggerEnter(Collider orther)
    {
        if(orther.tag =="Player")
        {
            m_Following = true;
        }

        if(orther.tag =="Bullet")
        {
            //Destroy(gameObject);
            currentState = State.Death;
            m_anim.enabled = false;
             GameObject obj = Instantiate(BloodPrefab,this.transform.position, Quaternion.identity);
             SoundManager.PlaySound("Splash");
             Destroy(Body);
            //agent.enabled =false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position ,offset);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position ,outRange);
    }
}
