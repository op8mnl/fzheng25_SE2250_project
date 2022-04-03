using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public float speed; //speed
    public float jump; //jump
    public GameObject healthManager;
    private Rigidbody2D _dragon; //dragon player sprite
    private PolygonCollider2D basicAttack;
    private PolygonCollider2D FlyKick;
    private bool _facingRight = true; //facing direction
    private bool _isOnHill = false;
    private bool _inPortal = false;
    public float _healthPoints;
    public GameObject fireballPrefab;
    private bool _isAb1Disabled;
    private bool _isAb2Disabled;
    private bool _isAb3Disabled;

    Animator dragonAnim; //animator for the dragon

    public void Start()
    {
        //get all the components
        _dragon = GetComponent<Rigidbody2D>();
        dragonAnim = GetComponent<Animator>();
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

    //flip the direction of the dragon sprite
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
            dragonAnim.SetBool("IsWalking", true);
        }
        else
        {
            
            dragonAnim.SetBool("IsWalking", false);
        }

        //Crouch animation
        if (Input.GetAxis("Vertical") < 0 && _inPortal == false && dragonAnim.GetBool("IsWalking") == false)
        {
            dragonAnim.SetBool("IsCrouch", true);
        }
        else
        {
            dragonAnim.SetBool("IsCrouch", false);
        }

        //get input for player to jump, add impulse force for jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_dragon.velocity.y) < 0.001f)
        {
            if (_isOnHill)
            {
                Vector3 targetVelocity = new Vector2(move * 10f, _dragon.velocity.y);
                _dragon.AddForce(new Vector2(0, jump * _dragon.gravityScale / 2), ForceMode2D.Impulse);
            }
            else
            {
                _dragon.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }

        }
    }

    private void Attack()
    {

        //Attack 1 Animations
        if (Input.GetButtonDown("Attack1") && !dragonAnim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_BasicAttack"))
        {
            dragonAnim.SetTrigger("BasicAttack");
            //basicAttack.enabled = true;
            StartCoroutine(DisableBasicAttackCollider());
        }

        //Attack 2 Animations
        if (Input.GetButtonDown("Attack2") && !dragonAnim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_FlyKick"))
        {
            dragonAnim.SetTrigger("FlyKick");
            //strike.enabled = true;
            StartCoroutine(DisableStrikeCollider());
        }

        //Fireball Ability Stuff
        if (Input.GetButtonDown("Attack3"))
        {
            if (_facingRight)
            {
                dragonAnim.SetTrigger("Fireball");
                Instantiate(fireballPrefab, new Vector2(transform.position.x + 0.2f, transform.position.y - 0.5f), fireballPrefab.transform.rotation);
                Instantiate(fireballPrefab, new Vector2(transform.position.x + 0.2f, transform.position.y - 0.35f), fireballPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 10f));
                Instantiate(fireballPrefab, new Vector2(transform.position.x + 0.2f, transform.position.y - 0.65f), fireballPrefab.transform.rotation * Quaternion.Euler(0f, 0f, -10f));
            }

            if (!_facingRight)
            {
                dragonAnim.SetTrigger("Fireball");
                Instantiate(fireballPrefab, new Vector2(transform.position.x - 0.2f,transform.position.y - 0.5f), fireballPrefab.transform.rotation* Quaternion.Euler(0f, 180f, 0f));
                Instantiate(fireballPrefab, new Vector2(transform.position.x - 0.2f, transform.position.y - 0.35f), fireballPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 170f));
                Instantiate(fireballPrefab, new Vector2(transform.position.x - 0.2f, transform.position.y - 0.65f), fireballPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 190f));
            }


        }
    }

    public int getDirection()
    {
        var dir = 1;
        if (_facingRight)
        {
            dir = 1;
        }
        if (!_facingRight)
        {
            dir = -1;
        }
        return dir;
    }

    private IEnumerator DisableStrikeCollider()
    {
        yield return new WaitForSeconds(0.03f);
        //strike.enabled = false;
        StopCoroutine(DisableStrikeCollider());
    }
    private IEnumerator DisableBasicAttackCollider()
    {
        yield return new WaitForSeconds(0.04f);
        //basicAttack.enabled = false;
        StopCoroutine(DisableBasicAttackCollider());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Hill"))
        {
            _isOnHill = true;
            _dragon.gravityScale = 3;
        }
        else
        {
            _isOnHill = false;
            _dragon.gravityScale = 1.5f;
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
