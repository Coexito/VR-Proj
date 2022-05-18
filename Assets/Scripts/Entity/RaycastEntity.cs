using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaycastEntity : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    bool canLookAgain = true;

    void Start()
    {
        ray = new Ray(transform.position, transform.forward);   
    }

    void FixedUpdate()
    {
        ray = new Ray(transform.position, transform.forward);   
        Debug.DrawRay(ray.origin, ray.direction * 20);

        // If the ray hits in the layer 10 (the entity)
        if(canLookAgain && Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
        {
            //Debug.Log("Entity Hit!");
            StartCoroutine(LookedAtEntity());
        }

    }

    IEnumerator LookedAtEntity()
    {
        canLookAgain = false;
        EntityFollowing.instance.LookedAt();
        
        // wait 20 seconds until entity can damage again
        yield return new WaitForSeconds(20);
        canLookAgain = true;
    }

}
