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
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox { BindingContext: ItemViewModel myItem })
        {
            myItem.ItemTextDecorations = e.Value ? TextDecorations.Strikethrough : TextDecorations.None;
        }
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

public class ItemViewModel : INotifyPropertyChanged
{
    private string _text;
    private ColorItem _color;
    private TextDecorations itemTextDecorations;

    public string Text
    {
        get => _text;
        set
        {
            if(Equals(value, _text)) return;
            _text = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    public ColorItem Color
    {
        get => _color;
        set
        {
            if (Equals(value, _color)) return;
            _color = value;
            OnPropertyChanged(nameof(Color));
        }
    }

    public TextDecorations ItemTextDecorations
    {
        get => itemTextDecorations;
        set
        {
            if (itemTextDecorations != value)
            {
                itemTextDecorations = value;
                OnPropertyChanged(nameof(ItemTextDecorations));
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
class ViewModel : INotifyPropertyChanged
{
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }
    public ViewModel()
    {
        AddCommand = new Command<string>(obj =>
        {
            Items.Add(new ItemViewModel{ Text = obj, Color = ChosenColor });
        },
        obj => !string.IsNullOrWhiteSpace(obj));

        DeleteCommand = new Command<ItemViewModel>(obj =>
        {
            Items.Remove(obj);
        });
    }

    private ColorItem _chosenColor;

    public ColorItem ChosenColor
    {
        get => _chosenColor;
        set
        {
            if(Equals(value, _chosenColor)) return;
            _chosenColor = value;
            OnPropertyChanged(nameof(ChosenColor));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<ItemViewModel> Items { get; set; } = new ObservableCollection<ItemViewModel>();

    public ObservableCollection<ColorItem> MyColors { get; set; } = new ObservableCollection<ColorItem>
    {
        new ColorItem { Name = "Без категории", Value = Colors.White },
		new ColorItem { Name = "Черный", Value = Colors.Black },
        new ColorItem { Name = "Синий", Value = Colors.Blue },
        new ColorItem { Name = "Красный", Value = Colors.Red },
        new ColorItem { Name = "Зеленый", Value = Colors.Green },
    };

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

