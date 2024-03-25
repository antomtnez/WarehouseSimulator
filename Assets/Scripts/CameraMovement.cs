using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera m_GameCamera;
    public float PanSpeed = 10.0f;

    [Header("Camera Bounds")]
    public Vector2 XAxisBounds = Vector2.zero;
    public Vector2 ZAxisBounds = Vector2.zero;

    void Start(){
        m_GameCamera = Camera.main;
    }

    void Update(){
        MoveCamera();
    }

    void MoveCamera(){
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 newPosition = m_GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;

        newPosition = new Vector3(Mathf.Clamp(newPosition.x, XAxisBounds.x, XAxisBounds.y),
                                    newPosition.y,
                                    Mathf.Clamp(newPosition.z, ZAxisBounds.x, ZAxisBounds.y));
        
        m_GameCamera.transform.position = newPosition;
    }
}
