﻿using System;

namespace Demo.Events.Invoice
{
    public class InvoiceSentEvent : Event<InvoiceSentEvent, InvoiceSentEventData>
    {
        public static InvoiceSentEvent Create(string correlationId, Guid id)
        {
            var data = new InvoiceSentEventData
            {
                CorrelationId = correlationId,
                Id = id
            };
            return new InvoiceSentEvent
            {
                Topic = Topics.Invoice,
                Data = data,
                Subject = $"Invoice/{data.Id}",
                DataVersion = data.EventDataVersion,
                CorrelationId = data.CorrelationId
            };
        }
    }

    public class InvoiceSentEventData : IEventData
    {
        public string EventDataVersion => "1.0";
        public string CorrelationId { get; set; }

        public Guid Id { get; set; }
    }
}
