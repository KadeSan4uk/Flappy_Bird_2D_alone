using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _birdCollider;
    [SerializeField] private PipeController _pipesController;
    [SerializeField] private GameOverPopup _gameOverPopup;

    [SerializeField] private float _forcePower = 1.5f;

    private int _points;
    private bool gameOver;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnClick();
        }

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-5, 20, _rigidbody.linearVelocity.y ));
    }

    public void OnClick()
    {
        _rigidbody.AddForce(Vector2.up * _forcePower, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _points += 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameOver = true;
        Time.timeScale = 0;

        _birdCollider.enabled = false;
        _pipesController.enabled = false;       

        StartCoroutine(UnfreezeGame());
    }

    private IEnumerator UnfreezeGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;

        _gameOverPopup.ShowPopup(_points);
    }

}
