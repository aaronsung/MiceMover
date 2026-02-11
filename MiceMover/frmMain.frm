VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "MiceMover - Mouse Position Controller"
   ClientHeight    =   3195
   ClientLeft      =   45
   ClientTop       =   375
   ClientWidth     =   5055
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3195
   ScaleWidth      =   5055
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnMoveCustom 
      Caption         =   "Move Mouse to Custom Position"
      Height          =   375
      Left            =   240
      TabIndex        =   7
      Top             =   2640
      Width           =   4575
   End
   Begin VB.TextBox txtY 
      Height          =   285
      Left            =   2520
      TabIndex        =   6
      Text            =   "500"
      Top             =   2160
      Width           =   1215
   End
   Begin VB.TextBox txtX 
      Height          =   285
      Left            =   720
      TabIndex        =   4
      Text            =   "500"
      Top             =   2160
      Width           =   1215
   End
   Begin VB.CommandButton btnMoveTopLeft 
      Caption         =   "Move Mouse to Top Left"
      Height          =   375
      Left            =   240
      TabIndex        =   2
      Top             =   1440
      Width           =   4575
   End
   Begin VB.CommandButton btnMoveCenter 
      Caption         =   "Move Mouse to Screen Center"
      Height          =   375
      Left            =   240
      TabIndex        =   1
      Top             =   840
      Width           =   4575
   End
   Begin VB.Timer tmrUpdatePos 
      Interval        =   100
      Left            =   4560
      Top             =   120
   End
   Begin VB.Label lblY 
      Caption         =   "Y:"
      Height          =   255
      Left            =   2160
      TabIndex        =   5
      Top             =   2160
      Width           =   255
   End
   Begin VB.Label lblX 
      Caption         =   "X:"
      Height          =   255
      Left            =   360
      TabIndex        =   3
      Top             =   2160
      Width           =   255
   End
   Begin VB.Label lblCurrentPos 
      Caption         =   "Current Position: X=0, Y=0"
      Height          =   255
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   4575
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' Windows API declarations for mouse control
Private Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long
Private Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long

' Structure for mouse position
Private Type POINTAPI
    x As Long
    y As Long
End Type

Private Sub Form_Load()
    ' Form is initialized with design-time properties
    ' Timer is already enabled at design time
End Sub

Private Sub btnMoveCenter_Click()
    ' Move mouse cursor to screen center
    Dim screenWidth As Long
    Dim screenHeight As Long
    
    screenWidth = Screen.Width \ Screen.TwipsPerPixelX
    screenHeight = Screen.Height \ Screen.TwipsPerPixelY
    
    SetCursorPos screenWidth \ 2, screenHeight \ 2
End Sub

Private Sub btnMoveTopLeft_Click()
    ' Move mouse cursor to top-left corner (0, 0)
    SetCursorPos 0, 0
End Sub

Private Sub btnMoveCustom_Click()
    ' Move mouse cursor to custom position
    Dim x As Long
    Dim y As Long
    Dim screenWidth As Long
    Dim screenHeight As Long
    
    ' Validate input
    If Not IsNumeric(txtX.Text) Or Not IsNumeric(txtY.Text) Then
        MsgBox "Please enter valid integer values for X and Y coordinates.", vbExclamation, "Invalid Input"
        Exit Sub
    End If
    
    x = CLng(txtX.Text)
    y = CLng(txtY.Text)
    
    ' Get screen dimensions
    screenWidth = Screen.Width \ Screen.TwipsPerPixelX
    screenHeight = Screen.Height \ Screen.TwipsPerPixelY
    
    ' Validate coordinates are within screen bounds
    If x < 0 Or x >= screenWidth Or y < 0 Or y >= screenHeight Then
        MsgBox "Coordinates must be within screen bounds (0-" & (screenWidth - 1) & ", 0-" & (screenHeight - 1) & ").", vbExclamation, "Out of Bounds"
        Exit Sub
    End If
    
    SetCursorPos x, y
End Sub

Private Sub tmrUpdatePos_Timer()
    ' Update current mouse position display
    Dim pt As POINTAPI
    
    If GetCursorPos(pt) Then
        lblCurrentPos.Caption = "Current Position: X=" & pt.x & ", Y=" & pt.y
    End If
End Sub
