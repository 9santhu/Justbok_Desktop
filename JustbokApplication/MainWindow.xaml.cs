using JustbokApplication.Helpers;
using JustbokApplication.Models;
using JustbokApplication.Views.Settings;
using JustbokApplication.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using JustbokApplication.Views;
using JustbokApplication.Views.StockConfig;
using JustbokApplication.Views.DietConfig;
using JustbokApplication.Views.WorkoutConfig;

namespace JustbokApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool ismembershipvisible = false;
        private bool isattvisible = false;
        private bool iscalvisible = false;
        private bool isworkvisible = false;
        private bool isdietvisible = false;
        private bool istockvisible = false;
        private bool issettvisible = false;
        private bool issmsvisible = false;

        private string viewName;
        public MainWindow()
        {
            InitializeComponent();
            BaseViewModel.BranchControl = cmbBranches;
            LoggedAs.Text = SessionManager.FirstName + " " + SessionManager.LastName;

            DataContext = new MainWindowViewModel();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            WelcomeView welcomeView = new WelcomeView();
            viewName = "JustbokApplication.Views.WelcomeView";
            grdControl.Children.Add(welcomeView);
        }

        private void cmbBranches_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewName))
            {
                grdControl.Children.Clear();
                UIElement control = (UIElement)Invoker.CreateAndInvoke(viewName, null);
                grdControl.Children.Add(control);
            }
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Navigating(sender);
        }

        private void ListViewItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Navigating(sender);   
            }
        }


        public void Navigating(object sender)
        {
            try
            {
                ListViewItem listViewItem = sender as ListViewItem;

                string tag = listViewItem.Tag.ToString();

                if (tag != null)
                {
                    switch (tag.ToUpper())
                    {
                        case "DASHBOARD":
                            {
                                break;
                            }
                        case "QUICKPRINT":
                            {
                                break;
                            }
                        case "FINDMEMBER":
                            {
                                break;
                            }
                        case "MEMBERSHIP":
                            {
                                if (!ismembershipvisible)
                                {
                                    membershiplist.Visibility = Visibility.Visible;
                                    ismembershipvisible = true;
                                    memberleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    membershiplist.Visibility = Visibility.Collapsed;
                                    ismembershipvisible = false;
                                    memberleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }
                                break;
                            }
                        case "MEMBERSHIPLIST":
                            {
                                break;
                            }
                        case "ENQUIRYLIST":
                            {
                                break;
                            }
                        case "ATTENDENCE":
                            {
                                if (!isattvisible)
                                {
                                    attStaff.Visibility = Visibility.Visible;
                                    attmem.Visibility = Visibility.Visible;
                                    isattvisible = true;
                                    attleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    attStaff.Visibility = Visibility.Collapsed;
                                    attmem.Visibility = Visibility.Collapsed;
                                    isattvisible = false;
                                    attleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }
                                break;
                            }
                        case "ATTSTAFF":
                            {
                                break;
                            }
                        case "ATTMEMBER":
                            {
                                break;
                            }
                        case "CALENDAR":
                            {
                                if (!iscalvisible)
                                {
                                    calconfig.Visibility = Visibility.Visible;
                                    calapp.Visibility = Visibility.Visible;
                                    calcal.Visibility = Visibility.Visible;
                                    calclass.Visibility = Visibility.Visible;
                                    caleve.Visibility = Visibility.Visible;
                                    iscalvisible = true;
                                    calleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    calconfig.Visibility = Visibility.Collapsed;
                                    calapp.Visibility = Visibility.Collapsed;
                                    calcal.Visibility = Visibility.Collapsed;
                                    calclass.Visibility = Visibility.Collapsed;
                                    caleve.Visibility = Visibility.Collapsed;
                                    iscalvisible = false;
                                    calleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }
                                break;
                            }
                        case "APPOINTMENTCONFIG":
                            {
                                break;
                            }
                        case "BOOKAPPOINTMENT":
                            {
                                break;
                            }
                        case "INNERCALENDAR":
                            {
                                break;
                            }
                        case "CLASS":
                            {
                                break;
                            }
                        case "EVENT":
                            {
                                break;
                            }
                        case "WORKOUTCONFIG":
                            {
                                if (!isworkvisible)
                                {
                                    work.Visibility = Visibility.Visible;
                                    workplan.Visibility = Visibility.Visible;
                                    allocation.Visibility = Visibility.Collapsed;
                                    isworkvisible = true;
                                    workleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    work.Visibility = Visibility.Collapsed;
                                    workplan.Visibility = Visibility.Collapsed;
                                    allocation.Visibility = Visibility.Collapsed;
                                    isworkvisible = false;
                                    workleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }
                                break;
                            }
                        case "WORKOUT":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.WorkoutConfig.WorkoutView";
                                grdControl.Children.Add(new WorkoutView());
                                break;
                            }
                        case "WORKOUTPLAN":
                            {
                                break;
                            }
                        case "ALLOCATION":
                            {
                                break;
                            }
                        case "DIETCONFIG":
                            {
                                if (!isdietvisible)
                                {
                                    meal.Visibility = Visibility.Visible;
                                    dietplan.Visibility = Visibility.Visible;
                                    isdietvisible = true;
                                    dietleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    meal.Visibility = Visibility.Collapsed;
                                    dietplan.Visibility = Visibility.Collapsed;
                                    isdietvisible = false;
                                    dietleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }
                                break;
                            }
                        case "MEALTIME":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.DietConfig.MealTimeView";
                                grdControl.Children.Add(new MealTimeView());
                                break;
                            }
                        case "DIETPLAN":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.DietConfig.DietPlanView";
                                grdControl.Children.Add(new DietPlanView());
                                break;
                            }

                        case "POS":
                            {
                                break;
                            }
                        case "STOCKCONFIG":
                            {
                                if (!istockvisible)
                                {
                                    ARStock.Visibility = Visibility.Visible;
                                    stockHistory.Visibility = Visibility.Visible;
                                    istockvisible = true;
                                    stockleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    ARStock.Visibility = Visibility.Collapsed;
                                    stockHistory.Visibility = Visibility.Collapsed;
                                    istockvisible = false;
                                    stockleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }
                                break;
                            }
                        case "ADDREMOVESTOCK":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.StockConfig.StockView";
                                grdControl.Children.Add(new StockView());
                                break;
                            }
                        case "STOCKHISTORY":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.StockConfig.StockHistoryView";
                                grdControl.Children.Add(new StockHistoryView());
                                break;
                            }
                        case "EXPENSE":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.ExpensesView";
                                grdControl.Children.Add(new ExpensesView());
                                break;
                            }
                        case "SETTING":
                            {
                                if (!issettvisible)
                                {
                                    staff.Visibility = Visibility.Visible;
                                    branch.Visibility = Visibility.Visible;
                                    product.Visibility = Visibility.Visible;
                                    supplier.Visibility = Visibility.Visible;
                                    packages.Visibility = Visibility.Visible;
                                    task.Visibility = Visibility.Visible;
                                    expensetype.Visibility = Visibility.Visible;
                                    category.Visibility = Visibility.Visible;
                                    role.Visibility = Visibility.Visible;
                                    issettvisible = true;
                                    settleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    staff.Visibility = Visibility.Collapsed;
                                    branch.Visibility = Visibility.Collapsed;
                                    product.Visibility = Visibility.Collapsed;
                                    supplier.Visibility = Visibility.Collapsed;
                                    packages.Visibility = Visibility.Collapsed;
                                    task.Visibility = Visibility.Collapsed;
                                    expensetype.Visibility = Visibility.Collapsed;
                                    category.Visibility = Visibility.Collapsed;
                                    role.Visibility = Visibility.Collapsed;
                                    issettvisible = false;
                                    settleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }

                                break;
                            }
                        case "ROLE":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.RoleView";
                                grdControl.Children.Add(new RoleView());
                                break;
                            }
                        case "STAFF":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.StaffView";
                                grdControl.Children.Add(new StaffView());
                                break;
                            }
                        case "BRANCH":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.BranchView";
                                grdControl.Children.Add(new BranchView());
                                break;
                            }
                        case "PRODUCT":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.ProductView";
                                grdControl.Children.Add(new ProductView());
                                break;
                            }
                        case "SUPPLIER":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.SupplierView";
                                grdControl.Children.Add(new SupplierView());
                                break;
                            }
                        case "PACKAGES":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.PackageView";
                                grdControl.Children.Add(new PackageView());
                                break;
                            }
                        case "TASK":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.TaskView";
                                grdControl.Children.Add(new TaskView());
                                break;
                            }
                        case "EXPENSETYPE":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.ExpenseTypeView";
                                grdControl.Children.Add(new ExpenseTypeView());
                                break;
                            }
                        case "CATEGORY":
                            {
                                grdControl.Children.Clear();
                                viewName = "JustbokApplication.Views.Settings.CategoryView";
                                grdControl.Children.Add(new CategoryView());
                                break;
                            }
                        case "SMS":
                            {

                                if (!issmsvisible)
                                {
                                    sendmsg.Visibility = Visibility.Visible;
                                    msglog.Visibility = Visibility.Visible;
                                    issmsvisible = true;
                                    smsleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuDown;
                                }
                                else
                                {
                                    sendmsg.Visibility = Visibility.Collapsed;
                                    msglog.Visibility = Visibility.Collapsed;
                                    issmsvisible = false;
                                    smsleft.Kind = MaterialDesignThemes.Wpf.PackIconKind.MenuLeft;
                                }
                                break;
                            }
                        case "SENDMESSAGE":
                            {
                                break;
                            }
                        case "MESSAGELOG":
                            {
                                break;
                            }
                        case "REPORTS":
                            {
                                break;
                            }


                    }
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}


