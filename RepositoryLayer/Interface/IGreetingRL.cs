using RepositoryLayer.Entity;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        List<GreetingEntity> GetGreetings();

        public GreetingEntity GetGreetingById(int id);
        GreetingEntity AddGreeting(GreetingEntity greeting);
    }
}
