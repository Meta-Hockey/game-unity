using UnityEngine;

namespace Runtime
{
    public abstract class Runner : MonoBehaviour
    {
        public virtual void StartRunning() { }
        public virtual void TickController(IController controller) { }
    }
}