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
    public float _healthPoints = 100f;
    public float _expPoints = 1f;
    private bool _inPortal0 = false;
    private bool _inPortal1 = false;
    private float _expLevel = 1f;
    public float damageToEnemy;
    private LevelManager _script;
    private bool _isAb1Disabled;
    private bool _isAb2Disabled;
    private bool _isAb3Disabled;

    Animator ninjaAnim; //animator for the ninja

    public void Start()
    {
        //get all the components
        _ninja = GetComponent<Rigidbody2D>();
        ninjaAnim = GetComponent<Animator>();
        _healthPoints = 100f;
        _script = GameObject.FindGameObjectWithTag("Script").GetComponent<LevelManager>();
        slash = GameObject.FindGameObjectWithTag("slash").GetComponent<PolygonCollider2D>();
        strike = GameObject.FindGameObjectWithTag("strike").GetComponent<PolygonCollider2D>();
        _expPoints = 1f;
        GetComponent<HealthManager>().healthUpdate(_healthPoints);
        GetComponent<ExpManager>().expUpdate(_expPoints);
    }


    public void Awake()
    {
        DontDestroyOnLoad(this);
        _expPoints = 1f;
        _healthPoints = 100f;
    }

    private void Update()
    {
        Movement();
        Attack();
        Beam();
    }

    public bool getDirection()
    {
        return _facingRight;
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
        if (Input.GetAxis("Vertical") < 0 && (_inPortal1 == false || _inPortal0 == false) && ninjaAnim.GetBool("IsWalking") == false)
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
            //slash.enabled = true;
            StartCoroutine(DisableBasicAttackCollider());
        }

        //Attack 2 Animations
        if (Input.GetButtonDown("Attack2") && !ninjaAnim.GetCurrentAnimatorStateInfo(0).IsName("ninja_strike"))
        {
            ninjaAnim.SetTrigger("Strike");
            //strike.enabled = true;
            StartCoroutine(DisableStrikeCollider());
        }
    }

    private IEnumerator DisableStrikeCollider()
    {
        yield return new WaitForSeconds(0.03f);
        if (_facingRight)
        {
            Instantiate(strike, new Vector2(transform.position.x + 0.9f, transform.position.y - 0.7f), strike.transform.rotation);
        }

        if (!_facingRight)
        {
            Instantiate(strike, new Vector2(transform.position.x - 0.9f, transform.position.y - 0.7f), strike.transform.rotation);

        }
        strike.enabled = false;
        StopCoroutine(DisableStrikeCollider());
    }
    private IEnumerator DisableBasicAttackCollider()
    {
        yield return new WaitForSeconds(0.04f);
        //slash.enabled = false;
        StopCoroutine(DisableBasicAttackCollider());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (_ninja == null)
        {
            _ninja = GetComponent<Rigidbody2D>();
        }

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
        if (other.gameObject.CompareTag("portal1") || other.gameObject.CompareTag("portal0"))
        {
            _inPortal1 = false;
        }
        if (other.gameObject.CompareTag("portal0"))
        {
            _inPortal0 = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("portal1"))
        {
            _inPortal1 = true;
        }
        if (other.gameObject.CompareTag("portal0"))
        {
            _inPortal0 = true;
        }

    }

    public void takeDamage(float damage)
    {
        if (true)
        {
            _healthPoints -= damage;
            GetComponent<HealthManager>().healthUpdate(_healthPoints);
        }
    }

    void Beam()
    {
        if ((_inPortal1 == true || _inPortal0 == true) && Input.GetButtonDown("Down"))
        {
            ninjaAnim.SetTrigger("Beam");
            //Invoke("toggleVisibility", 1.25f);
            StartCoroutine(nextLevel(1.5f, "right"));

        }

    }

    public void gainExp(float points)
    {
        _expPoints += points;
        Debug.Log("_expPoints = " + _expPoints);

        if (_expPoints >= 100)
        {
            Debug.Log("old jump: " + jump + ", old speed: " + speed + ", old damage: " + damageToEnemy);
            _expPoints -= 100;
            speed += 1;
            jump += 1;
            damageToEnemy += 1;
            GetComponent<HealthManager>().healthUpdate(100f);
            Debug.Log("NEW _expPoints: " + _expPoints + ", new jump: " + jump + ", new speed: " + speed + ", new damage: " + damageToEnemy);
        }

        GetComponent<ExpManager>().expUpdate(_expPoints);
    }

    public float getScottDamage()
    {
        return damageToEnemy;
    }

    IEnumerator nextLevel(float delayTime, string direction)

    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
        _script.getNextLevel(direction);
        StopAllCoroutines();
    }


}