using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SafeGuard;
//------------------------------------------------------------------------------------------------
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    public GameObject Viser;

    [Header("displacement")]
    public float WalkSpeed;
    public float RunSpeed;
	public float timeVelocity;
    public float Gravity;
    public float jumpSpeed;

    [Header("Camera")]
    public float Sensitivity;

    [Range(0.0f, 90.0f)]
    public float viewRange = 90;
    public LayerMask layer;

    void SetCursorState(CursorLockMode wantedMode)
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking

        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

    float speed;

	float moveHorizontal;
    float moveVertical;
    float yRotate;
    float xRotate;
    bool invertMouseY;

    private CharacterController Cc;
    Vector3 Movement;

    KeyCode Up;
    KeyCode Down;
    KeyCode Right;
    KeyCode Left;
    KeyCode jump;
    KeyCode Run;
    KeyCode Action;

    //------------------------------------------------------------------------------------------------
    void Start ()
    {
        SetCursorState(CursorLockMode.Locked);

        Cc = GetComponent<CharacterController>();

        List<Key> saveInput = (List<Key>)file.Load("/saves/Input.dat");

        Up = (KeyCode)Enum.Parse(typeof(KeyCode), GetInput(saveInput, "Up"));
        Down = (KeyCode)Enum.Parse(typeof(KeyCode), GetInput(saveInput, "Down"));
        Right = (KeyCode)Enum.Parse(typeof(KeyCode), GetInput(saveInput, "Right"));
        Left = (KeyCode)Enum.Parse(typeof(KeyCode), GetInput(saveInput, "Left"));
        jump = (KeyCode)Enum.Parse(typeof(KeyCode), GetInput(saveInput, "Jump"));
        Run = (KeyCode)Enum.Parse(typeof(KeyCode), GetInput(saveInput, "Run"));
        Action = (KeyCode)Enum.Parse(typeof(KeyCode), GetInput(saveInput, "Action"));
        print("load sucess");
    }

    private string GetInput(List<Key> _saveInput, string name)
    {
        string _name = "None";

        for (int i = 0; i < _saveInput.Count; i++)
        {
            if (_saveInput[i].Name == name)
            {
                _name = _saveInput[i].key;
            }       
        }
        return _name;
    }

    void Update(){

        float velocity = timeVelocity * Time.deltaTime;
        moveHorizontal = (Mathf.Lerp(moveHorizontal, 0, velocity));
        if (Input.GetKey(Right)) { moveHorizontal = (Mathf.Lerp(moveHorizontal, 1, velocity)); }
        if (Input.GetKey(Left)) { moveHorizontal = (Mathf.Lerp(moveHorizontal, -1, velocity)); }

        moveVertical = (Mathf.Lerp(moveVertical, 0, velocity));
        if (Input.GetKey(Up)) { moveVertical = (Mathf.Lerp(moveVertical, 1, velocity)); }
        if (Input.GetKey(Down)) { moveVertical = (Mathf.Lerp(moveVertical, -1, velocity)); }

        Movement.y -= Gravity * Time.deltaTime;

        if (Cc.isGrounded)
        {
            Movement.y = 0;
            if (Input.GetKey(jump))
            {
                Movement.y = jumpSpeed;
            }
        }

        if (Input.GetKey(Run)) { speed = RunSpeed; }
        else { speed = WalkSpeed; }

        Movement = new Vector3(moveHorizontal, Movement.y, moveVertical);
        Movement = transform.TransformDirection(Movement);

        Cc.Move(Movement * speed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3, layer))
        {
            if (Input.GetKeyDown(Action))
            {
                if (hit.collider.gameObject.GetComponent<DoorSystem>() != null)
                {
                    hit.collider.gameObject.GetComponent<DoorSystem>().Action();
                }

                if (hit.collider.GetComponentInParent<Interupteur>() != null)
                {
                    hit.collider.GetComponentInParent<Interupteur>().Action();
                }

                if (hit.collider.GetComponentInParent<Four>() != null)
                {
                    hit.collider.GetComponentInParent<Four>().Action();
                }
            }

            if (!Viser.activeSelf)
                Viser.SetActive(true);
        }
        else if(Viser.activeSelf)
        {         
                Viser.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        xRotate += Input.GetAxis("Mouse X") * Sensitivity;
        yRotate += Input.GetAxis("Mouse Y") * Sensitivity;
        yRotate = Mathf.Clamp(yRotate, -viewRange, viewRange);
        transform.eulerAngles = new Vector3(0.0f, xRotate, 0.0f);
        Camera.main.transform.localEulerAngles = new Vector3(-yRotate, 0.0f, 0.0f);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == 13)
        {
            hit.gameObject.GetComponent<DoorSystem>().OnCollision(transform.position);
        }

        
        
    }
}
