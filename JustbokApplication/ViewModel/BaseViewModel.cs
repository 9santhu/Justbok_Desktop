using JustbokApplication.EventBinding;
using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.ViewModel.Command;
using JustbokApplication.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace JustbokApplication
{
    public abstract class BaseViewModel : ViewModelBase
    {
        #region [Variables]
        private DataGridColumn currentSortColumn;

        private ListSortDirection currentSortDirection;

        public static ComboBox BranchControl;

        public string _subDialogCommandParameter;

        private object _items;

        private object _item;

        private IList<Node> _dataItems;

        private int _startIndex = 1;

        private int _itemCount = 10;

        private int _totalItems = 0;

        private string _sortColumn = "";

        private bool _ascending = true;

        private object _id;

        private ICommand firstCommand;

        private ICommand previousCommand;

        private ICommand nextCommand;

        private ICommand lastCommand;

        private LoaderControl loader;

        public ICommand OpenDialogCommand { get; }
        public ICommand SaveDialogCommand { get; }
        public ICommand YesDialogCommand { get; }
        public ICommand CancelDialogCommand { get; }

        public ICommand OpenSubDialogCommand { get; }
        public ICommand CancelSubDialogCommand { get; }
        public ICommand SaveSubDialogCommand { get; }

        public static int Errors { get; set; }

        private bool _isDialogOpen;
        private object _dialogContent;

        private bool _isSubDialogOpen;
        private object _subDialogContent;

        //DataGrid Related
        public DelegateCommand DataGridLoadedCommand { get; set; }
        public DelegateCommand DataGridTargetUpdatedCommand { get; set; }
        public DelegateCommand DataGridSortCommand { get; set; }

        //Edit & Delete Commands
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }


        //Search & Reset Commands
        public ICommand SearchCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        #endregion

        public BaseViewModel()
        {
            Init();
            OpenDialogCommand = new RelayCommand(OpenDialog);
            SaveDialogCommand = new RelayCommand(SaveDialog,CanSave);
            CancelDialogCommand = new RelayCommand(CancelDialog);
            YesDialogCommand = new RelayCommand(YesDialog);

            OpenSubDialogCommand = new RelayCommand(OpenSubDialog);
            SaveSubDialogCommand = new RelayCommand(SaveSubDialog);
            CancelSubDialogCommand = new RelayCommand(CancelSubDialog);

            SearchCommand = new RelayCommand(Search);
            ResetCommand = new RelayCommand(Reset);
        }

        #region [Data]

        public object Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (object.ReferenceEquals(_items, value) != true)
                {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        public object Item
        {
            get
            {
                return _item;
            }
            set
            {
                if (object.ReferenceEquals(_item, value) != true)
                {
                    _item = value;
                    NotifyPropertyChanged("Item");
                }
            }
        }

        //Multiselect Data
        public IList<Node> DataItems
        {
            get
            {
                return _dataItems;
            }
            set
            {
                if (object.ReferenceEquals(_dataItems, value) != true)
                {
                    _dataItems = value;
                    NotifyPropertyChanged("DataItems");
                }
            }
        }

        #endregion

        #region [Pagination]

        public int Start { get { return _totalItems == 0 ? 0 : StartIndex; } }

        public int End { get { return (StartIndex + ItemCount)-1 < _totalItems ? (StartIndex + ItemCount)-1 : _totalItems; } }

        public int TotalItems { get => _totalItems; set => _totalItems = value;}

        public ICommand FirstCommand
        {
            get
            {
                if (firstCommand == null)
                {
                    firstCommand = new RelayCommand
                    (
                        param =>
                        {
                            StartIndex = 1;
                            RefreshItems();
                        },
                        param =>
                        {
                            return StartIndex - ItemCount >= 0 ? true : false;
                        }
                    );
                }

                return firstCommand;
            }
        }
        
        public ICommand PreviousCommand
        {
            get
            {
                if (previousCommand == null)
                {
                    previousCommand = new RelayCommand
                    (
                        param =>
                        {
                            StartIndex -= ItemCount;
                            RefreshItems();
                        },
                        param =>
                        {
                            return StartIndex - ItemCount >= 0 ? true : false;
                        }
                    );
                }

                return previousCommand;
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommand
                    (
                        param =>
                        {
                            StartIndex += ItemCount;
                            RefreshItems();
                        },
                        param =>
                        {
                            return StartIndex + ItemCount < _totalItems ? true : false;
                        }
                    );
                }

                return nextCommand;
            }
        }
        
        public ICommand LastCommand
        {
            get
            {
                if (lastCommand == null)
                {
                    lastCommand = new RelayCommand
                    (
                        param =>
                        {
                            StartIndex = (_totalItems / ItemCount - 1) * ItemCount;
                            StartIndex += _totalItems % ItemCount == 0 ? 0 : ItemCount;
                            RefreshItems();
                        },
                        param =>
                        {
                            return StartIndex + ItemCount < _totalItems ? true : false;
                        }
                    );
                }
                return lastCommand;
            }
        }

        public int StartIndex { get => _startIndex; set => _startIndex = value; }

        public int ItemCount { get => _itemCount; set => _itemCount = value; }

        #endregion

        #region [Sorting]

        public string SortColumn { get => _sortColumn; set => _sortColumn = value; }

        public bool Ascending { get => _ascending; set => _ascending = value; }

        public object Id { get => _id; set => _id = value; }

        public void Sort(string sortColumn, bool ascending)
        {
            this.SortColumn = sortColumn;
            this.Ascending = ascending;
            RefreshItems();
        }

        #endregion

        public abstract void RefreshItems();

        protected void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        #region [Loader]

        public void ShowLoader()
        {
            loader = new LoaderControl();
            loader.Show();
            loader.Activate();
        }

        public void HideLoader()
        {
            loader.Close();
        }

        #endregion

        #region [Dialog]

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                if (_isDialogOpen == value) return;
                _isDialogOpen = value;
                NotifyPropertyChanged("IsDialogOpen");
            }
        }
   
        public object DialogContent
        {
            get { return _dialogContent; }
            set
            {
                if (_dialogContent == value) return;
                _dialogContent = value;
                NotifyPropertyChanged("DialogContent");
            }
        }

        private void OpenDialog(object obj)
        {
            Errors = 0;
            DisablingBranch();
            OpeningDialog(obj);
        }

        private void CancelDialog(object obj)
        {
            EnablingBranch();
            IsDialogOpen = false;
        }

        private void SaveDialog(object obj)
        {
            SaveContent(obj);
            EnablingBranch();
        }

        private bool CanSave(object parameter)
        {
            if (Errors == 0)
                return true;
            else
                return false;
        }

        private void YesDialog(object obj)
        {
            Delete(obj);
        }

        public abstract void OpeningDialog(object obj);

        public abstract void SaveContent(object obj);

        public abstract void Delete(object obj);

        #endregion

        #region [Sub Dialog]

        public string SubDialogCommandParameter
        {
            get
            {
                return _subDialogCommandParameter;
            }
            set
            {
                if (object.ReferenceEquals(_subDialogCommandParameter, value) != true)
                {
                    _subDialogCommandParameter = value;
                    NotifyPropertyChanged("SubDialogCommandParameter");
                }
            }
        }

        public bool IsSubDialogOpen
        {
            get { return _isSubDialogOpen; }
            set
            {
                if (_isSubDialogOpen == value) return;
                _isSubDialogOpen = value;
                NotifyPropertyChanged("IsSubDialogOpen");
            }
        }

        public object SubDialogContent
        {
            get { return _subDialogContent; }
            set
            {
                if (_subDialogContent == value) return;
                _subDialogContent = value;
                NotifyPropertyChanged("SubDialogContent");
            }
        }

        private void OpenSubDialog(object obj)
        {
            OpeningSubDialog(obj);
        }

        private void CancelSubDialog(object obj)
        {
            IsSubDialogOpen = false;
        }

        private void SaveSubDialog(object obj)
        {
            SaveSubContent(obj);
        }
        
        public abstract void OpeningSubDialog(object obj);

        public abstract void SaveSubContent(object obj);

        #endregion

        #region [Branch Related]
        private void DisablingBranch()
        {
            if (BranchControl != null)
            {
                BranchControl.IsEnabled = false;
            }
        }

        private void EnablingBranch()
        {
            if (BranchControl != null)
            {
                BranchControl.IsEnabled = true;
            }
        }
        #endregion

        #region [DeleteDialog]
        public void OpenDeleteDialog(object obj)
        {
            ShowLoader();
            Id = obj;
            DialogContent = new DeleteView();
            IsDialogOpen = true;
            HideLoader();
        }
        #endregion

        public override void Init()
        {
            DataGridLoadedCommand = CreateCommand(DataGridLoaded);
            DataGridTargetUpdatedCommand = CreateCommand(DataGridTargetUpdated);
            DataGridSortCommand = CreateCommand(DataGridSorting);
            EditCommand = CreateCommand(OpenDialog);
            DeleteCommand = CreateCommand(OpenDeleteDialog);
        }

        #region [DataGrid]

        private void DataGridLoaded(object arg)
        {
            if (arg != null)
            {
                DataGrid dataGrid = (DataGrid)arg;

                // The current sorted column must be specified in XAML.
                currentSortColumn = dataGrid.Columns.Where(c => c.SortDirection.HasValue).Single();
                currentSortDirection = currentSortColumn.SortDirection.Value;
            }
        }

        private void DataGridTargetUpdated(object argdataGrid)
        {
            if (currentSortColumn != null)
            {
                currentSortColumn.SortDirection = currentSortDirection;
            }
        }

        private void DataGridSorting(object arg)
        {
            if (arg != null)
            {

                DataGridSortingEventArgs e = (DataGridSortingEventArgs)arg;

                e.Handled = true;
                string sortField = e.Column.SortMemberPath;

                ListSortDirection direction = (e.Column.SortDirection != ListSortDirection.Ascending) ?
                    ListSortDirection.Ascending : ListSortDirection.Descending;

                bool sortAscending = direction == ListSortDirection.Ascending;

                currentSortColumn.SortDirection = null;

                e.Column.SortDirection = direction;

                currentSortColumn = e.Column;
                currentSortDirection = direction;
                Sort(sortField, sortAscending);
            }
        }

        #endregion

        #region [Search & Reset]
        private void Search(object arg)
        {
            StartIndex = 1;
            RefreshItems();
        }
        #endregion

        private void Reset(object arg)
        {
            ClearValues();
            StartIndex = 1;
            RefreshItems();
        }

        public abstract void ClearValues();
    }
}
