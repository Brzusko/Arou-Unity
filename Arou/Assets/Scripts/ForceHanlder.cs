using EventArguments;
using UnityEngine;

public class ForceHanlder : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _forceVectors = { Vector3.zero, Vector3.zero, Vector3.zero};
    private GameManager _gameManager = null;

    [SerializeField]
    private Camera _mainCamera;
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = GetMouseGlobalPos();
        }

        if (Input.GetMouseButtonUp(0))
        {
            var mouseLocalPos = GetMouseGlobalPos() - transform.position;
            var mouseGlobalPos = GetMouseGlobalPos();
            _forceVectors[0] = new Vector3(mouseGlobalPos.x, mouseGlobalPos.y);
            _forceVectors[1] = new Vector3(mouseLocalPos.x, mouseLocalPos.y);
            _forceVectors[2] = Vector3.ClampMagnitude(_forceVectors[1] * -1, _gameManager.MaxForce);
            _gameManager.Notify(new OnShootArgs { Force = _forceVectors[2] });
        }
    }

    private Vector3 GetMouseGlobalPos()
    {
        var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePos.x, mousePos.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.7f);


            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _forceVectors[0]);

            Gizmos.color = Color.green;
            var relativePosOfForce = transform.TransformDirection(_forceVectors[2]) - transform.position;
            Gizmos.DrawLine(transform.position, transform.position + _forceVectors[2]);
        
    }
}
