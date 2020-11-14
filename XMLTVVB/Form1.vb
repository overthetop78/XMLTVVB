
Imports System.Xml
Imports System.Text

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnGrabCanalsat_Click(sender As Object, e As EventArgs) Handles BtnGrabCanalsat.Click
        BtnGrabCanalsat.Enabled = False
        CanalSatGrabChannel()
        BtnGrabCanalsat.Enabled = True
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        Close()
    End Sub

    Private Sub BtnGrabLeFigaro_Click(sender As Object, e As EventArgs) Handles BtnGrabLeFigaro.Click
        BtnGrabLeFigaro.Enabled = False
        LeFigaroGrabChannel()
        BtnGrabLeFigaro.Enabled = True
    End Sub
End Class
