using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float speed; //speed
    public float jump; //jump
    public GameObject healthManager;
    private Rigidbody2D _ninja; //ninja player sprite
    private PolygonCollider2D slash;
    private PolygonCollider2D strike;
    private bool _facingRight = true; //facing direction
    private bool _isOnHill = false;
    private bool _inPortal = false;
    public float _healthPoints;

    Animator ninjaAnim; //animator for the ninja

    public void Start()
    {
        //get all the components
        _ninja = GetComponent<Rigidbody2D>();
        ninjaAnim = GetComponent<Animator>();
        _healthPoints = 100f;
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        Movement();
        Attack();
    }

    //flip the direction of the ninja sprite
    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Movement()
    {

        //flip the character based on which direction ur moving
        var move = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") > 0 && _facingRight == false)
        {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") < 0 && _facingRight == true)
        {
            Flip();
        }

        //translate the position of the player
        transform.position += new Vector3(move, 0, 0) * Time.deltaTime * speed;

        //change animation when player is walking
        if (Input.GetAxis("Horizontal") != 0)
        {
            ninjaAnim.SetBool("IsWalking", true);
        }
        else
        {
            ninjaAnim.SetBool("IsWalking", false);
        }

        //Crouch animation
        if (Input.GetAxis("Vertical") < 0 && _inPortal == false && ninjaAnim.GetBool("IsWalking") == false)
        {
            ninjaAnim.SetBool("IsCrouch", true);
        }
        else
        {
            ninjaAnim.SetBool("IsCrouch", false);
        }

        //get input for player to jump, add impulse force for jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_ninja.velocity.y) < 0.001f)
        {
            if (_isOnHill)
            {
                Vector3 targetVelocity = new Vector2(move * 10f, _ninja.velocity.y);
                _ninja.AddForce(new Vector2(0, jump * _ninja.gravityScale / 2), ForceMode2D.Impulse);
            }
            else
            {
                _ninja.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }

        }
    }

    private void Attack()
    {

        //Attack 1 Animations
        if (Input.GetButtonDown("Attack1") && !ninjaAnim.GetCurrentAnimatorStateInfo(0).IsName("ninja_slash"))
        {
            ninjaAnim.SetTrigger("Slash");
            slash.enabled = true;
            StartCoroutine(DisableBasicAttackCollider());
        }

        //Attack 2 Animations
        if (Input.GetButtonDown("Attack2") && !ninjaAnim.GetCurrentAnimatorStateInfo(0).IsName("ninja_strike"))
        {
            ninjaAnim.SetTrigger("Strike");
            strike.enabled = true;
            StartCoroutine(DisableStrikeCollider());
        }
    }

    private IEnumerator DisableStrikeCollider()
    {
        yield return new WaitForSeconds(0.03f);
        strike.enabled = false;
        StopCoroutine(DisableStrikeCollider());
    }
    private IEnumerator DisableBasicAttackCollider()
    {
        yield return new WaitForSeconds(0.04f);
        slash.enabled = false;
        StopCoroutine(DisableBasicAttackCollider());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Hill"))
        {
            _isOnHill = true;
            _ninja.gravityScale = 3;
        }
        else
        {
            _isOnHill = false;
            _ninja.gravityScale = 1.5f;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("portal1")) //CHANGE TO PORTAL2 once created
        {
            _inPortal = false;
        }
    }

    public void takeDamage(float damage)
    {
        _healthPoints -= damage;
        healthManager.GetComponent<HealthManager>().healthUpdate(_healthPoints);
    }


}