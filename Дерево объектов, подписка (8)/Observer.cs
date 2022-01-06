using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дерево_объектов__подписка__8_ {
    internal class Observer : IObserver {
        public System.EventHandler observer;
        public void update() {
            observer.Invoke(this, null);
        }
    }

    interface IObserver {
        void update();

    }
    
}
