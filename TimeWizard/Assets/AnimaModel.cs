using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class AnimaModel : MonoBehaviour {

    public Material material;

    public GameObject front;
    public GameObject side;
    public GameObject back;

    public SpriteMeshInstance[] frontSprite;
    public SpriteMeshInstance[] sideSprite;
    public SpriteMeshInstance[] backSprite;

    public Animator animator;
    public Rigidbody rb;

    public Transform aimSprite;

    public Direction currentDirection;


    private void Awake()
    {

    }

    private void Start()
    {
        currentDirection = Direction.east;
        SetDirection((int)currentDirection);
        rb = transform.GetComponent<Rigidbody>();

        SpriteMeshInstance[] list = front.transform.parent.GetComponentsInChildren<SpriteMeshInstance>();

        for (int i = 0; i < list.Length; i++)
        {
            list[i].sharedMaterial = material;
        }

        
        /*
        for (int i = 0; i < frontSprite.Length; i++)
        {
            frontSprite[i].sharedMaterial = material;
        }
        for (int i = 0; i < sideSprite.Length; i++)
        {
            sideSprite[i].sharedMaterial = material;
        }
        for (int i = 0; i < backSprite.Length; i++)
        {
            backSprite[i].sharedMaterial = material;
        }
        */
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void SetDirection(int direction)
    {
        bool north = direction == (int)Direction.north;
        bool east = direction == (int)Direction.east;
        bool south = direction == (int)Direction.south;
        bool west = direction == (int)Direction.west;

        animator.SetInteger("Direction", direction);

        if((Direction)direction != currentDirection)
        {
            currentDirection = (Direction)direction;

            back.SetActive(north);
            front.SetActive(south);
            side.SetActive((east || west));

            animator.SetLayerWeight(animator.GetLayerIndex("Side"), east || west ? 1 : 0);
            animator.SetLayerWeight(animator.GetLayerIndex("SideAddative"), east || west ? 1 : 0);
            animator.SetLayerWeight(animator.GetLayerIndex("Front"), south ? 1 : 0);
            animator.SetLayerWeight(animator.GetLayerIndex("Back"), north ? 1 : 0);

            if (east)
            {
                side.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                side.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void Update()
    {
        //Debug.Log(rb.velocity);
    }

    public void ChangeSprite(ModelSlot modelSlot, Sprite fSprite, Sprite sSprite, Sprite bSprite)
    {
        int index = (int)modelSlot;
        if (frontSprite[index].spriteMeshIsInstance && sideSprite[index].spriteMeshIsInstance && backSprite[index].spriteMeshIsInstance)
        {
            frontSprite[index].spriteMesh.sprite = fSprite;
            sideSprite[index].spriteMesh.sprite = sSprite;
            backSprite[index].spriteMesh.sprite = bSprite;
        }
    }
}

public enum Direction
{
    north, east, south, west
}


public enum ModelSlot
{
    head,
    neck,
    torso,
    leftArm,
    leftHand,
    rightArm,
    rightHand,
    hip,
    leftLeg,
    legFoot,
    rightLeg,
    rightFoot
}
