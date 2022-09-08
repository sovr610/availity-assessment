SELECT Customer.FirstName, Customer.LastName, COALESCE(OrderLine.Cost, 0) AS Cost FROM [dbo].[Customer] 
LEFT JOIN [dbo].[Order]
on [Order].CustomerID = Customer.CustID
LEFT JOIN [dbo].[OrderLine]
on OrderLine.OrdID = [Order].OrderID
where GETDATE() < DATEADD(month, 6, [Order].OrderDate)
GROUP BY OrderLine.Cost, OrderLine.Quantity, Customer.FirstName, Customer.LastName, [Order].OrderDate
HAVING (OrderLine.Cost * OrderLine.Quantity) > 100 AND (OrderLine.Cost * OrderLine.Quantity) < 500