using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace toDoList;

public partial class MainPage : ContentPage
{

    public MainPage()
	{
		InitializeComponent();
	}
    public void ClickOnAddButton(object sender, EventArgs e)
    {
        inputText.Text = string.Empty;
    }
}

public class ColorItem
{
    public string Name { get; set; }
    public Color Value { get; set; }
    public override string ToString()
    {
        return Name;
    }

}
class ViewModel : INotifyPropertyChanged
{
	private string _surname;
	public string Surname
	{
		get => _surname;
        set
		{
			if (_surname == value) return;
			_surname = value;
			OnPropertyChanged(nameof(Surname));
		}
	}
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObservableCollection<ColorItem> MyColors { get; set; } = new ObservableCollection<ColorItem>
    {
        new ColorItem { Name = "Без категории", Value = Colors.White },
		new ColorItem { Name = "Черный", Value = Colors.Black },
        new ColorItem { Name = "Синий", Value = Colors.Blue },
        new ColorItem { Name = "Красный", Value = Colors.Red },
        new ColorItem { Name = "Зеленый", Value = Colors.Green },
    };
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand DoneCommand { get; }
}

