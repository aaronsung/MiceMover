Option Explicit

'--- Win32 API declarations
Private Type POINTAPI
    X As Long
    Y As Long
End Type

Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Long
Private Declare Function SetCursorPos Lib "user32" (ByVal X As Long, ByVal Y As Long) As Long

'--- Settings
Private Const MOVE_PIXELS As Long = 5
Private Const POLL_MS As Long = 100          'Timer tick (ms). Keep small-ish, not too small.

'--- State
Private mIntervalMs As Long                  'user interval in milliseconds
Private mElapsedMs As Long                   'accumulated elapsed ms
Private mMoveRight As Boolean                'True => move right, False => move left
Private mRunning As Boolean

Private Sub Form_Load()
    txtInterval.Text = "5"
    cmdStop.Enabled = False
    tmrMove.Enabled = False
    tmrMove.Interval = POLL_MS
End Sub

Private Sub cmdStart_Click()
    Dim sec As Double
    Dim ms As Double

    sec = Val(txtInterval.Text)

    If sec <= 0 Then
        MsgBox "Please enter an interval greater than 0 seconds.", vbExclamation
        txtInterval.SetFocus
        Exit Sub
    End If

    ms = sec * 1000#

    'Clamp to at least one poll tick to avoid rapid looping
    If ms < POLL_MS Then ms = POLL_MS

    'Protect against overflow (VB6 Long max ~2.1 billion ms ~24.8 days)
    If ms > 2147483000# Then
        MsgBox "Interval too large. Please enter a smaller value.", vbExclamation
        txtInterval.SetFocus
        Exit Sub
    End If

    mIntervalMs = CLng(ms)
    mElapsedMs = 0
    mMoveRight = True
    mRunning = True

    tmrMove.Interval = POLL_MS
    tmrMove.Enabled = True

    cmdStart.Enabled = False
    cmdStop.Enabled = True
End Sub

Private Sub cmdStop_Click()
    StopMover
End Sub

Private Sub tmrMove_Timer()
    Dim pt As POINTAPI

    If Not mRunning Then Exit Sub

    'Accumulate time
    mElapsedMs = mElapsedMs + POLL_MS

    'If we passed the interval, perform the move.
    'Use a loop + subtraction to preserve timing if the app lags.
    Do While mElapsedMs >= mIntervalMs
        mElapsedMs = mElapsedMs - mIntervalMs

        GetCursorPos pt

        If mMoveRight Then
            SetCursorPos pt.X + MOVE_PIXELS, pt.Y
        Else
            SetCursorPos pt.X - MOVE_PIXELS, pt.Y
        End If

        mMoveRight = Not mMoveRight
    Loop
End Sub

Private Sub Form_Unload(Cancel As Integer)
    StopMover
End Sub

Private Sub StopMover()
    mRunning = False
    tmrMove.Enabled = False
    mElapsedMs = 0

    cmdStart.Enabled = True
    cmdStop.Enabled = False
End Sub
