using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    public bool jump;
    public bool isGrounded;
    public float jumpForce;
    Rigidbody _rigidbody;

    public GameObject[] plataforms;

    public Transform inPos;

    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (jump && isGrounded) //isGrounded == false
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        Move(); 
    }

    public void Move()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        transform.Translate(0,0,moveVertical*speed*Time.deltaTime);
        transform.Rotate(0, moveHorizontal, 0 * turnSpeed * Time.deltaTime);      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUpJump"))
        {
            jump = true;
            plataforms[0].GetComponent<Rigidbody>().useGravity = true;
            plataforms[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            plataforms[1].GetComponent<Rigidbody>().useGravity = true;
            plataforms[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            plataforms[2].GetComponent<Rigidbody>().useGravity = true;
            plataforms[2].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.CompareTag("Danger"))
        {
            transform.position = inPos.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


}
