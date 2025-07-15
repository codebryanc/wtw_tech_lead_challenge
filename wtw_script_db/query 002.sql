-- Insert request types
INSERT INTO RequestTypes (rtyId, Name) VALUES
('BB1DF24E-51B2-4D41-8FB8-738B64AE27F6', 'Loan'),
('F2348456-A1E0-4EF1-B777-4651B392D14D', 'Permission'),
('1ED88739-26A8-4F62-842C-5F8019F5B791', 'Vacation');

-- Insert request statuses
INSERT INTO RequestStatus (resId, Name) VALUES
(NEWID(), 'Pending'),
(NEWID(), 'Approved'),
(NEWID(), 'Rejected');
