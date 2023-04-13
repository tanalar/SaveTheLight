using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    private float delay = 0.1f;

    public UnityEvent OnBegin;
    public UnityEvent OnDone;

    public void PlayFeedback(GameObject sender, float strength)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity= Vector3.zero;
        OnDone?.Invoke();
    }
}
