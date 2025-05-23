using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using InterpoleApp.Models;
using InterpoleApp.ViewModels;

namespace InterpoleApp.Views;

public partial class AddComplaintsView : ReactiveWindow<AddComplaintsViewModel>
{
    public AddComplaintsView()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public async Task<Complaint?> ShowDialogAsync(Window owner)
    {
        var vm = new AddComplaintsViewModel();
        DataContext = vm;

        var tcs = new TaskCompletionSource<Complaint?>();

        vm.SaveCommand.Subscribe(complaint =>
        {
            tcs.SetResult(complaint);
            Close();
        });

        await this.ShowDialog(owner);
        return await tcs.Task;
    }
}