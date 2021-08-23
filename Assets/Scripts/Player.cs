using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;
    private Animator anim;
    private SpriteRenderer sr;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private string IN_THE_AIR_ANIMATION = "InTheAir";

    private bool isGrounded = true;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMoveKeyboard(){

        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

    }

    void AnimatePlayer(){

        if(isGrounded){
            anim.SetBool(IN_THE_AIR_ANIMATION, false);
            if(movementX < 0){
                anim.SetBool(WALK_ANIMATION, true);
                sr.flipX = true;
            } else if(movementX > 0){

                anim.SetBool(WALK_ANIMATION, true);
                sr.flipX = false;    
            }else{
                anim.SetBool(WALK_ANIMATION, false);
            }
        } else {
            if(movementX < 0){
                sr.flipX = true;
                anim.SetBool(IN_THE_AIR_ANIMATION, true);
            } else if(movementX > 0){
                sr.flipX = false;
                anim.SetBool(IN_THE_AIR_ANIMATION, true);
            }
        }

    }
    void PlayerJump(){
        
        if(Input.GetButtonDown("Jump") && isGrounded){
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool(IN_THE_AIR_ANIMATION, true);
        } 

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GROUND_TAG)) {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag(ENEMY_TAG)) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(ENEMY_TAG)){
            Destroy(gameObject);
        }
    }
}
