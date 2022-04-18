using System.Collections.Generic;
using Runtime;

namespace UI.Marketplace
{
    public class MarketplaceRunner : Runner
    {
        private List<IController> _controllers;
        private bool _isRunning = false;
        
        public override void StartRunning()
        {
            CreateControllers();
            _isRunning = true;
        }
        
        private void CreateControllers()
        {
            _controllers = new List<IController>
            {
                
            };
        }

        public void TickController(IController controller)
        {
            if (_isRunning)
            {
                controller.Tick();
            }
        }
    }
}