using UnityEngine;
using UnityEngine.AI;

// Base class for all Unit. It will handle movement order given through the UserControl script.
// It require a NavMeshAgent to navigate the scene.
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Unit : MonoBehaviour
{
    public float Speed = 3;

    protected NavMeshAgent m_Agent;
    protected Inventory m_Target;

    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = Speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
    }
/*
    private void Start()
    {
        SetColor(MainManager.Instance.teamColor);
    }

    void SetColor(Color c)
    {
        var colorHandler = GetComponentInChildren<ColorHandler>();
        if (colorHandler != null)
        {
            colorHandler.SetColor(c);
        }
    }
*/
    private void Update()
    {
        if (m_Target != null)
        {
            float distance = Vector3.Distance(m_Target.transform.position, transform.position);
            if (distance < 2.0f)
            {
                m_Agent.isStopped = true;
                BuildingInRange();
            }
        }
    }

    public virtual void GoTo(Inventory target)
    {
        m_Target = target;

        if (m_Target != null)
        {
            m_Agent.SetDestination(m_Target.transform.position);
            m_Agent.isStopped = false;
        }
    }

    public virtual void GoTo(Vector3 position)
    {
        //we don't have a target anymore if we order to go to a random point.
        m_Target = null;
        m_Agent.SetDestination(position);
        m_Agent.isStopped = false;
    }


    /// <summary>
    /// Override this function to implement what should happen when in range of its target.
    /// Note that this is called every frame the current target is in range, not only the first time we get in range! 
    /// </summary>
    protected abstract void BuildingInRange();
}
