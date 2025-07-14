----Insert example data TO work with them

INSERT INTO Requests (rtyId, resId, Data)
VALUES 
-- Vacation
('1ED88739-26A8-4F62-842C-5F8019F5B791', '580BB21A-9624-48AE-B21B-34C4FF480551', N'{"startDate":"2025-07-15","endDate":"2025-07-20","reason":"Family trip"}'),
('1ED88739-26A8-4F62-842C-5F8019F5B791', '9B484C2A-3D94-4C40-BC12-E61F3A8B0302',  N'{"startDate":"2025-08-01","endDate":"2025-08-10","reason":"Travel"}'),
('1ED88739-26A8-4F62-842C-5F8019F5B791', '2307877F-BE56-4756-819E-DDF647AFEE85', N'{"startDate":"2025-09-01","endDate":"2025-09-07","reason":"Too short notice"}'),
('1ED88739-26A8-4F62-842C-5F8019F5B791', '580BB21A-9624-48AE-B21B-34C4FF480551', N'{"startDate":"2025-12-20","endDate":"2026-01-05","reason":"Holiday"}'),

-- Loan
('BB1DF24E-51B2-4D41-8FB8-738B64AE27F6', '9B484C2A-3D94-4C40-BC12-E61F3A8B0302',  N'{"amount":2000,"installments":6,"reason":"Medical expenses"}'),
('BB1DF24E-51B2-4D41-8FB8-738B64AE27F6', '580BB21A-9624-48AE-B21B-34C4FF480551', N'{"amount":5000,"installments":12,"reason":"Car repair"}'),
('BB1DF24E-51B2-4D41-8FB8-738B64AE27F6', '580BB21A-9624-48AE-B21B-34C4FF480551', N'{"amount":1500,"installments":3,"reason":"Tuition"}'),

-- Permission
('F2348456-A1E0-4EF1-B777-4651B392D14D', '2307877F-BE56-4756-819E-DDF647AFEE85', N'{"date":"2025-07-10","hours":4,"reason":"Personal errand"}'),
('F2348456-A1E0-4EF1-B777-4651B392D14D', '580BB21A-9624-48AE-B21B-34C4FF480551', N'{"date":"2025-07-12","hours":2,"reason":"Doctor appointment"}'),
('F2348456-A1E0-4EF1-B777-4651B392D14D', '9B484C2A-3D94-4C40-BC12-E61F3A8B0302',  N'{"date":"2025-07-05","hours":1,"reason":"Early leave"}');
