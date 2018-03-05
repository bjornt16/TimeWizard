using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Player : MonoBehaviour {

    public SpriteMeshInstance torso;

    public Sprite torsoSprite;

    IEnumerator wait()
    {
        yield return new WaitForSeconds(.1f);
        torso.spriteMesh.sprite = torsoSprite;
    }

    private void Start()
    {
        if(torsoSprite != null)
        {
            //SpriteMesh sM = ScriptableObject.CreateInstance<SpriteMesh>();
            //sM.sprite = torsoSprite;
            //sM.sharedMesh = torso.spriteMesh.sharedMesh;
            StartCoroutine(wait());
        }
    }
}
