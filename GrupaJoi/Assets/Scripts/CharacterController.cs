using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float laneSpeed;
    public float jumpLength;
    public float jumpHeight;
    public float slideLength;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 targetPosition;
    private int currentLane = 0;
    private Vector3 hTargetPosition;
    private float jumpStart;
    private bool jumping;
    private bool sliding;
    private float slideStart;
    private BoxCollider bc;
    private Vector3 bcSize;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        anim.Play("runLoop");
        bc = GetComponent<BoxCollider>();
        bcSize = bc.size;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        rb.velocity = Vector3.forward * speed;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(1);
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }
        

        if(jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;
            if(ratio >= 1f)
            {
                jumping = false;
                anim.SetBool("Jumping", false);
            }
            else
            {
                hTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            }
        }

        if(sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;
            if(ratio >= 1f)
            {
                sliding = false;
                anim.SetBool("Sliding", false);
                bc.size = bcSize;
            }
        }

        targetPosition = new Vector3(hTargetPosition.x, hTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);
    }

    void ChangeLane(int dir)
    {
        int targetLane = currentLane + dir;
        if (targetLane < -1 || targetLane > 1) return;
        currentLane = targetLane;
        hTargetPosition = new Vector3(currentLane, 0, 0);
    }

    void Jump()
    {
        if(jumping == false) // if(!jumping)
        {
            jumpStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / jumpLength);
            anim.SetBool("Jumping", true);
            jumping = true;
        }
    }

    void Slide()
    {
        if(!sliding && !jumping)
        {
            slideStart = transform.position.z;
            anim.SetBool("Sliding",true);
            anim.SetFloat("JumpSpeed", speed / slideLength);
            sliding = true;
            Vector3 newSize = bc.size;
            newSize.y = newSize.y / 2;
            bc.size = newSize;
        }
    }


}
