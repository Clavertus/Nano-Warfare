using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChromaticControl : MonoBehaviour
{

    public float trackedHP;
    public Target target;

    public Volume v;
    public ChromaticAberration ca;

    void Start()
    {
        target = GetComponent<Target>();
        v.profile.TryGet(out ca);
        trackedHP = target.maxHP;

    }

    void Update()
    {
        if(trackedHP != target.hp)
        {
            CA_control(Mathf.Abs(trackedHP - target.hp));
            trackedHP = target.hp;          
        }


        ca.intensity.value -= Time.deltaTime;
    }

    void CA_control(float damage)
    {
        ca.intensity.value = damage / 100 + 0.2f;
    }
}
