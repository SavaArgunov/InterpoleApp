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

public partial class EditEvidenceView : ReactiveWindow<EditEvidenceViewModel>
{
    public EditEvidenceView()
    {
        InitializeComponent();
        this.WhenActivated(d => { /* bindings if needed */ });
    }
    public async Task<Evidence?> ShowEdiotDialog(Window owner, Evidence evidence)
    {
        var vm = new EditEvidenceViewModel(evidence);
        DataContext = vm;
        var tcs = new TaskCompletionSource<Evidence?>();
        ((IObservable<Evidence>)vm.SaveCommand)
            .Subscribe(result =>
            {
                tcs.SetResult(result);
                Close();
            });
        
        await this.ShowDialog(owner);
        return await tcs.Task;
    }
}