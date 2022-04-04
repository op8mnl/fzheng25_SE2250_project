using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public float speed; //speed
    public float jump; //jump
    private Rigidbody2D _dragon; //dragon player sprite
    public GameObject player;
    private PolygonCollider2D basicAttack;
    private PolygonCollider2D flyAttack;
    private bool _facingRight = true; //facing direction
    private bool _isOnHill = false;
    private bool _inPortal = false;
    public float _healthPoints;
    public GameObject fireballPrefab;
    private float _expPoints = 1f;
    private LevelManager _script;
    private bool _inPortal1 = false;
    private bool _inPortal0 = false;
    public float damageToEnemy;

    private AbilitySelector _abilitySelector;


    Animator dragonAnim; //animator for the dragon

    public void Start()
    {
        //get all the components
        _script = GameObject.FindGameObjectWithTag("Script").GetComponent<LevelManager>();
        _dragon = GetComponent<Rigidbody2D>();
        dragonAnim = GetComponent<Animator>();
        _healthPoints = 100f;
        _abilitySelector = GameObject.FindGameObjectWithTag("Script").GetComponent<AbilitySelector>();
        basicAttack = GameObject.FindGameObjectWithTag("Slash").GetComponent<PolygonCollider2D>();
        flyAttack = GameObject.FindGameObjectWithTag("Strike").GetComponent<PolygonCollider2D>();
        _expPoints = 1f;
        GetComponent<HealthManager>().healthUpdate(_healthPoints);
        GetComponent<ExpManager>().expUpdate(_expPoints);

    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        Movement();
        Attack();
        Beam();
        DeathToDragon();
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
        if (_abilitySelector == null) {
            _abilitySelector = GameObject.FindGameObjectWithTag("Script").GetComponent<AbilitySelector>();
        }
        
        if (_abilitySelector.getDisabled1() == false)
        {
            //Attack 1 Animations
            if (Input.GetButtonDown("Attack1") && !dragonAnim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_BasicAttack"))
            {
                dragonAnim.SetTrigger("BasicAttack");
                basicAttack.enabled = true;
                StartCoroutine(DisableBasicAttackCollider());
            }
        }

        if (_abilitySelector.getDisabled2() == false)
        {
            //Attack 2 Animations
            if (Input.GetButtonDown("Attack2") && !dragonAnim.GetCurrentAnimatorStateInfo(0).IsName("Dragon_FlyKick"))
            {

                dragonAnim.SetTrigger("FlyKick");
                flyAttack.enabled = true;
                StartCoroutine(DisableFlyAttackCollider());

            }
        }

            
        if (_abilitySelector.getDisabled3() == false)
        {
            //Fireball Ability Stuff
            if (Input.GetButtonDown("Attack3"))
            {

                dragonAnim.SetTrigger("Fireball");
                Instantiate(fireballPrefab, new Vector2(transform.position.x - 0.3f,transform.position.y - 0.5f), fireballPrefab.transform.rotation* Quaternion.Euler(0f, 180f, 0f));
                Instantiate(fireballPrefab, new Vector2(transform.position.x - 0.1f, transform.position.y - 0.35f), fireballPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 170f));
                Instantiate(fireballPrefab, new Vector2(transform.position.x - 0.1f, transform.position.y - 0.65f), fireballPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 190f));

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

    private IEnumerator DisableFlyAttackCollider()
    {
        yield return new WaitForSeconds(0.03f);
        flyAttack.enabled = false;
        StopCoroutine(DisableFlyAttackCollider());
    }
    private IEnumerator DisableBasicAttackCollider()
    {
        yield return new WaitForSeconds(0.04f);
        basicAttack.enabled = false;
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


    public void takeDamage(float damage)
    {
        _healthPoints -= damage;
        GetComponent<HealthManager>().healthUpdate(_healthPoints);
    }
    void Beam()
    {
        if ((_inPortal1 == true) && Input.GetButtonDown("Down"))
        {
            dragonAnim.SetTrigger("Beam");
            StartCoroutine(nextLevel(1.5f, "right"));

        }
        if ((_inPortal0 == true) && Input.GetButtonDown("Down"))
        {
            dragonAnim.SetTrigger("Beam");
            StartCoroutine(nextLevel(1.5f, "left"));

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
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("portal1") || other.gameObject.CompareTag("portal0"))
        {
            _inPortal1 = false;
            _inPortal0 = false;
        }
    }

    public float getScottDamage()
    {
        return damageToEnemy;
    }

    public void DeathToDragon()
    {
        if (_healthPoints <= 0)
        {
            Camera.main.transform.parent = null;
            Destroy(player);
        }

    }

    IEnumerator nextLevel(float delayTime, string direction)

    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
        _script.getNextLevel(direction);
        getScenario();
        StopAllCoroutines();
    }
    void getScenario()
    {
        if (_script == null)
        {
            _script = GameObject.FindGameObjectWithTag("Script").GetComponent<LevelManager>();
        }


        int level = _script.getLevel();
        if (level == 1)
        {
            transform.position = new Vector3(-10.07f, -2.69f, 0);
            _inPortal1 = false;
            _inPortal0 = false;
        }
        else if (level == 2)
        {
            transform.position = new Vector3(-20.08054f, -3.560295f, 0);
            _inPortal1 = false;
            _inPortal0 = false;
        }
        else if (level == 3)
        {
            transform.position = new Vector3(-22.91002f, -2.755001f, 0);
            _inPortal1 = false;
            _inPortal0 = false;

        }
    }
}

