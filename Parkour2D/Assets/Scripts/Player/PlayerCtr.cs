using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 人物移动 碰撞检测
public class PlayerCtr : MonoBehaviour {
    public static PlayerCtr manager = null;
    public GameObject sword;
    public float jumpForce = 300f;
    public float jumpOffsetTime = 0.2f;

    private Rigidbody2D rig;  
    private bool isOnGround = true;
    private Vector3 playerBornPos;
    private float jumpOffsetTimer;
    private bool isFirstJumpStart = false;
    private bool isCanSecondJump = false;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        sword.SetActive(false);
        playerBornPos = new Vector3(transform.position.x, 0, transform.position.z);
        jumpOffsetTimer = jumpOffsetTime;

    }

    private void Update() {
        // 二连跳
        float xboxLRT = Input.GetAxis(XBOXInput.xboxLRT);
        if ((xboxLRT > XBOXInput.detectionThreshold || Input.GetKeyDown(KeyCode.Space))&& isOnGround && !AnimMan.manager.isPlayerDead) {   
            rig.AddForce(new Vector2(0, jumpForce));   
            AnimMan.manager.isPlayerJump = true;
            AudioMan.manager.PlayPlayerJumpAudio();
            isOnGround = false;
            isFirstJumpStart = true;
        }  
        
        if (isOnGround) {
            isFirstJumpStart = false;
            jumpOffsetTimer = jumpOffsetTime;
            isCanSecondJump = false;
        }
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
            sword.SetActive(true);
        }
        if (sword.activeSelf && !AnimMan.manager.isPlayerAttack) {
            sword.SetActive(false);
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
            GameCtr.manager.coinNum++;
        }

        if (sword.activeSelf && collision.tag == "Enemy") {
            GameCtr.manager.coinNum += 3;
            AudioMan.manager.PlayPlayerAttackAudio();
            collision.gameObject.SetActive(false);
        } else if (!sword.activeSelf && collision.tag == "Enemy") {
            GameCtr.manager.GameOver();
        }
    }

    public void SetPlayerBornPos() {
        transform.position = playerBornPos;
    }
}
