using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MediaCenter.Models.Entity;

namespace MediaCenter.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
            CbManagers.ItemsSource = MediaCenterEntities.GetContext().Managers.ToList();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (DpStart.SelectedDate == null || DpEnd.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты");
                return;
            }
            if (CbManagers.SelectedItem != null)
            {
                var orders = MediaCenterEntities.GetContext().Orders
                    .Where(x => x.IDManager == ((Manager)CbManagers.SelectedItem).ID && x.Date >= DpStart.SelectedDate && x.Date <= DpEnd.SelectedDate).ToList();
                TblCount.Text = $"Количество заказов: {orders.Count}";
                TblSum.Text = $"Сумма: {orders.Sum(x => x.Sum)}";
            }
            else
            {
                var orders = MediaCenterEntities.GetContext().Orders
                    .Where(x => x.Date >= DpStart.SelectedDate && x.Date <= DpEnd.SelectedDate).ToList();
                TblCount.Text = $"Количество заказов: {orders.Count}";
                TblSum.Text = $"Сумма: {orders.Sum(x => x.Sum)}";
            }
        }

        private void BtnClear_OnClick(object sender, RoutedEventArgs e)
        {
            CbManagers.SelectedItem = null;
        }
    }
}
