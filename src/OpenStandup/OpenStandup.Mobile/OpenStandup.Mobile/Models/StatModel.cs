

namespace OpenStandup.Mobile.Models
{
    public class StatModel
    {
        public string Name { get; }
        public long Value { get; }

        public StatModel(string name, long value)
        {
            Name = name;
            Value = value;
        }
    }
}
