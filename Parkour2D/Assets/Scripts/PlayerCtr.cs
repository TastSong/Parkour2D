using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 人物移动 碰撞检测
public class PlayerCtr : MonoBehaviour {
    public static PlayerCtr manager = null;
    public float jumpForce = 300f;  

    private Rigidbody2D rig;  
    private bool isOnGround = true;
    private bool isOnBorder = false;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        rig = GetComponent<Rigidbody2D>();   
    }

    private void Update() {
        float xboxLRT = Input.GetAxis("XBOXLRT");
        if ((xboxLRT > 0.9f || Input.GetKeyDown(KeyCode.Space))&& isOnGround) {   
            rig.AddForce(new Vector2(0, jumpForce));   
            AnimMan.manager.isPlayerJump = true;
            AudioMan.manager.PlayPlayerJumpAudio();
            isOnGround = false;
        }

        if (AnimMan.manager.isPlayerJump && isOnGround) {
            AnimMan.manager.isPlayerJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Ground") {
            isOnGround = true;
        } 

        if (collision.collider.tag == "Border") {
            isOnBorder = true;
        }
    }
}
