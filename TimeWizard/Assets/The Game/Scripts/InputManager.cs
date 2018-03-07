using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMB<InputManager> {

    public AnimaModel player;
    private Rigidbody rb;

    float maximumSpeed = 10;
    float decreaseIncrement = 1f;
    float increaseIncrement = .333f;
    Vector3 currentSpeed;
    float currentHighestSpeed;

    bool horInput;
    bool vertInput;

    private void Awake()
    {
        rb = player.transform.GetComponent<Rigidbody>();
        currentSpeed = new Vector3(0, 0, 0);
    }

    public override void CopyValues(SingletonMB<InputManager> copy)
    {

    }

    private float MaxSpeed(float input, float currentSpeed, float maxSpeed)
    {

        if(input < -.3f)
        {
            currentSpeed -= increaseIncrement;
        }
        else if(input > .3f)
        {
            currentSpeed += increaseIncrement;
        }
        else
        {
            currentSpeed = currentSpeed > 0 ? Mathf.Max(currentSpeed - decreaseIncrement, 0) : Mathf.Min(currentSpeed + decreaseIncrement, 0);
        }

        if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        else if(currentSpeed < -maxSpeed)
        {
            currentSpeed = -maxSpeed;
        }

        return currentSpeed;
    }

    private void FixedUpdate()
    {
        horInput = Mathf.Abs(Input.GetAxis("Horizontal")) > .5f;
        vertInput = Mathf.Abs(Input.GetAxis("Vertical")) > .5f;

        currentSpeed.x = MaxSpeed(Input.GetAxis("Horizontal"), currentSpeed.x, maximumSpeed);
        currentSpeed.z = MaxSpeed(Input.GetAxis("Vertical"), currentSpeed.z, maximumSpeed);

        currentHighestSpeed = Mathf.Max(Mathf.Abs(currentSpeed.x), Mathf.Abs(currentSpeed.z));
        //Debug.Log(player.animator.speed);
        player.SetSpeed(currentHighestSpeed);
        player.animator.speed = horInput || vertInput ? Mathf.Max(currentHighestSpeed / 10f,.2f) : 1f;

        rb.velocity = currentSpeed;


        if (Mathf.Abs(Input.GetAxis("Horizontal")) > .1f)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                player.animator.SetBool("Backwards", player.currentDirection == Direction.west);
            }
            else
            {
                player.animator.SetBool("Backwards", player.currentDirection == Direction.east);
            }
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            if(Input.GetAxis("Vertical") > 0)
            {
                player.animator.SetBool("Backwards", player.currentDirection == Direction.south);
            }
            else
            {
                player.animator.SetBool("Backwards", player.currentDirection == Direction.north);
            }
        }

        AngleTowardsMouse(player.aimSprite);
    }

    private void AngleTowardsMouse(Transform transform)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("GameWorld");
        if (Physics.Raycast( ray, out hit, 50f, layer_mask) && hit.transform.tag == "GameWorld")
        {

            Vector3 mouse = (hit.point);
            Quaternion q = new Quaternion();
            q.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
           
            transform.LookAt(mouse);
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.z, -transform.eulerAngles.y+90f);
            transform.rotation = q;

            if((q.eulerAngles.y > (360 - 45) && q.eulerAngles.y <= 360 ) || (q.eulerAngles.y >= 0 && q.eulerAngles.y < (0 + 45)))
            {
                player.SetDirection(1);
                //Debug.Log("East");
            }
            else if (q.eulerAngles.y > 90 - 45 && q.eulerAngles.y < 90 + 45)
            {
                player.SetDirection(2);
                //Debug.Log("South");
            }
            else if (q.eulerAngles.y > 180 - 45 && q.eulerAngles.y < 180 + 45)
            {
                player.SetDirection(3);
                //Debug.Log("West");
            }
            else if (q.eulerAngles.y > 270 - 45 && q.eulerAngles.y < 270 + 45)
            {
                player.SetDirection(0);
                //Debug.Log("North");
            }

        }

    }

}
