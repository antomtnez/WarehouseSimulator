using UnityEngine;

public abstract class CarrierState{
    protected Carrier m_Carrier;
    protected CarrierState m_NextState;
    protected Carrier.Status m_StateStatus;
    public CarrierState(Carrier carrier){ m_Carrier = carrier; }
    public virtual Carrier.Status EnterState(){
        return m_StateStatus;
    }
    public virtual void UpdateState(){}
    public virtual void ChangeState(){ m_Carrier.ChangeState(m_NextState); }
    public virtual void GoTo(Vector3 position){
        m_Carrier.SetAgentDestination(position);
        m_NextState = new GoToRandomPositionState(m_Carrier);
        ChangeState();
    }

    public virtual void GoTo(IStorageInteractable storageInteractable){
        m_Carrier.SetStorageTarget(storageInteractable);
        if(m_Carrier.IsStorageTargetARack()){
            m_NextState = new GoToRackState(m_Carrier);
        }else{
            m_NextState = new GoToDropPointState(m_Carrier);
        }

        ChangeState();
    }
}

public class IdleState : CarrierState{
    public IdleState(Carrier carrier) : base(carrier){
        m_StateStatus = Carrier.Status.Parado;
    }
}

public class GoToRandomPositionState : CarrierState{
    public GoToRandomPositionState(Carrier carrier) : base(carrier){
        m_StateStatus = Carrier.Status.Movimiento;
    }

    public override void UpdateState(){
        if(m_Carrier.IsDestinationInRange()){
            m_NextState = new IdleState(m_Carrier);
            ChangeState();
        }
    }
}

public class GoToRackState : CarrierState{
    public GoToRackState(Carrier carrier) : base(carrier){
        m_StateStatus = Carrier.Status.Movimiento;
    }
    public override Carrier.Status EnterState(){
        m_Carrier.GoBackToCurrentWorkingStorage();
        return m_StateStatus;
    }

    public override void UpdateState(){
        if(m_Carrier.IsDestinationInRange()){
            if(m_Carrier.IsEmpty()){
                m_NextState = new LoadItemsState(m_Carrier);
            }else{
                m_NextState = new ReturnItemsToRackState(m_Carrier);
            }
            ChangeState();
        }
    }
}

public class ReturnItemsToRackState : CarrierState{
    public ReturnItemsToRackState(Carrier carrier) : base(carrier){
        m_StateStatus = Carrier.Status.Descargando;
    }
    public override Carrier.Status EnterState(){
        m_Carrier.ReturnItemsToRack();
        m_Carrier.OnTaskFinished += FinishReturningItems;
        return m_StateStatus;
    }

    private void FinishReturningItems(){
        m_Carrier.OnTaskFinished -= FinishReturningItems;
        m_NextState = new IdleState(m_Carrier);
        ChangeState();
    }
}

public class LoadItemsState : CarrierState{
    public LoadItemsState(Carrier carrier) : base(carrier){
        m_StateStatus = Carrier.Status.Cargando;
    }

    public override Carrier.Status EnterState(){
        m_Carrier.LoadItems();
        m_Carrier.OnTaskFinished += FinishLoadingItems;
        return m_StateStatus;
    }

    private void FinishLoadingItems(){
        m_Carrier.OnTaskFinished -= FinishLoadingItems;
        m_NextState = new GoToDropPointState(m_Carrier);
        ChangeState();
    }
}

public class GoToDropPointState : CarrierState{
    public GoToDropPointState(Carrier carrier) : base(carrier){
        m_StateStatus = Carrier.Status.Movimiento;
    }
    public override Carrier.Status EnterState(){
        m_Carrier.SetStorageTarget(DropPoint.Instance);
        return m_StateStatus;
    }

    public override void UpdateState(){
        if(m_Carrier.IsDestinationInRange()){
            m_NextState = new DropItemsState(m_Carrier);
            ChangeState();
        }
    }
}

public class DropItemsState : CarrierState{
    public DropItemsState(Carrier carrier) : base(carrier){
        m_StateStatus = Carrier.Status.Descargando;
    }
    public override Carrier.Status EnterState(){
        m_Carrier.DropItems();
        m_Carrier.OnTaskFinished += FinishDroppingItems;
        return m_StateStatus;
    }

    private void FinishDroppingItems(){
        m_Carrier.OnTaskFinished -= FinishDroppingItems;
        m_NextState = new GoToRackState(m_Carrier);
        ChangeState();
    }
}