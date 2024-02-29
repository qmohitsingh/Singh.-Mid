using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalBounce : MonoBehaviour
{
    private Vector3 originalPosition;
    public float bounceHeight = 2f; // Adjust this value to control the bounce height
    public float bounceSpeed = 5f; // Adjust this value to control the bounce speed

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(BounceRoutine());
    }

    private IEnumerator BounceRoutine()
    {
        while (true)
        {
            // Calculate target position for bounce
            Vector3 targetPosition = transform.position + Vector3.up * bounceHeight;

            // Move upwards
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, bounceSpeed * Time.deltaTime);
                yield return null;
            }

            // Move downwards
            while (transform.position != originalPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, bounceSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

}
