using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCouterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCounter;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        

        containerCounter.OnPlayerGrabbed += ContainerCounter_OnPlayerGrabbed;
    }

    private void ContainerCounter_OnPlayerGrabbed(object sender, System.EventArgs e)
    {
        anim.SetTrigger(OPEN_CLOSE);
    }
}
