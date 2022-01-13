using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дерево_объектов__подписка__8_ {
    internal class Observer : IObserver {
        public MouseEventHandler observer;
        public void update(MouseEventArgs e) {
            observer.Invoke(this, e);
        }
    }

    interface IObserver {
        void update(MouseEventArgs e);

    }

    interface IObservable {
        void addObserver(IObserver o);
        void removeObserver(IObserver o);
        void removeObservers();
        void notifyEveryone(MouseEventArgs e);
        bool isEmpty();
    }

}
