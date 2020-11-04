using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleEffectAnim : MonoBehaviour
{
    private void OnEnable() {
        GetComponent<ParticleSystem>().Play();
    }
}
