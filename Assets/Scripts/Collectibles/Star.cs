using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Collectible
{
    public override void TriggerEffect()
    {
        GameManager.Instance.AddStar();
    }

}
