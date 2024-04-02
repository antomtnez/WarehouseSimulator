using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Carrier : MonoBehaviour, InfoPopUpView.IUIInfoContent
{
    public enum Status { Parado, Movimiento, Cargando, Descargando }

    public float Speed = 5;
    public float LoadedSpeed = 3;
    private NavMeshAgent m_Agent;
    protected IStorageInteractable m_StorageTarget;
    private IStorageInteractable m_CurrentWorkingStorage;
    private Vector3 m_CurrentTargetPosition = Vector3.zero;
    protected CarrierState m_CurrentState;
    protected CarrierState m_PreviousState;
    public Status m_CurrentStatus { protected get; set; }
    public event Action OnTaskFinished;
    public event Action OnStateChanged;
    
    protected void OnTaskFinishedActionCall(){
        OnTaskFinished();
    }

    void Awake(){
        InitNavMeshAgent();   
    }

    void InitNavMeshAgent(){
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = Speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
    }

    void Update(){
        m_CurrentState.UpdateState();
    }

    public void ChangeState(CarrierState state){
        m_PreviousState = m_CurrentState;
        m_CurrentState = state;
        m_CurrentStatus = m_CurrentState.EnterState();
        try{
            OnStateChanged();
        }catch(Exception e){
            Debug.LogWarning(e);
        }
    }

    public virtual void GoTo(IStorageInteractable target){
        m_CurrentState.GoTo(target);
    }

    public virtual void GoTo(Vector3 position){
        m_CurrentState.GoTo(position);
    }

    public void SetStorageTarget(IStorageInteractable target){
        if(target != DropPoint.Instance as IStorageInteractable)
            m_CurrentWorkingStorage = target;
        
        m_StorageTarget = target;

        if(m_StorageTarget != null)
            SetAgentDestination(m_StorageTarget.GetPosition());
    }

    public void SetAgentDestination(Vector3 position){
        m_CurrentTargetPosition = position;
        m_Agent.SetDestination(m_CurrentTargetPosition);
        m_Agent.isStopped = false;
    }

    public void GoBackToCurrentWorkingStorage(){
        SetStorageTarget(m_CurrentWorkingStorage);
    }

    public bool IsStorageTargetARack(){
        return m_StorageTarget != DropPoint.Instance as IStorageInteractable;
    }

    public virtual bool IsDestinationInRange(){
        if(m_CurrentTargetPosition != null){
            float distanceToStorage = Vector3.Distance(m_CurrentTargetPosition, transform.position);
            return distanceToStorage < 2.0f;
        }
        return false;
    }

    protected bool CanIGetItemsFromStorage(){
        return !m_CurrentWorkingStorage.IsEmpty();
    }

    public abstract bool IsEmpty();
    public abstract void DropItems();
    public abstract void ReturnItemsToRack();
    public abstract void LoadItems();

    //UIInfoContent Interface methods
    public abstract string GetName();
    public abstract string GetData();
    public abstract UnityEngine.Object GetContent();
}
