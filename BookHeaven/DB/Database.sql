--BookOrders
SELECT TOP (1000) [OrderID]
      ,[BookName]
      ,[Quantity]
      ,[Status]
      ,[SupplierID]
  FROM [BookHeaven].[dbo].[BookOrders]

--BooksTable
SELECT TOP (1000) [BookID]
      ,[Title]
      ,[Author]
      ,[Genre]
      ,[ISBN]
      ,[Price]
      ,[StockQuantity]
      ,[SupplierID]
      ,[BookImage]
      ,[Discount]
  FROM [BookHeaven].[dbo].[BooksTable]

--CustomersTable
SELECT TOP (1000) [CustomerID]
      ,[Name]
      ,[Email]
      ,[PhoneNumber]
      ,[Address]
  FROM [BookHeaven].[dbo].[CustomersTable]

--OrdersTable
SELECT TOP (1000) [OrderID]
      ,[OrderedBook]
      ,[OrderDate]
      ,[Status]
      ,[DeliveryType]
      ,[Total]
      ,[CompletedDate]
      ,[CustomerID]
  FROM [BookHeaven].[dbo].[OrdersTable]

--StaffTable
SELECT TOP (1000) [StaffID]
      ,[FullName]
      ,[Email]
      ,[Gender]
      ,[PhoneNumber]
      ,[Address]
      ,[UserID]
      ,[Photo]
      ,[NIC]
      ,[DoB]
  FROM [BookHeaven].[dbo].[StaffTable]

--SuppliersTable
SELECT TOP (1000) [SupplierID]
      ,[BusinessName]
      ,[AgentName]
      ,[NIC]
      ,[Email]
      ,[ContactNumber]
      ,[Address]
  FROM [BookHeaven].[dbo].[SuppliersTable]

--UserTable
SELECT TOP (1000) [UserID]
      ,[Username]
      ,[PasswordHash]
      ,[Role]
  FROM [BookHeaven].[dbo].[UserTable]
