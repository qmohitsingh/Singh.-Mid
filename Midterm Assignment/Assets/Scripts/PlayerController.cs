using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController instance;

    public float moveSpeed, gravityModifier, jumpPower, runSpeed = 12f;
    public CharacterController charCon;

    private Vector3 moveInput;

    public Transform camTrans;

    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    public LayerMask whatIsGround;

    public Transform firePoint;

    public GameObject bullet;

    public bool isPlayerAlive = false;
    
    void Update()
    {

            if (!isPlayerAlive) return;

            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = horiMove + vertMove;
            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * runSpeed;
            }
            else
            {
                moveInput = moveInput * moveSpeed;
            }

            moveInput.y = yStore;

            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (charCon.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }
            //Handle Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = jumpPower;

            } 

            charCon.Move(moveInput * Time.deltaTime);

            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            if (invertX)
            {
                mouseInput.x = -mouseInput.x;
            }
            if (invertY)
            {
                mouseInput.y = -mouseInput.y;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

            camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

            if (Input.GetMouseButtonDown(0))
            {
                FireShot();
            }      
    }

    void FireShot()
    {
        // Instantiate bullet prefab at the fire point's position and rotation
        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

        // Enable Trail Renderer
        TrailRenderer trailRenderer = newBullet.GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
    }


    public void SetIsPlayerAlive(bool status) {
        isPlayerAlive = status;
    }


}
