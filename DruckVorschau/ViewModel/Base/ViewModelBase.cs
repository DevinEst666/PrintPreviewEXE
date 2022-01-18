using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Threading.Tasks;

namespace DruckVorschau.ViewModel.Base
{
    public abstract class ViewModelBase : ReactiveObject
    {
        [Reactive] public bool IsBusy { get; protected set; }
        [Reactive] public bool IsInitializing { get; private set; }

        public async Task InitializeAsync(object parameter)
        {
            IsInitializing = true;
            await DoInitializeAsync(parameter);
            IsInitializing = false;
        }

        protected virtual Task DoInitializeAsync(object parameter) => Task.CompletedTask;
    }
}
