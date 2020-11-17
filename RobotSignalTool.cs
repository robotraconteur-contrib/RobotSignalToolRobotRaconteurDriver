using System;
using System.Collections.Generic;
using System.Text;
using com.robotraconteur.robotics.tool;
using com.robotraconteur.robotics.robot;
using RobotRaconteur;

namespace RobotSignalToolRobotRaconteurDriver
{
    struct SignalCommand
    {
        public string signal_name;
        public double signal_value;
    }

    class RobotSignalTool : Tool_default_impl, IDisposable
    {
        protected SignalCommand[] _open_command;
        protected SignalCommand[] _close_command;

        protected ServiceSubscription _robot_sub;



        public RobotSignalTool(string robot_url, SignalCommand[] open_command, SignalCommand[] close_command)
        {
            _robot_sub = RobotRaconteurNode.s.SubscribeService(robot_url);
            _open_command = open_command;
            _close_command = close_command;
        }

        public override void open()
        {
            Robot robot = (Robot)_robot_sub.GetDefaultClient();
            foreach (var c in _open_command)
            {
                robot.setf_signal(c.signal_name, new double[] { c.signal_value });
            }
        }

        public override void close()
        {
            Robot robot = (Robot)_robot_sub.GetDefaultClient();
            foreach (var c in _close_command)
            {
                robot.setf_signal(c.signal_name, new double[] { c.signal_value });
            }
        }

        public void Dispose()
        {
            _robot_sub?.Close();
        }
    }
}
