﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Domain.FeatureFlagSettings.Interfaces;
using Demo.Domain.Shared.Interfaces;

namespace Demo.Infrastructure.Services
{
    internal class FeatureFlagChecker : IFeatureFlagChecker
    {
        private readonly ICurrentUser _currentUser;
        private readonly IFeatureFlagSettingsProvider _featureFlagSettingsProvider;

        public FeatureFlagChecker(
            IFeatureFlagSettingsProvider featureFlagSettingsProvider,
            ICurrentUser currentUser
        )
        {
            _featureFlagSettingsProvider = featureFlagSettingsProvider;
            _currentUser = currentUser;
        }

        public async Task<bool> IsEnabledAsync(string name, CancellationToken cancellationToken = default)
        {
            var featureFlagSettings = await _featureFlagSettingsProvider.GetAsync(cancellationToken);
            return featureFlagSettings.Settings.FeatureFlags.Any(x =>
                x.Name == name && (x.EnabledForAll || x.EnabledForUsers.Contains(_currentUser.Id)));
        }
    }
}