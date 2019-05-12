Public Class Main

    Dim AppName As String = "AutoclickMaker"
    Dim AppVersion As String = "BETA 1.0"

    Dim MinCPS As Integer
    Dim MaxCPS As Integer

    Dim TrackValue1 As Integer
    Dim TrackValue2 As Integer

    Dim LeftClick As Boolean
    Dim RightClick As Boolean
    Dim MiddleClick As Boolean
    Dim JitterClick As Boolean

    Dim JitterValue As String

    Dim File As String
    Dim Separator As String = "(--AcM--)"

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = AppName & " " & AppVersion

        Label4.Text = "(" & TrackBar1.Value.ToString & ")"
        Label5.Text = "(" & TrackBar2.Value.ToString & ")"
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Label4.Text = "(" & TrackBar1.Value.ToString & ")"
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        Label5.Text = "(" & TrackBar2.Value.ToString & ")"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MinCPS = 1000 / TrackBar1.Value
        MaxCPS = 1000 / TrackBar2.Value

        TrackValue1 = TrackBar1.Value
        TrackValue2 = TrackBar2.Value

        LeftClick = True

        If CheckBox2.Checked = True Then
            RightClick = True
        Else
            RightClick = False
        End If

        If CheckBox3.Checked = True Then
            MiddleClick = True
        Else
            MiddleClick = False
        End If

        If CheckBox4.Checked = True Then
            JitterClick = True
        Else
            JitterClick = False
        End If

        JitterValue = TextBox1.Text

        SaveFileDialog1.FileName = AppName & "_AC_" & New Random().Next(1, 99999)
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub Build()
        FileOpen(1, Application.StartupPath & "\Stub.exe", OpenMode.Binary, OpenAccess.Read)
        Dim Data As String = Space(LOF(1))
        FileGet(1, Data)
        FileClose(1)

        FileOpen(2, File, OpenMode.Binary, OpenAccess.Default)
        FilePut(2, Data & Separator & MinCPS & Separator & MaxCPS & Separator & TrackValue1 & Separator & TrackValue2 & Separator & LeftClick & Separator & RightClick & Separator & MiddleClick & Separator & JitterClick & Separator & JitterValue)
        FileClose(2)
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        File = SaveFileDialog1.FileName
        Build()

        MsgBox("Sucessfully generated the program!")
    End Sub
End Class
