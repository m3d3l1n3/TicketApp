USE TicketAppDb;

-- Projects: ids will be 1, 2, 3 (fresh tables, IDENTITY in insert order)
INSERT  INTO Projects (
    Name,
    Description
)
VALUES               ('Zeta Dungeons', 'dungeon crawler backend'),
('Alpha UI', 'frontend overhaul'),
('Empty Quests', 'side content'); -- deliberately zero tickets: JOIN/report edge case

INSERT  INTO Tickets (
    Title,
    Description,
    CreatedAt,
    DueDate,
    Status,
    Priority,
    ProjectId
)
VALUES              -- proj 1: covers overdue-open-critical (q5 target), future InProgress, overdue Resolved, overdue Closed
('Crash on level 3', 'falls through floor', '2026-09-20', '2026-09-25', 'Open', 'Critical', 1), -- OVERDUE + Open + Critical
('Fall through floor', 'physics gap', '2026-09-22', '2026-09-30', 'InProgress', 'High', 1), -- future due date
('Missing texture', 'wall is pink', '2026-09-15', '2026-09-20', 'Resolved', 'Medium', 1), -- overdue but Resolved: does it count?
('Health bar overlap', 'ui collision', '2026-09-01', '2026-09-10', 'Closed', 'Low', 1), -- overdue but Closed: must NOT count
-- proj 2: covers NULL due date (NULLs-last test), due-today boundary, future Open, NULL Closed
('Menu crash', ' ref on open', '2026-09-25', NULL, 'Open', 'Critical', 2), -- NULL due date, sorts last
('Font rendering', 'glyphs missing', '2026-09-26', '2026-09-28', 'InProgress', 'Medium', 2), -- due TODAY: boundary, not overdue
('Tooltip missing', 'hover does nothing', '2026-09-27', '2026-10-05', 'Open', 'Low', 2), -- future
('Subtitle desync', 'audio lags text', '2026-09-10', NULL, 'Closed', 'High', 2);