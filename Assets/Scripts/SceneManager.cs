using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private Image navigatorEye;
    [SerializeField]
    private Slider healthBar;
    private Animator navigatorEyeAnimator;

    private int numOfEyesOpen = 0;

    public int NumOfEyesOpen { 
        get { return numOfEyesOpen; } 
        set
        {
            numOfEyesOpen = value;
            if (numOfEyesOpen > 0)
            {
                navigatorEyeAnimator.SetBool("OpenEye", true);
            }
            else
            {
                StartCoroutine("CloseEyeDelay");
            }
        }
    }

    public void DecreaseHealth(float healthLoss)
    {
        healthBar.value = healthLoss;
    }

    private IEnumerator CloseEyeDelay()
    {
        yield return new WaitForSeconds(1f);
        if (numOfEyesOpen == 0)
            navigatorEyeAnimator.SetBool("OpenEye", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        navigatorEyeAnimator = navigatorEye.GetComponent<Animator>();
    }

    public void TriggerNavigatorEye(string trigger)
    {
        navigatorEyeAnimator.SetTrigger(trigger);
    }
}
