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
        static float throt = 0.0f;
        static float steer = 0.0f;
        static float spinSpeed = 0.04f;
        float change;
        float speed;
        double dec = 0.00;
        bool once = false;
        public Main()
        {
            Tick += onTick;
        }
        void onTick(object sender, EventArgs e)
        {
            Ped player = Game.Player.Character;
            if (!player.IsInVehicle())
            {
                once = false;
            }
            if (player.IsInVehicle())
            {
                if (!player.CurrentVehicle.IsAircraft && !player.CurrentVehicle.IsBoat && !player.CurrentVehicle.IsMotorcycle && !player.CurrentVehicle.IsHelicopter && !player.CurrentVehicle.IsTrain)
                {
                    Vehicle curVeh = player.CurrentVehicle;
                    if (!once)
                    {
                        steer = curVeh.SteeringScale;
                        once = true;
                    }
                    //Function.Call(Hash.SET_VEHICLE_STEER_BIAS, curVeh, steer);
                    //GTA.UI.Screen.ShowHelpText("SPEED: ~y~" + curVeh.Speed + "\n~s~THROT: ~y~" + throt + "\n~s~STEER: ~y~" + steer + "\n~s~curVeh.SteeringScale: ~y~" + curVeh.SteeringScale + "\n~s~RTRNSPD: ~y~" + dec + "\n~s~GEAR: ~y~" + curVeh.CurrentGear, 1500, false, false);
                    if (!GTA.Game.IsKeyPressed(Keys.ShiftKey))
                    {
                        if (steer < 0.02f && steer > -0.02f)
                        {
                            steer = curVeh.SteeringScale;
                            steer = 0;
                        }
                        if (steer > 1.0f)
                        {
                            steer = 1.0f;
                        }
                        if (steer < -1.0f)
                        {
                            steer = -1.0f;
                        }
                        curVeh.SteeringScale = (float)dec;
                        if (GTA.Game.IsKeyPressed(Keys.A))
                        {
                            if (steer <= 1.0f)
                            {
                                curVeh.SteeringScale = (float)dec;
                                steer = steer + (spinSpeed);
                                dec = Math.Round(steer, 2);
                                curVeh.SteeringScale = (float)dec;
                            }
                        }
                        else if (GTA.Game.IsKeyPressed(Keys.D))
                        {
                            if (steer >= -1.0f)
                            {
                                curVeh.SteeringScale = (float)dec;
                                steer = steer - (spinSpeed);
                                dec = Math.Round(steer, 2);
                                curVeh.SteeringScale = (float)dec;
                            }
                        }
                        else if (!GTA.Game.IsKeyPressed(Keys.D) && !GTA.Game.IsKeyPressed(Keys.A))
                        {
                            if (steer > 0.0f)
                            {
                                curVeh.SteeringScale = (float)dec;
                                if (Math.Abs(curVeh.Speed) > 1)
                                {
                                    if (Math.Abs(curVeh.Speed) >= 30f)
                                    {
                                        speed = 30f;
                                    }
                                    else if (Math.Abs(curVeh.Speed) < 30)
                                    {
                                        speed = curVeh.Speed;
                                    }
                                    if (steer < 1.0f)
                                    {
                                        change = (spinSpeed * (Math.Abs(0.07f * speed / curVeh.SteeringScale / 2)));
                                        if (Math.Abs(change) > 0.1f)
                                        {
                                            change = 0.1f;
                                        }
                                        steer = steer - change;
                                        dec = Math.Round(steer, 2);

                                    }
                                    else if (steer == 1.0f)
                                    {
                                        steer = steer - (spinSpeed * Math.Abs((0.07f * speed)));
                                        dec = Math.Round(steer, 2);
                                    }
                                    curVeh.SteeringScale = (float)dec;
                                }
                            }
                            else

                            if (steer < 0.0f)
                            {
                                curVeh.SteeringScale = (float)dec;
                                if (Math.Abs(curVeh.Speed) > 1)
                                {
                                    if (Math.Abs(curVeh.Speed) >= 30f)
                                    {
                                        speed = 30f;
                                    }
                                    else if (Math.Abs(curVeh.Speed) < 30)
                                    {
                                        speed = curVeh.Speed;
                                    }
                                    if (steer > -1.0f)
                                    {
                                        change = (spinSpeed * (Math.Abs(0.07f * speed / curVeh.SteeringScale / 2)));
                                        if (Math.Abs(change) > 0.1f)
                                        {
                                            change = 0.1f;
                                        }
                                        steer = steer + change;
                                        dec = Math.Round(steer, 2);
                                    }
                                    else if (steer == -1.0f)
                                    {
                                        steer = steer + (spinSpeed * Math.Abs((0.07f * speed)));
                                        dec = Math.Round(steer, 2);
                                    }
                                    curVeh.SteeringScale = (float)dec;
                                }
                            }
                        }

                        if (GTA.Game.IsKeyPressed(Keys.W))
                        {
                            if (curVeh.CurrentGear < curVeh.HighGear)
                            {
                                curVeh.CurrentRPM = throt;
                            }
                            if (throt <= 1.0f)
                            {
                                throt = throt + 0.001f;
                            }
                            if (throt >= 1.0f && curVeh.CurrentGear < curVeh.HighGear)
                            {
                                curVeh.CurrentRPM = 0.38f;
                                curVeh.CurrentGear++;
                                throt = curVeh.CurrentRPM;
                            }
                        }
                        else if (!GTA.Game.IsKeyPressed(Keys.W))
                        {
                            throt = curVeh.CurrentRPM;
                            if (throt >= 0.0f)
                            {
                                throt = throt - 0.001f;
                            }
                            if (throt <= 0.1f && curVeh.CurrentGear > 1)
                            {
                                curVeh.CurrentRPM = 0.88f;
                                curVeh.CurrentGear--;
                                throt = curVeh.CurrentRPM;
                            }

                        }
                        if (GTA.Game.IsKeyPressed(Keys.S))
                        {
                            curVeh.BrakePower = 0.0001f;
                            curVeh.AreBrakeLightsOn = true;
                        }
                        else
                        if (!GTA.Game.IsKeyPressed(Keys.S))
                        {
                            curVeh.AreBrakeLightsOn = false;
                            curVeh.BrakePower = 1.0f;

                        }
                    }
                    else if (GTA.Game.IsKeyPressed(Keys.ShiftKey))
                    {
                        steer = curVeh.SteeringScale;
                        dec = Math.Round(steer, 2);
                    }
                }
            }
             
        }
    }
}
