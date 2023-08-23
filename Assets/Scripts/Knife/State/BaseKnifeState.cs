public abstract class BaseKnifeState
{
    protected KnifeBehaviour Context { get; private set; }

    public BaseKnifeState(KnifeBehaviour context)
    {
        Context = context;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Tick();
    public virtual void Idle() { }
    public virtual void Throw() { }
    public virtual void PlaceOnStand() { }
    public virtual void Stick() { }
}