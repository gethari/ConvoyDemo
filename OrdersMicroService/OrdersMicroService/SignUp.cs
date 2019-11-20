using System;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace OrdersMicroService
{
    public class SignUp : ICommand
    {
	    public string Name { get; set; }
	    public string Email { get; set; }
	    public Guid Id { get; set; }
    }

    public class SignUpComplete : IEvent
    {
	    public SignUpComplete(Guid id)
	    {
		    Id = id;
	    }
	    public Guid Id { get; }
    }
}
