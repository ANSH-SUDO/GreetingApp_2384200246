using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {    
        private readonly IGreetingRL _greetingRL;
        public GreetingBL(IGreetingRL greetingRL) 
        { 
            _greetingRL = greetingRL; 
        }

        public string GreetMessage(string? firstName = null, string? lastName = null)
        {
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return $"Hello, {firstName} {lastName}";
            }
            else if(!string.IsNullOrEmpty(firstName))
            {
                return $"Hello, {firstName}";
            }
            else if(!string.IsNullOrEmpty(lastName))
            {
                return $"Hello, {lastName}";
            }
            return "Hello World.";
        }


        public List<GreetingEntity> GetGreetings()
        {
            return _greetingRL.GetGreetings();
        }

        public GreetingEntity AddGreeting(GreetingEntity greeting)
        {
            return _greetingRL.AddGreeting(greeting);
        }
    }
}
