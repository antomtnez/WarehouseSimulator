using UnityEngine;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point or building when right clicking.
/// </summary>
public class UserControl : MonoBehaviour
{
    private Camera m_GameCamera;
    private GameObject m_Marker;
    private Carrier m_Selected = null;

    void Start(){
        m_GameCamera = Camera.main;
        m_Marker = GameObject.FindGameObjectWithTag("Marker");
        m_Marker.SetActive(false);
    }

    void Update(){
        if (Input.GetMouseButtonDown(0))
        {
            var ray = m_GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                //the collider could be children of the unit, so we make sure to check in the parent
                var carrier = hit.collider.GetComponentInParent<Carrier>();
                m_Selected = carrier;
            }
        }else if (m_Selected != null && Input.GetMouseButtonDown(1)){
            //right click give order to the unit
            var ray = m_GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                var storageInteractable = hit.collider.GetComponentInParent<IStorageInteractable>();
                
                if (storageInteractable!= null){
                    m_Selected.GoTo(storageInteractable);
                }else{
                    m_Selected.GoTo(hit.point);
                }
            }
        }

        MarkerHandling();
    }
    
    // Handle displaying the marker above the unit that is currently selected (or hiding it if no unit is selected)
    void MarkerHandling(){
        if (m_Selected == null && m_Marker.activeInHierarchy){
            m_Marker.SetActive(false);
            m_Marker.transform.SetParent(null);
        }
        else if (m_Selected != null && m_Marker.transform.parent != m_Selected.transform){
            m_Marker.SetActive(true);
            m_Marker.transform.SetParent(m_Selected.transform, false);
            m_Marker.transform.localPosition = Vector3.zero;
        }    
    }
}