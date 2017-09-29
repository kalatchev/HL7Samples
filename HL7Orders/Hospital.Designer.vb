<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Hospital
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
        Me.StatusStripMain = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelWard = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelPatient = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripMain = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.ListViewWards = New System.Windows.Forms.ListView()
        Me.ColumnHeaderWardName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SplitContainerChild = New System.Windows.Forms.SplitContainer()
        Me.ListViewCases = New System.Windows.Forms.ListView()
        Me.ColumnHeaderCase = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderRoom = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderBed = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderGender = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderBorn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderPID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderContract = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListViewOrders = New System.Windows.Forms.ListView()
        Me.ColumnHeaderId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ColumnHeaderCreated = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderDoc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderUrgent = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderSent = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStripButtonView = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonOrder = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonExit = New System.Windows.Forms.ToolStripButton()
        Me.StatusStripMain.SuspendLayout()
        Me.ToolStripMain.SuspendLayout()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        CType(Me.SplitContainerChild, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerChild.Panel1.SuspendLayout()
        Me.SplitContainerChild.Panel2.SuspendLayout()
        Me.SplitContainerChild.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStripMain
        '
        Me.StatusStripMain.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.StatusStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelWard, Me.ToolStripStatusLabelPatient})
        Me.StatusStripMain.Location = New System.Drawing.Point(0, 882)
        Me.StatusStripMain.Name = "StatusStripMain"
        Me.StatusStripMain.Size = New System.Drawing.Size(1519, 22)
        Me.StatusStripMain.TabIndex = 0
        Me.StatusStripMain.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelWard
        '
        Me.ToolStripStatusLabelWard.Name = "ToolStripStatusLabelWard"
        Me.ToolStripStatusLabelWard.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripStatusLabelPatient
        '
        Me.ToolStripStatusLabelPatient.Name = "ToolStripStatusLabelPatient"
        Me.ToolStripStatusLabelPatient.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripMain
        '
        Me.ToolStripMain.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonOrder, Me.ToolStripSeparator1, Me.ToolStripButtonExit})
        Me.ToolStripMain.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripMain.Name = "ToolStripMain"
        Me.ToolStripMain.Size = New System.Drawing.Size(1519, 31)
        Me.ToolStripMain.TabIndex = 1
        Me.ToolStripMain.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 31)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.Controls.Add(Me.ListViewWards)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.SplitContainerChild)
        Me.SplitContainerMain.Size = New System.Drawing.Size(1519, 851)
        Me.SplitContainerMain.SplitterDistance = 322
        Me.SplitContainerMain.SplitterWidth = 10
        Me.SplitContainerMain.TabIndex = 2
        '
        'ListViewWards
        '
        Me.ListViewWards.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderWardName})
        Me.ListViewWards.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewWards.FullRowSelect = True
        Me.ListViewWards.HideSelection = False
        Me.ListViewWards.LabelWrap = False
        Me.ListViewWards.Location = New System.Drawing.Point(0, 0)
        Me.ListViewWards.MultiSelect = False
        Me.ListViewWards.Name = "ListViewWards"
        Me.ListViewWards.Size = New System.Drawing.Size(322, 851)
        Me.ListViewWards.TabIndex = 0
        Me.ListViewWards.UseCompatibleStateImageBehavior = False
        Me.ListViewWards.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderWardName
        '
        Me.ColumnHeaderWardName.Text = "Ward"
        Me.ColumnHeaderWardName.Width = 143
        '
        'SplitContainerChild
        '
        Me.SplitContainerChild.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerChild.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerChild.Name = "SplitContainerChild"
        Me.SplitContainerChild.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerChild.Panel1
        '
        Me.SplitContainerChild.Panel1.Controls.Add(Me.ListViewCases)
        '
        'SplitContainerChild.Panel2
        '
        Me.SplitContainerChild.Panel2.Controls.Add(Me.ListViewOrders)
        Me.SplitContainerChild.Panel2.Controls.Add(Me.ToolStrip1)
        Me.SplitContainerChild.Size = New System.Drawing.Size(1187, 851)
        Me.SplitContainerChild.SplitterDistance = 201
        Me.SplitContainerChild.SplitterWidth = 10
        Me.SplitContainerChild.TabIndex = 1
        '
        'ListViewCases
        '
        Me.ListViewCases.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderCase, Me.ColumnHeaderRoom, Me.ColumnHeaderBed, Me.ColumnHeaderName, Me.ColumnHeaderGender, Me.ColumnHeaderBorn, Me.ColumnHeaderPID, Me.ColumnHeaderContract})
        Me.ListViewCases.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewCases.FullRowSelect = True
        Me.ListViewCases.HideSelection = False
        Me.ListViewCases.Location = New System.Drawing.Point(0, 0)
        Me.ListViewCases.MultiSelect = False
        Me.ListViewCases.Name = "ListViewCases"
        Me.ListViewCases.Size = New System.Drawing.Size(1187, 201)
        Me.ListViewCases.TabIndex = 0
        Me.ListViewCases.UseCompatibleStateImageBehavior = False
        Me.ListViewCases.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderCase
        '
        Me.ColumnHeaderCase.Text = "Case"
        Me.ColumnHeaderCase.Width = 61
        '
        'ColumnHeaderRoom
        '
        Me.ColumnHeaderRoom.Text = "Room"
        Me.ColumnHeaderRoom.Width = 40
        '
        'ColumnHeaderBed
        '
        Me.ColumnHeaderBed.Text = "Bed"
        Me.ColumnHeaderBed.Width = 35
        '
        'ColumnHeaderName
        '
        Me.ColumnHeaderName.Text = "Name"
        Me.ColumnHeaderName.Width = 160
        '
        'ColumnHeaderGender
        '
        Me.ColumnHeaderGender.Text = "G"
        Me.ColumnHeaderGender.Width = 25
        '
        'ColumnHeaderBorn
        '
        Me.ColumnHeaderBorn.Text = "DOB"
        Me.ColumnHeaderBorn.Width = 66
        '
        'ColumnHeaderPID
        '
        Me.ColumnHeaderPID.Text = "PID"
        Me.ColumnHeaderPID.Width = 80
        '
        'ColumnHeaderContract
        '
        Me.ColumnHeaderContract.Text = "Contract"
        Me.ColumnHeaderContract.Width = 100
        '
        'ListViewOrders
        '
        Me.ListViewOrders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderId, Me.ColumnHeaderCreated, Me.ColumnHeaderDoc, Me.ColumnHeaderUrgent, Me.ColumnHeaderSent})
        Me.ListViewOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewOrders.FullRowSelect = True
        Me.ListViewOrders.Location = New System.Drawing.Point(0, 33)
        Me.ListViewOrders.Name = "ListViewOrders"
        Me.ListViewOrders.Size = New System.Drawing.Size(1187, 607)
        Me.ListViewOrders.TabIndex = 1
        Me.ListViewOrders.UseCompatibleStateImageBehavior = False
        Me.ListViewOrders.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderId
        '
        Me.ColumnHeaderId.Text = "Order Id"
        Me.ColumnHeaderId.Width = 70
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonView})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1187, 33)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStripOrders"
        '
        'ColumnHeaderCreated
        '
        Me.ColumnHeaderCreated.Text = "Created"
        Me.ColumnHeaderCreated.Width = 75
        '
        'ColumnHeaderDoc
        '
        Me.ColumnHeaderDoc.Text = "Ordered By"
        Me.ColumnHeaderDoc.Width = 200
        '
        'ColumnHeaderUrgent
        '
        Me.ColumnHeaderUrgent.Text = "STAT"
        Me.ColumnHeaderUrgent.Width = 55
        '
        'ColumnHeaderSent
        '
        Me.ColumnHeaderSent.Text = "Sent"
        Me.ColumnHeaderSent.Width = 55
        '
        'ToolStripButtonView
        '
        Me.ToolStripButtonView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonView.Image = Global.HL7Orders.My.Resources.Resources.glasses_32
        Me.ToolStripButtonView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonView.Name = "ToolStripButtonView"
        Me.ToolStripButtonView.Size = New System.Drawing.Size(28, 30)
        Me.ToolStripButtonView.ToolTipText = "View message"
        '
        'ToolStripButtonOrder
        '
        Me.ToolStripButtonOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonOrder.Image = Global.HL7Orders.My.Resources.Resources.plus
        Me.ToolStripButtonOrder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonOrder.Name = "ToolStripButtonOrder"
        Me.ToolStripButtonOrder.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButtonOrder.Text = "ToolStripButtonOrder"
        Me.ToolStripButtonOrder.ToolTipText = "New Order"
        '
        'ToolStripButtonExit
        '
        Me.ToolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExit.Image = Global.HL7Orders.My.Resources.Resources.door_64
        Me.ToolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExit.Name = "ToolStripButtonExit"
        Me.ToolStripButtonExit.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButtonExit.Text = "ToolStripButtonExit"
        Me.ToolStripButtonExit.ToolTipText = "Exit"
        '
        'Hospital
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1519, 904)
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Controls.Add(Me.ToolStripMain)
        Me.Controls.Add(Me.StatusStripMain)
        Me.Name = "Hospital"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hospital Demo"
        Me.StatusStripMain.ResumeLayout(False)
        Me.StatusStripMain.PerformLayout()
        Me.ToolStripMain.ResumeLayout(False)
        Me.ToolStripMain.PerformLayout()
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.SplitContainerChild.Panel1.ResumeLayout(False)
        Me.SplitContainerChild.Panel2.ResumeLayout(False)
        Me.SplitContainerChild.Panel2.PerformLayout()
        CType(Me.SplitContainerChild, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerChild.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStripMain As StatusStrip
    Friend WithEvents ToolStripMain As ToolStrip
    Friend WithEvents SplitContainerMain As SplitContainer
    Friend WithEvents ListViewWards As ListView
    Friend WithEvents ListViewCases As ListView
    Friend WithEvents ColumnHeaderCase As ColumnHeader
    Friend WithEvents ColumnHeaderRoom As ColumnHeader
    Friend WithEvents ColumnHeaderBed As ColumnHeader
    Friend WithEvents ColumnHeaderName As ColumnHeader
    Friend WithEvents ColumnHeaderGender As ColumnHeader
    Friend WithEvents ColumnHeaderBorn As ColumnHeader
    Friend WithEvents ColumnHeaderPID As ColumnHeader
    Friend WithEvents ColumnHeaderWardName As ColumnHeader
    Friend WithEvents ToolStripStatusLabelWard As ToolStripStatusLabel
    Friend WithEvents ToolStripButtonOrder As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButtonExit As ToolStripButton
    Friend WithEvents ColumnHeaderContract As ColumnHeader
    Friend WithEvents SplitContainerChild As SplitContainer
    Friend WithEvents ListViewOrders As ListView
    Friend WithEvents ColumnHeaderId As ColumnHeader
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripStatusLabelPatient As ToolStripStatusLabel
    Friend WithEvents ColumnHeaderCreated As ColumnHeader
    Friend WithEvents ColumnHeaderDoc As ColumnHeader
    Friend WithEvents ColumnHeaderUrgent As ColumnHeader
    Friend WithEvents ColumnHeaderSent As ColumnHeader
    Friend WithEvents ToolStripButtonView As ToolStripButton
End Class
