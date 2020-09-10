using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlayerController : MonoBehaviour
{
   //public SimplePlayerController simplePlayerController;
	public float InputX;

	public float InputY;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed;
	public Animator anim;
	public Slider StaminaBar;
	public float Speed ,sprint,stamina,Maxstamina=100f;
	public int Horde,Kill;
	public GameObject[] zombie ;
	public GameObject BloodPrefab;

	public float allowPlayerRotation;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded , isSprint;
	private bool CantRun;
	private float verticalVel;
	private Vector3 moveVector;
    public GameObject hitbox_attack;
	private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
	public Text Killscore;
	//public Text name;
	//public string m_name;

	private Coroutine regen;
	public bool Death;
	
	void Start () {
		anim = this.GetComponent<Animator> ();
		cam = GetComponent<Camera>();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();
		zombie = GameObject.FindGameObjectsWithTag("Zombie");
		Killscore.text =""+Kill;


		//stamina =30f;
		isSprint = false;
		//name.text = m_name;
		Death =false;
	}
	
	void Update () {
		InputMagnitude ();
        Attack();
        Sprint();
		RegenStamina();
		Horde = GameObject.FindGameObjectsWithTag("Zombie").Length;
		zombie = GameObject.FindGameObjectsWithTag("Zombie");
		Kill = Horde-1;
		Killscore.text =""+Kill;

		StaminaBar.value = stamina;
		
		isGrounded = controller.isGrounded;
		if (isGrounded) {
			verticalVel -= 0;
		} else {
			verticalVel -= 2;
		}
		moveVector = new Vector3 (0, verticalVel, 0);
		controller.Move (moveVector);

		//Vector3 namePos= cam.WorldToScreenPoint(this.transform.position);
		//name.transform.position = namePos;
		//
		if(Horde == 1)
		{
			Death =true;
		}else
		if(Horde>1)
		{
			Death =false;
		}

			///
		
			/// 
		

	}
	private IEnumerator RegenStamina()
	{
		if(!isSprint )
		{
			yield return new WaitForSeconds(2);
			while(stamina < Maxstamina)
			{
				stamina+=Maxstamina/75;
				StaminaBar.value = stamina;
				yield return regenTick;
				//Debug.Log("regen");

			}
			regen = null;
			

		}

	}



	void PlayerMoveAndRotation() {
		InputX = Input.GetAxis ("Horizontal");
		InputY = Input.GetAxis ("Vertical");

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputY + right * InputX;

		if (blockRotationPlayer == false) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
		}
	}

	void InputMagnitude() {
		
		InputX = Input.GetAxis ("Horizontal");
		InputY = Input.GetAxis ("Vertical");

		anim.SetFloat ("speed", InputY, 0.0f, Time.deltaTime * 2f);
		anim.SetFloat ("angularspeed", InputX, 0.0f, Time.deltaTime * 2f);

        anim.SetFloat("speed",Speed);

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputY).sqrMagnitude;

		//Physically move player
		if (Speed > allowPlayerRotation) {
			anim.SetFloat ("InputMagnitude", Speed, 0.0f, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (Speed < allowPlayerRotation) {
			anim.SetFloat ("InputMagnitude", Speed, 0.0f, Time.deltaTime);
		}
	}
     void Attack ( )
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("attack",true);
            hitbox_attack.SetActive(true);
			//Destroy(anim);
			
            

        }
        else
        hitbox_attack.SetActive(false);

		/*if (Input.GetKeyDown(KeyCode.F1))
        {
			anim.enabled = true;

		}
		if (Input.GetKeyDown(KeyCode.F2))
        {
			anim.enabled = false;
			simplePlayerController.enabled= false;

		}*/
    }
	
	void OnTriggerEnter(Collider orther)
    {

        if(orther.tag =="Bullet" )
        {
            if(Death)
			{
				anim.enabled = false;
				GameManager.instance.MinusScore();
			
			}
			 GameObject obj = Instantiate(BloodPrefab,this.transform.position, Quaternion.identity);
            //agent.enabled =false;
			SoundManager.PlaySound("Splash");
			

        }
    }



	void UseStamina()
	{
		if(stamina >= 0f)
		{
			stamina -=30*Time.deltaTime;
			StaminaBar.value = stamina;

			if(regen != null)
				StopCoroutine(regen);
			regen = StartCoroutine(RegenStamina());

		}
		else
		Debug.Log("Not Enough Stamina");

	}

	
    void Sprint()
    {
		if(stamina > 1f && !isSprint)
		{
        	if (Input.GetKey(KeyCode.LeftShift) )
			{
				hitbox_attack.SetActive(true);
				Speed = sprint*2;
				UseStamina();
				isSprint = true;

			}
			else
			Speed = new Vector2(InputX, InputY).sqrMagnitude;
			isSprint = false;
        
    	}
		
	}

	
	
}
