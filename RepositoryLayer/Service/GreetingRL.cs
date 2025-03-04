using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly GreetingContext _context;
        public GreetingRL(GreetingContext context) 
        { 
            _context = context; 
        }
        public List<GreetingEntity> GetGreetings()
        {
            return _context.Greetings.ToList();
        }

        public GreetingEntity AddGreeting(GreetingEntity greeting)
        {
            _context.Greetings.Add(greeting);
            _context.SaveChanges();
            return greeting;
        }

        public GreetingEntity GetGreetingById(int id)
        {
            return _context.Greetings.FirstOrDefault(g => g.Id == id);
        }

        public GreetingEntity UpdateGreeting(int id, GreetingEntity greeting)
        {
            var existingGreeting = _context.Greetings.FirstOrDefault(g => g.Id == id);
            if (existingGreeting == null) 
                return null;
            existingGreeting.Message = greeting.Message;
            _context.SaveChanges();
            return existingGreeting;
        }

        public bool DeleteGreeting(int id)
        {
            var greeting = _context.Greetings.FirstOrDefault(g => g.Id == id);
            if (greeting == null) return false;
            _context.Greetings.Remove(greeting);
            _context.SaveChanges();
            return true;
        }
    }
}
