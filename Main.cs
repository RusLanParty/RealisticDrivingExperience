using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.Native;

namespace RealisticDrivingExperience
{
    public class Main : Script
    {
        float throt = 0.0f;
        float steer = 0.0f;
        public Main()
        {
            Tick += onTick;
        }
        void onTick(object sender, EventArgs e)
        {
            Ped player = Game.Player.Character;
            if (player.IsInVehicle())
            {
                Vehicle curVeh = player.CurrentVehicle;
                GTA.UI.Screen.ShowHelpText("THROT= " + throt + " BRAKEPOW= " + curVeh.BrakePower, 1500, false, false);
                if (!GTA.Game.IsKeyPressed(Keys.ShiftKey))
                {
                    if (GTA.Game.IsKeyPressed(Keys.A))
                    {
                        curVeh.SteeringScale = steer;
                        if(steer < 0.5f)
                        {
                            steer = steer + 0.03f;
                        }
                    }
                   
                    else if(GTA.Game.IsKeyPressed(Keys.D))
                    {
                        curVeh.SteeringScale = steer;
                        if(steer > -0.5f)
                        {
                            steer = steer - 0.03f;
                        }
                    }
                    else if (!GTA.Game.IsKeyPressed(Keys.D) && !GTA.Game.IsKeyPressed(Keys.A))
                    {
                        curVeh.SteeringScale = steer;
                        if(steer > 0.0f)
                        {
                            steer = steer - 0.03f;
                        }
                        else
                            if(steer < 0.0f)
                        {
                            steer = steer + 0.03f;
                        }
                    }
                    if (GTA.Game.IsKeyPressed(Keys.W))
                    {

                        curVeh.CurrentRPM = throt;
                        if (throt <= 1.0f)
                        {
                            throt = throt + 0.001f;
                        }
                    }
                    else if (!GTA.Game.IsKeyPressed(Keys.W))
                    {
                        throt = curVeh.CurrentRPM;
                        if (throt >= 0.0f)
                        {
                            throt = throt - 0.001f;
                        }
                    }
                    if (GTA.Game.IsKeyPressed(Keys.S))
                    {
                        curVeh.BrakePower = 0.1f;
                        curVeh.AreBrakeLightsOn = true;
                    }
                    else
                    if (!GTA.Game.IsKeyPressed(Keys.S))
                    {
                        curVeh.AreBrakeLightsOn = false;
                        curVeh.BrakePower = 1.0f;
                        Function.Call(Hash.SET_VEHICLE_BRAKE, curVeh, false);
                    }
                }
            }
        }
    }
}
