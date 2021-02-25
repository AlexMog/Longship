namespace Longship.Events
{
    public interface ICancellable
    {
        bool Cancelled { get; set;  }
    }
}