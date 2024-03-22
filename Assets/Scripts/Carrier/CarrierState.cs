using UnityEngine;

public abstract class CarrierState{
    protected Carrier m_Carrier;
    protected CarrierState m_NextState;
    public CarrierState(Carrier carrier){ m_Carrier = carrier; }
    public virtual void EnterState(){}
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
    public IdleState(Carrier carrier) : base(carrier){}
}

public class GoToRandomPositionState : CarrierState{
    public GoToRandomPositionState(Carrier carrier) : base(carrier){}

    public override void UpdateState(){
        if(m_Carrier.IsDestinationInRange()){
            m_NextState = new IdleState(m_Carrier);
            ChangeState();
        }
    }
}

public class GoToRackState : CarrierState{
    public GoToRackState(Carrier carrier) : base(carrier){}
    public override void EnterState(){
        m_Carrier.GoBackToCurrentWorkingStorage();
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
    public ReturnItemsToRackState(Carrier carrier) : base(carrier){}
    public override void EnterState(){
        m_Carrier.ReturnItemsToRack();
        m_NextState = new IdleState(m_Carrier);
        ChangeState();
    }
}

public class LoadItemsState : CarrierState{
    public LoadItemsState(Carrier carrier) : base(carrier){}

    public override void EnterState(){
        m_Carrier.LoadItems();
        m_NextState = new GoToDropPointState(m_Carrier);
        ChangeState();
    }
}

public class GoToDropPointState : CarrierState{
    public GoToDropPointState(Carrier carrier) : base(carrier){}
    public override void EnterState(){
        m_Carrier.SetStorageTarget(DropPoint.Instance);
    }

    public override void UpdateState(){
        if(m_Carrier.IsDestinationInRange()){
            m_NextState = new DropItemsState(m_Carrier);
            ChangeState();
        }
    }
}

public class DropItemsState : CarrierState{
    public DropItemsState(Carrier carrier) : base(carrier){}
    public override void EnterState(){
        m_Carrier.DropItems();
        m_NextState = new GoToRackState(m_Carrier);
        ChangeState();
    }
}