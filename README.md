# KingstonFuryRgbCLI
This project is about taking a .net decompile of Kingston's "Fury CTRL" software and tweaking it to be a command-line based tool.<br/>
I was specifically interested in the lowest possible overhead approach to **disable** the RGB lights on my RAM.

- i just run it via a shortcut in my startup folder (%appdata%\Microsoft\Windows\Start Menu\Programs\Startup)
- see [/scripts/fury_rgb_off.ps1](/FuryControllerService/scripts/fury_rgb_off.ps1) for this example usage
  - running this kind of system level stuff does require you to enable Advanced > **"Run as Administrator"** on the shortcut
  - and that does means we get a UAC prompt which could be avoided by running as a Windows Scheduled Task under the "SYSTEM" account

#### Driver & DLL dependencies
- under the [dlls folder](/FuryControllerService/dlls) there are a few libraries required that start from this code calling into FuryCTRL.dll (see [Class_DLL.cs](FuryControllerService/Class_DLL.cs))
- also in there is a NTIOLib_X64.sys driver necessary to pull off the communication with the RGB controller, so in my example script I include a simple driver install command line as well as `net start/stop` commands before and after. I am very pleased this boils down to a **completely clean minimal shot at setting the RGB with nothing left running after**.

#### Enabling more RGB settings
- This code base does include all the fancy RGB effects possible beyond my personal interest to just turn the lights off
  - the initial tweaks i applied actually support `-climode=static_color` already as well (setting to a hard coded color)
- to learn all the possible commands, just fire up the FuryCTRL.exe client that comes with the install and watch the log file coming from the service to see the corresponding payloads.
- **or** do the service install via `-ksinstall` command line arg (see Program.cs), start the service and attach to it with Visual Studio then set breakpoint on [`FuryController_Service.cs :: MainWindow_OnRequestFromClient()`](https://github.com/Beej126/KingstonFuryRgbCLI/blob/main/FuryControllerService/FuryContorller_Service.cs#L978) and observe the inbound json strings.
- I'll answer any questions posted to issues as best i can and discussions are welcome here as a community rallying point.
- **However I do not intend to entertain requests for enhancement**.


### other helpful references
- [great thread on GitLab OpenRGB](https://gitlab.com/CalcProgrammer1/OpenRGB/-/issues/2879#note_1286027635) (an open source RGB controller suite) about various ways to slam these RGBs
- download originall install from: https://www.kingston.com/en/gaming/fury-ctrl
  - tip/reminder: we can avoid installing anything unwanted on our primary Windows instance by leveraging windows sandbox and then copying back out from there

### legal disclaimer
i went looking for any licensing related info and couldn't find anything<br/>
of course if anyone official contacts me to the contrary, i will immediatelly take this private with all respect due to the owners.
- nothing talking about licensing or rights on the kingston.com download page
- nothing listed on the corresponding Windows Store page
- no licensing acceptance during any of the installations
- also, the parts being modified here get installed from a file named "FuryCTRL_SDK.exe" (as in software development kit), which is exactly how we're using it
