using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;

namespace RealisticDrivingExperience
{
    public class Main : Script
    {
        public Main()
        {
            Tick += onTick;
            KeyDown += onKeyDown;
        }
        void onKeyDown(object sender, KeyEventArgs e)
        {
            Ped player = Game.Player.Character;
            if (player.IsInVehicle())
            {
                Vehicle curVeh = player.CurrentVehicle;
                if(e.KeyCode == Keys.W)
                {
                    curVeh.SteeringScale = 1f;
                    curVeh.Throttle = 10f;

                }
            }
        }
        void onTick(object sender, EventArgs e)
        {
            
           
        }
    }
}
