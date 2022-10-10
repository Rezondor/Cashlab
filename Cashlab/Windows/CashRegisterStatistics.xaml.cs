using System.Windows;

namespace Cashlab.Windows
{
    /// <summary>
    /// Логика взаимодействия для CashRegisterStatistics.xaml
    /// </summary>
    public partial class CashRegisterStatistics : Window
    {
        public CashRegisterStatistics(Cash selectedCash)
        {
            DataContext = new SelectedCashRegistersViewModel(selectedCash);
            InitializeComponent();
        }
    }
}
