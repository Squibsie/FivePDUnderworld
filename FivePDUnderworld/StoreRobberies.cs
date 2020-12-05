using System.Threading.Tasks;
using FivePD.API;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;

namespace FivePDUnderworld
{
    
    public class StoreRobberies : FivePD.API.Plugin
    {
        //GLOBALS
        private static Vector3 _RobberyLocation;
        public static Vector3 RobberyLocation
        {
            get { return _RobberyLocation; }
            set { _RobberyLocation = value; }
        }

        private static int _AlarmType;
        public static int AlarmType
        {
            get { return _AlarmType; }
            set { _AlarmType = value; }
        }

        private static bool _RobberyActive;
        public static bool RobberyActive
        {
            get { return _RobberyActive;}
            set { _RobberyActive = value; }
        }

        private static string _CalloutName;

        public  static string CalloutName
        {
            get { return _CalloutName; }
            set { _CalloutName = value; }
        }

        private static Ped _RobberPed;
        public static Ped RobberPed
        {
            get { return _RobberPed; }
            set { _RobberPed = value; }
        }

        //END GLOBALS

        bool RobberyAttempt;

        protected Vector3[] robberyLocations = new Vector3[12]
        {
            new Vector3(-1223.239f, -906.8f, 12.326f),
            new Vector3(-707.92f, -914.141f, 19.216f),
            new Vector3(26.152f, -1346.455f, 29.497f),
            new Vector3(126.626f, -1284.196f, 29.282f),
            new Vector3(76.457f, -1389.821f, 29.376f),
            new Vector3(-48.544f, -1757.224f, 29.421f),
            new Vector3(134.955f, -1707.856f, 29.292f),
            new Vector3(424.509f, -809.595f, 29.491f),
            new Vector3(1136.179f, -981.778f, 46.416f),
            new Vector3(1211.617f, -470.742f, 66.208f),
            new Vector3(1162.55f, -324.063f, 69.205f),
            new Vector3(374.899f, 327.573f, 103.566f)
        };


        Vector3 playerPos;

        internal StoreRobberies()
        {
            _CalloutName = "UW:StoreRobbery";
            _AlarmType = 0;
            _RobberyActive = false;
            Tick += RobberyStart;

            EventHandlers["UW:TriggerCallout"] += new Action<string, Vector3>(TriggerCallout);
        }

        private void TriggerCallout(string calloutName, Vector3 calloutLocation)
        {
            bool DutyStatus = FivePD.API.Utilities.IsPlayerOnDuty();
            _RobberyLocation = calloutLocation;
            if (DutyStatus)
            {
                Utilities.ForceCallout(calloutName);
            }
            
        }

