using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    bool isCollectible = true;
    public float dissolveTime;
    public GameObject item;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(isCollectible)
        {
            isCollectible = false;
            TriggerEffect();
            MakeItemDisappear();
        }
        
    }

    public abstract void TriggerEffect();

    public void MakeItemDisappear()
    {
        StartCoroutine(DisappearCoroutine(dissolveTime));
    }

    public IEnumerator DisappearCoroutine(float duration) 
    {
        float timeElapsed = 0;
        var material = item.GetComponent<Renderer>().material;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float step = timeElapsed / duration;
            material.SetFloat("_Dissolvance", Mathf.Lerp(0f, 1f, step));
            yield return null;
        }
        item.GetComponent<Renderer>().sharedMaterial.SetFloat("_Dissolvance", 1f);
        Destroy(this.gameObject);
    }

}
