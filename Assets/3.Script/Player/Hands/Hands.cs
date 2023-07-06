using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] PlayerInteraction playerInteration;

    public void HandsAfterInteraction()
    {
        playerInteration.AfterInteractionAnimation();
    }
}
