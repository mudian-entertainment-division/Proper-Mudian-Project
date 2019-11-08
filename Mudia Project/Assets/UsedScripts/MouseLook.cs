using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [AddComponentMenu("RPG/Player/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        public enum RotationalAxis
        {
            MouseX,
            MouseY,
        }
        [Header("Rotation Variables")]
        public RotationalAxis axis = RotationalAxis.MouseX;
        [Range(0,200)]
        public float sensitivity = 15;
        public float minY = -60, maxY = 60;
        private float _rotY;

        private void Start()
        {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true; // freeze the rigid body rotation so it doesn't muk up our mouse look code
        }
        Cursor.lockState = CursorLockMode.Locked; // lock the mouse cursor
            Cursor.visible = true; // hide the mouse cursor


            if (GetComponent<Camera>()) // if the object is a camera
            {
                axis = RotationalAxis.MouseY; // look up and down
            }
        }

        private void Update()
        {
            if (!PlayerHandler.isDead)
            {
                // If axis movement is X (the player left and rght)
                if (axis == RotationalAxis.MouseX)
                {
                    //Change rotation of X Axis
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
                }
                else // otherwise axis movement is Y (up/down look)
                {

                    _rotY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                    _rotY = Mathf.Clamp(_rotY, minY, maxY);
                    transform.localEulerAngles = new Vector3(-_rotY, 0, 0);
                }
            }
        }

    }

