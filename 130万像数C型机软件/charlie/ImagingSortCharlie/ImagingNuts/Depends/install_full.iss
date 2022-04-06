; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
AppId={{C0A263E1-474E-4250-A3CD-E73D95D52201}
AppName=�Զ���˨Ӱ�������� ������Ŀ
AppVerName=ImagingSortCharlie 1.0
AppPublisher=�Ϻ�����ɽ���ܻ�е
DefaultDirName={pf}\ImagingSortCharlie
DefaultGroupName=ImagingSortCharlie
OutputBaseFilename=ImagingSort.depends
OutputDir=2011-02-14
Compression=lzma
SolidCompression=yes
;UninstallDisplayIcon={app}\ImagingSortCharlie.exe,0
Uninstallable=no
SetupIconFile=..\charlie.ico
wizardimagefile=install_big.bmp
wizardsmallimagefile=install_small.bmp
WizardImageStretch=no

[Languages]
Name: "chinese"; MessagesFile: "compiler:Languages\Chinese.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
;Source: "Dotfuscated\ImagingSortCharlie.exe"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\ImagingNuts.exe"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\Adsapi32.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\IFCGrabNuts.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\ImageAna.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\PCI1710.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\DotNetSpeech.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\itextsharp.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\ICSharpCode.SharpZipLib.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\clrdump.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\dbghelp.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "background_1033.png"; DestDir: "{app}\Resources"; Flags: ignoreversion
;Source: "background_2052.png"; DestDir: "{app}\Resources"; Flags: ignoreversion
;Source: "..\..\release\PGGrab.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\PvAPI.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\DefaultCamera.xml"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\C����ѡ��.exe"; DestDir: "{app}"; Flags: ignoreversion
;Source: "Dotfuscated\en-US\ImagingSortCharlie.resources.dll"; DestDir: "{app}\en-US"; Flags: ignoreversion
;Source: "dotnetfx35.exe"; destdir:"{tmp}"; flags: ignoreversion
Source: "vcredist_x86.exe"; destdir:"{tmp}"; flags: ignoreversion
Source: "vision850rte.exe"; destdir:"{tmp}"; flags: ignoreversion
Source: "nik.exe"; destdir:"{tmp}"; flags: ignoreversion

;Source: "..\..\release\CamConfig.exe"; DestDir: "{app}"; Flags: ignoreversion
;Source: "..\..\release\log_reader.exe"; DestDir: "{app}"; Flags: ignoreversion
;Source: "msyh.ttf"; DestDir: "{fonts}"; FontInstall: "msyh.ttf"; Flags: onlyifdoesntexist uninsneveruninstall
;Source: "msyhbd.ttf"; DestDir: "{fonts}"; FontInstall: "msyhbd.ttf"; Flags: onlyifdoesntexist uninsneveruninstall
;Source: "NeoSpeech��������(Kate).exe"; DestDir: "{tmp}"; Flags: ignoreversion deleteafterinstall
;Source: "Ů���ʶ������(Mei Ling).msi"; DestDir: "{tmp}"; Flags: ignoreversion deleteafterinstall
;Source: "vcredist_x86.exe"; DestDir: "{tmp}"; Flags: ignoreversion deleteafterinstall
;Source: ".NET Framework 3.5 Setup.exe"; DestDir: "{tmp}"; Flags: ignoreversion deleteafterinstall
;Source: "vision850rte.exe"; DestDir: "{tmp}"; Flags: ignoreversion deleteafterinstall
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
;Name: "{group}\ImagingSortCharlie"; Filename: "{app}\ImagingSortCharlie.exe"
;Name: "{userdesktop}\�Զ���ñӰ��������"; Filename: "{app}\ImagingSortCharlie.exe"; Tasks: desktopicon

[Run]
Filename: "{tmp}\dotnetfx35.exe"; Flags:waituntilterminated;
Filename: "{tmp}\vcredist_x86.exe"; Flags:waituntilterminated;
Filename: "{tmp}\vision850rte.exe"; Flags:waituntilterminated;
Filename: "{tmp}\nik.exe"; Flags:waituntilterminated;
;Filename: "{app}\ImagingSortCharlie.exe"; Description: "{cm:LaunchProgram,ImagingSortCharlie}"; Flags: nowait postinstall skipifsilent

[installDelete]
Type: files; name: "{tmp}\dotnetfx35.exe";
Type: files; name: "{tmp}\vcredist_x86.exe";
Type: files; name: "{tmp}\vision850rte.exe";
Type: files; name: "{tmp}\nik.exe";



