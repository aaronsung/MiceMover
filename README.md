# MiceMover

A simple Visual Basic Windows Forms application for controlling mouse cursor position.

## Features

- Move mouse cursor to screen center
- Move mouse cursor to top-left corner (0, 0)
- Move mouse cursor to custom X, Y coordinates
- Real-time display of current mouse position

## Requirements

- Windows Operating System
- .NET Framework 4.7.2 or higher
- Visual Studio 2017 or higher (for building from source)

## Building the Application

1. Open `MiceMover.sln` in Visual Studio
2. Build the solution (Build → Build Solution or press F6)
3. The executable will be created in `MiceMover\bin\Debug\MiceMover.exe` (Debug build) or `MiceMover\bin\Release\MiceMover.exe` (Release build)

### Command Line Build

You can also build from the command line using MSBuild:

```bash
# For Debug build
msbuild MiceMover.sln /p:Configuration=Debug

# For Release build
msbuild MiceMover.sln /p:Configuration=Release
```

## Usage

1. Run `MiceMover.exe`
2. Use the buttons to move the mouse cursor:
   - **Move Mouse to Screen Center**: Moves cursor to the center of your primary screen
   - **Move Mouse to Top Left**: Moves cursor to coordinates (0, 0)
   - **Move Mouse to Custom Position**: Enter X and Y coordinates and click to move the cursor to that position
3. The current mouse position is displayed at the top and updates in real-time

## License

See LICENSE file for details.