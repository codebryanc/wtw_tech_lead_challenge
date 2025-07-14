-- Insert request types
INSERT INTO RequestTypes (rtyId, Name) VALUES
(NEWID(), 'Vacation'),
(NEWID(), 'Loan'),
(NEWID(), 'Permission');

-- Insert request statuses
INSERT INTO RequestStatus (resId, Name) VALUES
(NEWID(), 'Pending'),
(NEWID(), 'Approved'),
(NEWID(), 'Rejected');
