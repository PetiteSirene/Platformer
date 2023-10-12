using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleGenerator : MonoBehaviour
{

    public VisualEffect vfx;

    public void PlayVFX()
    {
        vfx.Play();
    }

}
