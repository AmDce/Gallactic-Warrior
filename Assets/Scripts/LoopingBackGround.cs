using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackGround : MonoBehaviour
{
    public float BgSpeed;
    public Renderer BgRender;



    // Update is called once per frame
    void Update()
    {
        BgRender.material.mainTextureOffset += new Vector2(BgSpeed * Time.deltaTime, 0f);
    }
}
