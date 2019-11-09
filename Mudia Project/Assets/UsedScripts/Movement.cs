
using UnityEngine;


    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed Vars")]
        //value Variables
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        public static float _gravity = 20;
        //Struct - Contains Multiple Variables (eg...3 floats)
        private Vector3 _moveDir;
        //Reference Variable
        public PlayerHandler player;
        private CharacterController _charC;
    public Collider capsule, sphere;
        private void Start()
        {
            _charC = GetComponent<CharacterController>();
        //want to make it do the capsule turns into a sphere when you press crouch thus lowering the profile
        //capsule.SetActive(true);
        _gravity = 20;
    }

        private void Update()
        {
            Move();
            
        }
        private void Move()
        {
            if (_charC.isGrounded && !PlayerHandler.isDead)
            {
            //set speed
            if (Input.GetButton("Crouch"))
            {

                moveSpeed = crouchSpeed;
            }

            else if (Input.GetButton("Sprint"))
            {
                moveSpeed = runSpeed;

            }

            else
            {
                moveSpeed = walkSpeed;

            }
                
                _moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                }
            }
            if(PlayerHandler.isDead)
            {
                _moveDir = Vector3.zero;
            }
        
        _moveDir.y -= _gravity * Time.deltaTime;
            
           
            _charC.Move(_moveDir * Time.deltaTime);
        }

    }


