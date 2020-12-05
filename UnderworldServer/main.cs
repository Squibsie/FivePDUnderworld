using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace UnderworldServer
{
    public class main : BaseScript
    {
        public main()
        {
            CitizenFX.Core.Debug.WriteLine("^4FivePD Underwold by Squibsie loaded!");
            EventHandlers["UW:TriggerCalloutForAll"] += new Action<Player, string, Vector3>(UWTriggerCalloutForAll);
        
        }

        private void UWTriggerCalloutForAll([FromSource] Player source, string calloutName, Vector3 calloutLocation)
        {
            CitizenFX.Core.Debug.WriteLine("TRIGGER CALLOUT FOR ALL");
            TriggerClientEvent("UW:TriggerCallout",  calloutName, calloutLocation);
        }
    }
}
