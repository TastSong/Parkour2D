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
        float xboxLRT = Input.GetAxis("XBOXLRT");
        if ((xboxLRT > 0.9f || Input.GetKeyDown(KeyCode.Space))&& isOnGround && !AnimMan.manager.isPlayerDead) {   
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
        if ((xboxLRT   > 0.9f || Input.GetKeyDown(KeyCode.Space)) && isCanSecondJump && !AnimMan.manager.isPlayerDead) {
            rig.AddForce(new Vector2(0, jumpForce));
            AnimMan.manager.isPlayerJump = true;
            AudioMan.manager.PlayPlayerJumpAudio();
            isCanSecondJump = false;
        }

        if (AnimMan.manager.isPlayerJump && isOnGround) {
            AnimMan.manager.isPlayerJump = false;
        }
        // 二连跳↑

        // 攻击
        float xboxA = Input.GetAxis("XBOXA");
        if (xboxA > 0.9f || Input.GetKeyDown(KeyCode.A)) {
            AnimMan.manager.isPlayerAttack = true;
            sword.SetActive(true);
        }
        //
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
            GameCtr.manager.coinNum++;
        }
    }

    public void SetPlayerBornPos() {
        transform.position = playerBornPos;
    }
}
