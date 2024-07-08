using UnityEngine;

public class GoalAndScoreManager : MonoBehaviour
{
    private bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding)
        {
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "box")
        {
            isColliding = true;
            Debug.Log("goal");
            Destroy(collision.gameObject);
        }
    }

}
