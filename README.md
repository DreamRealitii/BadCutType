# BadCutType
What the mod does:
- When you get a bad cut, replace the "x" with text of the bad cut reason.
- Has settings to enable/disable, change font size/color, write your own text for each bad cut type.

1.0.0 Issues:
- Hitting a bomb gives WrongColor text. Update to 1.1.0 add new BombHit text.

Possible improvements:
- Other languages besides English? I don't know them.

Quick install:
- Step 1: Copy .dll file from release into Beat Saber Plugins folder.

Source code install:
- Step 1: Download source code from release or GitHub code button.
- Step 2: Create project file `BadCutType/BadCutType.csproj.user` with the text below, but replace BeatSaberDir and ReferencePath with your own Beat Saber directories.
```
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <BeatSaberDir>C:\Program Files (x86)\Steam\steamapps\common\Beat Saber</BeatSaberDir>
    <ReferencePath>C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\Beat Saber_Data\Managed;C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\Libs;C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\Plugins</ReferencePath>
  </PropertyGroup>
</Project>
```
- Step 3: Open BadCutType.sln in Visual Studio and build solution.
