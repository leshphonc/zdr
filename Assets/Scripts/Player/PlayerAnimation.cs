using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _anim;
    private Rigidbody2D _rb;
    
    private PlayerController _player;
    
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int VelocityY = Animator.StringToHash("velocityY");
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Ground = Animator.StringToHash("ground");
    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat(Speed, Mathf.Abs(_rb.velocity.x));
        _anim.SetFloat(VelocityY, _rb.velocity.y);
        _anim.SetBool(Jump, _player.isJump);
        _anim.SetBool(Ground, _player.isGround);
    }
}
