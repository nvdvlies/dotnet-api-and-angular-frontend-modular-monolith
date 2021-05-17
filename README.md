# clean-architecture-with-business-components-demo

An example architecture for a ASP.NET CORE API inspired by Jason Taylor's [CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture) template. 
Notable difference is an encapsulation of domain layer entities with a thing I called a 'BusinessComponent'. 
A BusinessComponent encapsulates an aggregate root entity and offers:

 - Validation. [View example](https://github.com/nvdvlies/clean-architecture-with-business-components-demo/blob/main/src/Demo.Domain/Invoice/BusinessComponent/Validators/NotAllowedToDeleteInvoiceInStatusValidator.cs).
 - Hooks (BeforeCreate, BeforeUpdate, BeforeDelete, AfterCreate, AfterUpdate, AfterDelete). [View example](https://github.com/nvdvlies/clean-architecture-with-business-components-demo/blob/main/src/Demo.Domain/Invoice/BusinessComponent/Hooks/SynchronizeInvoicePdfDomainEventHook.cs#).
 - Publishing of domain events. [View example](https://github.com/nvdvlies/clean-architecture-with-business-components-demo/blob/main/src/Demo.Domain/Invoice/BusinessComponent/Hooks/InvoiceStatusDomainEventHook.cs).
 - Pristine values + IsPropertyDirty check available in validators and hooks. [View example](https://github.com/nvdvlies/clean-architecture-with-business-components-demo/blob/main/src/Demo.Domain/Invoice/BusinessComponent/Validators/NotAllowedToModifyInvoiceContentInStatusValidator.cs).
 - Auditlogging. [View example](https://github.com/nvdvlies/clean-architecture-with-business-components-demo/blob/main/src/Demo.Infrastructure/Auditlogging/InvoiceAuditlogger.cs).
 
