-- Request types
CREATE TABLE RequestTypes (
    rtyId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL UNIQUE
);

-- Request status
CREATE TABLE RequestStatus (
    resId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(50) NOT NULL UNIQUE
);

-- Main Requests table with foreign keys
CREATE TABLE Requests (
    reqId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    rtyId UNIQUEIDENTIFIER NOT NULL,
    resId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    Data NVARCHAR(MAX) NOT NULL,
    CONSTRAINT CHK_Data_JSON CHECK (ISJSON(Data) > 0),
    CONSTRAINT FK_Requests_Type FOREIGN KEY (rtyId) REFERENCES RequestTypes(rtyId),
    CONSTRAINT FK_Requests_Status FOREIGN KEY (resId) REFERENCES RequestStatus(resId)
);

-- Indexes
CREATE INDEX IX_Requests_TypeId ON Requests(rtyId);
CREATE INDEX IX_Requests_StatusId ON Requests(resId);
CREATE INDEX IX_Requests_CreatedAt ON Requests(CreatedAt);
