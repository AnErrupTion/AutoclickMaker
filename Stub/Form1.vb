Imports System.Text

Public Class Form1

    Public Declare Function apimouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dX As Integer, ByVal dY As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer) As Boolean
    Private Declare Function GetForegroundWindow Lib "user32" Alias "GetForegroundWindow" () As IntPtr
    Private Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As IntPtr, ByVal lpString As StringBuilder, ByVal cch As Integer) As Integer

    Private MCTitle As String

    Public Const MOUSEEVENTF_LEFTDOWN = &H2
    Public Const MOUSEEVENTF_LEFTUP = &H4
    Public Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Public Const MOUSEEVENTF_MIDDLEUP = &H40
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8
    Public Const MOUSEEVENTF_RIGHTUP = &H10
    Public Const MOUSEEVENTF_MOVE = &H1

    Dim Separator As String = "(--AcM--)"
    Dim Options() As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read)
        Dim Data As String = Space(LOF(1))
        FileGet(1, Data)
        FileClose(1)

        Options = Split(Data, Separator)

        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Randomize()

        Dim ACRnd As New Random
        Dim JRnd As New Random

        Dim MinVal As Integer = Options(1)
        Dim MaxVal As Integer = Options(2)

        Dim TrackValue1 As Integer = Options(3)
        Dim TrackValue2 As Integer = Options(4)

        Dim LeftClick As Boolean = Options(5)
        Dim RightClick As Boolean = Options(6)
        Dim MiddleClick As Boolean = Options(7)
        Dim JitterClick As Boolean = Options(8)

        Dim JitterValue As String = Options(9)

        Timer1.Interval = ACRnd.Next(MaxVal, MinVal)

        If GetActiveWindowTitle().Contains("Minecraft") Or GetActiveWindowTitle().Contains("Badlion") Or GetActiveWindowTitle().Contains("Labymod") Or GetActiveWindowTitle().Contains("OCMC") Or GetActiveWindowTitle().Contains("Cheatbreaker") Or GetActiveWindowTitle().Contains("J3Ultimate") Or GetActiveWindowTitle().Contains("Lunar") Or GetActiveWindowTitle().Contains("Pactify") Then
            If MouseButtons = MouseButtons.Left Then
                apimouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
                apimouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)

                If JitterClick = True Then
                    Dim Jitter As Integer = TrackValue2 - TrackValue1 + TrackValue1 / 2 + JitterValue
                    apimouse_event(MOUSEEVENTF_MOVE, JRnd.Next(-Jitter, Jitter), JRnd.Next(-Jitter, Jitter), 0, 0)
                End If
            End If

            If MouseButtons = MouseButtons.Right Then
                If RightClick = True Then
                    apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                    apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                End If

                If JitterClick = True Then
                    Dim Jitter As Integer = TrackValue2 - TrackValue1 + TrackValue1 / 2 + JitterValue
                    apimouse_event(MOUSEEVENTF_MOVE, JRnd.Next(-Jitter, Jitter), JRnd.Next(-Jitter, Jitter), 0, 0)
                End If
            End If

            If MouseButtons = MouseButtons.Middle Then
                If MiddleClick = True Then
                    apimouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)
                    apimouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0)
                End If

                If JitterClick = True Then
                    Dim Jitter As Integer = TrackValue2 - TrackValue1 + TrackValue1 / 2 + JitterValue
                    apimouse_event(MOUSEEVENTF_MOVE, JRnd.Next(-Jitter, Jitter), JRnd.Next(-Jitter, Jitter), 0, 0)
                End If
            End If
        End If
    End Sub

    Private Function GetCaptionOfActiveWindow() As String
        Dim Caption As New StringBuilder(256)
        Dim hWnd As IntPtr = GetForegroundWindow()

        GetWindowText(hWnd, Caption, Caption.Capacity)

        Return Caption.ToString()
    End Function

    Private Function GetActiveWindowTitle() As String
        Dim CapTxt As String = GetCaptionOfActiveWindow()

        If MCTitle <> CapTxt Then
            MCTitle = CapTxt
        End If

        Return MCTitle
    End Function
End Class
