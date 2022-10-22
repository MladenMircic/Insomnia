using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private Image navigatorEye;
    private Animator navigatorEyeAnimator;

    private int numOfEyesOpen = 0;

    public int NumOfEyesOpen {
        get { return numOfEyesOpen; }
        set {
            numOfEyesOpen = value;
            Debug.Log(numOfEyesOpen);
            if (numOfEyesOpen > 0)
            {
                Debug.Log(numOfEyesOpen);
                navigatorEyeAnimator.SetTrigger("OpenEye");
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        navigatorEyeAnimator = navigatorEye.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
