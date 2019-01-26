using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public bool run;
    private bool oldRun;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (run != oldRun)
        {
            if (run)
                animator.SetTrigger("Run");
            else
                animator.SetTrigger("Idle");
            oldRun = run;
        }
        
    }
}
