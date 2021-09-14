using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public Transform followTarget;
	private Vector3 targetPos;
    private float duration = 1.5f;
    private Vector3 originalPosition;
    private Transform targetPosition;
    private bool isLerping = false;

    private void Start()
    {
        targetPosition = transform;
    }

    void Update()
	{
		if (followTarget != null && !isLerping)
		{
			targetPos = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
			Vector3 velocity = (targetPos - transform.position) * moveSpeed;
			transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, Time.deltaTime);
		}
	}

    IEnumerator MoveToDialogue()
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        GameState.GetInstance().gamePaused = true;
        isLerping = true;

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition.position;
    }

    IEnumerator MoveToPlayer()
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, originalPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        GameState.GetInstance().gamePaused = false;
        isLerping = false;
    }

    public void DialogueTriggerReached(Transform goalPosition)
    {
        originalPosition = transform.position;
        targetPosition.position = new Vector3(goalPosition.position.x, goalPosition.position.y, -10);
        StartCoroutine(MoveToDialogue());
    }

    public void GetBackToPlayer()
    {
        StartCoroutine(MoveToPlayer());
    }
}
