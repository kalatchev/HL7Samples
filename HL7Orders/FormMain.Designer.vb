<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.ToolStripMain = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonGenerate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonSend = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextBoxMessage = New System.Windows.Forms.TextBox()
        Me.TextBoxAnswer = New System.Windows.Forms.TextBox()
        Me.ToolStripMain.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripMain
        '
        Me.ToolStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonGenerate, Me.ToolStripSeparator1, Me.ToolStripButtonSend})
        Me.ToolStripMain.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripMain.Name = "ToolStripMain"
        Me.ToolStripMain.Size = New System.Drawing.Size(730, 25)
        Me.ToolStripMain.TabIndex = 1
        Me.ToolStripMain.Text = "ToolStrip1"
        '
        'ToolStripButtonGenerate
        '
        Me.ToolStripButtonGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonGenerate.Image = CType(resources.GetObject("ToolStripButtonGenerate.Image"), System.Drawing.Image)
        Me.ToolStripButtonGenerate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonGenerate.Name = "ToolStripButtonGenerate"
        Me.ToolStripButtonGenerate.Size = New System.Drawing.Size(58, 22)
        Me.ToolStripButtonGenerate.Text = "Generate"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButtonSend
        '
        Me.ToolStripButtonSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonSend.Image = CType(resources.GetObject("ToolStripButtonSend.Image"), System.Drawing.Image)
        Me.ToolStripButtonSend.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSend.Name = "ToolStripButtonSend"
        Me.ToolStripButtonSend.Size = New System.Drawing.Size(37, 22)
        Me.ToolStripButtonSend.Text = "Send"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBoxMessage)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxAnswer)
        Me.SplitContainer1.Size = New System.Drawing.Size(730, 383)
        Me.SplitContainer1.SplitterDistance = 259
        Me.SplitContainer1.TabIndex = 2
        '
        'TextBoxMessage
        '
        Me.TextBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxMessage.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TextBoxMessage.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxMessage.Multiline = True
        Me.TextBoxMessage.Name = "TextBoxMessage"
        Me.TextBoxMessage.ReadOnly = True
        Me.TextBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxMessage.Size = New System.Drawing.Size(730, 259)
        Me.TextBoxMessage.TabIndex = 0
        Me.TextBoxMessage.WordWrap = False
        '
        'TextBoxAnswer
        '
        Me.TextBoxAnswer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxAnswer.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TextBoxAnswer.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxAnswer.Multiline = True
        Me.TextBoxAnswer.Name = "TextBoxAnswer"
        Me.TextBoxAnswer.ReadOnly = True
        Me.TextBoxAnswer.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxAnswer.Size = New System.Drawing.Size(730, 120)
        Me.TextBoxAnswer.TabIndex = 0
        Me.TextBoxAnswer.WordWrap = False
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 408)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStripMain)
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HL7 Orders"
        Me.ToolStripMain.ResumeLayout(False)
        Me.ToolStripMain.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStripMain As ToolStrip
    Friend WithEvents ToolStripButtonGenerate As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButtonSend As ToolStripButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TextBoxMessage As TextBox
    Friend WithEvents TextBoxAnswer As TextBox
End Class
