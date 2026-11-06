using _Project.Develop.Runtime.Meta.Features.Stats;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class StatsPresenter : IPresenter
    {
        private readonly StatsView _view;
        private readonly StatsService _statsService;

        public StatsPresenter(StatsView view, StatsService statsService)
        {
            _view = view;
            _statsService = statsService;
        }

        public void Initialize()
        {
            _statsService.StatsChanged += OnStatsChanged;
            _view.SetStats(_statsService.Wins, _statsService.Losses);
        }

        public void Dispose()
        {
            _statsService.StatsChanged -= OnStatsChanged;
        }

        private void OnStatsChanged(int wins, int losses)
        {
            _view.SetStats(wins, losses);
        }
    }
}