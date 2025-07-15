-- Insert request types
INSERT INTO RequestTypes (rtyId, Name) VALUES
('0617B26B-6583-435F-B32E-35D76F70E39D', 'Vacation'),
('35290059-1516-4D9E-9D84-1903D7DDF3CD', 'Loan'),
('7EDE7098-092B-4816-957A-A96881CA3154', 'Permission');

-- Insert request statuses
INSERT INTO RequestStatus (resId, Name) VALUES
(NEWID(), 'Pending'),
(NEWID(), 'Approved'),
(NEWID(), 'Rejected');
