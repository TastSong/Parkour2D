using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 人物移动 碰撞检测
public class PlayerCtr : MonoBehaviour {
    public static PlayerCtr manager = null;
    public GameObject swordSpace;
    public GameObject sword;
    public float jumpForce = 300f;
    public float jumpOffsetTime = 0.2f;

    private Rigidbody2D rig;
    private float startGravity;
    private bool isOnGround = true;
    private Vector3 playerBornPos;
    private Vector3 playerFlyPos;
    private float flyTime = 3f;
    private bool isFly = false;
    private float jumpOffsetTimer;
    private bool isFirstJumpStart = false;
    private bool isCanSecondJump = false;
    private int enemyReward = 3;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        startGravity = rig.gravityScale;
        swordSpace.SetActive(false);
        playerBornPos = new Vector3(transform.position.x, 0, transform.position.z);
        playerFlyPos = playerBornPos + new Vector3(0, 0.6f, 0);
        jumpOffsetTimer = jumpOffsetTime;
    }

    private void Update() {
        // 二连跳
        float xboxLRT = Input.GetAxis(XBOXInput.xboxLRT);
        if ((xboxLRT > XBOXInput.detectionThreshold || Input.GetKeyDown(KeyCode.Space))&&
            isOnGround && !AnimMan.manager.isPlayerDead && !isFly) {   
             
        }  
        
        if (isOnGround) {
            isFirstJumpStart = false;
            jumpOffsetTimer = jumpOffsetTime;
            isCanSecondJump = false;
        }
        // 由于xbox的输入的连续性问题，所以要用计时器隔开两次输入
        if (isFirstJumpStart) {
            jumpOffsetTimer -= Time.deltaTime;
            if (jumpOffsetTimer < 0) {
                isFirstJumpStart = false;
                jumpOffsetTimer = jumpOffsetTime;
                isCanSecondJump = true;
            }
        }       
        if ((xboxLRT > XBOXInput.detectionThreshold || Input.GetKeyDown(KeyCode.Space)) && 
            isCanSecondJump && !AnimMan.manager.isPlayerDead) {
            rig.AddForce(new Vector2(0, jumpForce));
            AnimMan.manager.isPlayerJump = true;
            AudioMan.manager.PlayPlayerJumpAudio();
            isCanSecondJump = false;
        }

        if (AnimMan.manager.isPlayerJump && isOnGround) {
            AnimMan.manager.isPlayerJump = false;
        }
        //  ------------------end-------------

        // 攻击
        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if ((xboxA > XBOXInput.detectionThreshold || Input.GetKeyDown(KeyCode.A)) && !AnimMan.manager.isPlayerAttack && 
            !AnimMan.manager.isPlayerDead) {
            AnimMan.manager.isPlayerAttack = true;
            swordSpace.SetActive(true);
        }
        if (swordSpace.activeSelf && !AnimMan.manager.isPlayerAttack) {
            swordSpace.SetActive(false);
        }
        // ------------------end-------------
    }
     
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Ground") {
            isOnGround = true;
        } 

        if (collision.collider.tag == "Border") {
            GameCtr.manager.GameOver();
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Coin") {
            collision.gameObject.SetActive(false);
            AudioMan.manager.PlayCoinAudio();
            GameCtr.manager.score++;
        }

        if (swordSpace.activeSelf && collision.tag == "Enemy") {
            GameCtr.manager.score += enemyReward;
            AudioMan.manager.PlayPlayerAttackAudio();
            collision.gameObject.SetActive(false);
        } else if (!swordSpace.activeSelf && collision.tag == "Enemy") {
            GameCtr.manager.GameOver();
        }

        if (collision.tag == "FlyGift") {
            collision.gameObject.SetActive(false);
            StartCoroutine(PlayerFly());
        }

        if (collision.tag == "SpeedupGift") {
            collision.gameObject.SetActive(false);
            StartCoroutine(PlayerSpeedup());
        }
    }

    public void SetPlayerBornPos() {
        transform.position = playerBornPos;
    }

    private IEnumerator PlayerFly() {
        isOnGround = false;
        isFly = true;
        sword.SetActive(true);
        transform.position = playerFlyPos;
        rig.gravityScale = 0;
        AnimMan.manager.isPlayerFly = true;
        yield return new WaitForSeconds(3f);
        isFly = false;
        sword.SetActive(false);
        rig.gravityScale = startGravity;
        transform.position = playerBornPos;
        AnimMan.manager.isPlayerFly = false;
    }

    private IEnumerator PlayerSpeedup() {
        GameCtr.manager.SetGameSpeed(1.2f, 1.4f);
        yield return new WaitForSeconds(4f);
        GameCtr.manager.SetGameSpeed();
    }
}
