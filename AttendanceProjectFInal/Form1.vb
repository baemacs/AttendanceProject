Imports System.IO
Imports System.IO.Ports

Public Class Form1

    Dim secs As Integer
    Dim mins As Integer

    ' teacher flag 
    Dim tflag As Integer

    'flags for students 
    Dim sflag1 As Integer ' student1 flag
    Dim sflag2 As Integer 'student2 flag 
    Dim sflag3 As Integer
    Dim sflag4 As Integer




    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        tflag = 0
        sflag1 = 0
        sflag2 = 0
        sflag3 = 0
        sflag4 = 0


        SerialPort1.Close()
        SerialPort1.PortName = "com3"
        SerialPort1.BaudRate = "9600" 'use same baud rate as used in Arduino
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default
        SerialPort1.Open()
    End Sub



    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        ' if the students comes in time, within 2 mints then the students will be marked present. 
        If InStr(TextBox1.Text, "stu1") And sflag1 = 0 And tflag = 1 And secs <= 30 Then
            chkstu1.Checked = True
            sflag1 = 1
            chkstu1.Text = "Present"
        End If

        If InStr(TextBox1.Text, "stu2") And sflag2 = 0 And tflag = 1 And secs <= 30 Then
            chkstu2.Checked = True
            sflag2 = 1
            chkstu2.Text = "Present"
        End If

        If InStr(TextBox1.Text, "stu3") And sflag3 = 0 And tflag = 1 And secs <= 30 Then
            chkstu3.Checked = True
            sflag3 = 1
            chkstu3.Text = "Present"
        End If

        If InStr(TextBox1.Text, "stu4") And sflag4 = 0 And tflag = 1 And secs <= 30 Then
            chkstu4.Checked = True
            sflag4 = 1
            chkstu4.Text = "Present"
        End If





        ' the following conditions will be used to check if the students are late.

        If InStr(TextBox1.Text, "stu1") And (sflag1 = 0) And tflag = 1 And secs > 30 And secs < 60 Then
            chkstu1.Checked = True
            sflag1 = 1
            chkstu1.Text = "late"
        End If

        If InStr(TextBox1.Text, "stu2") And (sflag2 = 0) And tflag = 1 And secs > 30 And secs < 60 Then
            chkstu2.Checked = True
            sflag2 = 1
            chkstu2.Text = "late"
        End If

        If InStr(TextBox1.Text, "stu3") And (sflag3 = 0) And tflag = 1 And secs > 30 And secs < 60 Then
            chkstu3.Checked = True
            sflag3 = 1
            chkstu3.Text = "late"
        End If

        If InStr(TextBox1.Text, "stu4") And (sflag4 = 0) And tflag = 1 And secs > 30 And secs < 60 Then
            chkstu4.Checked = True
            sflag4 = 1
            chkstu4.Text = "late"
        End If



        ' the following conditions will be used to check if the students are absent.

        If (sflag1 = 0) And tflag = 1 And mins >= 1 Then

            sflag1 = 1
            chkstu1.Text = "absent"
        End If

        If (sflag2 = 0) And tflag = 1 And mins >= 1 Then

            sflag2 = 1
            chkstu2.Text = "absent"
        End If

        If (sflag3 = 0) And tflag = 1 And mins >= 1 Then

            sflag3 = 1
            chkstu3.Text = "absent"
        End If


        If (sflag4 = 0) And tflag = 1 And mins >= 1 Then

            sflag4 = 1
            chkstu4.Text = "absent"
        End If


    End Sub

    Private Sub DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Try

            Dim mydata As String
            mydata = SerialPort1.ReadExisting()

            If TextBox1.InvokeRequired Then
                TextBox1.Invoke(DirectCast(Sub() TextBox1.Text &= mydata, MethodInvoker))
            Else
                TextBox1.Text &= mydata
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        'timer2 will be used to empty the textbox1 for the new data. the timer interval is set to 1 second. 
        TextBox1.Text = ""
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        ' timer3 will be used for the elapsed time. and this timer will be activated only when the teacher will check the box.
        secs = secs + 1
        lblsecs.Text = secs
        lblmins.Text = mins
        If secs = 60 Then
            secs = 0
            mins = mins + 1
        End If

    End Sub

    Private Sub chkteacher_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkteacher.CheckedChanged
        If chkteacher.Enabled = True Then
            tflag = 1
            Timer3.Enabled = True
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles chkstu1.CheckedChanged

    End Sub
End Class