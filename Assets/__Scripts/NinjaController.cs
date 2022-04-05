using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float speed; //speed
    public float jump; //jump
    private Rigidbody2D _ninja; //ninja player sprite
    public GameObject player;
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
    private AbilitySelector _abilitySelector;
    private LevelDisplayManager _levelScript;
    public GameObject arrowPrefab;




    Animator ninjaAnim; //animator for the ninja

    public void Start()
    {
        //get all the components
        _ninja = GetComponent<Rigidbody2D>();
        ninjaAnim = GetComponent<Animator>();
        _healthPoints = 100f;
        _script = GameObject.FindGameObjectWithTag("Script").GetComponent<LevelManager>();
        slash = GameObject.FindGameObjectWithTag("Slash").GetComponent<PolygonCollider2D>();
        strike = GameObject.FindGameObjectWithTag("Strike").GetComponent<PolygonCollider2D>();
        _expPoints = 1f;
        GetComponent<HealthManager>().healthUpdate(_healthPoints);
        GetComponent<ExpManager>().expUpdate(_expPoints);
        _abilitySelector = GameObject.FindGameObjectWithTag("Script").GetComponent<AbilitySelector>();
        _levelScript = GameObject.FindGameObjectWithTag("levelScript").GetComponent<LevelDisplayManager>();


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
        DeathToNinja();
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
        if (_abilitySelector == null) {
            _abilitySelector = GameObject.FindGameObjectWithTag("Script").GetComponent<AbilitySelector>();
        }

        // prevents user from using the first ability if it was selected to be disabled in the menu
        if (_abilitySelector.getDisabled1() == false)
        {
            //Attack 1 Animations
            if (Input.GetButtonDown("Attack1") && !ninjaAnim.GetCurrentAnimatorStateInfo(0).IsName("ninja_slash"))
            {
                ninjaAnim.SetTrigger("Slash");
                slash.enabled = true;
                StartCoroutine(DisableSlashCollider());
            }
        }

        // prevents user from using the second ability if it was selected to be disabled in the menu
        if (_abilitySelector.getDisabled2() == false)
        {
            //Attack 2 Animations
            if (Input.GetButtonDown("Attack2") && !ninjaAnim.GetCurrentAnimatorStateInfo(0).IsName("ninja_strike"))
            {
                ninjaAnim.SetTrigger("Strike");
                strike.enabled = true;
                StartCoroutine(DisableStrikeCollider());
            }
        }

        // prevents user from using the third ability if it was selected to be disabled in the menu
        if (_abilitySelector.getDisabled3() == false)
        {
            //Arrow Ability Stuff
            if (Input.GetButtonDown("Attack3"))
            {
                ninjaAnim.SetTrigger("Arrow");
                if (_facingRight)
                {
                    Instantiate(arrowPrefab, new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f), arrowPrefab.transform.rotation);
                }

                if (!_facingRight)
                {
                    
                    Instantiate(arrowPrefab, new Vector2(transform.position.x - 0.2f, transform.position.y - 0.2f), arrowPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                }

            }
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
    private IEnumerator DisableSlashCollider()
    {
        yield return new WaitForSeconds(0.04f);
        slash.enabled = false;
        StopCoroutine(DisableSlashCollider());
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



    // reduces the health of the player
    public void takeDamage(float damage)
    {
        if (true)
        {
            _healthPoints -= damage;
            GetComponent<HealthManager>().healthUpdate(_healthPoints);
        }
    }

    // increases health points
    public void gainHealth(float hp)
    {
        _healthPoints += hp;
        GetComponent<HealthManager>().healthUpdate(_healthPoints);

    }

    public void DeathToNinja()
    {
        if (_healthPoints <= 0)
        {
            // destroys player, but sets main camera parent to null so as not to destroy the camera too
            Camera.main.transform.parent = null;
            Destroy(player);
        }

    }

    public float getScottDamage()
    {
        return damageToEnemy;
    }

    // updates experience points, and buffs player if experience meets or exceeds 100
    public void gainExp(float points)
    {
        _expPoints += points;

        if (_expPoints >= 100)
        {
            _expPoints -= 100;
            _expLevel += 1;  // implement a switch statement or smt to make this relavent
            speed += 1;
            damageToEnemy += 1;
        }

        GetComponent<ExpManager>().expUpdate(_expPoints);
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
        if (other.gameObject.CompareTag("birdy"))
        {
            // if player collides with the small bird, gain 1 experience point and 2 health points
            gainExp(1f);
            gainHealth(2f);
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
    void Beam()
    {
        if ((_inPortal1 == true) && Input.GetButtonDown("Down"))
        {
            ninjaAnim.SetTrigger("Beam");
            StartCoroutine(nextLevel(1.5f, "right"));

        }
        if ((_inPortal0 == true) && Input.GetButtonDown("Down"))
        {
            ninjaAnim.SetTrigger("Beam");
            StartCoroutine(nextLevel(1.5f, "left"));

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

            // sets level display text and font color
            _levelScript.setLevelText(1);
                _levelScript.setTextBlack();
            }
            else if (level == 2)
            {
                transform.position = new Vector3(-20.08054f, -3.560295f, 0);
                _inPortal1 = false;
                _inPortal0 = false;

            // sets level display text and font color
            _levelScript.setLevelText(2);
                _levelScript.setTextWhite();
            }
            else if (level == 3)
            {
                transform.position = new Vector3(22.91002f, -2.755001f, 0);
                _inPortal1 = false;
                _inPortal0 = false;

            // sets level display text and font color
            _levelScript.setLevelText(3);
                _levelScript.setTextWhite();
            }  
        }
    }

