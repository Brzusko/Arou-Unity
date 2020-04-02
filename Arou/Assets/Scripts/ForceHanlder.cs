using EventArguments;
using UnityEngine;

public class ForceHanlder : MonoBehaviour
{
    public enum MOUSE_STATE
    {
        NONE,
        HOLDING
    }

    [SerializeField]
    private Vector3[] _forceVectors = { Vector3.zero, Vector3.zero, Vector3.zero};
    private GameManager _gameManager = null;

    [SerializeField]
    private Camera _mainCamera;

    public MOUSE_STATE mouseState = MOUSE_STATE.NONE;
    public Vector3 Force
    {
        get => _forceVectors[2];
    }

    public Vector3 GetMouseGlobalPos()
    {
        var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePos.x, mousePos.y);
    }

    protected virtual void OnFailHandler(object sender, OnFaileArgs onFailArgs)
    {
        transform.gameObject.SetActive(false);
    }
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnFailEvent += OnFailHandler;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = GetMouseGlobalPos();
            mouseState = MOUSE_STATE.HOLDING;
        }

        var mouseLocalPos = GetMouseGlobalPos() - transform.position;
        var mouseGlobalPos = GetMouseGlobalPos();
        _forceVectors[0] = new Vector3(mouseGlobalPos.x, mouseGlobalPos.y);
        _forceVectors[1] = new Vector3(mouseLocalPos.x, mouseLocalPos.y);
        _forceVectors[2] = Vector3.ClampMagnitude(_forceVectors[1] * -1, _gameManager.MaxForce);

        if (Input.GetMouseButtonUp(0))
        {

            _gameManager.Notify(new OnShootArgs { Force = _forceVectors[2] });
            mouseState = MOUSE_STATE.NONE;
        }
    }
}
