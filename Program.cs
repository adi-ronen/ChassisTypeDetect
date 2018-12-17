using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ChassisTypeDetect
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length>0)
            {
                PrintAbout();
            }
            ChassisTypes a = GetCurrentChassisType();
            if (a == ChassisTypes.Other || a==ChassisTypes.Unknown || a==ChassisTypes.SpaceSaving || a==ChassisTypes.LunchBox||
                a == ChassisTypes.MainSystemChassis || a == ChassisTypes.ExpansionChassis || a == ChassisTypes.SubChassis ||
                a == ChassisTypes.BusExpansionChassis || a== ChassisTypes.PeripheralChassis || a == ChassisTypes.SealedCasePC)
                return 0; //Other
            if (a == ChassisTypes.Desktop || a == ChassisTypes.LowProfileDesktop || a == ChassisTypes.PizzaBox ||
                a == ChassisTypes.MiniTower || a == ChassisTypes.Tower || a==ChassisTypes.AllInOne || 
                a == ChassisTypes.StorageChassis || a == ChassisTypes.RackMountChassis)
                return 1; //Desktop
            if (a == ChassisTypes.Portable || a == ChassisTypes.Laptop || a == ChassisTypes.Notebook ||
                a == ChassisTypes.DockingStation || a==ChassisTypes.SubNotebook)
                return 2; //Laptop
            if (a == ChassisTypes.Handheld)
                return 3; //Tablet
            return 0; //Error
        }

        private static void PrintAbout()
        {
            Console.WriteLine("");
            Console.WriteLine("About");
            Console.WriteLine("=====");
            Console.WriteLine("This utility identify computer equipment type (Desktop, Laptop etc.)");
            Console.WriteLine("and returns appropriated value into ERRORLEVEL variable:");
            Console.WriteLine("1 - Desktop");
            Console.WriteLine("2 - Laptop");
            Console.WriteLine("3 - Tablet");
            Console.WriteLine("0 - Other / Unknown");
            Console.WriteLine("");
            Console.WriteLine("How to use");
            Console.WriteLine("=========");
            Console.WriteLine("For example from CMD script:");
            Console.WriteLine("----------------------------");
            Console.WriteLine("@echo Off");
            Console.WriteLine("ChassisTypeDetect.exe");
            Console.WriteLine("goto :CASE_ % ERRORLEVEL %");
            Console.WriteLine(":CASE_1");
            Console.WriteLine("echo I'm Desktop");
            Console.WriteLine("goto :END_CASE");
            Console.WriteLine(": CASE_2");
            Console.WriteLine("echo I'm Laptop");
            Console.WriteLine("goto :END_CASE");
            Console.WriteLine(": CASE_3");
            Console.WriteLine("echo I'm Tablet");
            Console.WriteLine("goto :END_CASE");
            Console.WriteLine(": CASE_0");
            Console.WriteLine("echo I'm Unknown Divice");
            Console.WriteLine("goto :END_CASE");
            Console.WriteLine(": END_CASE");
        }

        public enum ChassisTypes
        {
            Other = 1,
            Unknown,
            Desktop,
            LowProfileDesktop,
            PizzaBox,
            MiniTower,
            Tower,
            Portable,
            Laptop,
            Notebook,
            Handheld,
            DockingStation,
            AllInOne,
            SubNotebook,
            SpaceSaving,
            LunchBox,
            MainSystemChassis,
            ExpansionChassis,
            SubChassis,
            BusExpansionChassis,
            PeripheralChassis,
            StorageChassis,
            RackMountChassis,
            SealedCasePC
        }

        public static ChassisTypes GetCurrentChassisType()
        {
            ManagementClass systemEnclosures = new ManagementClass("Win32_SystemEnclosure");
            foreach (ManagementObject obj in systemEnclosures.GetInstances())
            {
                foreach (int i in (UInt16[])(obj["ChassisTypes"]))
                {
                    if (i > 0 && i < 25)
                    {
                        return (ChassisTypes)i;
                    }
                }
            }
            return ChassisTypes.Unknown;
        }

    }
}
