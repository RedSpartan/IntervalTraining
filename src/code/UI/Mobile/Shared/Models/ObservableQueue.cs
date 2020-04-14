using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Models
{
    public class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region Constructors
        public ObservableQueue() { }

        /// <summary>
        /// Initializes a new instance of the ObservableCollection class that contains
        /// elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <remarks>
        /// The elements are copied onto the ObservableCollection in the
        /// same order they are read by the enumerator of the collection.
        /// </remarks>
        /// <exception cref="ArgumentNullException"> collection is a null reference </exception>
        public ObservableQueue(IEnumerable<T> collection) 
        {
            CopyFrom(collection ?? throw new ArgumentNullException(nameof(collection)));
        }

        private void CopyFrom(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                using (IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        base.Enqueue(enumerator.Current);
                    }
                }
            }
        }
        #endregion Constructors

        #region Public Methods
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
        #endregion Public Methods

        #region Public Events
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;
        public virtual event PropertyChangedEventHandler PropertyChanged;
        #endregion Public Events

        #region Protected Methods
        //HACK: OnCollectionChanged should not invoke on main thread but this has been done because sfListView crashes if it doesn't
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => CollectionChanged?.Invoke(this, e));
        }

        //HACK: OnPropertyChanged should not invoke on main thread but this has been done because sfListView crashes if it doesn't
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        #endregion Protected Methods
    }
}
