SELECT   e.id  
        ,e.name
        ,e.surname
        ,e.phone
        ,e.company_id
        ,p.type
        ,p.number
        ,d.name
        ,d.phone
    FROM employees e
    JOIN departments d ON d.id = e.department_id
    JOIN passport p ON p.id = e.pasport_id
/**where**/
ORDER BY /**orderby**/