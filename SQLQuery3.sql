-- Create the Sessions table
CREATE TABLE Sessions (
    SessionID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    CurrentWeight DECIMAL(5,2) NOT NULL,  -- in kilograms
    SessionDate DATETIME NOT NULL DEFAULT GETDATE(),
    Notes NVARCHAR(500),
    
    -- Foreign key to link with Customers table
    CONSTRAINT FK_Sessions_Customers FOREIGN KEY (CustomerID) 
    REFERENCES Customers(ID)
);

-- Insert sample session data for the 10 customers
INSERT INTO Sessions (CustomerID, CurrentWeight, SessionDate, Notes)
VALUES
    -- Customer 1 (John Smith) with weight fluctuations
    (1, 72.5, '2023-02-01 10:00', 'Initial assessment'),
    (1, 71.8, '2023-03-15 10:30', 'Showing good progress'),
    (1, 70.2, '2023-05-01 11:00', 'Reached first milestone'),
    
    -- Customer 2 (Maria Garcia)
    (2, 58.5, '2023-03-01 14:00', 'New member orientation'),
    (2, 57.3, '2023-04-15 14:30', 'Consistent improvement'),
    
    -- Customer 3 (David Johnson)
    (3, 85.0, '2023-04-10 09:00', 'Initial measurements'),
    (3, 83.7, '2023-06-20 09:30', 'Responding well to program'),
    
    -- Customer 4 (Sarah Williams)
    (4, 63.5, '2023-05-05 16:00', NULL),
    
    -- Customer 5 (Robert Brown)
    (5, 76.0, '2023-06-01 13:00', 'Traveled recently'),
    
    -- Customer 6 (Emma Davis)
    (6, 60.0, '2023-07-10 08:00', 'Regular check-in'),
    
    -- Customer 7 (Michael Wilson)
    (7, 80.5, '2023-08-01 15:00', 'Initial assessment'),
    
    -- Customer 8 (Sophia Martinez)
    (8, 55.0, '2023-09-05 10:00', 'New program start'),
    
    -- Customer 9 (James Taylor)
    (9, 71.0, '2023-10-10 11:00', 'Progress review'),
    
    -- Customer 10 (Olivia Anderson)
    (10, 63.2, '2023-11-15 14:00', 'Final session of the year');

-- Optional: View the session data with customer names
SELECT 
    s.SessionID,
    c.Name AS CustomerName,
    s.CurrentWeight,
    s.SessionDate,
    s.Notes
FROM 
    Sessions s
JOIN 
    Customers c ON s.CustomerID = c.ID
ORDER BY 
    s.SessionDate;