using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScottController : MonoBehaviour
{
    public float speed;     //speed
    public float jump;      //jump
    public float damageToEnemy;    // damage to give to enemies
    private Rigidbody2D _scott; //scott player
    private PolygonCollider2D basicAttack;
    private PolygonCollider2D strike;
    private bool _facingRight = true; //facing direction
    private float _expLevel = 1;
    public GameObject player;


    public bool getDirection()
    {
        return _facingRight;
    }

    private bool _isOnHill = false;
    private bool _inPortal1 = false;
    private bool _inPortal0 = false;
    public float _healthPoints = 100f;
    public float _expPoints = 1f;
    Animator scottAnim; //animator
    private LevelManager _script;
    private AbilitySelector _abilitySelector;
    public GameObject swordWave;

    public SpriteRenderer shield;
    private bool activeShield = false;

    private LevelDisplayManager _levelScript;

    // Start is called before the first frame update
    public void Start()
    {
        //get components
        _script = GameObject.FindGameObjectWithTag("Script").GetComponent<LevelManager>();
        _scott = GetComponent<Rigidbody2D>();
        scottAnim = GetComponent<Animator>();
        basicAttack = GameObject.FindGameObjectWithTag("basicAttack").GetComponent<PolygonCollider2D>();
        strike = GameObject.FindGameObjectWithTag("strike").GetComponent<PolygonCollider2D>();
        _healthPoints = 100f;
        _expPoints = 1f;
        GetComponent<HealthManager>().healthUpdate(_healthPoints);
        GetComponent<ExpManager>().expUpdate(_expPoints);
        shield = GameObject.FindGameObjectWithTag("shield").GetComponent<SpriteRenderer>();
        _abilitySelector = GameObject.FindGameObjectWithTag("Script").GetComponent<AbilitySelector>();
        _levelScript = GameObject.FindGameObjectWithTag("levelScript").GetComponent<LevelDisplayManager>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        _expPoints = 1f;
        _healthPoints = 100f;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();

        Attack();

        Beam();

        DeathToScott();

        
    }

    //flip the direction of the player sprite
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

        if (scottAnim == null) {
            scottAnim = GetComponent<Animator>();
        }

        //change animation when player is walking
        if (Input.GetAxis("Horizontal") != 0)
        {
            scottAnim.SetBool("IsWalking", true);
        }
        else
        {
            scottAnim.SetBool("IsWalking", false);
        }

        //Crouch animation
        if (Input.GetAxis("Vertical") < 0 && (_inPortal1 == false||_inPortal0 == false) && scottAnim.GetBool("IsWalking") == false)
        {
            scottAnim.SetBool("IsCrouch", true);
        }
        else
        {
            scottAnim.SetBool("IsCrouch", false);
        }

        //get input for player to jump, add impulse force for jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_scott.velocity.y) < 0.001f)
        {
            if (_isOnHill)
            {
                Vector3 targetVelocity = new Vector2(move * 10f, _scott.velocity.y);
                _scott.AddForce(new Vector2(0, jump * _scott.gravityScale / 2), ForceMode2D.Impulse);
            } else
            {
                _scott.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
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
            if (shield.enabled == false)
            {
                //Attack 1 Animations
                if (Input.GetButtonDown("Attack1") && !scottAnim.GetCurrentAnimatorStateInfo(1).IsName("Scott_BasicAttack"))
                {
                    scottAnim.SetTrigger("BasicAttack");
                    basicAttack.enabled = true;
                    StartCoroutine(DisableBasicAttackCollider());
                }
            }
        }

        // prevents user from using the second ability if it was selected to be disabled in the menu
        if (_abilitySelector.getDisabled2() == false)
        {
            if (shield.enabled == false)
            {
                //Attack 2 Animations
                if (Input.GetAxis("Attack2") > 0 && !scottAnim.GetCurrentAnimatorStateInfo(1).IsName("Scott_Strike"))
                {
                    scottAnim.SetTrigger("Strike");
                    strike.enabled = true;
                    StartCoroutine(DisableStrikeCollider());

                }
            }
        }

        // prevents user from using the third ability if it was selected to be disabled in the menu
        if (_abilitySelector.getDisabled3() == false)
        {
            if (Input.GetButtonDown("Shield"))
            {
                shield.enabled = !shield.enabled;
                activeShield = !activeShield;

            }
        }



    }
    private IEnumerator DisableStrikeCollider()
    {
        yield return new WaitForSeconds(0.03f);
        if (_facingRight)
        {
            
            Instantiate(swordWave, new Vector2(transform.position.x+0.9f, transform.position.y - 0.7f), swordWave.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
     

        }

        if (!_facingRight)
        {
            
            Instantiate(swordWave, new Vector2(transform.position.x - 0.9f, transform.position.y - 0.7f), swordWave.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
        
        }
        strike.enabled = false;
        StopCoroutine(DisableStrikeCollider());
    }
    private IEnumerator DisableBasicAttackCollider()
    {
        yield return new WaitForSeconds(0.04f);
        basicAttack.enabled = false;
        StopCoroutine(DisableBasicAttackCollider());
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (_scott == null) {
            _scott = GetComponent<Rigidbody2D>();
        }
        
        if (col.gameObject.CompareTag("Hill"))
        {
            _isOnHill = true;
            _scott.gravityScale = 3;
        }
        else
        {
            _isOnHill = false;
            _scott.gravityScale = 1.5f;
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
        if (other.gameObject.CompareTag("birdy"))
        {
            // if player collides with the small bird, gain 1 experience point and 2 health points
            gainExp(1f);
            gainHealth(2f);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("portal1")||other.gameObject.CompareTag("portal0"))
        {
            _inPortal1 = false;
            _inPortal0 = false;
        }
    }

    void Beam()
    {
        if ((_inPortal1 == true) && Input.GetButtonDown("Down"))
        {
            scottAnim.SetTrigger("Beam");
            StartCoroutine(nextLevel(1.5f, "right"));
            Debug.Log("Right");
            
        }
        if ((_inPortal0 == true) && Input.GetButtonDown("Down"))
        {
            scottAnim.SetTrigger("Beam");
            StartCoroutine(nextLevel(1.5f, "left"));
            Debug.Log("Left");
        }
    }

    // reduces the health of the player
    public void takeDamage(float damage){
        // Debug.Log("enabled?" + shield.enabled);
        if (shield.enabled == false)
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

    public void DeathToScott()
    {
        if (_healthPoints <= 0)
        {
            // destroys player, but sets main camera parent to null so as not to destroy the camera too
            Camera.main.transform.parent = null;
            Destroy(player);
        }
            
    }

    // updates experience points, and buffs player if experience meets or exceeds 100
    public void gainExp(float points)
    {
        _expPoints += points;
        // Debug.Log("_expPoints = " + _expPoints);

        if (_expPoints >= 100) {
            // Debug.Log("old jump: " + jump + ", old speed: " + speed + ", old damage: " + damageToEnemy);
            _expPoints -= 100;
            _expLevel += 1;  // implement a switch statement or smt to make this relavent
            speed += 1;
            // jump += 1;
            damageToEnemy += 1;
            //_healthPoints = 100f;
            //GetComponent<HealthManager>().healthUpdate(_healthPoints);
            // Debug.Log("NEW _expPoints: " + _expPoints + ", new jump: " + jump + ", new speed: " + speed + ", new damage: " + damageToEnemy);
        }

        GetComponent<ExpManager>().expUpdate(_expPoints);
    }

    public float getScottDamage() {
        return damageToEnemy;
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
