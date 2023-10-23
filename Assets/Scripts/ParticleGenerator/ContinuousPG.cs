using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousPG : ParticleGenerator
{
    public float spawnRate;
    public override void PlayVFX()
    {    
       vfx.SetFloat("spawnRate", spawnRate); 
    }

    public void PauseVFX()
    {
        vfx.SetFloat("spawnRate", 0); 
    }
}
