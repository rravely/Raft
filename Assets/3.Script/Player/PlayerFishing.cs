using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField] Animator fishingHandsAni;

    bool isFishing = false;
    bool isCatchFish = false;

    float time = 0f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (isFishing && !isCatchFish && playerInput.isRMDDown)
        {
            fishingHandsAni.SetBool("RodBack", true);
            fishingHandsAni.SetBool("RodDrop", false);

            time = 0f;
            isFishing = false;
        }
        if (isFishing && isCatchFish && playerInput.isLMDDown)
        {
            fishingHandsAni.SetTrigger("RodBackCatched");
        }

        if (!isFishing && playerInput.isLMDDown)
        {
            fishingHandsAni.SetBool("RodDrop", true);
            time = 0f;
            isFishing = true;
            PlayerAudio.instance.Throw();
        }

        
        if (isFishing && !isCatchFish)
        {
            time += Time.deltaTime;
        }
        if (time > 8f)
        {
            fishingHandsAni.SetBool("FishCatch", true);
            fishingHandsAni.SetBool("RodDrop", false);

            isCatchFish = true;
        }
    }
}