        private async Task RobberyStart()
        {
            CitizenFX.Core.Debug.WriteLine("------------------------------------------");
            CitizenFX.Core.Debug.WriteLine("^4UNDERWORLD:^0 Store Robberies are active!");
            CitizenFX.Core.Debug.WriteLine("------------------------------------------");
            foreach (Vector3 pos in robberyLocations)
            {
                Blip RobberyLoc = World.CreateBlip(pos);
                RobberyLoc.Sprite = BlipSprite.Package;
                RobberyLoc.Name = "Robbery Location";
            }
            bool DutyStatus = FivePD.API.Utilities.IsPlayerOnDuty();
            while (!_RobberyActive && !DutyStatus)
            {
                foreach (Vector3 pos in robberyLocations)
                {
                    playerPos = LocalPlayer.Character.Position;
                    float dist = World.GetDistance(pos, playerPos);

                    if (dist < 5f)
                    {
                        API.SetTextComponentFormat("String");
                        API.AddTextComponentString("Press E to begin a Robbery!");
                        API.DisplayHelpTextFromStringLabel(0, false, true, -1);
                        if (Game.IsControlJustReleased(1, Control.Pickup))
                        {
                            _RobberPed = LocalPlayer.Character;
                            _RobberyActive = true;

                            API.BeginTextCommandThefeedPost("STRING");
                            API.AddTextComponentSubstringPlayerName("ROBBERY HAS BEGUN, REMAIN AT LOCATION UNTIL IT COMPLETES");
                            API.EndTextCommandThefeedPostTicker(isImportant: false, bHasTokens: true);
                            StoreAlarm();
                            await Delay(5000);
                            if (dist < 5f)
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("25 Seconds Remains.");
                                API.EndTextCommandThefeedPostTicker(isImportant: false, bHasTokens: true);
                            }
                            else
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("You left too soon, your robbery was not successful.");
                                API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
                                RobberyAttempt = true;
                                _RobberyActive = false;
                            }
                            await Delay(5000);
                            if (dist < 5f)
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("20 Seconds Remains.");
                                API.EndTextCommandThefeedPostTicker(isImportant: false, bHasTokens: true);
                            }
                            else
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("You left too soon, your robbery was not successful.");
                                API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
                                RobberyAttempt = true;
                                _RobberyActive = false;
                            }
                            await Delay(5000);
                            if (dist < 5f)
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("15 Seconds Remains.");
                                API.EndTextCommandThefeedPostTicker(isImportant: false, bHasTokens: true);
                            }
                            else
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("You left too soon, your robbery was not successful.");
                                API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
                                RobberyAttempt = true;
                                _RobberyActive = false;
                            }
                            await Delay(5000);
                            if (dist < 5f)
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("10 Seconds Remains.");
                                API.EndTextCommandThefeedPostTicker(isImportant: false, bHasTokens: true);
                            }
                            else
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("You left too soon, your robbery was not successful.");
                                API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
                                RobberyAttempt = true;
                                _RobberyActive = false;
                            }
                            await Delay(5000);
                            if (dist < 5f)
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("5 Seconds Remains.");
                                API.EndTextCommandThefeedPostTicker(isImportant: false, bHasTokens: true);
                            }
                            else
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("You left too soon, your robbery was not successful.");
                                API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
                                RobberyAttempt = true;
                                _RobberyActive = false;
                            }

                            await Delay(5000);
                            if (dist < 5f)
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("Robbery complete. Escape.");
                                API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
                                RobberyAttempt = false;
                                _RobberyActive = false;
                            }
                            else
                            {
                                API.BeginTextCommandThefeedPost("STRING");
                                API.AddTextComponentSubstringPlayerName("You left too soon, your robbery was not successful.");
                                API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);
                                RobberyAttempt = true;
                                _RobberyActive = false;
                            }

                            //TODO: DISCORD POST OF ROBBERY BEGINNING.
                        }
                    }
                }
                await Delay(10);
            }
        }

        private async void StoreAlarm()
        {
            Random random = new Random();

            uint streetName = 0u;
            uint crossing = 0u;

            int dice = random.Next(0, 4);
            switch (dice)
            {
                case 1:
                    //Silent Alarm triggered
                    _AlarmType = 1;
                    AlertPD();
                    
                    CitizenFX.Core.Native.API.GetStreetNameAtCoord(LocalPlayer.Character.Position.X, LocalPlayer.Character.Position.Y, LocalPlayer.Character.Position.Z, ref streetName, ref crossing);

                    BaseScript.TriggerServerEvent("GenericDiscordPost", CitizenFX.Core.Native.API.GetPlayerName(CitizenFX.Core.Native.API.PlayerId()), CitizenFX.Core.Native.API.GetStreetNameFromHashKey(streetName), "**SILENT ALARM TRIGGERED AT LOCATION**");
                    break;
                case 2:
                    //5 second delay and alarm triggered.
                    await BaseScript.Delay(5000);
                    _AlarmType = 2;
                    AlertPD();
                    API.BeginTextCommandThefeedPost("STRING");
                    API.AddTextComponentSubstringPlayerName("The store alarm has been triggered!");
                    API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);

                    CitizenFX.Core.Native.API.GetStreetNameAtCoord(LocalPlayer.Character.Position.X, LocalPlayer.Character.Position.Y, LocalPlayer.Character.Position.Z, ref streetName, ref crossing);

                    BaseScript.TriggerServerEvent("GenericDiscordPost", CitizenFX.Core.Native.API.GetPlayerName(CitizenFX.Core.Native.API.PlayerId()), CitizenFX.Core.Native.API.GetStreetNameFromHashKey(streetName), "**STORE ALARM TRIGGERED AT LOCATION**");
                    break;
                case 3:
                    //Police called after completion.
                    await BaseScript.Delay(30000);
                    _AlarmType = 3;
                    AlertPD();
                    API.BeginTextCommandThefeedPost("STRING");
                    API.AddTextComponentSubstringPlayerName("The store has called the police");
                    API.EndTextCommandThefeedPostTicker(isImportant: true, bHasTokens: true);

                    CitizenFX.Core.Native.API.GetStreetNameAtCoord(LocalPlayer.Character.Position.X, LocalPlayer.Character.Position.Y, LocalPlayer.Character.Position.Z, ref streetName, ref crossing);

                    BaseScript.TriggerServerEvent("GenericDiscordPost", CitizenFX.Core.Native.API.GetPlayerName(CitizenFX.Core.Native.API.PlayerId()), CitizenFX.Core.Native.API.GetStreetNameFromHashKey(streetName), "**STORE REPORTING THEY HAVE BEEN ROBBED.**");
                    break;
            }
        }

        private void AlertPD()
        {            
            BaseScript.TriggerServerEvent("UW:TriggerCalloutForAll", _CalloutName, LocalPlayer.Character.Position);
        }

    }
}