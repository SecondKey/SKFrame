using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class TestButtonList : UILogicNodeBase
    {
        [Inject]
        public override IUIViewModel DataContext { get; set; }
        public override void Initialization()
        {
            Model = new TestButtonModel();
        }
    }
}