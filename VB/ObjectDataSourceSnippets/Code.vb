﻿Imports DevExpress.DataAccess.ObjectBinding
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ObjectDataSourceSnippets
	Public Class BindingToParametrizedConstructor
		Private Sub ObjectDataSourceInitialization()
			Dim objds As New ObjectDataSource()
			objds.Name = "ObjectDataSource1"
			objds.BeginUpdate()
			objds.DataMember = "Items"
			objds.DataSource = GetType(BusinessObject)
			objds.EndUpdate()
			'this line of code allows passing a parameter value to a parametrized constructor of an underlying data source object
			Dim parameter = New DevExpress.DataAccess.ObjectBinding.Parameter("p1", GetType(Integer), 3)
			objds.Constructor = New DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo(parameter)
		End Sub
		Public Class BusinessObject
			Public Property Items() As SampleItems
			Public Sub New(ByVal p1 As Integer)
				Items = New SampleItems(p1)
			End Sub
		End Class

		Public Class SampleItems
			Inherits List(Of SampleItem)

			Public Sub New()
				Me.New(10)
			End Sub
			Public Sub New(ByVal itemNumber As Integer)
				For i As Integer = 0 To itemNumber - 1
					Add(New SampleItem() With {.Name = i.ToString()})
				Next i
			End Sub
		End Class
		Public Class SampleItem
			Public Property Name() As String
		End Class
	End Class


	Public Class BindingToMethod
		Private Sub ObjectDataSourceInitialization()
			Dim objds As New DevExpress.DataAccess.ObjectBinding.ObjectDataSource()
			objds.Name = "ObjectDataSource1"

			objds.BeginUpdate()
			objds.DataMember = "GetData"
			objds.DataSource = GetType(SampleItem)
			objds.EndUpdate()

			Dim parameter = New DevExpress.DataAccess.ObjectBinding.Parameter("value", GetType(Integer), 3)
			objds.Parameters.Add(parameter)
			'this line of code is required to obtain the data source object schema
			objds.Fill()
		End Sub

		Public Class SampleItem
			Public Property Name() As String

			Public Shared Function GetData(ByVal value As Integer) As List(Of SampleItem)
				Dim items As New List(Of SampleItem)()
				For i As Integer = 0 To value - 1
					items.Add(New SampleItem() With {.Name = i.ToString()})
				Next i
				Return items
			End Function
		End Class
	End Class
End Namespace
