using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    float alpha = 0;
    public MeshRenderer[] meshRenderers;

    void Start()
    {
        
    }

    void Update()
    {
        if(alpha < 0.45){
            alpha += 0.15f * Time.deltaTime;
            meshRenderer.material.SetFloat("_Alpha", alpha);
        }else{
            alpha = 0.45f;
            meshRenderer.material.SetFloat("_Alpha", alpha);
            meshRenderers[0].enabled = true;
            meshRenderers[1].enabled = true;
        }
    }
}
