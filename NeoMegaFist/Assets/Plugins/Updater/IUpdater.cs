namespace Utility
{
    public interface IUpdater
    {
        void AddUpdate(IUpdate update);
        void RemoveUpdate(IUpdate update);
        void AddFixedUpdate(IFixedUpdate fixedUpdate);
        void RemoveFixedUpdate(IFixedUpdate fixedUpdate);
    } 
}