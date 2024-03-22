using UnityEngine;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point or building when right clicking.
/// </summary>
public class UserControl : MonoBehaviour
{
    private Camera GameCamera;
    public float PanSpeed = 10.0f;
    private GameObject Marker;
    
    private Carrier m_Selected = null;

    private void Start()
    {
        GameCamera = Camera.main;
        Marker = GameObject.FindGameObjectWithTag("Marker");
        Marker.SetActive(false);
    }

    private void Update()
    {
        MoveCamera();

        if (Input.GetMouseButtonDown(0))
        {
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //the collider could be children of the unit, so we make sure to check in the parent
                var unit = hit.collider.GetComponentInParent<Unit>();
                var carrier = hit.collider.GetComponentInParent<Carrier>();
                m_Selected = carrier;
                
                
                //check if the hit object have a IUIInfoContent to display in the UI
                //if there is none, this will be null, so this will hid the panel if it was displayed
                //var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
                //UIMainScene.Instance.SetNewInfoContent(uiInfo);
            }
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {//right click give order to the unit
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var storageInteractable = hit.collider.GetComponentInParent<IStorageInteractable>();
                
                if (storageInteractable!= null)
                {
                    m_Selected.GoTo(storageInteractable);
                }
                else
                {
                    m_Selected.GoTo(hit.point);
                }
            }
        }

        MarkerHandling();
    }

    void MoveCamera(){
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;
    }
    
    // Handle displaying the marker above the unit that is currently selected (or hiding it if no unit is selected)
    void MarkerHandling()
    {
        if (m_Selected == null && Marker.activeInHierarchy)
        {
            Marker.SetActive(false);
            Marker.transform.SetParent(null);
        }
        else if (m_Selected != null && Marker.transform.parent != m_Selected.transform)
        {
            Marker.SetActive(true);
            Marker.transform.SetParent(m_Selected.transform, false);
            Marker.transform.localPosition = Vector3.zero;
        }    
    }
}