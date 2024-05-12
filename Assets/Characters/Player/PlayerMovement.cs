using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Component
    private Rigidbody2D _rbody;
    private Animator _animator;

    // Serialized Fieles
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;

    // private Atributes
    #region Properties
    private bool _grounded = false;
    public bool Grounded
    {
        private set
        {
            if(_grounded != value)
            {
                _grounded = value;
                _animator.SetBool("grounded", _grounded);
            }
        }
        get => _grounded;
    }
    #endregion
    
        
   
    private float _horizontalMovement;
    public float HorizontalMovement
    {
        private set 
        {
            if(value != _horizontalMovement)
            {
                _horizontalMovement = value;

                if(_horizontalMovement != 0)
                    FaceRight = _horizontalMovement > 0;

                // Vector3 temp = transform.localScale;
                // if(_horizontalMovement > 0)
                //     temp.x = Mathf.Abs(temp.x);
                // else if(_horizontalMovement < 0)
                //     temp.x = -temp.x;
                // transform.localScale= temp;

                _animator.SetFloat("xSpeed", MathF.Abs(_horizontalMovement)); //untuk mereturn nilai horizontal positif

            }
        }
        get => _horizontalMovement;
    }

    private float _verticalDelta;

    public float VerticalDelta
    {
        private set
        {
            if(_verticalDelta != value)
            {
                _verticalDelta = value;
                _animator.SetFloat("yDelta", _verticalDelta);
            }
        }
        get=> _verticalDelta;
    }
    private bool _faceRight = true;
    public bool FaceRight
    {
        private set
        {
            if(_faceRight != value)
            {
                _faceRight = value;
                Flip();
            }
        }
        get => _faceRight;
    }

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        VerticalDelta = _rbody.velocity.y;
        // Debug.Log(_onTheGround);
        HorizontalMovement = Input.GetAxis("Horizontal");

        Jump();
    }

    private void FixedUpdate()
    {
         //untuk meliat hasil // Debug.Log(Input.GetAxis("Horizontal"));
        Move();
    }
    
    private void Move()
    {
        _rbody.velocity = new Vector2(
            moveSpeed * _horizontalMovement, 
            _rbody.velocity.y);

    }

    private void Jump()
    {
        if(Grounded && Input.GetButtonDown("Jump"))
        {
          _animator.SetTrigger("jumpTrigger");
          _rbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }


    private void GroundCheck()
    {
        // _rbody => akan menembakkan sebuah array tergantung arahanya
        // collider = > menembakakkan collider itu tersendiri 
        // phsyic2d => Akan  mengambil titik tengah
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhits = _rbody.Cast(Vector2.down, hits,0.5f); //mengembalikan nilai int
        // if numhits == 0, then player is on the ground
        Grounded = numhits > 0; // mengecek apakah lompatannya nyampai ke tanah
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
}
 