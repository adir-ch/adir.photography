
@echo on
For /f "tokens=1-4 delims=/ " %%a in ('date /t') do (set nowdate=%%a-%%b-%%c)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME%") do (set nowtime=%%a%%b)
set now=%mydate%_%mytime%

cd adir.photography 
copy galleries.xml galleries_%now%.xml
..\PhotosDataExtractor\bin\Debug\PhotosDataExtractor.exe Content\Photos galleries.xml
cd ..