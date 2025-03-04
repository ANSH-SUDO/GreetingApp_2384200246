using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        public string GreetMessage(string? firstName = null, string? lastName = null);

        List<GreetingEntity> GetGreetings();
        GreetingEntity AddGreeting(GreetingEntity greeting);

        public GreetingEntity GetGreetingById(int id);
        GreetingEntity UpdateGreeting(int id, GreetingEntity greeting);
        bool DeleteGreeting(int id);
    }
}
