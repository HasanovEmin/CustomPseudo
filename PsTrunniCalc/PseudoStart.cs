using Aveva.ApplicationFramework;

namespace PsTrunniCalc
{
    public class PseudoStart : IAddin
    {
        public string Name => "PsTrunniCalc";

        public string Description => "PsTrunniCalc Calculating Trunni's attributes";

        public void Start(ServiceManager serviceManager)
        {
            Calculator.Start();
        }

        public void Stop() { }
    }
}
