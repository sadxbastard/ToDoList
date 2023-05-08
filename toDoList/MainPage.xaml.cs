using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace toDoList;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}
}
class PersonViewModel : INotifyPropertyChanged
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
		PropertyChanged ?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}

