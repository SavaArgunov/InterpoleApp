using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using InterpoleApp.Models;
using InterpoleApp.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;

namespace InterpoleApp.Views;

public partial class EditCriminalView : ReactiveWindow<EditCriminalViewModel>
{
    public EditCriminalView()
    {
        InitializeComponent();
        this.WhenActivated(d => { /* bindings if needed */ });
    }

    public async Task<Criminal?> ShowEdiotDialog(Window owner, Criminal criminal)
    {
        var vm = new EditCriminalViewModel(criminal);
        DataContext = vm;
        Console.WriteLine("Before: " + criminal.Name);
        var tcs = new TaskCompletionSource<Criminal?>();
        ((IObservable<Criminal>)vm.SaveCommand)
            .Subscribe(result =>
            {
                tcs.SetResult(result);
                Close();
            });
        
        await this.ShowDialog(owner);
        return await tcs.Task;
    }
}