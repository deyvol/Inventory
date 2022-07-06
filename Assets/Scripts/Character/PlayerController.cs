using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float gravity;
    public float jumpForce;

    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _forward, _right;
    private float fallVelocity;
    public float angularVelocity = 360f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _forward = Camera.main.transform.forward;
        _right = Camera.main.transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameActive)
        {
            var directionHorz = Input.GetAxis("Horizontal");
            var directionVert = Input.GetAxis("Vertical");

            SetAnimation(directionHorz, directionVert);

            _forward.y = 0;
            _right.y = 0;

            Vector3 movementDirection = Vector3.Normalize((_forward * directionVert) + (_right * directionHorz));

            if (movementDirection.magnitude > 0f)
            {
                float angle = Vector3.SignedAngle(transform.forward, movementDirection, Vector3.up);
                float rotationAngleToPerform = Mathf.Sign(angle) * angularVelocity * Time.deltaTime;                
                rotationAngleToPerform = Mathf.Clamp(rotationAngleToPerform, -Mathf.Abs(angle), Mathf.Abs(angle));

                Quaternion rotationToPerform = Quaternion.AngleAxis(rotationAngleToPerform, Vector3.up);
                Quaternion newRotation = rotationToPerform * transform.rotation;
                transform.rotation = newRotation;
            }

            movementDirection *= moveSpeed;

            movementDirection = SetGravity(movementDirection);

            _characterController.Move(movementDirection * Time.deltaTime);
        }        
    }

    private void SetAnimation(float directionH, float directionV)
    {        
        if (directionH == 0 && directionV == 0)
        {
            //Idle
            _animator.SetInteger("State", 0);
        } else
        {
            //Walk
            _animator.SetInteger("State", 1);
        }
    }

    private Vector3 SetGravity(Vector3 movement)
    {
        if (_characterController.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movement.y = fallVelocity;
        } else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movement.y = fallVelocity;
        }        
        return movement;
    }
}
