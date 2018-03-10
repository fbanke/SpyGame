using SpyLib;

namespace SpyLibTest
{
    public class MockValidatorAlwaysTrue : IBoardValidator
    {
        public string GetDebug()
        {
            return "";
        }
        public bool IsValid(Board board)
        {
            return true;
        }
    }
}
