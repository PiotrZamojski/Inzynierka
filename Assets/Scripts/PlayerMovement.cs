#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -10f;
    public float jumpHeight = 2f;
    public float handRange = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Raycast raycast;

    Vector3 velocity;
    bool isGrounded;

    public GameObject holdArea;
    private GameObject label; 


    private GameObject heldObj;
    private Rigidbody objRigidbody;
    [SerializeField] private float pickupForce = 5.0f;

#if ENABLE_INPUT_SYSTEM
    InputAction movement;
    InputAction jump;

    void Start()
    {
        label = new GameObject(); 
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");

        jump = new InputAction("PlayerJump", binding: "<Gamepad>/a");
        jump.AddBinding("<Keyboard>/space");

      


        movement.Enable();
        jump.Enable();
    }

#endif

    // Update is called once per frame
    void Update()
    {
        float x;
        float z;
        bool jumpPressed = false;

#if ENABLE_INPUT_SYSTEM
        var delta = movement.ReadValue<Vector2>();
        x = delta.x;
        z = delta.y;
        jumpPressed = Mathf.Approximately(jump.ReadValue<float>(), 1);
        
#else
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");
  
#endif
        


        GameObject pointing_at = raycast.checkHit(handRange);
        
        if(pointing_at != null){
            if(pointing_at.GetComponent<HasVoltage>() != null){
                HasVoltage power = pointing_at.GetComponent<HasVoltage>();
                power.interact();
                label = power.getLabel();
            }
            else{
                print("im here!!");
                label.SetActive(false);               
            }

            if(pointing_at.GetComponent<Interact_Interface>() != null){

                if(pointing_at != null && Input.GetButtonDown("Fire1")){
                    Interact_Interface item = pointing_at.GetComponent<Interact_Interface>();
                    print("Interacting");
                    item.interact();
                }             
            }
                  
        }
            
        
       
       

        if(Input.GetButtonDown("Fire2") && heldObj != null){
                dropItem();
        }
        
        if(heldObj != null){
            moveObject();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,groundMask);
        //print("isGrouded"+isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

    /*    if (jumpPressed && isGrounded)
        {
            print("in jump method");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f *gravity);
        }*/

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

   
    public void pick(GameObject nameOfObject)
    {
        if(heldObj == null){
            if(nameOfObject.GetComponent<Rigidbody>()){
              objRigidbody = nameOfObject.GetComponent<Rigidbody>();
              objRigidbody.useGravity = false;
              objRigidbody.drag = 10;
              objRigidbody.constraints = RigidbodyConstraints.FreezeRotation;


             nameOfObject.transform.SetParent(holdArea.transform);
             heldObj = nameOfObject;
            }
        }
        //check which hand is free then attach an object
       
                
    }

    private void dropItem(){
        if(heldObj.GetComponent<Rigidbody>()){
            objRigidbody = heldObj.GetComponent<Rigidbody>();
            objRigidbody.useGravity = true;
            objRigidbody.drag = 1;
            objRigidbody.constraints = RigidbodyConstraints.None;
            heldObj.transform.parent = null;
            heldObj = null;
        }
    }

    private void moveObject(){
        Vector3 holdAreaPosition = holdArea.transform.position;
        if(Vector3.Distance(heldObj.transform.position, holdAreaPosition) > 0.1f){
            Vector3 moveDirection = (holdAreaPosition - heldObj.transform.position);
            objRigidbody.AddForce(moveDirection);
        }
    }

}
