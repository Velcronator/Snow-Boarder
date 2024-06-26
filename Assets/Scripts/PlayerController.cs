using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 3.0f;

    Rigidbody2D rd2d;

    private void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            // rotate the player to the left using torque
            rd2d.AddTorque(torqueAmount);

        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            // rotate the player to the right using torque
            rd2d.AddTorque(-torqueAmount);
        }


    }
}
