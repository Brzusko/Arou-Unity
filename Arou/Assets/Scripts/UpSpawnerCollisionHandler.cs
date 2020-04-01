using EventArguments;
using UnityEngine;

public class UpSpawnerCollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ScoreArea")
        {
            var pipe = collision.transform.parent.GetComponent<Pipe>();
            var eventArgs = new OnSpawnArgs { Pipe = pipe };
            GameManager.Instance.PipeSpawner.OnSpawnHandler(eventArgs);
        }
    }
}
