using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 人物移动 碰撞检测
public class PlayerCtr : MonoBehaviour {
    public static PlayerCtr manager = null;
    public GameObject swordSpace;
    public GameObject sword;
    public float jumpForce = 300f;
    public float flyTime = 3f;
    public float speedTime = 4f;

    private Rigidbody2D rig;
    private float startGravity;
    private bool isOnGround = true;
    private Vector3 playerBornPos;
    private Vector3 playerFlyPos;
    private bool isPause = false;
    private bool isFly = false;
    private float jumpOffsetTime = 0.2f;
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
        swordSpace.SetActiveFast(false);
        playerBornPos = new Vector3(transform.position.x, 0, transform.position.z);
        playerFlyPos = playerBornPos + new Vector3(0, 0.6f, 0);
        jumpOffsetTimer = jumpOffsetTime;
    }

    private void Update() {
       // --------------------- 二连跳 第一跳 ---------------------
        float xboxLRT = Input.GetAxis(XBOXInput.xboxLRT);
        if ((xboxLRT > XBOXInput.detectionThreshold || UIManager.manager.gameUI.isJump) &&
            isOnGround && !AnimMan.manager.isPlayerDead && !isFly) {
            PlayerJump();
            isOnGround = false;
            isFirstJumpStart = true;
        }

        if (isOnGround) {
            isFirstJumpStart = false;
            jumpOffsetTimer = jumpOffsetTime;
            isCanSecondJump = false;
        }
        // 由于xbox的输入的连续性问题，所以要用计时器隔开两次输入 第二跳
        if (isFirstJumpStart) {
            jumpOffsetTimer -= Time.deltaTime;
            if (jumpOffsetTimer < 0) {
                isFirstJumpStart = false;
                jumpOffsetTimer = jumpOffsetTime;
                isCanSecondJump = true;
            } else {
                isCanSecondJump = false;
            }
        }
        if ((xboxLRT > XBOXInput.detectionThreshold || UIManager.manager.gameUI.isJump) &&
            isCanSecondJump && !AnimMan.manager.isPlayerDead && !isFly) {
            PlayerJump();
            isCanSecondJump = false;
        }

        if (AnimMan.manager.isPlayerJump && isOnGround) {
            AnimMan.manager.isPlayerJump = false;
        }
        //  --------------------- end ---------------------

        // --------------------- 攻击 ---------------------
        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if ((xboxA > XBOXInput.detectionThreshold || UIManager.manager.gameUI.isAttack) &&
            !AnimMan.manager.isPlayerAttack && !AnimMan.manager.isPlayerDead) {
            AnimMan.manager.isPlayerAttack = true;
            swordSpace.SetActiveFast(true);
            AudioMan.manager.PlayPlayerAttackAudio();
            UIManager.manager.gameUI.isAttack = false;
        }
        if (swordSpace.activeSelf && !AnimMan.manager.isPlayerAttack) {
            swordSpace.SetActiveFast(false);
        }
        // --------------------- end ---------------------

        // 检测Xbox是否连接
        if (!GameController.manager.isConnectXbox && 
            (xboxLRT > XBOXInput.detectionThreshold || xboxA > XBOXInput.detectionThreshold)) {
            GameController.manager.isConnectXbox = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Ground") {
            isOnGround = true;
        }

        if (collision.collider.tag == "Border") {
            GameController.manager.curPlayerLife -= 1;
            GameController.manager.CheckGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Coin") {
            collision.gameObject.SetActiveFast(false);
            AudioMan.manager.PlayCoinAudio();
            GameController.manager.score++;
        }

        if (swordSpace.activeSelf && collision.tag == "Enemy") {
            GameController.manager.score += enemyReward;
            collision.gameObject.SetActiveFast(false);
        } else if (!swordSpace.activeSelf && collision.tag == "Enemy") {
            GameController.manager.curPlayerLife -= 1;
            GameController.manager.CheckGameOver();
        }

        if (collision.tag == "FlyGift") {
            collision.gameObject.SetActiveFast(false);
            StartCoroutine(PlayerFly());
        }

        if (collision.tag == "SpeedupGift") {
            collision.gameObject.SetActiveFast(false);
            StartCoroutine(PlayerSpeedup());
        }
    }

    public void SetPlayerBornPos() {
        transform.position = playerBornPos;
    }

    private void PlayerJump() {
        rig.AddForce(new Vector2(0, jumpForce));
        AnimMan.manager.isPlayerJump = true;
        AudioMan.manager.PlayPlayerJumpAudio();
        UIManager.manager.gameUI.isJump = false;
    }

    private IEnumerator PlayerFly() {
        isFly = true;
        UIManager.manager.gameUI.SetFlySkill(isFly);
        rig.velocity = Vector2.zero;
        sword.SetActiveFast(true);
        transform.position = playerFlyPos;
        rig.gravityScale = 0;
        isOnGround = false;
        AudioMan.manager.PlayFlyAudio();
        AnimMan.manager.isPlayerFly = true;
        yield return new WaitForSeconds(flyTime);
        isFly = false;
        UIManager.manager.gameUI.SetFlySkill(isFly);
        sword.SetActiveFast(false);
        rig.gravityScale = startGravity;
        transform.position = playerBornPos;
        AnimMan.manager.isPlayerFly = false;
    }

    private IEnumerator PlayerSpeedup() {
        UIManager.manager.SetGameSpeed(1.2f, 1.4f);
        UIManager.manager.gameUI.SetSpeedSkill(true);
        yield return new WaitForSeconds(speedTime);
        UIManager.manager.SetGameSpeed();
        UIManager.manager.gameUI.SetSpeedSkill(false);
    }
}