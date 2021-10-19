using Mediator_Memento;
using System;

namespace Mediator_Memento
{
    abstract class Mediator
    {
        public abstract void Send(string msg, Colleague colleague);
    }

    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }

    class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(Mediator mediator)
            : base(mediator)
        { }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public void Notify(string message)
        { }
    }

    class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator)
            : base(mediator)
        { }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public void Notify(string message)
        { }
    }

    class ConcreteMediator : Mediator
    {
        public ConcreteColleague1 Colleague1 { get; set; }
        public ConcreteColleague2 Colleague2 { get; set; }
        public override void Send(string msg, Colleague colleague)
        {
            if (Colleague1 == colleague)
                Colleague2.Notify(msg);
            else
                Colleague1.Notify(msg);
        }
    }
    //-------------------------------------------------------------------

    
    /// Memento

    public class Originator
    {
        string state;
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                Console.WriteLine("State = " + state);
            }
        }
        
        public Memento CreateMemento()
        {
            return (new Memento(state));
        }
        
        public void SetMemento(Memento memento)
        {
            Console.WriteLine("Restoring state...");
            State = memento.State;
        }
    }

    public class Memento
    {
        string state;

        public Memento(string state)
        {
            this.state = state;
        }
        public string State
        {
            get { return state; }
        }
    }

    public class Caretaker
    {
        Memento memento;
        public Memento Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}


class Program
{
    public static void Main(string[] args)
    {
        Originator o = new Originator();
        o.State = "On";
        // Store internal state
        Caretaker c = new Caretaker();
        c.Memento = o.CreateMemento();
        // Continue changing originator
        o.State = "Off";
        // Restore saved state
        o.SetMemento(c.Memento);
        // Wait for user
        Console.ReadKey();
    }
}
