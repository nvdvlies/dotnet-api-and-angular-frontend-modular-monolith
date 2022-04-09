﻿using Azure.Messaging.EventGrid;
using Demo.Infrastructure.Events;
using Demo.WebApi.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ApiControllerBase
    {
        [Authorize(nameof(Policies.Machine))]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [Authorize(nameof(Policies.Machine))]
        public async Task<ActionResult> Post([FromBody] EventGridEvent[] eventGridEvents, CancellationToken cancellationToken)
        {
            var @events = eventGridEvents.Select(eventGridEvent => eventGridEvent.ToEvent());

            foreach (var @event in @events)
            {
                await Mediator.Publish(@event, cancellationToken);
            }

            return Ok();
        }
    }
}
