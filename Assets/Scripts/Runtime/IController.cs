namespace Runtime
{
    public interface IController
    {
        void OnStart();
        
        void OnStop();
        
        void Tick();
    }
}