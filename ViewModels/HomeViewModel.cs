using System;
using ReactiveUI;
using System.Reactive;
using Avalonia.Controls;
using InterpoleApp.ViewModels;
using InterpoleApp.Views;

namespace InterpoleApp.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> SayHelloCommand { get; }
    public string Title => "Домашняя страница";

    public HomeViewModel()
    {
        SayHelloCommand = ReactiveCommand.Create(() =>
        {
            Console.WriteLine("Hello");
        });
    }
}