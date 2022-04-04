using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine.Utility;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

// piss you cuck

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    Vector2 move;
    Animator animator;
    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    [SerializeField]
    private InputActionReference moveControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private InputActionReference shootControl;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -20f;
    private Transform cameraMainTransform;
    [SerializeField]
    private float rotationSpeed = 4f;
    [SerializeField]
    public bool autoReticleActivated;
    [SerializeField]
    public bool isMoving;
    [SerializeField]
    public float analogX;
    [SerializeField]
    public float analogY;
    [SerializeField]
    public string moveStatus;
    [SerializeField]
    public Vector2 analogXY;
    [SerializeField]
    public float damage = 10f;
    public float range = 100f;
    public GameObject shootArea;
    public GameObject freelooker;
    public AudioSource modChangeSFX;
    public GameObject TheInventoryThing;
    public GameObject CoreManager;
    public bool isPlayer = true;
    public string currentScene;
    public string currentLocation;
    public InputActionReference quitControl;
    public GameObject worldObject;
    public GameObject characterObject;
    public Vector3 characterLocation;
    public bool hasBeenSeen = false;
    public string characterGUID;
    private static System.Timers.Timer seenTimer;
    public GameObject debugMenu;


    PlayerControls controls;

    public void GetSceneName()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void OnEnable()
    {
        moveControl.action.Enable();
        jumpControl.action.Enable();
        shootControl.action.Enable();
        quitControl.action.Enable();
    }

    public void OnDisable()
    {
        moveControl.action.Disable();
        jumpControl.action.Disable();
        shootControl.action.Disable();
        controls.Player.Disable();
        quitControl.action.Disable();
    }

    private void Start()
    {
        Cursor.visible = false;

        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();

        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Shoot.performed += ctx => Shoot();

        controls.Player.Debug.performed += ctx => Quit();

        SceneDependantInit();
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
    }

    public string GenerateGUID()
    {
        return "aa";
    }

    public void SceneDependantInit()
    {

        switch(currentScene)
        {
            case "Main":
                characterObject.SetActive(true);
                Cursor.visible = false;
                break;

            case "Battle":
                characterObject.SetActive(false);
                Cursor.visible = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider collide)
    {
        switch (collide.tag)
        {
            case "Enemy":
                SceneManager.LoadScene("Battle");
                break;

            case "DetectCollider":
                collide.transform.parent.parent.GetComponent<EnemyBase>().targetName = characterGUID;
                collide.transform.parent.parent.GetComponent<EnemyBase>().playerHasBeenSeen = true;
                //SeenFunction();
                break;
        }
    }

    public void SeenFunction()
    {
        seenTimer = new System.Timers.Timer(7000);
        seenTimer.Start();
    }

    public int PlayerInstanceID()
    {
        var playerID = characterObject.GetInstanceID();
        return playerID;
    }

    public void Awake()
    {
        worldObject = GameObject.Find("WorldObject9274839");
    }

    public void Quit()
    {
        debugMenu.SetActive(true);
    }



    public void Shoot()
        {
            Debug.Log("why no work");
            RaycastHit hit;
            if (Physics.Raycast(shootArea.transform.position, shootArea.transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
            }
        }

        void Update()
        {
        if (quitControl.action.triggered)
        {
           // Cursor.visible = true;
        }

        GetSceneName();

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }


            Vector2 movement = moveControl.action.ReadValue<Vector2>();
            Vector3 move = new Vector3(movement.x, 0, movement.y);
            move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            //Debug.Log(movement);
            
            // changes the fuckin hight position an shit yknow
            if (jumpControl.action.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);


        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            isMoving = true;
            animator.SetBool("isWalking", true);
        }

        if (movement == Vector2.zero)
        {
            isMoving = false;
            animator.SetBool("isWalking", false);

        }

        analogX = movement.x;

            analogY = movement.y;

            analogXY = movement;

            //animation shit


            // idle bruhhhhhhhhhhhhhhhhhhhhhhh

            if (movement == Vector2.zero)
            {
                moveStatus = "Idling";
            }

            //x value shit starts here | UPDATE: X SHIT GONE WOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO PERFORMANCE HERE WE GO BABY | nvm its back yall :( |  GONE AGAIN LMAOOOOO YEAH BOYYY



            //same shit but for y value lol

            if (analogY >= 0.1 && analogY <= 0.6)
            {
                moveStatus = "Walking";
            }

        if (analogX >= 0.1 && analogX <= 0.6)
        {
            moveStatus = "Walking";
        }




        //END OF THE DIRT ROAD. I'LL PROBABLY CLEAN THIS UP WHEN I HAVE SLEEP AND AM NOT RUNNING ON 3 COKE WITH COFFEES.

        //shit that handles falling animation

        {
                if (groundedPlayer == true)
                {
                    animator.SetBool("isGrounded", true);
                }
            }

            {
                if (groundedPlayer == false)
                {
                    animator.SetBool("isGrounded", false);
                }
            }

       
       
        }
    }