using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAnim : MonoBehaviour
{
    public Transform Camera;
    public Material mat;
    public Vector2 offset = Vector2.zero;
    public float scale=1f; 
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(0f, Camera.position.y / 100f/scale);
        mat.mainTextureOffset = offset;
    }
}
