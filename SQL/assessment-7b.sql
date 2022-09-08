SELECT Customer.FirstName, Customer.LastName, COALESCE(OrderLine.Cost, 0) AS Cost FROM [dbo].[Customer] 
LEFT JOIN [dbo].[Order]
on [Order].CustomerID = Customer.CustID
LEFT JOIN [dbo].[OrderLine]
on OrderLine.OrdID = [Order].OrderID
where GETDATE() < DATEADD(month, 6, [Order].OrderDate) or OrderLine.Cost IS NULL