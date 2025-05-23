using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using InterpoleApp.Models;
using InterpoleApp.ViewModels;
using Evidence = InterpoleApp.Models.Evidence;

namespace InterpoleApp.Views;

public partial class AddEvidenceView : ReactiveWindow<AddEvidenceViewModel>
{
    public AddEvidenceView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public async Task<Evidence?> ShowDialogAsync(Window owner)
    {
        var vm = new AddEvidenceViewModel();
        DataContext = vm;

        var tcs = new TaskCompletionSource<Evidence?>();

        vm.SaveCommand.Subscribe(evidence =>
        {
            tcs.SetResult(evidence);
            Close();
        });

        await this.ShowDialog(owner);
        return await tcs.Task;
    }
}