using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashlab.ViewModels;

public class SelectedCashRegistersViewModel:INotifyPropertyChanged
{
    private Cash selectedCash;

    public Cash SelectedCash
    {
        get { return selectedCash; }
        set { 
            selectedCash = value;
            OnPropertyChanged();
        }
    }

    public SelectedCashRegistersViewModel(Cash selectedCash)
    {
        SelectedCash = selectedCash;
    }

    #region MVVM 
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    #endregion

}
