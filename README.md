# KingstonFuryRgbCLI
This project is a tweaked decompile of Kingston's "Fury CTRL" software making it into a command-line based tool.<br/>
I was specifically interested in the lowest possible overhead approach to **disable** the RGB lights on my RAM.

#### Example usage
- `KingstonFuryRgbCLI.exe -setrgb=all_off`
- running via a Windows Startup shortcut (%appdata%\Microsoft\Windows\Start Menu\Programs\Startup) is fine for my needs
  - only the **first time** needs to be **run as Administrator** so it can install a driver (see next section)

#### Driver & DLL dependencies
- The KingstonFuryRgbCLI.exe depends on [.Net Framework v4.8.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net481-web-installer) so you'll need that installed
- under the [dlls folder](/KingstonFuryRgbCLI/dlls) there are a few libraries required that start from this code calling into FuryCTRL.dll (see [Class_DLL.cs](KingstonFuryRgbCLI/Class_DLL.cs))
- also in there is a NTIOLib_X64.sys driver necessary to pull off the communication with the RGB controller <s>, so in my example script I include a simple driver install command line as well as `net start/stop` commands before and after. I am very pleased this boils down to a **completely clean minimal shot at setting the RGB with nothing left running after**.</s>
  - turns out somehow the subcomponents handle installing the NTIOLib_X64.sys automatically... i wasn't seeing this intially so something must've gotten more "aligned" after subsequent code mods??

#### Enabling more RGB settings
- This code base does include all the fancy RGB effects possible beyond my personal interest to just turn the lights off
  - the initial tweaks i applied support `-setrgb=static_color` already as well (setting to a hard coded color)
  - for a nice quick way to snag the commands you want: fire up the FuryCTRL.exe client, attach to the running "FuryContorller_Service" process [their typo not mine :] via [DnSpy](https://github.com/dnSpyEx) and watch the json strings comming into [`FuryController_Service :: MainWindow_OnRequestFromClient()`](/KingstonFuryRgbCLI/FuryController_Service.cs#L980).<br/><br/>
- I'll answer any questions posted to issues as best i can and discussions are welcome here as a community rallying point.
- **However I do not intend to entertain requests for enhancement**.

#### other helpful references
- [great thread on GitLab OpenRGB](https://gitlab.com/CalcProgrammer1/OpenRGB/-/issues/2879#note_1286027635) (an open source RGB controller suite) about various ways to slam these RGBs
- [another OpenRGB thread where folks appear to have created some Windows equivalents to linux's i2c-tools](https://gitlab.com/CalcProgrammer1/OpenRGB/-/issues/2)
- FuryCTRL installation download: https://www.kingston.com/en/gaming/fury-ctrl
  - reminder tip: we can avoid installing anything unwanted on our primary Windows instance by leveraging Windows Sandbox and then copying back out from there

#### legal disclaimer
i went looking for any licensing related info and couldn't find anything<br/>
of course if anyone official contacts me to the contrary, i will immediatelly take this private with all respect due to the owners.
- nothing about licensing or rights on the kingston.com download page
- nothing on the corresponding Windows Store page
- no licensing acceptance during any of the installations
- also, the parts being modified here get installed from a file named "FuryCTRL_SDK.exe" (as in software development kit), which is exactly how we're using it
