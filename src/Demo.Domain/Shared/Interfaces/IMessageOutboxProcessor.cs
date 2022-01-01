﻿using Demo.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Domain.Shared.Interfaces
{
    public interface IMessageOutboxProcessor
    {
        Task AddToOutboxAsync(Message message, CancellationToken cancellationToken);
        Task SendAllAsync(CancellationToken cancellationToken);
    }
}