# MiceMover

A simple Visual Basic 6 application for controlling mouse cursor position.

## Features

- Move mouse cursor to screen center
- Move mouse cursor to top-left corner (0, 0)
- Move mouse cursor to custom X, Y coordinates
- Real-time display of current mouse position

## Requirements

- Windows Operating System
- Visual Basic 6.0 IDE (for building from source)
- Microsoft Visual Basic 6.0 Runtime (for running the compiled executable)

## Project Files

- `MiceMover/frmMain.frm` - Main form with GUI and mouse control logic
- `MiceMover/modMain.bas` - Application entry point module
- `MiceMover/MiceMover.vbp` - Visual Basic 6 project file

## Building the Application

**Note:** This is a Visual Basic 6 project and requires the VB6 IDE to compile.

1. Install Visual Basic 6.0 IDE on a Windows system
2. Open `MiceMover/MiceMover.vbp` in the VB6 IDE
3. From the menu: File → Make MiceMover.exe
4. The executable will be created in the `MiceMover` directory

### VB6 IDE Notes

- Visual Basic 6.0 was released in 1998 and officially retired in 2008
- VB6 projects cannot be built with modern .NET tools
- The VB6 IDE runs on Windows XP, Vista, 7, and can run on newer versions with compatibility mode
- Alternative: Use VB6 runtime on modern systems to run pre-compiled VB6 executables

## Usage

1. Build and run `MiceMover.exe` (or run from VB6 IDE by pressing F5)
2. Use the buttons to move the mouse cursor:
   - **Move Mouse to Screen Center**: Moves cursor to the center of your primary screen
   - **Move Mouse to Top Left**: Moves cursor to coordinates (0, 0)
   - **Move Mouse to Custom Position**: Enter X and Y coordinates and click to move the cursor to that position
3. The current mouse position is displayed at the top and updates in real-time

## Technical Details

- Uses Windows API (`user32.dll`) for mouse control:
  - `SetCursorPos` - Moves cursor to specified coordinates
  - `GetCursorPos` - Retrieves current cursor position
- Timer control updates position display every 100ms
- Input validation ensures coordinates are within screen bounds

## License

See LICENSE file for details.