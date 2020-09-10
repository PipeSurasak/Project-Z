
﻿using UnityEngine;

public class controller : MonoBehaviour 
{

    Rigidbody rb;
    CapsuleCollider caps;
    public HingeJoint[] MorteJoint; // em algum outro tutorial ensinarei...
    [Space(20)]
    public CapsuleCollider collcap;

    public HingeJoint hj1, hj2;
    public JointSpring hs1, hs2;
    public float SpringMin = 30, SpringMax = 300;

    [Space(20)]
    public float resistance = 10;
    public Animator anim;
    bool Morto = false;
    public float Velocity;

    [Space(20)]
    public bool AtivarAutoConserto;  //
    public Transform checkRootable; //nao mecher, porem nao há necessidade de existir
    public bool Corrigindo; // private will //nao mecher, porem nao há necessidade de existir
    public float MinRoot, MaxRoot; //o eixo X //nao mecher, porem nao há necessidade de existir
    public float Inclinaçao; //nao mecher, porem nao há necessidade de existir
    //private bool prefeiçao; //nao mecher, porem nao há necessidade de existir
    private float pretime; //nao mecher, porem nao há necessidade de existir



    void OnCollisionEnter(Collision col)
    {
        if (col.relativeVelocity.magnitude > resistance) 
        {
            //caps.enabled = false;
            rb.constraints = RigidbodyConstraints.None;
            for (int x = 0; x < MorteJoint.Length; x++)
            {
                MorteJoint[x].useSpring = false;
            }
            anim.SetBool("IsRuning", false);
            Morto = true;
        }
    }


    void Start()
    {
        Velocity = GetComponent<Rigidbody>().velocity.magnitude;
        rb = GetComponent<Rigidbody>();
        caps = GetComponent<CapsuleCollider>();
        //prefeiçao = true;

        hs1 = hj1.spring;
        hs2 = hj2.spring;
    }


    void Update()
    {
        if (!Morto)
        {

            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("IsRuning", true);

                hs1.spring = SpringMin;
                hs2.spring = SpringMin;
                 //transform.position += Vector3.forward *2* Time.deltaTime;

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(120 * Time.deltaTime, 0,0); // rotaçao nao realista, pode-se utilizar o rigbory.addtorque
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(-120 * Time.deltaTime, 0 ,0);// rotaçao nao realista, pode-se utilizar o rigbory.addtorque
                }
            }



            if (Input.GetKey(KeyCode.W) == false )
            {
              anim.SetBool("IsRuning", false);

                hs1.spring = SpringMax;
                hs2.spring = SpringMax;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //traz;
            }

            if (Input.GetKey(KeyCode.A))
            {

            }
            if (Input.GetKey(KeyCode.D))
            {

            }
            hj1.spring = hs1;
            hj2.spring = hs2;
        }
    }
}
