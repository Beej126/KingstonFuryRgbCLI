using System;
using System.Reflection;
using System.ServiceProcess;
using System.Linq;
using WebSocketSharp;

namespace KingstonFuryRgbCLI
{
    // Token: 0x0200002E RID: 46
    internal static class Program
    {
        // Token: 0x06000181 RID: 385 RVA: 0x000101F4 File Offset: 0x0000E3F4
        private static void Main(string[] args)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    var interactiveCmd = args.FirstOrDefault(s => s.ToLowerInvariant().Contains("-setrgb"));

                    //here is the main start of patching in a direct command line flow...
                    // check for special argument and just call the bar min methods to turn off lights for now
                    if (!interactiveCmd.IsNullOrEmpty())
                    {
                        //set the logging to spit out directly to console so it's that much more visible than clicking into a log file
                        Log.IsCreateConsole = true;

                        //there appears to be just a few static class variables that the native service "OnStart" method code sets up
                        // so it seems like we can simply initialize the instance just long enough for that instance method to execute and then dispose of it
                        using (var svc = new FuryController_Service())
                        {

                            //that OnStart was coded to fork a task, so wait on that to complete before attempting to send commands
                            svc.OnStartHack(args).Wait();

                            // then there's a static method for handling actual commands coming in from the client
                            // that we can just jack into with our "all off" command
                            // to find more command strings just debug the running service on that method and see what comes in
                            // this could obviously be readily extended to define more simple inbound commands from the command line via additional args strings

                            // for starters i simple combined the logic command string values of both "all_off" and "static_color" into one command string
                            // and then just flip that one "mode" value between the two to turn off and on the lights

                            // so expecting this from the command line:
                            //   -setrgb=all_off
                            // -OR-
                            //   -setrgb=static_color
                            var modeStr = interactiveCmd.Split('=')[1];
                            var speedAndBrightness = (modeStr == "all_off" ? new[] { "0", "0" } : new[] { "60", "80" });

                            Log.LibCmdLogWriter("setting mode from command line: " + modeStr, false);

                            FuryController_Service.MainWindow_OnRequestFromClient($@"
                                {{""root"":{{""api"":""set_dram_led"",""ctrl_settings_ddr5"":{{
                                    ""slot_0"":{{""index"":0,""mode"":""{modeStr}"",""ctrl_mode"":""ctrl_color"",""multicolor"":false,
                                        ""reset_default_effect"":false,""reset_color_table"":false,""direction"":0,""ir_delay"":0,""speed"":{speedAndBrightness[0]},""brightness"":{speedAndBrightness[1]},""width"":0,""hue"":0,""led_number"":0,""power_saving"":false,
                                        ""number_colors"":1,""color_table"":[[255,0,255],[0,255,0],[255,100,0],[0,0,255],[239,239,0],[128,0,128],[0,109,119],[255,200,0],[255,85,255],[60,125,255]],""background_color"":[16,16,16]}},

                                    ""slot_1"":{{""index"":1,""mode"":""{modeStr}"",""ctrl_mode"":""ctrl_color"",""multicolor"":false,
                                        ""reset_default_effect"":false,""reset_color_table"":false,""direction"":0,""ir_delay"":0,""speed"":{speedAndBrightness[0]},""brightness"":{speedAndBrightness[1]},""width"":0,""hue"":0,""led_number"":0,""power_saving"":false,
                                        ""number_colors"":1,""color_table"":[[255,0,255],[0,255,0],[255,100,0],[0,0,255],[239,239,0],[128,0,128],[0,109,119],[255,200,0],[255,85,255],[60,125,255]],""background_color"":[16,16,16]}}
                                }}}}}}
                            ");

                            //example of sending static_color command
                            //var x = @"{""root"":{""api"":""set_dram_led"",""ctrl_settings_ddr5"":{""slot_0"":{""index"":0,""mode"":""static_color"",""ctrl_mode"":""ctrl_color"",""multicolor"":false,""reset_default_effect"":false,""reset_color_table"":false,""direction"":0,""ir_delay"":0,""speed"":60,""brightness"":80,""width"":0,""hue"":0,""led_number"":0,""power_saving"":false,""number_colors"":1,""color_table"":[[255,0,255],[0,255,0],[255,100,0],[0,0,255],[239,239,0],[128,0,128],[0,109,119],[255,200,0],[255,85,255],[60,125,255]],""background_color"":[16,16,16]},""slot_1"":{""index"":1,""mode"":""static_color"",""ctrl_mode"":""ctrl_color"",""multicolor"":false,""reset_default_effect"":false,""reset_color_table"":false,""direction"":0,""ir_delay"":0,""speed"":60,""brightness"":80,""width"":0,""hue"":0,""led_number"":0,""power_saving"":false,""number_colors"":1,""color_table"":[[255,0,255],[0,255,0],[255,100,0],[0,0,255],[239,239,0],[128,0,128],[0,109,119],[255,200,0],[255,85,255],[60,125,255]],""background_color"":[16,16,16]}}}}";

                            //svc.OnStopHack();

                            Log.LibCmdLogWriter("exiting quick interactive CLI hack process", false);
                        }
                    }

                    else if (args.Contains("-ksinstall"))
                    {
                        Log.LibCmdLogWriter("install start", false);
                        ServiceInstaller.InstallAndStart("FuryController_Service", "FuryController_Service", "\"" + Assembly.GetExecutingAssembly().Location + "\"");
                    }

                    else if (args.Contains("-ksuninstall"))
                    {
                        Log.LibCmdLogWriter("uninstall start", false);
                        ServiceInstaller.Uninstall("FuryController_Service");
                        Class_DLL.InitialSMBusDriver();
                        Class_DLL.ReleaseDll();
                    }

                }
                else
                {
                    ServiceBase.Run(new ServiceBase[]
                    {
                        new FuryController_Service()
                    });
                }
            }

            catch (Exception ex)
            {
                Log.LibCmdLogWriter("problem during initial startup: " + ex.Message, true);
            }
        }
    }
}
