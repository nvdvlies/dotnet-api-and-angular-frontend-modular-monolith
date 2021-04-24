﻿using AutoMapper;
using Demo.Domain.Customer;

namespace Demo.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerMappingProfile : Profile
    {
        public UpdateCustomerMappingProfile()
        {
            CreateMap<UpdateCustomerCommand, Customer>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Code, opt => opt.Ignore())
                .ForMember(x => x.InvoiceEmailAddress, opt => opt.Ignore())
                .ForMember(x => x.Invoices, opt => opt.Ignore())
                .ForMember(x => x.Deleted, opt => opt.Ignore())
                .ForMember(x => x.DeletedBy, opt => opt.Ignore())
                .ForMember(x => x.DeletedOn, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedOn, opt => opt.Ignore());
        }
    }
}