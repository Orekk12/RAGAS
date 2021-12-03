using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControler : MonoBehaviour
{
    private LineRenderer linerenderer;

    [SerializeField]
    public Texture[] textures;
    public int animationStep;

    [SerializeField]
    public float fps = 20f;
    public float fpsCounter;
    public Transform target;
    public Transform starting;

    private void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();
    }

    public void AssignTarget(Vector3 startPosition,Transform newTarget)
    {
        linerenderer.positionCount = 2;
        linerenderer.SetPosition(0, startPosition);
        target = newTarget;
    }
    private void Update()
    {
        linerenderer.SetPosition(1,new Vector3(target.position.x, target.position.y, target.position.z));
        linerenderer.SetPosition(0, new Vector3(starting.position.x, starting.position.y, starting.position.z));

        fpsCounter += Time.deltaTime;
        if (fpsCounter>=1f/fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
            {
                animationStep = 0;
            }
            linerenderer.material.SetTexture("_MainTex", textures[animationStep]);
            fpsCounter = 0f;
        }
    }
}
