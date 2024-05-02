using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCardAnimations : MonoBehaviour
{
    private Coroutine currentAnimation;

    private Vector3 originalPosition;
    private Vector3 originalScale;

    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    public void SetOriginalPosition(Vector3 position)
    {
        originalPosition = position;
    }

    public void StartHoverWithDraggedCardAnimation()
    {
        if(currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        
        currentAnimation = StartCoroutine(HoverWithDraggedCardAnimation());
    }

    private IEnumerator HoverWithDraggedCardAnimation()
    {
        originalPosition = transform.position;
        Debug.Log("Hovering with dragged card");
        while(true)
        {
            transform.position = originalPosition + new Vector3(0, 2f, 0);
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            yield return null;
        }
    }

    public void StartCardBeingDraggedAnimation()
    {
        if(currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        
        currentAnimation = StartCoroutine(CardBeingDraggedAnimation());
    }

    private IEnumerator CardBeingDraggedAnimation()
    {
        while(true)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            yield return null;
        }
    }

    public void StopHoverWithDraggedCardAnimation()
    {
        if(currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            transform.position = originalPosition;
            transform.localScale = originalScale;
        }
    }

    public void ReturnToOriginalPosition()
    {
        if(currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        transform.position = originalPosition;
        transform.localScale = originalScale;
    }


}
