using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using InterpoleApp.Models;
using InterpoleApp.ViewModels;

namespace InterpoleApp.Views;

public partial class AddCriminalView : ReactiveWindow<AddCriminalViewModel>
{
    public AddCriminalView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public async Task<Criminal?> ShowDialogAsync(Window owner)
    {
        var vm = new AddCriminalViewModel();
        DataContext = vm;

        var tcs = new TaskCompletionSource<Criminal?>();

        vm.SaveCommand.Subscribe(criminal =>
        {
            tcs.SetResult(criminal);
            Close();
        });

        await this.ShowDialog(owner);
        return await tcs.Task;
    }
}