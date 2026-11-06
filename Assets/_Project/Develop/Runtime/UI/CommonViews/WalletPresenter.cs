using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.CommonViews
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletView _view;
        private readonly WalletService _walletService;
        private readonly List<IDisposable> _subscriptions = new();

        public WalletPresenter(WalletView view, WalletService walletService)
        {
            _view = view;
            _walletService = walletService;
        }

        public void Initialize()
        {
            _subscriptions.Add(_walletService.GetCurrency(CurrencyTypes.Coins).Subscribe(OnGoldChanged));
            _view.SetValue(_walletService.GetCurrency(CurrencyTypes.Coins).Value);
        }

        public void Dispose()
        {
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();

            _subscriptions.Clear();
        }

        private void OnGoldChanged(int oldValue, int newValue)
        {
            _view.SetValue(newValue);
        }
    }
}