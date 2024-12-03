using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float health = 100.0f;

    public List<GameObject> inventory = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null) return;

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKey(KeyCode.Space);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
        if (!controller.isGrounded)
        {
            velocity.y = 0f;
        }

        if (isRunning)
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        if (isJumping)
        {
            velocity.y = Mathf.Sin(Time.deltaTime * Mathf.PI);  
        }


    }

    /* both of these methods are probably not gonna be used except if the player somwhow doesn't realize stepping in fire is dangerous */
    public void damage(int damage)
    {
        // decrease health by the amount of damage
        health -= damage;
        Debug.Log("Damaging the player by " + damage);
    }

    // kill the player if their health reaches 0
    void die()
    { 
        if (health == 0.0f)
        {
            // destroy the PlayerController 
            Destroy(this);
            Debug.Log("Player has died");
        }
    }
}