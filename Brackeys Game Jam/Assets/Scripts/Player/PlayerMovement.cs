///Written By Kabungus 8/23/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    const float GRAVITY = -9.8f;

    [Header("Input")]
    private PlayerInputActions _inputActions;
    private InputAction _movement;
    [SerializeField] CharacterController _controller;
    public float _speed = 5f;
    public bool inputDisabled = true;
    float _valx, _valz; // used in input smoothing. 

    [Header("Graphics")]
    public Transform Graphics;
    public Animator Animator;

    [Header("Ground Stuff")]
    public LayerMask GroundMask;
    [SerializeField] bool _isGrounded = true;
    Vector3 _velocity;

    private int stepIndex;
    public float stepInterval;
    public bool invokingStep = false;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        _movement = _inputActions.Player.Movement;
        _movement.Enable();
    }
    private void OnDisable()
    {
        _movement.Disable();
    }



    // Start is called before the first frame update
    void Start()
    {
        stepIndex = 1;
        invokingStep = false;
    }

    private void RotateGraphics(Vector2 input)
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(input.x, 0, input.y), Vector3.up);
        Graphics.rotation = Quaternion.RotateTowards(Graphics.rotation, rot, 720f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        float x=0, z = 0;
        Vector2 input;
        if (!inputDisabled)
        {
            input = _movement.ReadValue<Vector2>();

            x = GetSmoothRawAxis("Horizontal", input);
            z = GetSmoothRawAxis("Vertical", input);
        }
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize(); // get camera forward diretion ignoring y
        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();// get camera right diretion ignoring y
        
        Vector3 moveVec = camRight * x + camForward * z;

        if (moveVec.magnitude != 0f)
        {
            _controller.Move(moveVec * _speed * Time.deltaTime);

        }
        if (moveVec.magnitude > 0)
        {
            RotateGraphics(new Vector2(moveVec.normalized.x, moveVec.normalized.z));
            Animator.SetBool("isRunning", true);
            if(!invokingStep)
                InvokeRepeating("PlayNextStep", 0f, stepInterval);
            invokingStep = true;
        }
        else
        {
            Animator.SetBool("isRunning", false);
            CancelInvoke();
            invokingStep=false;
        }

        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1f;
            // resets velocity. kept at -1 to keep player tied to the ground
        }
        _velocity.y += GRAVITY * Time.deltaTime * 4f; // the extra 4f is to speed up the fall.


        _controller.Move(_velocity * Time.deltaTime);


    }
    private float GetSmoothRawAxis(string name, Vector2 input)
    {
        float sensitivity = 5f;
        float dead = 0.001f;

        if (name == "Horizontal")
        {
            float target = input.x;
            _valx = Mathf.MoveTowards(_valx, target, sensitivity * Time.unscaledDeltaTime);
            return (Mathf.Abs(_valx) < dead) ? 0f : _valx;

        }
        if (name == "Vertical")
        {
            float target = input.y;
            _valz = Mathf.MoveTowards(_valz, target, sensitivity * Time.unscaledDeltaTime);
            return (Mathf.Abs(_valz) < dead) ? 0f : _valz;

        }

        return 0;

    }

    private void PlayNextStep()
    {
        AudioManager.instance.Play("Step" + stepIndex);
        stepIndex++;
        if(stepIndex > 7)
        {
            stepIndex = 1;
        }
    }

    public void Teleport(Vector3 newPosition)
    {
        _controller.enabled = false;
        transform.position = newPosition;
        _controller.enabled = true;
    }
}
