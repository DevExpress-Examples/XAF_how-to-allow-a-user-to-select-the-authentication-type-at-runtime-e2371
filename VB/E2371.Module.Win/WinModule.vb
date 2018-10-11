﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports DevExpress.Persistent.BaseImpl

Namespace E2371.Module.Win
	' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
	<ToolboxItemFilter("Xaf.Platform.Win")> _
	Public NotInheritable Partial Class E2371WindowsFormsModule
		Inherits ModuleBase
		'private void Application_CreateCustomModelDifferenceStore(Object sender, CreateCustomModelDifferenceStoreEventArgs e) {
		'    e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), true, "Win");
		'    e.Handled = true;
		'}
		Private Sub Application_CreateCustomUserModelDifferenceStore(ByVal sender As Object, ByVal e As CreateCustomModelDifferenceStoreEventArgs)
			e.Store = New ModelDifferenceDbStore(CType(sender, XafApplication), GetType(ModelDifference), False, "Win")
			e.Handled = True
		End Sub
		Public Sub New()
			InitializeComponent()
		End Sub
		Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
			Return ModuleUpdater.EmptyModuleUpdaters
		End Function
		Public Overrides Overloads Sub Setup(ByVal application As XafApplication)
			MyBase.Setup(application)
			'application.CreateCustomModelDifferenceStore += Application_CreateCustomModelDifferenceStore;
			AddHandler application.CreateCustomUserModelDifferenceStore, AddressOf Application_CreateCustomUserModelDifferenceStore
			' Manage various aspects of the application UI and behavior at the module level.
		End Sub
	End Class
End Namespace
