using Demo.Application.Invoices.Commands.CreateInvoice.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace Demo.Application.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<CreateInvoiceResponse>
    {
        public Guid CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int PaymentTerm { get; set; }
        public string OrderReference { get; set; }
        public List<CreateInvoiceCommandInvoiceLine> InvoiceLines { get; set; }
    }
}