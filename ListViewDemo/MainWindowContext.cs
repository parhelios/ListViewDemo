using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ListViewDemo;

public class MainWindowContext : INotifyPropertyChanged
{
    private string _prodName;

    public string ProdName
    {
        get
        {
            return _prodName;
        }
        set
        {
            _prodName = value;
            OnPropertyChanged("ProdName");
        }
    }

    private string _prodCost;

    public string ProdCost
    {
        get
        {
            return _prodCost;
        }
        set
        {
            _prodCost = value;
            OnPropertyChanged("ProdCost");
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}