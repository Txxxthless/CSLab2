
namespace pppilab2.Interfaces
{
    public interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}
