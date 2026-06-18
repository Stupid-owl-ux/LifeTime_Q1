using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAge playerAge;

    void Start()
    {
        playerAge = GetComponent<PlayerAge>();
    }

    void Update()
    {
        float moveSpeed = GetMoveSpeed();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    float GetMoveSpeed()
    {
        float age = playerAge.currentAge;

        if (age < 20)
            return Mathf.Lerp(2f, 8f, age / 20f);

        if (age < 60)
            return 8f;

        return Mathf.Lerp(8f, 3f, (age - 60f) / 40f);
    }
}