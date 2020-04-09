using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace RedSpartan.IntervalTraining.Common
{
    public class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        public ObservableQueue() { }

        public ObservableQueue(IEnumerable<T> collection) : base(collection) { }

        public new virtual void Clear()
        {
            base.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public new virtual T Dequeue()
        {
            var item = base.Dequeue();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return item;
        }

        public new virtual void Enqueue(T item)
        {
            base.Enqueue(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            RaiseCollectionChanged(e);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e);
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        private void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }
    }
}
