using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRope : MonoBehaviour
{
    [Header("Point")]
    [SerializeField] Transform startPos, endPos;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(startPos.position, endPos.position);
        transform.localScale = new Vector3(transform.localScale.x, distance * 0.5f, transform.localScale.z);

        Vector3 middlePoint = (startPos.position + endPos.position) * 0.5f;
        transform.position = middlePoint;

        Vector3 rotationDirection = (endPos.position - startPos.position);
        transform.up = rotationDirection;
    }
}
