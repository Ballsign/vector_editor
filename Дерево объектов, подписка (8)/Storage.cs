using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дерево_объектов__подписка__8_;
	class Storage<T> : IObservable {
	private class Node {
		public Node pNext;
		public T data;
		public Node(T data_, Node pNext_ = null) {
			data = data_;
			pNext = pNext_;
		}
	};

	private Node arr;
	private int size;
	private Node iteratorElement;
	private Node previousIteratorElement;
	private bool cancelNext = false;
	private Stack<Node> stack;
	public Storage() {
		size = 0;

		observers = new List<IObserver>();
		stack = new Stack<Node>();
	}

	public T getObject(int index) {
		if (index > size || index < 0) return default(T);
		Node tmp = arr;
		for (int i = 0; i < index; i++)
			tmp = tmp.pNext;
		return tmp.data;
	}
	public int getSize() {
		return size;
    }
	public void first() {
		stack.Push(iteratorElement);
		stack.Push(previousIteratorElement);
		previousIteratorElement = arr;
		iteratorElement = arr;
	}
	public bool eol() {
		if (iteratorElement == null) {
			previousIteratorElement = stack.Pop();
			iteratorElement = stack.Pop();
			return true;
		}
		return false;
	}
	public void next() {
		if (cancelNext) {
			cancelNext = false;
			return;
		}
		previousIteratorElement = iteratorElement;
		iteratorElement = iteratorElement.pNext;
	}
	public T getIterator() {
		return iteratorElement.data;
	}
	public void push_back(T data_) {
		if (arr == null)
			arr = new Node(data_);
		else {
			Node tmp = arr;

			while (tmp.pNext != null)
				tmp = tmp.pNext;

			tmp.pNext = new Node(data_);
		}
		size++;
		notifyEveryone(null);
	}
	public void insert(T data_, int index) {
		if (index > size || index < 0) return;

		if (index == 0) {
			arr = new Node(data_, arr);
			size++;
			return;
		}
		Node previous = arr;
		for (int i = 0; i < index - 1; i++)
			previous = previous.pNext;
		Node newNode = new Node(data_, previous.pNext);
		previous.pNext = newNode;

		size++;
		notifyEveryone(null);
	}
	public void remove(int index) {
		if (index > size || index < 0) return;
		size--;
		if (index == 0) {
			arr = arr.pNext;
			notifyEveryone(null);
			//delete tmp;
			return;
		}

		Node previous = arr;

		for (int i = 0; i < index - 1; i++)
			previous = previous.pNext;

		Node toDelete = previous.pNext;
		previous.pNext = toDelete.pNext;
		notifyEveryone(null);
		//delete toDelete;
	}
	public void remove() {
		size--;
		if (iteratorElement == arr) {
			arr = arr.pNext;
			return;
		}

		previousIteratorElement.pNext = iteratorElement.pNext;
		iteratorElement = iteratorElement.pNext;
		cancelNext = true;
	}//использовать только в цикле для перебора

	public void push_front(T data_) {
		arr = new Node(data_, arr);
		size++;

		notifyEveryone(null);
	}

	string path;
	public void save(string path) {
		try {
			using (StreamWriter sw = new StreamWriter(path, false))
				sw.Write("");

			Node tmp = arr;

			for (first(); !eol(); next())
				using (StreamWriter sw = new StreamWriter(path, true))
					sw.WriteLine(getIterator().ToString());

		}
		catch { }
	}


	private List<IObserver> observers;
	public void addObserver(IObserver o) {
		observers.Add(o);
    }
	public void removeObservers() {
		for (int i = 0; i < observers.Count; i++) {
			observers.RemoveAt(i);
			i--;
		}
	}
	public void removeObserver(IObserver o) {
		observers.Remove(o);
    }
	public void notifyEveryone(MouseEventArgs e) {
		foreach (IObserver observer in observers)
			observer.update(null);
    }
	public bool isEmpty() {
		return observers.Count == 0;
    }
}

