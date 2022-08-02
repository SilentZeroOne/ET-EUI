using System;
using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public Action<Collider2D> OnTriggerEnter2DAction;
    public Action<Collider2D> OnTriggerExit2DAction;
    public Action<Collider2D> OnTriggerStay2DAction;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter2DAction?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit2DAction?.Invoke(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStay2DAction?.Invoke(other);
    }
}
