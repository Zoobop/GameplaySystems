
namespace Entity
{
    public interface IState
    {
        public void OnEnter();
        public void OnTick();
        public void OnFixedTick();
        public void OnExit();
    }
}