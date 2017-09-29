<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddExam
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAddExam))
        Me.ListViewExams = New System.Windows.Forms.ListView()
        Me.ColumnHeaderLoinc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ImageListExams = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'ListViewExams
        '
        Me.ListViewExams.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListViewExams.CheckBoxes = True
        Me.ListViewExams.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderLoinc, Me.ColumnHeaderName})
        Me.ListViewExams.FullRowSelect = True
        Me.ListViewExams.Location = New System.Drawing.Point(12, 12)
        Me.ListViewExams.Name = "ListViewExams"
        Me.ListViewExams.Size = New System.Drawing.Size(753, 518)
        Me.ListViewExams.SmallImageList = Me.ImageListExams
        Me.ListViewExams.TabIndex = 0
        Me.ListViewExams.UseCompatibleStateImageBehavior = False
        Me.ListViewExams.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderLoinc
        '
        Me.ColumnHeaderLoinc.Text = "Code"
        Me.ColumnHeaderLoinc.Width = 100
        '
        'ColumnHeaderName
        '
        Me.ColumnHeaderName.Text = "Name"
        Me.ColumnHeaderName.Width = 200
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(620, 548)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(145, 40)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonOK.Location = New System.Drawing.Point(469, 548)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(145, 40)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ImageListExams
        '
        Me.ImageListExams.ImageStream = CType(resources.GetObject("ImageListExams.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListExams.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListExams.Images.SetKeyName(0, "doc-16.png")
        Me.ImageListExams.Images.SetKeyName(1, "folder-16.png")
        '
        'FormAddExam
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(777, 600)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ListViewExams)
        Me.Name = "FormAddExam"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Examination"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListViewExams As ListView
    Friend WithEvents ColumnHeaderLoinc As ColumnHeader
    Friend WithEvents ColumnHeaderName As ColumnHeader
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ImageListExams As ImageList
End Class
