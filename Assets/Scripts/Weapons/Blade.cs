using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private GameObject owner;
    public float timeToLive = 10f;
    private float elapsedTime = 0f;
    private float angle = 0;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeToLive)
            Destroy(gameObject);

        var da = 15 * speed * Time.deltaTime;
        angle += da;
        if(angle > 90)
            Destroy(gameObject);

        transform.localPosition = Quaternion.Euler(0, da, 0) * gameObject.transform.localPosition;

        var direction = 2 * transform.position - owner.transform.position;
        direction.y = transform.position.y;
        transform.LookAt(direction);
    }

    public void Launch(GameObject owner, Vector3 direction, float speed)
    {
        this.owner = owner;
        this.speed = speed;

        transform.SetParent(owner.transform);
        transform.position = transform.position + Quaternion.Euler(0, 360-45, 0) * direction;
        transform.LookAt(transform.position + Quaternion.Euler(0, 360-45, 0) * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != owner)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetKilled(owner.GetComponent<ScoreSystem>());
            }
            Destroy(gameObject);
        }
    }
}
