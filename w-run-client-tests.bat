@echo off

n:
cd N:\Dropbox\Development\Projects\adir.photography\Source\AspAngularIntegration_main\packages\Chutzpah.4.2.3\tools

chutzpah.console.exe ..\..\..\adir.photography.Test\ClientTests\Common
chutzpah.console.exe ..\..\..\adir.photography.Test\ClientTests\Gallery\GalleryViewModelShould.js
chutzpah.console.exe ..\..\..\adir.photography.Test\ClientTests\Gallery\GalleryServiceShould.js
cd N:\Dropbox\Development\Projects\adir.photography\Source\AspAngularIntegration_main
pause