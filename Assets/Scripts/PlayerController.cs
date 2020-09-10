using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 0.5f, sprint;
    private float currentSpeed = 0f  ;   
    private float speedSmoothVelocity = 0.1f;
    private float speedSmoothTime = 0.1f;
    private float rotationSpeed = 0.1f;
    private float gravity = 3f;
    //public Transform mainCameraTransform;

    //private CharacterController controller ;
    public Animator anim;
   // public GameObject hitbox_attack;



    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        //hitbox_attack.SetActive(false);
        

       // mainCameraTransform = Camera.main.transform;
        //h = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    private void Update()
    {
        //Move();
        Sprint();
        Attack ();


        if (Input.GetKey(KeyCode.W))
            {
                //anim.SetBool("IsRuning", true);
                anim.SetFloat("speed",1f);

                //hs1.spring = SpringMin;
               // hs2.spring = SpringMin;
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
              anim.SetFloat("speed",0f);

                //hs1.spring = SpringMax;
                //hs2.spring = SpringMax;
            }
    }
    private void Move()
    {
        /*
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        animator.SetFloat("speed",currentSpeed);
        animator.SetFloat("angularspeed",Input.GetAxis("Horizontal"));
        

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.Normalize();
        right.Normalize();

       
        Vector3 desiredMoveDirection =(forward * movementInput.y + right * movementInput.x).normalized;

        Vector3 gravityVector = Vector3.zero;

        if(!controller.isGrounded)
        {
            gravityVector.y -= gravity;
        }

        if(desiredMoveDirection != Vector3.zero)
        {
            transform.rotation =Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection),rotationSpeed);
        }
        
        
        float targetSpeed = MovementSpeed * movementInput.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime) ;
        

        controller.Move(desiredMoveDirection* currentSpeed *Time.deltaTime);
        controller.Move(gravityVector* Time.deltaTime);  */
    }

    void Attack ()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("attack",true);
            //hitbox_attack.SetActive(true);
            

        }
        //
        //hitbox_attack.SetActive(false);
    }
    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) )
        {
            //hitbox_attack.SetActive(true);
            MovementSpeed = sprint;
            anim.SetFloat("speed",4f);
            

        }
        else
        MovementSpeed = 0.5f;
        
    }

}
  