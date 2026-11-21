global using NUnit.Framework;
global using System.Net;

global using FinancialHub.Common.Entities;
global using FinancialHub.Common.Responses.Success;
global using FinancialHub.Common.Responses.Errors;
global using FinancialHub.Core.Domain.Models;
global using FinancialHub.Core.Domain.Entities;
global using FinancialHub.Core.Domain.Tests.Builders.Models;
global using FinancialHub.Core.Domain.Tests.Builders.Entities;
global using FinancialHub.Core.Transactions.IntegrationTests.Base;
global using FinancialHub.Core.Transactions.IntegrationTests.Setup;
global using FinancialHub.Core.Transactions.IntegrationTests.Extensions;

[assembly: Category("Integration")]