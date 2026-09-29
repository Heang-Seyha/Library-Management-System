using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LibraryManagementSystem.Helpers
{
    /// <summary>
    /// A generic BindingList that supports two-way sorting for DataGridView columns.
    /// Enables automatic column header click sorting for data-bound grids.
    /// </summary>
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor? _sortProperty;

        public SortableBindingList() : base() { }

        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;

        protected override bool IsSortedCore => _isSorted;

        protected override ListSortDirection SortDirectionCore => _sortDirection;

        protected override PropertyDescriptor? SortPropertyCore => _sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if (prop == null) return;

            if (Items is List<T> list)
            {
                var comparer = new PropertyComparer<T>(prop, direction);
                list.Sort(comparer);
                _isSorted = true;
                _sortDirection = direction;
                _sortProperty = prop;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }

    /// <summary>
    /// Comparer for sorting objects of type T by a given PropertyDescriptor.
    /// </summary>
    internal class PropertyComparer<T> : IComparer<T>
    {
        private readonly PropertyDescriptor _prop;
        private readonly ListSortDirection _direction;

        public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
        {
            _prop = prop;
            _direction = direction;
        }

        public int Compare(T? x, T? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return _direction == ListSortDirection.Ascending ? -1 : 1;
            if (y == null) return _direction == ListSortDirection.Ascending ? 1 : -1;

            object? valX = _prop.GetValue(x);
            object? valY = _prop.GetValue(y);

            int result;
            if (valX == null && valY == null)
            {
                result = 0;
            }
            else if (valX == null)
            {
                result = -1;
            }
            else if (valY == null)
            {
                result = 1;
            }
            else if (valX is IComparable compX)
            {
                result = compX.CompareTo(valY);
            }
            else if (valY is IComparable compY)
            {
                result = -compY.CompareTo(valX);
            }
            else
            {
                result = string.Compare(valX.ToString(), valY.ToString(), StringComparison.OrdinalIgnoreCase);
            }

            return _direction == ListSortDirection.Ascending ? result : -result;
        }
    }
}
