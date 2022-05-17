using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFollowing : MonoBehaviour
{
    public static EntityFollowing instance;

    [SerializeField] int lives = 5;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void LookedAt()
    {
        
    }
}
