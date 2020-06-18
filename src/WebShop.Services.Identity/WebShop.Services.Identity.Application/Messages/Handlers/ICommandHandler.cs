using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Services.Identity.Application.Messages.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
