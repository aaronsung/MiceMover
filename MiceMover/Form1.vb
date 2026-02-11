Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class Form1

    Private WithEvents btnMoveCenter As Button
    Private WithEvents btnMoveTopLeft As Button
    Private WithEvents btnMoveCustom As Button
    Private txtX As TextBox
    Private txtY As TextBox
    Private lblX As Label
    Private lblY As Label
    Private lblCurrentPos As Label
    Private WithEvents tmrUpdatePos As Timer

    ' Import Windows API for mouse movement
    <DllImport("user32.dll")>
    Private Shared Function SetCursorPos(ByVal X As Integer, ByVal Y As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetCursorPos(ByRef lpPoint As POINT) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure POINT
        Public X As Integer
        Public Y As Integer
    End Structure

    Public Sub New()
        InitializeComponent()
        InitializeCustomComponents()
    End Sub

    Private Sub InitializeCustomComponents()
        ' Remove the redundant settings since they're now in InitializeComponent
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Initialize Timer and add to components for proper disposal
        If components Is Nothing Then
            components = New System.ComponentModel.Container()
        End If
        tmrUpdatePos = New Timer(components)
        tmrUpdatePos.Interval = 100 ' Update every 100ms
        tmrUpdatePos.Enabled = True

        ' Create Controls
        Dim yOffset As Integer = 20

        ' Current Position Label
        lblCurrentPos = New Label()
        lblCurrentPos.Location = New System.Drawing.Point(20, yOffset)
        lblCurrentPos.Size = New System.Drawing.Size(350, 23)
        lblCurrentPos.Text = "Current Position: X=0, Y=0"
        Me.Controls.Add(lblCurrentPos)
        yOffset += 40

        ' Move to Center Button
        btnMoveCenter = New Button()
        btnMoveCenter.Location = New System.Drawing.Point(20, yOffset)
        btnMoveCenter.Size = New System.Drawing.Size(350, 30)
        btnMoveCenter.Text = "Move Mouse to Screen Center"
        Me.Controls.Add(btnMoveCenter)
        yOffset += 40

        ' Move to Top Left Button
        btnMoveTopLeft = New Button()
        btnMoveTopLeft.Location = New System.Drawing.Point(20, yOffset)
        btnMoveTopLeft.Size = New System.Drawing.Size(350, 30)
        btnMoveTopLeft.Text = "Move Mouse to Top Left"
        Me.Controls.Add(btnMoveTopLeft)
        yOffset += 50

        ' Custom Position Label X
        lblX = New Label()
        lblX.Location = New System.Drawing.Point(20, yOffset)
        lblX.Size = New System.Drawing.Size(30, 23)
        lblX.Text = "X:"
        Me.Controls.Add(lblX)

        ' Custom Position TextBox X
        txtX = New TextBox()
        txtX.Location = New System.Drawing.Point(50, yOffset)
        txtX.Size = New System.Drawing.Size(100, 23)
        txtX.Text = "500"
        Me.Controls.Add(txtX)

        ' Custom Position Label Y
        lblY = New Label()
        lblY.Location = New System.Drawing.Point(170, yOffset)
        lblY.Size = New System.Drawing.Size(30, 23)
        lblY.Text = "Y:"
        Me.Controls.Add(lblY)

        ' Custom Position TextBox Y
        txtY = New TextBox()
        txtY.Location = New System.Drawing.Point(200, yOffset)
        txtY.Size = New System.Drawing.Size(100, 23)
        txtY.Text = "500"
        Me.Controls.Add(txtY)
        yOffset += 40

        ' Move to Custom Position Button
        btnMoveCustom = New Button()
        btnMoveCustom.Location = New System.Drawing.Point(20, yOffset)
        btnMoveCustom.Size = New System.Drawing.Size(350, 30)
        btnMoveCustom.Text = "Move Mouse to Custom Position"
        Me.Controls.Add(btnMoveCustom)
    End Sub

    Private Sub btnMoveCenter_Click(sender As Object, e As EventArgs) Handles btnMoveCenter.Click
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        SetCursorPos(screenWidth \ 2, screenHeight \ 2)
    End Sub

    Private Sub btnMoveTopLeft_Click(sender As Object, e As EventArgs) Handles btnMoveTopLeft.Click
        SetCursorPos(0, 0)
    End Sub

    Private Sub btnMoveCustom_Click(sender As Object, e As EventArgs) Handles btnMoveCustom.Click
        Dim x As Integer
        Dim y As Integer

        If Integer.TryParse(txtX.Text, x) AndAlso Integer.TryParse(txtY.Text, y) Then
            ' Validate coordinates are within reasonable bounds
            Dim screenBounds As Rectangle = Screen.PrimaryScreen.Bounds
            If x < 0 OrElse x >= screenBounds.Width OrElse y < 0 OrElse y >= screenBounds.Height Then
                MessageBox.Show($"Coordinates must be within screen bounds (0-{screenBounds.Width - 1}, 0-{screenBounds.Height - 1}).", "Out of Bounds", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                SetCursorPos(x, y)
            End If
        Else
            MessageBox.Show("Please enter valid integer values for X and Y coordinates.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub tmrUpdatePos_Tick(sender As Object, e As EventArgs) Handles tmrUpdatePos.Tick
        Dim pt As POINT
        If GetCursorPos(pt) Then
            lblCurrentPos.Text = $"Current Position: X={pt.X}, Y={pt.Y}"
        End If
    End Sub
End Class
