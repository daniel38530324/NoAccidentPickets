using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    float alpha = 0;
    public MeshRenderer meshRenderers;
    public GameObject video;
    private bool start;

    void Start()
    {
        
    }

    void Update()
    {
        if (!start)
            return;
        if(alpha < 0.45){
            alpha += 0.15f * Time.deltaTime;
            meshRenderer.material.SetFloat("_Alpha", alpha);
        }else{
            alpha = 0.45f;
            meshRenderer.material.SetFloat("_Alpha", alpha);
            meshRenderers.enabled = true;
            video.SetActive(true);
        }
    }

    private void OnEnable()
    {
        start = true;
    }
    
    private void OnDisable()
    {
        start = false;
        alpha = 0;
        meshRenderer.material.SetFloat("_Alpha", alpha);
        meshRenderers.enabled = false;
        video.SetActive(false);
    }
}
