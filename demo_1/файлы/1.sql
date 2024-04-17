use db_demo_2024;
# 1
SELECT * from employees
JOIN db_demo_2024.role ON db_demo_2024.employees.employee_role = db_demo_2024.role.role_id;

# 2
use db_demo_2024;
INSERT INTO Requests (client_name, client_surname, client_patronymic, request_date, request_description)
VALUES ("Имя", "Фамилия", "Отчество", NOW(), "Описание");

# 3
use db_demo_2024;
INSERT INTO RequestEquipmentList (request_id, equipment_id)
VALUES (1, 3);

# 4
use db_demo_2024;
SELECT * FROM Employees WHERE employee_role = 2;

# 5
use db_demo_2024;
SELECT *
FROM request_designated_employee rde
JOIN requests r ON rde.request_id = r.request_id
WHERE rde.employee_id = 1;

# 6
use db_demo_2024;
SELECT *
FROM requests;

# 7
use db_demo_2024;
DELETE FROM request_equipment_list
WHERE equipment_id = 1;

# 8
use db_demo_2024;
INSERT INTO equipment (equipment_id, equipment_name)
VALUES (123, "наименовение");

# 9
use db_demo_2024;
SELECT *
FROM requests
WHERE request_id = 1;

# 10
use db_demo_2024;
INSERT INTO request_comments (comment_content, request_id)
VALUES ("комментарий", 1);

# 11
use db_demo_2024;
SELECT *
FROM request_equipment_list reql
INNER JOIN equipment e ON reql.equipment_id = e.equipment_id
INNER JOIN defect_types dt ON e.defect_type_id = dt.defect_type_id
WHERE reql.request_id = 1;

# 12
use db_demo_2024;
SELECT *
FROM request_comments
WHERE request_id = 1;

# 13
use db_demo_2024;
SELECT *
FROM request_designated_employees rde
JOIN employees e ON rde.employee_id = e.employee_id
WHERE rde.request-Id = 1;

# 14
use db_demo_2024;
SELECT *
FROM request_statuses rs
JOIN status s ON rs.status_id = s.status_id
WHERE rs.request_id = 1
ORDER BY rs.change_date DESC;

# 15
use db_demo_2024;
SELECT *
FROM status;

# 16
use db_demo_2024;
DELETE FROM request_designated_employees
WHERE request_id = 1 AND employee_id = 1;

# 17
use db_demo_2024;
INSERT INTO request_statuses (status_id, request_id, change_date)
VALUES (1, 1, NOW());

# 18
use db_demo_2024;
SELECT *
FROM request_statuses rs
WHERE rs.request_id = 1
ORDER BY rs.change_date DESC;

# 19
use db_demo_2024;
UPDATE requests
SET request_description = "новое описание"
WHERE request_id = 1;