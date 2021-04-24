﻿using Demo.Domain.Shared.Entities;
using Demo.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Domain.Shared.BusinessComponent.Hooks
{
    internal class LoggingHook<T> : IBeforeCreate<T>, IBeforeUpdate<T>, IBeforeDelete<T>, IAfterCreate<T>, IAfterUpdate<T>, IAfterDelete<T>
        where T : Entity
    {
        private readonly ILogger<LoggingHook<T>> _logger;

        public LoggingHook(ILogger<LoggingHook<T>> logger)
        {
            _logger = logger;
        }

        public Task ExecuteAsync(HookType type, IBusinessComponentContext<T> context, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{type}: {typeof(T).Name}");
            return Task.CompletedTask;
        }
    }
}
