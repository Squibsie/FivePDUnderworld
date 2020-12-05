using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePDUnderworld;

namespace UnderworldCallouts
{
    [CalloutProperties("UW:StoreRobbery", "Squibsie", "1.0")]
    public class StoreRobberyCallout : FivePD.API.Callout
    {
        bool RobberyActive = FivePDUnderworld.StoreRobberies.RobberyActive;
        int AlarmType = FivePDUnderworld.StoreRobberies.AlarmType;
        public StoreRobberyCallout()
        {
            InitInfo(FivePDUnderworld.StoreRobberies.RobberyLocation);
            ShortName = "(UW) Store Robbery";
            CalloutDescription = "A STORE IS REPORTING A ROBBERY IN PROGRESS. ATTEND AND INVESTIGATE. \n" +
                "(This is an underworld callout, the suspect will be a real player!)";
            ResponseCode = 3;
            StartDistance = 15f;
        }

        public override async Task OnAccept()
        {
            InitBlip();
            if(AlarmType == 1)
            {
                CitizenFX.Core.Native.API.BeginTextCommandThefeedPost("STRING");
                CitizenFX.Core.Native.API.AddTextComponentSubstringPlayerName("SILENT ALARM AT LOCATION. ROBBER WILL BE UNAWARE.");
                CitizenFX.Core.Native.API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
            }
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
        }

        public override async Task<bool> CheckRequirements()
        {
            if (RobberyActive)
                return true;
            return false;
            
        }

        public override void OnCancelAfter()
        {
        }
    }
}
