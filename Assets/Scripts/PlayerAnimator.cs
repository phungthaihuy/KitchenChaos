using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    private Animator anim;
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Playerr player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        anim.SetBool(IS_WALKING, player.IsWalking());
    }
}
