using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 input;
    public float moveSpeed = 20f;
    public GameObject landParticles;
    bool hasLanded;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var newInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Mathf.Abs(newInput.x) > 0 && Mathf.Abs(newInput.y) > 0)
        {
            newInput.y = 0;
        }

        if(rb.velocity.magnitude < 0.1f && !hasLanded && newInput != input && newInput != Vector2.zero)
        {
            if (landParticles != null)
            {
                Instantiate(landParticles, transform.position, Quaternion.identity);
            }
             hasLanded = true;
        }

        if(newInput != Vector2.zero && rb.velocity.magnitude < 0.1f)
        {
            input = newInput;
            transform.up = -input;
            hasLanded = false;
        }
        rb.velocity = input * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}
