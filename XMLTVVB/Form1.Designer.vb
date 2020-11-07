<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CreerXML_btn = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BtnGrabCanalsat = New System.Windows.Forms.Button()
        Me.BtnGrabEPG = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CreerXML_btn
        '
        Me.CreerXML_btn.Location = New System.Drawing.Point(924, 1011)
        Me.CreerXML_btn.Name = "CreerXML_btn"
        Me.CreerXML_btn.Size = New System.Drawing.Size(210, 76)
        Me.CreerXML_btn.TabIndex = 0
        Me.CreerXML_btn.Text = "Creer XML"
        Me.CreerXML_btn.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Black
        Me.TextBox1.ForeColor = System.Drawing.Color.Lime
        Me.TextBox1.Location = New System.Drawing.Point(12, 12)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(1435, 396)
        Me.TextBox1.TabIndex = 1
        '
        'BtnGrabCanalsat
        '
        Me.BtnGrabCanalsat.Location = New System.Drawing.Point(22, 424)
        Me.BtnGrabCanalsat.Name = "BtnGrabCanalsat"
        Me.BtnGrabCanalsat.Size = New System.Drawing.Size(193, 34)
        Me.BtnGrabCanalsat.TabIndex = 2
        Me.BtnGrabCanalsat.Text = "Grabber Canalsat"
        Me.BtnGrabCanalsat.UseVisualStyleBackColor = True
        '
        'BtnGrabEPG
        '
        Me.BtnGrabEPG.Location = New System.Drawing.Point(22, 1011)
        Me.BtnGrabEPG.Name = "BtnGrabEPG"
        Me.BtnGrabEPG.Size = New System.Drawing.Size(210, 76)
        Me.BtnGrabEPG.TabIndex = 3
        Me.BtnGrabEPG.Text = "Grabber EPG"
        Me.BtnGrabEPG.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1459, 1130)
        Me.Controls.Add(Me.BtnGrabEPG)
        Me.Controls.Add(Me.BtnGrabCanalsat)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.CreerXML_btn)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CreerXML_btn As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents BtnGrabCanalsat As Button
    Friend WithEvents BtnGrabEPG As Button
End Class
