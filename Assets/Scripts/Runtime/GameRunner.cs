using System.Collections.Generic;
using UI.Marketplace;
using UnityEngine;

namespace Runtime
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
    }
}