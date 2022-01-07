﻿using System;

namespace Demo.Messages.Invoice
{
    public class SynchronizeInvoicePdfMessage : Message<SynchronizeInvoicePdfMessage, SynchronizeInvoicePdfMessageData>
    {
        public static SynchronizeInvoicePdfMessage Create(string correlationId, Guid id)
        {
            var data = new SynchronizeInvoicePdfMessageData
            {
                CorrelationId = correlationId,
                Id = id
            };
            return new SynchronizeInvoicePdfMessage
            {
                Queue = Queues.SynchronizeInvoicePdf,
                Data = data,
                Subject = $"Invoice/{data.Id}",
                DataVersion = data.MessageDataVersion,
                CorrelationId = data.CorrelationId
            };
        }
    }

    public class SynchronizeInvoicePdfMessageData : IMessageData
    {
        public string MessageDataVersion => "1.0";
        public string CorrelationId { get; set; }

        public Guid Id { get; set; }
    }
}
