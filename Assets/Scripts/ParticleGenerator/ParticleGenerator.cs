using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class ParticleGenerator : MonoBehaviour
{

    public VisualEffect vfx;

    public abstract void PlayVFX();


}
